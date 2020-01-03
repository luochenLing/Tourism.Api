using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    [Table("Coupon")]
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 所属用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 优惠券标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 优惠券面额
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 货币单位
        /// </summary>
        public string PriceUnit { get; set; }

        /// <summary>
        /// 优惠券内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 优惠券简介
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否已经使用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 是否领取
        /// </summary>
        public bool IsReceive { get; set; }

        /// <summary>
        /// 有效开始日期
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 有效结束日期
        /// </summary>
        public DateTime EndTime { get; set; }

    }
}
