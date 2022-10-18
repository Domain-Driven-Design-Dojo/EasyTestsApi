using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace contoso.DOMAIN.Classes
{
    public class ClsSend
    {
        public class STC_SMSSend
        {
            public string SourceAddress;
            public string DestAddress;
            public string Status;
            public string Response;
            public string SMSID;
            public STC_SMSSend(string sourceAddress, string destAddress, string status, string response, string smsid)
            {
                this.SourceAddress = sourceAddress;
                this.DestAddress = destAddress;
                this.Status = status;
                this.Response = response;
                this.SMSID = smsid;
            }
        };
        private static readonly object syncLock = new object();
        public static string Validate_Number(string Number)
        {
            string ret = Number.Trim();
            if (ret.Substring(0, 4) == "0098")
            {
                ret = ret.Remove(0, 4);
            }
            if (ret.Substring(0, 3) == "098")
            {
                ret = ret.Remove(0, 3);
            }
            if (ret.Substring(0, 3) == "+98")
            {
                ret = ret.Remove(0, 3);
            }
            if (ret.Substring(0, 2) == "98")
            {
                ret = ret.Remove(0, 2);
            }
            if (ret.Substring(0, 1) == "0")
            {
                ret = ret.Remove(0, 1);
            }
            return "+98" + ret;
        }
        public static string Validate_Message(ref string Message, bool IsPersian)
        {
            char cr = (char)13;
            Message = Message.Replace(cr.ToString(), string.Empty);

            if (Message.EndsWith(Environment.NewLine))
            {
                Message = Message.TrimEnd(Environment.NewLine.ToCharArray());
            }
            if (IsPersian)
            {
                return C2Unicode(Message);
            }
            else
            {
                return Message;
            }
        }
        public static string C2Unicode(string Message)
        {
            int i;
            int preUnicode_Number;
            string preUnicode;
            string ret = "";
            string strHex = "";
            for (i = 0; i < Message.Length; i++)
            {
                preUnicode_Number = 4 - string.Format("{0:X}", (int)(Message.Substring(i, 1)[0])).Length;
                preUnicode = string.Format("{0:D" + preUnicode_Number.ToString() + "}", 0);
                strHex = preUnicode + string.Format("{0:X}", (int)(Message.Substring(i, 1)[0]));
                if (strHex.Length == 4)
                    ret += strHex;
            }
            return ret;
        }
        public static void FindTxtLanguageAndcount(string unicodeString, ref bool IsPersian, ref int SMSCount)
        {
            unicodeString = unicodeString.Replace("\r\n", "a");
            IsPersian = FindTxtLanguage(unicodeString);
            decimal msgCount = 0;
            int strLength = unicodeString.Length;
            if (IsPersian == true && strLength <= 70)
                msgCount = 1;
            else if (IsPersian == true && strLength > 70)
                msgCount = Convert.ToInt32(Math.Ceiling(strLength / 67.0));
            else if (IsPersian == false && strLength <= 160)
                msgCount = 1;
            else if (IsPersian == false && strLength > 160)
                msgCount = Convert.ToInt32(Math.Ceiling(strLength / 157.0));

            SMSCount = Convert.ToInt16(msgCount);
        }
        public static bool FindTxtLanguage(string unicodeString)
        {
            const int MaxAnsiCode = 255;
            bool IsPersian = true;
            if (unicodeString != string.Empty)
                IsPersian = unicodeString.ToCharArray().Any(c => c > MaxAnsiCode);
            else
                IsPersian = true;
            return IsPersian;
        }
        public static string[] SendSMS_Single(string Message, string DestinationAddress, string Number, string userName, string password, string IP_Send, string Company, bool IsFlash)
        {
            string rawMessage = Message;
            string strIsPersian;
            string Identity = string.Empty;
            string[] RetValue = new string[2];
            RetValue[0] = "False";
            RetValue[1] = "0";
            bool IsPersian = FindTxtLanguage(Message);
            Validate_Message(ref Message, IsPersian);
            if (IsPersian)
            {
                Message = C2Unicode(Message);
                strIsPersian = "true";
            }
            else
            {
                strIsPersian = "false";
            }

            lock (syncLock)
            {
                try
                {
                    Random _Random = new Random();
                    Identity = string.Format("{0:yyyyMMddHHmmssfff}", DateTimeOffset.Now) + string.Format("{0:000}", _Random.Next(1000));
                    string dcs = IsPersian ? "8" : "0";
                    string msgClass = IsFlash ? "0" : "1";
                    StringBuilder _StringBuilder = new StringBuilder();
                    _StringBuilder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("<!DOCTYPE smsBatch PUBLIC \"-//PERVASIVE//DTD CPAS 1.0//EN\" \"http://www.ubicomp.ir/dtd/Cpas.dtd\">");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("<smsBatch company=\"" + Company + "\" batchID=\"" + Company + "+" + Identity + "\">");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("<sms msgClass=\"" + msgClass + "\" binary=\"" + strIsPersian + "\" dcs=\"" + dcs + "\"" + ">");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("<destAddr><![CDATA[" + Validate_Number(DestinationAddress) + "]]></destAddr>");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("<origAddr><![CDATA[" + Validate_Number(Number) + "]]></origAddr>");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("<message><![CDATA[" + Message + "]]></message>");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("</sms>");
                    _StringBuilder.Append(Environment.NewLine);
                    _StringBuilder.Append("</smsBatch>");

                    string dataToPost = _StringBuilder.ToString();
                    byte[] buf = System.Text.UTF8Encoding.UTF8.GetBytes(_StringBuilder.ToString());
                    WebRequest objWebRequest = WebRequest.Create(IP_Send);
                    objWebRequest.Method = "POST";
                    objWebRequest.ContentType = "text/xml";
                    byte[] byt = System.Text.Encoding.UTF8.GetBytes(userName + ":" + password);
                    objWebRequest.Headers.Add("authorization", "Basic " + Convert.ToBase64String(byt));
                    Stream _Stream = objWebRequest.GetRequestStream();
                    StreamWriter _StreamWriter = new StreamWriter(_Stream);
                    _StreamWriter.Write(dataToPost);
                    _StreamWriter.Flush();
                    _StreamWriter.Close();
                    _Stream.Close();

                    WebResponse objWebResponse = objWebRequest.GetResponse();
                    Stream objResponseStream = objWebResponse.GetResponseStream();
                    StreamReader objStreamReader = new StreamReader(objResponseStream);
                    string dataToReceive = objStreamReader.ReadToEnd();
                    objStreamReader.Close();
                    objResponseStream.Close();
                    objWebResponse.Close();

                    if (dataToReceive.IndexOf("CHECK_OK") > 0)
                    {
                        RetValue[0] = "CHECK_OK";
                        RetValue[1] = Identity;
                        string[] Tonumber = new string[1];
                        Tonumber[0] = DestinationAddress;
                    }
                    else
                    {
                        try
                        {
                            string msg;
                            int firstIndex = dataToReceive.IndexOf("CDATA[");
                            int LastIndex = dataToReceive.IndexOf("]");
                            msg = dataToReceive.Substring(firstIndex, LastIndex - firstIndex);
                            RetValue[1] = msg;
                            return RetValue;
                        }
                        catch
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    return RetValue;
                }
                return RetValue;
            }
        }
    }
}
