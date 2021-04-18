using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Controllers {
    using ExampleApi.Model;
    using ExampleApi.Services;
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase {

        private IMenuServices services_;

        public MenuController(IMenuServices services) {
            services_ = services;
        }

        List<Menu> db_ = new List<Menu> {
            new Menu {
                Id=1,ParentId=0, Url="/index", Text = "Home"
            },
            new Menu {
                Id=2, ParentId=4, Url="/index/users",Text = "User Manage"
            },
            new Menu {
                Id=3,ParentId=4,Url="/index/roles",Text="Role Manage"
            },
            new Menu {
                Id=4,ParentId=0,Url=string.Empty,Text="Team"
            }
        };
    
        [HttpGet]
        public List<MenuDto> GetMenus() {
            /*
            List<MenuDto> result = new List<MenuDto>();

            foreach(var m in db_) {
                MenuDto item = new MenuDto();

                item.Id = m.Id;
                item.Text = m.Text;
                item.Url = m.Url;

                // 找到当前节点的所有子节点 
                var childs = (from child in db_
                              where child.ParentId == item.Id
                              select child)
                             .ToList();
                
                // 根据所有的子节点的信息 构造出若干个MenuDto实体

                // 把所有的子节点 加入 item.child =>  List.AddRange

                // parentId 为0 表示 m 是 一个 1级菜单
                if(childs.Count > 0 || m.ParentId == 0 ) {
                    result.Add(item);
                }
            }

            return result;*/
            return services_.GetMenus();
        }
        
    }
}
