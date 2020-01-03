using log4net;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tourism.DBContext;
using Tourism.DBContext.Customer;
using Tourism.IServer.Customer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Repository.MySql;

namespace Tourism.Server.Customer
{
    public class DeveloperServer : IDeveloperServer
    {
        private readonly ILog _log;
        private readonly MySqlRespository _mysqlRespository;

        public DeveloperServer()
        {
            if (_log == null)
            {
                _log = LogManager.GetLogger(typeof(CustomerServer));
            }

            if (_mysqlRespository == null)
            {
                MySqlContext context = new CustomerContext();
                _mysqlRespository = new MySqlRespository(context);
            }
        }

        /// <summary>
        /// 根据账号查找开发者信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public async Task<Developer> GetDeveloperByAccount(DeveloperQuery query)
        {
            try
            {
                _log.Debug("AddCustomerInfoAsync receive param:" + JsonConvert.SerializeObject(query));
                if (string.IsNullOrWhiteSpace(query.ClientName) || string.IsNullOrWhiteSpace(query.ClientSecret))
                {
                    throw new Exception("ClientName or ClientSecret is not empty");
                }
                return await _mysqlRespository.QueryInfo<Developer>(t => t.ClientName == query.ClientName && t.ClientSecret == query.ClientSecret).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _log.Error("GetDeveloperByAccount method error:" + ex);
                throw;
            }
        }
    }
}
