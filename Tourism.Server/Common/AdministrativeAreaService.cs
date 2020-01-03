using log4net;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tourism.IServer;
using Tourism.Model;
using Tourism.Repository.MongoDb;

namespace Tourism.Server
{
    public class AdministrativeAreaService : IAdministrativeAreaService
    {
        private readonly ILog _log;
        private readonly IMongoRepository _mongoRepository;
        public AdministrativeAreaService()
        {
            _log = LogManager.GetLogger(typeof(AdministrativeAreaService));
            _mongoRepository = new MongoRepository("Commont");
        }

        /// <summary>
        /// 获取热门城市
        /// </summary>
        /// <returns></returns>
        public async Task<List<HotCity>> GetHotCityListAsync()
        {
            try
            {
                Expression<Func<HotCity, bool>> BuildQuery = x => true;
                var res = await _mongoRepository.QueryListAsync(BuildQuery);
                return await res.ToListAsync();
            }
            catch (Exception ex)
            {
                _log.Error("GetCouponsAsync method error:" + ex);
                throw;
            }
        }

    }
}
