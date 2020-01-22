using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.IServer
{
    public interface ITravelInfoService
    {
        /// <summary>
        /// 根据条件查询产品列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<TravelInfo>> GetTravelInfoListAsync(TravelInfoQuery query);

        /// <summary>
        /// 根据ID查找产品详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TravelInfo> GetTravelInfoAsync(TravelInfoQuery query);

        /// <summary>
        /// 根据条件搜索活动列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<TravelActivity>> GetTravelActivityListByQuery(TravelActivityQuery query);

        /// <summary>
        /// 根据地名查找产品详细信息
        /// </summary>
        /// <param name="areaCondition"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<TravelInfo>> GetTravelListByAreaAsync(string areaCondition, int pageIndex, int pageSize);

        /// <summary>
        /// 获取筛选条件
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TravelCondition>> GetTravelConditionListAsync();

    }
}
