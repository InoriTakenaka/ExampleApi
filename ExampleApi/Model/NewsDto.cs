using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*----------------------------------*/
using Newtonsoft.Json;
/*----------------------------------*/

/*
 * 新闻实体 
 */
namespace ExampleApi.Model {
    public class NewsDto {
        [JsonProperty("nid")]
        public uint Id { get; set; }
        [JsonProperty("ntitle")]
        public string Title { get; set; }
        [JsonProperty("ncontent")]
        public string Content { get; set; }
        [JsonProperty("ntime")]
        public DateTime Time { get; set; }
    }
}
