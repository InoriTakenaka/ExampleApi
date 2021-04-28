using System.Collections.Generic;
using System.Linq;

namespace ExampleApi.Services {
    using ExampleApi.DataSource;
    using ExampleApi.Model;

    public class MenuServices:IMenuServices {
        private ExampleContext dbContext_ { set; get; }
        public MenuServices(ExampleContext context) {
            dbContext_ = context;
        }
        
       public List<MenuDto> GetMenus() {
            var menus = dbContext_.Menus.ToList();
            return LoadMenus(menus);
        }
       
        private List<MenuDto> LoadMenus(List<Menu> menus) {

            List<MenuDto> result = new List<MenuDto>();

            menus.ForEach(m => {
                if (m.ParentId == 0) {
                    MenuDto item = new MenuDto {
                        Id = m.Id,
                        Text = m.Text,
                        Url = m.Url
                    };

                    var children = menus.FindAll(e => e.ParentId == m.Id);
                    if (children.Count !=0 ) {
                        List<MenuDto> itemChild = new List<MenuDto>();
                        children.ForEach(m => {
                            itemChild.Add(new MenuDto {
                                Id = m.Id,
                                Text = m.Text,
                                Url = m.Url
                            });
                        });
                        item.Child = new List<MenuDto>();
                        item.Child.AddRange(itemChild);
                    }
                    result.Add(item);
                }
            });

            return result;
        }
    }
}
