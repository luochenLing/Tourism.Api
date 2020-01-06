using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tourism.Model
{
    /// <summary>
    /// 产品媒体
    /// </summary>
    [Table("MediaInfo")]
    public class MediaInfo
    {
        /// <summary>
        /// 媒体ID
        /// </summary>
        [Key, Column(Order = 1)]
        public int MId { get; set; }

        /// <summary>
        /// 媒体路径，只是图片的名称，具体路径写到配置文件
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
