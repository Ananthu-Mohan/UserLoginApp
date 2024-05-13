using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserLoginApplication.Models;

namespace UserLoginApplication.ResponseMessages
{
    public class ResponseMessage
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string apiKey { get; set; }
        public DateTime apiKeyExpiration { get; set; }
        public IdentityModel userDets { get; set; }
    }
}