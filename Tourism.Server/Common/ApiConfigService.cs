using Dapper;
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
    public class ApiConfigService : IApiConfigService
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;

        public ApiConfigService()
        {
            _log = LogManager.GetLogger(typeof(ApiConfigService));
            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.CommonService.ToString());
        }

        /// <summary>
        /// 获取ApiConfig列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ApiConfig>> GetApiConfigList()
        {
            try
            {
                string sql = "SELECT * FROM ApiConfig ";

                return await _mysqlRespository.QueryListAsync(sql, new ApiConfig());

            }
            catch (Exception ex)
            {
                _log.Error("GetApiConfigList method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 获取ApiConfig信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiConfig> GetApiConfigInfo(string name)
        {
            try
            {
                string sql = "SELECT * FROM ApiConfig WHERE 1=1";
                var info = new ApiConfig();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    info.Key = name;
                    sql += " AND key=@Key";
                }
                return await _mysqlRespository.QueryInfoAsync(sql, info);
            }
            catch (Exception ex)
            {
                _log.Error("GetApiConfigInfo method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 获取ApiConfig信息根据scope
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ApiConfig>> GetApiConfigsByScope(string[] scope)
        {
            try
            {
                string sql = "SELECT * FROM ApiConfig WHERE 1=1";
                var param = new DynamicParameters();
                if (scope != null)
                {
                    sql += " AND scopes IN @scope";
                    param.Add("@scope", scope);
                }
                return await _mysqlRespository.QueryListAsync<ApiConfig, DynamicParameters>(sql, param);
            }
            catch (Exception ex)
            {
                _log.Error("GetApiConfigsByScope method error:" + ex);
                throw;
            }
        }

    }
}
