using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    [Table("TravelCondition")]
    public class TravelCondition
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 条件父类型 1 日期计划 2 玩乐分类 
        /// </summary>
        public int PType { get; set; }

        /// <summary>
        /// 条件类型 1_1 计划天数 1_2 出行日期 2_1 特色推荐
        /// </summary>
        public int TypeCode { get; set; }

        /// <summary>
        /// 关联字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

    }
}
