using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    [Table("TravelActivity")]
    public class TravelActivity
    {
        /// <summary>
        /// 产品活动ID
        /// </summary>
        [Key, Column(Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// 相关信息ID
        /// </summary>
        public Guid PId { get; set; }

        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 活动标签
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; set; }
    }
}
