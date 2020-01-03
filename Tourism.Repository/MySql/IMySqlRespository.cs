using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tourism.Repository
{
    public interface IMySqlRespository
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
        Task<int> BatchAddAsync<T>(List<T> list) where T : class;

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <returns></returns>
        Task<int> DelAsync<T>(Expression<Func<T, bool>> selQuery) where T : class;

        /// <summary>
        /// 根据sql查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql</param>
        /// <param name="sqlParams">sql的参数</param>
        /// <returns></returns>
        Task<List<T>> OperatingDbBySqlQueryAsync<T>(string sql, params object[] sqlParams) where T : class;

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sort">排序方式</param>
        /// <param name="order">排序字段</param>
        /// <param name="totalCount">数据总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="showCount">是否返回查询总数</param>
        /// <returns></returns>
        IQueryable<T> PageListByQueryAsync<T>(Expression<Func<T, bool>> query, string sort, string order, int pageIndex = 0, int pageSize = 0, bool showCount = false) where T : class;

        /// <summary>
        /// 根据条件查询不分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryable<T> QueryList<T>(Expression<Func<T, bool>> query) where T : class;


        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        IQueryable<T> QueryInfo<T>(Expression<Func<T, bool>> query) where T : class;


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <param name="updateQuery">更新条件</param>
        /// <returns></returns>
        Task<int> UpdateAsync<T>(Expression<Func<T, bool>> selQuery, Expression<Func<T, T>> updateQuery) where T : class;

    }
}
