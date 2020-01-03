using MySql.Data.MySqlClient;
using System.Data;
using Tourism.Util;

namespace Tourism.Repository.MySql
{
    /// <summary>
    /// SQL类型数据库的数据连接生成
    /// </summary>
    public static class ConnectionFactory
    {
        public static IDbConnection MySqlConnection(string dbName)
        {
            string mysqlconnectionString = new ConfigurationManager().GetConnectionString(dbName);  //

            var connection = new MySqlConnection(mysqlconnectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
