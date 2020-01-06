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
    }
}
