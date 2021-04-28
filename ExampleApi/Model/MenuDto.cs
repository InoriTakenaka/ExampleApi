using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Model {
    public class Menu {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
    }
    // Dto: 数据传输对象 
    public class MenuDto {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }
        public List<MenuDto> Child { get; set; }
    }

    public class TreeData {
        public string key { get; set; }
        public string title { get; set; }
        public List<TreeData> children { get; set; }
    }
}
