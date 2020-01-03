using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.IServer
{
    public interface IUserCommServer
    {
        /// <summary>
        /// 批量添加评论记录
        /// </summary>
        /// <param name="infos">数据集合</param>
        /// <returns></returns>
        Task<int> BatchAddUserCommAsync(List<UserComm> infos);

        /// <summary>
        /// 删除符合条件的一条评论记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        Task<int> DelOneUserCommAsync(UserCommQuery query);

        /// <summary>
        /// 删除所有符合条件的评论记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        Task<int> DelManyUserCommAsync(UserCommQuery query);

        /// <summary>
        /// 根据条件分页显示分页评论信息
        /// </summary>
        /// <param name="sortQuery">排序条件</param>
        /// <param name="query">查询条件</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="showCount">是否显示总数</param>
        /// <returns></returns>
        Task<(IFindFluent<UserComm, UserComm> linq, long totalCount)> PageUserCommListByQuery(Dictionary<string, string> sortQuery, UserCommQuery query, long totalCount, int pageIndex = 0, int pageSize = 0, bool showCount = false);

        /// <summary>
        /// 根据条件返回评论列表（不分页）
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        Task<List<UserComm>> QueryUserCommListAsync(UserCommQuery query);

        /// <summary>
        /// 根据条件更新所有符合条件的数据
        /// </summary>
        /// <param name="query">查询信息</param>
        /// <param name="info">更新字段</param>
        /// <returns></returns>
        Task<UpdateResult> UpdateManyUserCommAsync(UserCommQuery query, UserComm info);

        /// <summary>
        /// 根据条件更新一条符合条件的数据
        /// </summary>
        /// <param name="query">查询信息</param>
        /// <param name="info">更新字段</param>
        /// <returns></returns>
        Task<UpdateResult> UpdateOneUserCommAsync(UserCommQuery query, UserComm info);
    }
}
