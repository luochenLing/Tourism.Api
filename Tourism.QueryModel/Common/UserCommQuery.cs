using System.Collections.Generic;

namespace Tourism.QueryModel
{
    public class UserCommQuery
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 评论开始时间
        /// </summary>
        public string CommentTimeBegin { get; set; }

        /// <summary>
        /// 评论开始时间
        /// </summary>
        public string CommentTimeEnd { get; set; }

        /// <summary>
        /// 评论分数
        /// </summary>
        public double? CommentScore { get; set; }

        /// <summary>
        /// 评论上传照片/视频
        /// </summary>
        public string CommentMediaUrl { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 追加评论
        /// </summary>
        public string AdditionalComments { get; set; }

        /// <summary>
        /// 客服评论
        /// </summary>
        public string CustomerServiceReply { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public Dictionary<string, string> sortQuery = new Dictionary<string, string>();
    }
}
