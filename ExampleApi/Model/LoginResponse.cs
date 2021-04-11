using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleApi.Model {
    public abstract class Response {
        /// <summary>
        /// request result code
        /// success: 0
        /// failed : 1
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// extra message
        /// </summary>
        public string message { get; set; }
    }

    public class LoginResponse:Response {
        public string username { get; set; }
    }
}
