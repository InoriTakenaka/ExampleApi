using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
/*------------------------------------------*/
using Newtonsoft.Json;
/*------------------------------------------*/
namespace ExampleApi.Model {
    public class LoginRequestDto {
        [Required]
        [JsonProperty("username")]
        public string UserName { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
