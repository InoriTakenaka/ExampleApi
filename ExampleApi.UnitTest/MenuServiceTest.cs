using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using ExampleApi.DataSource;
using ExampleApi.Services;
using ExampleApi.Model;
using System.Collections.Generic;

namespace ExampleApi.UnitTest {
    public class MenuServiceTest {

        private List<Menu> testdata = new List<Menu>() {
                new Menu {
                    Id= 1,
                    ParentId = 0,
                    Text = "Home",
                    Url = "/index/home"
                },
                new Menu {
                    Id = 2,
                    ParentId = 0,
                    Text = "Auth",
                    Url = "Auth"
                },
                new Menu {
                    Id = 3,
                    ParentId = 2,
                    Text="User Manage",
                    Url="/index/users"
                },
                new Menu {
                    Id=4,
                    ParentId = 2,
                    Text="Roles Manage",
                    Url="/index/roles"
                }
        };

        private async Task<ExampleContext> GetSqliteDbContextAsync() {
            //
            var options = new DbContextOptionsBuilder<ExampleContext>()
                //.UseSqlite("Data source = E:\\workspace\\SE 2\\ExampleApi\\DB\\example.test.db");
                .UseInMemoryDatabase(new Guid().ToString())
                .Options;
            var sqliteDbContext = new ExampleContext(options);
            sqliteDbContext.Menus.AddRange(testdata);
            await sqliteDbContext.SaveChangesAsync();
            return  sqliteDbContext;
        }


        [Fact]
        public async Task GetMenusTest() {
            // arrange 
            var context = await GetSqliteDbContextAsync();
            var services = new MenuServices(context);
            // action 
            var getMenusResult = services.GetMenus();
            // assertion
            Assert.NotEmpty(getMenusResult);
        }
    }
}
