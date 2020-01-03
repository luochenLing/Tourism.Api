using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Tourism.Model
{
    public class UserComm
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [BsonElement]
        public string NickName { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        [BsonElement]
        public DateTime CommentTime { get; set; }

        /// <summary>
        /// 评论分数
        /// </summary>
        [BsonElement]
        public double CommentScore { get; set; }

        /// <summary>
        /// 评论上传照片/视频
        /// </summary>
        [BsonElement]
        public string CommentMediaUrl { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [BsonElement]
        public string Content { get; set; }

        /// <summary>
        /// 追加评论
        /// </summary>
        [BsonElement]
        public string AdditionalComments { get; set; }

        /// <summary>
        /// 客服评论
        /// </summary>
        [BsonElement]
        public string CustomerServiceReply { get; set; }


    }
}
