﻿namespace Siemens.API.Models.Auth
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
