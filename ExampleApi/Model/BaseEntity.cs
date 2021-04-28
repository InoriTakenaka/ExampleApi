using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Model {
    /// <summary>
    /// 所有实体的基类
    /// </summary>
    public class BaseEntity {
        /// <summary>
        /// 记录的更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 记录的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
