using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static System.Console;
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
        // api/Menu 
        [HttpGet]
        public List<MenuDto> GetMenus() {
            WriteLine("/api/Menu");
            return services_.GetMenus();
        }

        /*
        
        [HttpGet("/getmenutree")] 
        public List<TreeData> GetMenuTree() {

        }
        */

    }
}
