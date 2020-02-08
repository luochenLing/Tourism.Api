using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    [Table("ClientConfig")]
    public class ClientConfig
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 客户端秘钥
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// 允许访问范围，多个竖线隔开
        /// </summary>
        public string AllowedScope { get; set; }

        /// <summary>
        /// 指定允许的URI
        /// </summary>
        public string RedirectUris { get; set; }

        /// <summary>
        /// 指定注销后重定向到的允许URI
        /// </summary>
        public string PostLogoutRedirectUris { get; set; }

        /// <summary>
        /// 只需将客户端的原点添加到集合中，IdentityServer中的默认配置将参考这些值以允许从原点进行跨源调用。
        /// </summary>
        public string[] AllowedCorsOrigins { get; set; }
    }
}
