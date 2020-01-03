using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tourism.Repository
{
    public interface IMySqlRespostitoryByDapper
    {
        /// <summary>
        /// 新增单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info">数据源</param>
        /// <returns></returns>
        Task<int> AddAsync<T>(T info) where T : class;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        Task<int> BatchAddAsync<T>(string sql, List<T> list) where T : class;

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <returns></returns>
        Task<int> DelAsync<T>(string sql, T info) where T : class;

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <param name="updateQuery">更新条件</param>
        /// <returns></returns>
        Task<int> UpdateAsync<T>(string sql, T info) where T : class;

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <param name="updateQuery">更新条件</param>
        /// <returns></returns>
        Task<int> UpdateAsync<T>(string sql, List<T> info) where T : class;

        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<T> QueryInfoAsync<T>(string sql, T query) where T : class;

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryListAsync<T>(string sql, T info) where T : class;

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="sql"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<IEnumerable<TModel>> QueryListAsync<TModel, TQuery>(string sql, TQuery info) where TModel : class where TQuery : class;

    }
}
