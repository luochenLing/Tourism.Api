namespace Tourism.QueryModel
{
    public class TravelConditionQuery
    {
        /// <summary>
        /// 计划天数
        /// </summary>
        public int[] dateList { get; set; }

        /// <summary>
        /// 出行时间
        /// </summary>
        public int[] travelTimeList { get; set; }

        /// <summary>
        /// 特色推荐
        /// </summary>
        public int[] specialList { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string orderFiled { get; set; }
    }
}
