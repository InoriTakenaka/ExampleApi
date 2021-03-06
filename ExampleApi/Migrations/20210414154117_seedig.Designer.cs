// <auto-generated />
using ExampleApi.DataSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExampleApi.Migrations
{
    [DbContext(typeof(ExampleContext))]
    [Migration("20210414154117_seedig")]
    partial class seedig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("ExampleApi.Model.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ParentId = 0,
                            Text = "Home",
                            Url = "/index/home"
                        },
                        new
                        {
                            Id = 2,
                            ParentId = 0,
                            Text = "Auth",
                            Url = "Auth"
                        },
                        new
                        {
                            Id = 3,
                            ParentId = 2,
                            Text = "User Manage",
                            Url = "/index/users"
                        },
                        new
                        {
                            Id = 4,
                            ParentId = 2,
                            Text = "Roles Manage",
                            Url = "/index/roles"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
