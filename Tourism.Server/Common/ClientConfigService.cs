using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Eums;
using Tourism.IServer;
using Tourism.Model;
using Tourism.Repository;

namespace Tourism.Server
{
    public class ClientConfigService : IClientConfigService
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;

        public ClientConfigService()
        {
            _log = LogManager.GetLogger(typeof(ClientConfigService));
            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.CommonService.ToString());
        }

        /// <summary>
        /// 获取ClientConfig列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ClientConfig>> GetClientConfigList()
        {
            try
            {
                var info = new ClientConfig();

                string sql = "SELECT * FROM ClientConfig ";
                return await _mysqlRespository.QueryListAsync(sql, info);

            }
            catch (Exception ex)
            {
                _log.Error("GetApiConfigList method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 获取ClientConfig一条信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<ClientConfig> GetClientConfigInfo(string clientId)
        {
            try
            {
                var info = new ClientConfig();

                string sql = "SELECT * FROM ClientConfig WHERE 1=1";
                if (!string.IsNullOrWhiteSpace(clientId))
                {
                    info.ClientId = clientId;
                    sql += " AND clientId=@ClientId";
                }
                return await _mysqlRespository.QueryInfoAsync(sql, info);

            }
            catch (Exception ex)
            {
                _log.Error("GetApiConfigList method error:" + ex);
                throw;
            }
        }
    }
}
