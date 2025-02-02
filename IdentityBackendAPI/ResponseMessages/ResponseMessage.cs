﻿using IdentityBackendAPI.Models;

namespace IdentityBackendAPI.ResponseMessages
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
