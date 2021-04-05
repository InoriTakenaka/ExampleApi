using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*-------------------------------------*/
using Microsoft.AspNetCore.Cors;
using ExampleApi.Model;
/*-------------------------------------*/

namespace ExampleApi.Controllers {
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase {
        private List<NewsDto> news_ = new List<NewsDto> {
            new NewsDto {
                Id=1,
                Title="abc",
                Time = DateTime.UtcNow,
                Content = "news content abc edf"
            },
            new NewsDto {
                Id= 2,
                Title = "cde",
                Time = DateTime.UtcNow,
                Content = "new content cde fgh"
            }
        };
        
        [HttpGet]
        public ICollection<NewsDto> GetNews() {
            return news_;
        }

        [Route("api/[controller]/nid/{nid}")]
        [HttpGet]
        public NewsDto GetNews([FromRoute] int nid) {
            return
            news_.Where(n => n.Id == nid)
                 .FirstOrDefault();
        }
    }
}
