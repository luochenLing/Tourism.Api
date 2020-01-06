using System.Collections.Generic;

namespace Tourism.Idp.Model
{
    public class ClientConfig
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端秘钥
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 允许访问的服务
        /// </summary>
        public ICollection<string> AllowedScopes { get; set; }
    }
}
