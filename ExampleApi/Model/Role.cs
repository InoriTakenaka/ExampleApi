using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ExampleApi.Model {
    public class Role:BaseEntity {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        /// <summary>
        /// 授权人/修改人
        /// </summary>
        public string Authorizer { get; set; }
    }
    public class RolePermission : BaseEntity {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }
    public class User : BaseEntity {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
    /**
     权限：
    可以是一个菜单，一个按钮，或者一些可以访问的资源 -> 转化成URL来表示
     */
    public class Permission : BaseEntity {
        [Key]
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Url { get; set; }
    }
}
