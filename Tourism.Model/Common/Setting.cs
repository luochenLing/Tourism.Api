using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    [Table("Setting")]
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 系统模块编码 旅游模块:travel 
        /// </summary>
        public string SystemCode { get; set; }

        /// <summary>
        /// 类型编码 1.首页轮播图
        /// </summary>
        public int TypeCode { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

    }
}
