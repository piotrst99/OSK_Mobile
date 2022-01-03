using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK_Mobile.Models
{
    public class LoginResult{
        [JsonProperty("result")]
        public bool result { get; set; }
    }
}
