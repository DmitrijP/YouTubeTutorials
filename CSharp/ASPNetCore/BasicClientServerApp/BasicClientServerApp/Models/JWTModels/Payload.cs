using System;
using System.Collections.Generic;

namespace BasicClientServerApp.Models.JWTModels
{
    public class Payload
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public double exp { get; set; }
        public double iat { get; set; }
        public int jit { get; set; }
        public string[] roles { get; set; }
        public Dictionary<string, string> claims {get;set;}
    }
}
