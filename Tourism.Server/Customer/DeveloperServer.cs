using log4net;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Tourism.Eums;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Repository;
using Tourism.Util;

namespace Tourism.Server
{
    public class DeveloperServer : IDeveloperServer
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;

        public DeveloperServer()
        {
            _log = LogManager.GetLogger(typeof(CustomerServer));
            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.CustomerService.ToString());
        }

        /// <summary>
        /// 根据账号信息查找开发者信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public async Task<Developer> GetDeveloperByAccount(DeveloperQuery query)
        {
            try
            {
                _log.Debug("GetDeveloperByAccount receive param:" + JsonConvert.SerializeObject(query));
                if (string.IsNullOrWhiteSpace(query.Name) || string.IsNullOrWhiteSpace(query.Secret))
                {
                    throw new Exception("Name or Secret is not empty");
                }
                string sql = "SELECT * FROM Developer WHERE name=@Name AND secret=@Secret";
                var info = MapperManager.SetMapper<Developer, DeveloperQuery>(query);
                return await _mysqlRespository.QueryInfoAsync(sql, info);
            }
            catch (Exception ex)
            {
                _log.Error("GetDeveloperByAccount method error:" + ex);
                throw;
            }
        }
    }
}
