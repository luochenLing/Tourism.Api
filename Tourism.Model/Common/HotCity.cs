using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tourism.Model
{
    public class HotCity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        [BsonElement]
        public string Title { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        [BsonElement]
        public string Code { get; set; }
    }
}
