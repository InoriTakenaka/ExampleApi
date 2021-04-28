using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationCenterApi {
    /// <summary>
    /// 与AppSettings里面的JwtToken结构保持一致
    /// </summary>
    public class TokenManagement {
        /// <summary>
        /// 密钥
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }
        /// <summary>
        /// 签发人
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
        /// <summary>
        /// 受众：这个token可以用于访问谁
        /// </summary>
        [JsonProperty("audience")]
        public string Audience { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }
        /// <summary>
        /// 刷新时间
        /// </summary>
        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}
