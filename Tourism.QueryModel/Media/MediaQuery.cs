using System;

namespace Tourism.QueryModel
{
    /// <summary>
    /// 产品媒体
    /// </summary>
    public class MediaQuery
    {
        /// <summary>
        /// 媒体ID
        /// </summary>
        public int MId { get; set; }

        /// <summary>
        /// 媒体路径
        /// </summary>
        public string MUrl { get; set; }

        /// <summary>
        /// 对应产品ID
        /// </summary>
        public Guid MPid { get; set; }

        /// <summary>
        /// 图像/视频描述
        /// </summary>
        public string MDes { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public int MType { get; set; }
    }
}
