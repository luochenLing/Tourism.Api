using System.ComponentModel.DataAnnotations;

namespace Tourism.Model
{
    /// <summary>
    /// 身份标识类
    /// </summary>
    public class Developer
    {
        /// <summary>
        /// 客户端ID
        /// </summary>
        [Key]
        public string ClientId { get; set; }

        /// <summary>
        /// 账号名
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 账号密码
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// 浏览权限
        /// </summary>
        public string ClientScope { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string ClientOrgan { get; set; }

        /// <summary>
        /// 开发者电话号码
        /// </summary>
        public string ClientPhone { get; set; }

        /// <summary>
        /// 开发者邮箱
        /// </summary>
        public string ClientEmail { get; set; }

    }
}
