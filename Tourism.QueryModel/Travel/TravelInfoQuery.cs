using System;

namespace Tourism.QueryModel
{
    /// <summary>
    /// 旅游产品表
    /// </summary>
    public class TravelInfoQuery : BaseQuery
    {
        /// <summary>
        /// 产品唯一标识FGUID即产品编号
        /// </summary>
        public Guid? ProId { get; set; }

        /// <summary>
        /// 产品类型(定制游、跟团游等)，有可能属于多个类型：eg：尾单和跟团
        /// </summary>
        public string ProType { get; set; }

        /// <summary>
        /// 产品所属(国家)
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 产品所属(城市)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 产品所属地区
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 产品出发地(杭州、日本等)
        /// </summary>
        public string PlaceOfDeparture { get; set; }

        /// <summary>
        /// 产品开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 产品结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 产品标题
        /// </summary>
        public string ProTitle { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public int ProPrice { get; set; }

        /// <summary>
        /// 产品目的地
        /// </summary>
        public string ProDestination { get; set; }

        /// <summary>
        /// 产品关联标签
        /// </summary>
        public string ProTag { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public int[] Them { get; set; }

        /// <summary>
        /// 游玩天数
        /// </summary>
        public int[] ProDays { get; set; }

        /// <summary>
        /// 产品开始年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 产品开始月份
        /// </summary>
        public int[] Month { get; set; }

        /// <summary>
        /// 产品开始日期
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 剩余座位
        /// </summary>
        public int Seat { get; set; }

    }
}
