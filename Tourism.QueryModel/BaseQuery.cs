namespace Tourism.QueryModel
{
    public class BaseQuery
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }



        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// 是否显示总数
        /// </summary>
        public bool ShowCount { get; set; }
    }

    public struct TotalCount
    {
        public int count { get; set; }
    }
}
