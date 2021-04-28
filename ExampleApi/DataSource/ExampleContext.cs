using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ExampleApi.DataSource {
    using ExampleApi.Model;
    using Microsoft.EntityFrameworkCore;

    public class ExampleContext : DbContext {
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public ExampleContext(DbContextOptions options) : base(options) {

        }
        public override int SaveChanges() {
            //添加操作
            ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is BaseEntity).ToList()
                .ForEach(e => ((BaseEntity)e.Entity).CreateTime = DateTime.Now);

            //修改操作
            ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity).ToList()
                .ForEach(e => ((BaseEntity)e.Entity).UpdateTime = DateTime.Now);


            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Menu>().HasData(
                new Menu {
                    Id = 1,
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
                    Text = "User Manage",
                    Url = "/index/users"
                },
                new Menu {
                    Id = 4,
                    ParentId = 2,
                    Text = "Roles Manage",
                    Url = "/index/roles"
                }
            );
            modelBuilder.Entity<Role>().HasData(
                    new Role {
                        RoleId = 1,
                        RoleName = "Admin",
                        UpdateTime = DateTime.Now,
                        Auth =                             
                           @"/index/home |
                             /index/users|
                             /index/roles"
                       
                     }                    
                );
        }
    }
}
