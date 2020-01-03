using MongoDB.Driver;
using Tourism.Eums;
using Tourism.Util;

namespace Tourism.DBContext
{
    public class MongoDbContext
    {
        private ConfigurationManager _config;
        public MongoDbContext()
        {
            _config = new ConfigurationManager();
        }

        /// <summary>
        /// 获取指定集合
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="dbName">数据库名称</param>
        /// <param name="document">集合名称</param>
        /// <returns></returns>
        public IMongoCollection<T> GetMongoCollection<T>(string dbName, string document) where T : class
        {
            var mongoUrl = new MongoUrlBuilder(_config.GetConnectionString(DbNameEnum.MongodServer.ToString()));
            var client = new MongoClient(mongoUrl.ToMongoUrl());
            var database = client.GetDatabase(dbName);
            var collection = database.GetCollection<T>(document);

            return collection;
        }
    }
}
