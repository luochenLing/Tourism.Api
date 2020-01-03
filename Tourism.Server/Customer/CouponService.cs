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
    public class CouponService : ICouponService
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;

        public CouponService()
        {
            _log = LogManager.GetLogger(typeof(CouponService));
            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.CustomerService.ToString());
        }

        /// <summary>
        /// 根据查询条件获取设置信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Coupon>> GetCouponsAsync(CouponQuery query)
        {
            try
            {
                string sql = "SELECT * FROM Coupon WHERE 1=1";

                if (query.UserId != null)
                {
                    sql += " AND userId = @UserId";
                }

                var info = MapperManager.SetMapper<Coupon, CouponQuery>(query);
                return await _mysqlRespository.QueryListAsync(sql, info);

            }
            catch (Exception ex)
            {
                _log.Error("GetCouponsAsync method error:" + ex);
                throw;
            }
        }


    }
}
