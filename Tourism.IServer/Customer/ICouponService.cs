using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.IServer
{
    public interface ICouponService
    {
        /// <summary>
        /// 根据查询条件获取设置信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<Coupon>> GetCouponsAsync(CouponQuery query);
    }
}
