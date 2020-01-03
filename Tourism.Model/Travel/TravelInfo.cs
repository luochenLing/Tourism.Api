using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    /// <summary>
    /// 旅游产品表
    /// </summary>
    [Table("TravelInfo")]
    public class TravelInfo
    {
        /// <summary>
        /// 产品唯一标识GUID即产品编号
        /// </summary>
        [Key, Column(Order = 1)]
        public Guid ProId { get; set; }

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
        /// 产品所属(县/区/乡/地区)
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 产品出发地(杭州、日本等)
        /// </summary>
        public string PlaceOfDeparture { get; set; }

        /// <summary>
        /// 产品开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 产品结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 产品标题
        /// </summary>
        public string ProTitle { get; set; }

        /// <summary>
        /// 产品价格
        /// </summary>
        public int ProPrice { get; set; }

        /// <summary>
        /// 价格单位
        /// </summary>
        public string PriceUint { get; set; }

        /// <summary>
        /// 产品目的地
        /// </summary>
        public string ProDestination { get; set; }

        /// <summary>
        /// 产品评分
        /// </summary>
        public float ProScore { get; set; }

        /// <summary>
        /// 产品使用人数
        /// </summary>
        public int ProPCount { get; set; }

        /// <summary>
        /// 产品关联标签
        /// </summary>
        public string ProTag { get; set; }

        /// <summary>
        /// 是否能使用产品优惠券
        /// </summary>
        public bool isCoupon { get; set; }

        /// <summary>
        /// 产品简介
        /// </summary>
        public string ProDes { get; set; }

        /// <summary>
        /// 产品特色
        /// </summary>
        public string ProCharacteristic { get; set; }

        /// <summary>
        /// 推荐行程
        /// </summary>
        public string ProRecommend { get; set; }

        /// <summary>
        /// 费用说明
        /// </summary>
        public string ProExplain { get; set; }

        /// <summary>
        /// 预订须知
        /// </summary>
        public string ProNotice { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string Cover { get; set; }

        /// <summary>
        /// 游玩天数
        /// </summary>
        public int ProDays { get; set; }

        /// <summary>
        /// 产品开始年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 产品开始月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 产品开始日期
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// 剩余座位
        /// </summary>
        public int Seat { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Them { get; set; }
    }
}
