using Microsoft.Extensions.Configuration;
using System;

namespace Api.Utilities
{
    public static class AppsettingsExtensions
    {
        public static string GetOracleConnection(IConfiguration configuration)
        {
            var ConnectionStringName = "OracleCS";
            try
            {
                if (configuration["SiteSettings:Environment"].Length > 0)
                    ConnectionStringName += configuration["SiteSettings:Environment"];
            }
            catch (Exception ex)
            {

                //Todo: log Exception
                var logMessage = ex.Message;
            }
            var result = configuration.GetConnectionString(ConnectionStringName);
            //Configs.OracleConnectionString = result;
            return result;
        }
        public static string GetSettingByEnvironment(string setting, IConfiguration configuration)
        {
            try
            {
                if (configuration["SiteSettings:Environment"].Length > 0)
                    setting += configuration["SiteSettings:Environment"];
            }
            catch (Exception ex)
            {

                //Todo: log Exception
                var logMessage = ex.Message;
            }

            return configuration[setting];
        }
    }
}
