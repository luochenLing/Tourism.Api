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
        public string Id { get; set; }

        /// <summary>
        /// 账号名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号密码
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        public string Organ { get; set; }

        /// <summary>
        /// 开发者电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 开发者邮箱
        /// </summary>
        public string Email { get; set; }

    }
}
