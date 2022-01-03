using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSK_Mobile.Models
{
    public class UserData{
        /// PERSONAL DATA
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("secondName")]
        public string SecondName { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("pesel")]
        public string PESEL { get; set; }

        /// ADDRESS DATA
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("nrHome")]
        public string NrHome { get; set; }

        [JsonProperty("nrLocal")]
        public string NrLocal { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("postCode")]
        public string PostCode { get; set; }

        [JsonProperty("post")]
        public string Post { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        /// INSTRUCTOR
        [JsonProperty("nrInstructor")]
        public string NrInstructor { get; set; }
    }
}
