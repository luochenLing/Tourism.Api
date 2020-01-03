using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    /// <summary>
    /// 用户类
    /// </summary>
    [Table("User")]
    public class User
    {
        /// <summary>
        /// 唯一用户GUID标识，可以用作业务身份标识
        /// </summary>
        [Key]
        public Guid CId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 客户性别
        /// </summary>
        public int? CSex { get; set; }

        /// <summary>
        /// 客户年龄
        /// </summary>
        public int? CAge { get; set; }

        /// <summary>
        /// 客户居住地址
        /// </summary>
        public string CAddress { get; set; }

        /// <summary>
        /// 客户电话号码
        /// </summary>
        public string CPhone { get; set; }

        /// <summary>
        /// 客户身份证号
        /// </summary>
        public string CIdNum { get; set; }

        /// <summary>
        /// 用户身份，目前只有管理员和普通用户两种
        /// </summary>
        public int? CIdentity { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string CNickName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string CEmail { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string CPasswd { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string CPic { get; set; }

    }
}
