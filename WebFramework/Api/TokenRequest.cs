using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Api
{
    public class TokenRequest
    {
        //[Required]
        public string grant_type { get; set; }
        //[Required]
        public string Username { get; set; }
        //[Required]
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
