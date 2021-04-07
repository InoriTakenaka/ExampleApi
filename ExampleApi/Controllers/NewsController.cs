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

    // access this controller by " /api/News "
    [EnableCors("MyCorsPolicy")]
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

        /* HTTP谓词
         * GET/PUT/POST/DELETE/HEAD/OPTION ...
         * 如果标记成了HttpGet： 就意味着 这个action 只会匹配GET请求
         */

        [HttpGet]
        public ICollection<NewsDto> GetNews() {
            return news_;
        }

        /**
         * FromRoute属性意味着 这个参数 nid 要从 【路由参数】 中去匹配
         * GET:/api/News/nid/1
         *  :int -> 要匹配的是一个整形数字
         *  nid/a -> 此时就是无法匹配的
         *  ANY: /api/News/getnews/1        * 
         */
        [HttpGet("nid/{nid:int}")]
        public NewsDto GetNews([FromRoute] int nid) {
            Console.WriteLine($"query news id {nid}");
            return
            news_.Where(n => n.Id == nid)
                 .FirstOrDefault();
        }

        /**
         * HTTP谓词属性 和 Route 属性 都可以用于配置【控制器/方法】的路由
         * 针对REST Api 项目，如果一个方法同时标记了HTTP谓词属性和Route属性的话
         * 则这两个路由配置会同时生效
         * 所以一般 只会标记一个
         * 并且不会采用传统mvc项目那样 统一配置路由的方式
         * 而是直接在每个控制器的每个action上直接使用HTTP谓词属性或Route属性来对路由进行配置     
         * 
         * 在.NET Core项目中 路由参数 是无法去配置查询字符串参数的 
         *          类似这种请求地址  /api/News/s?nid=1 -> http查询字符串传参
         *          在本项目中 无法使用
         * 如果要获取查询字符串的内容，需要访问httpContext
         * 
         * POST 方式提交数据给服务器 
         * 
         * HEADER : ......
         * BODY: XXXXXX
         * 
         * FromBody属性：表示从HTTP请求报文中 获取参数所需数据 
         * 请求报文中的数据 反序列化成 json  再转换成 .NET中的对象 
         */
        [HttpPost("postnews")]
        public void PostNews([FromBody] NewsDto news) {
               Task.Run(()=> news_.Add(news));
        }


        // 1.后端跨域策略的配置与支持
        // 2.控制器路由的匹配
    }
}
