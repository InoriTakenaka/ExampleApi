using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*----------------------------------*/
using Newtonsoft.Json;
/*----------------------------------*/
namespace ExampleApi.Model {
    public class MessageDto {
        [JsonProperty("mid")]
        public uint Id { get; set; }

        [JsonProperty("mtitle")]
        public string Title { get; set; }

        [JsonProperty("mfrom")]
        public string From { get; set; }

        [JsonProperty("mtime")]
        public DateTime Time { get; set; }

        [JsonProperty("mbody")]
        public string Body { get; set; }
    }
}
