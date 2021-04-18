using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Services {
    public interface IMenuServices {
        abstract List<ExampleApi.Model.MenuDto> GetMenus();
    }
}
