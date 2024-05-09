using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserLoginApplication.ResponseMessages
{
    public class ResponseMessage
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string apiKey { get; set; }
    }
}