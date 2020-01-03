using System;

namespace Tourism.QueryModel
{
    public class TravelActivityQuery : BaseQuery
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 相关信息ID
        /// </summary>
        public Guid? PId { get; set; }

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
