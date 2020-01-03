using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Eums;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Repository;
using Tourism.Util;

namespace Tourism.Server
{
    public class SettingService : ISettingService
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;

        public SettingService()
        {
            _log = LogManager.GetLogger(typeof(SettingService));
            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.CommonService.ToString());
        }

        /// <summary>
        /// 根据查询条件获取设置信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Setting>> GetSettingsAsync(SettingQuery query)
        {
            try
            {
                string sql = "SELECT * FROM Setting WHERE 1=1";

                if (query.Id != null)
                {
                    sql += " AND id = @Id";
                }

                if (!string.IsNullOrWhiteSpace(query.SystemCode))
                {
                    sql += " AND systemCode = @SystemCode";
                }

                if (query.TypeCode != null)
                {
                    sql += " AND typeCode = @TypeCode";
                }

                var info = MapperManager.SetMapper<Setting, SettingQuery>(query);
                return await _mysqlRespository.QueryListAsync(sql, info);

            }
            catch (Exception ex)
            {
                _log.Error("GetSettingsAsync method error:" + ex);
                throw;
            }
        }
    }
}
