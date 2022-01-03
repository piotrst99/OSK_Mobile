using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK_Mobile.Models
{
    public class UserName
    {
        [JsonProperty("userID")]
        public int userID { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }
    }
}
