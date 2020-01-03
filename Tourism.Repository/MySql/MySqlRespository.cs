using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tourism.DBContext;
using Tourism.QueryModel;
using Tourism.Util;

namespace Tourism.Repository
{
    public class MySqlRespository : IMySqlRespository
    {
        private MySqlContext _context;
        private readonly ILog _log;
        public MySqlRespository(MySqlContext contextObj)
        {

            _log = LogManager.GetLogger(typeof(MySqlRespository));
            _context = contextObj;
        }

        /// <summary>
        /// 新增单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info">数据源</param>
        /// <returns></returns>
        public async Task<int> AddAsync<T>(T info) where T : class
        {
            try
            {
                using (_context)
                {
                    await _context.Set<T>().AddAsync(info);
                    _context.SaveChanges();

                    return 0;
                }
            }
            catch (Exception ex)
            {
                _log.Error("AddAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据列表</param>
        /// <returns></returns>
        public async Task<int> BatchAddAsync<T>(List<T> list) where T : class
        {
            try
            {
                using (_context)
                {
                    await _context.AddRangeAsync(list);
                    _context.SaveChanges();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _log.Error("BatchAddAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <returns></returns>
        public async Task<int> DelAsync<T>(Expression<Func<T, bool>> selQuery) where T : class
        {
            try
            {
                using (_context)
                {
                    var res = await _context.Set<T>().Where(selQuery).DeleteFromQueryAsync();
                    return res;
                }
            }
            catch (Exception ex)
            {
                _log.Error("DelAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据sql查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql</param>
        /// <param name="sqlParams">sql的参数</param>
        /// <returns></returns>
        public Task<List<T>> OperatingDbBySqlQueryAsync<T>(string sql, params object[] sqlParams) where T : class
        {
            try
            {
                return _context.Set<T>().FromSqlRaw(sql, sqlParams).ToListAsync();
            }
            catch (Exception ex)
            {
                _log.Error("OperatingDbBySqlQueryAsync method error:" + ex);
                return null;
            }
        }

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
        public IQueryable<T> PageListByQueryAsync<T>(Expression<Func<T, bool>> query, string sort, string order, int pageIndex = 0, int pageSize = 0, bool showCount = false) where T : class
        {
            try
            {
                if (pageIndex <= 0 || pageSize <= 0)
                {
                    _log.Error("pageIndex and pageSize more than 0");
                    throw new Exception("pageIndex and pageSize more than 0");
                }
                using (_context)
                {
                    var ret = _context.Set<T>().Where(query);
                    var res = SqlHandler.SetQueryableOrder(ret, sort, order, pageIndex, pageSize);
                    if (showCount)
                    {
                        TotalCount totalCount = new TotalCount();
                        totalCount.count = ret.Count();
                    }
                    return res;
                }
            }
            catch (Exception ex)
            {
                _log.Error("PageListByQueryAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 根据条件查询不分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<T> QueryList<T>(Expression<Func<T, bool>> query) where T : class
        {
            try
            {
                var ret = _context.Set<T>().Where(query);
                return ret;
            }
            catch (Exception ex)
            {
                _log.Error("QueryListAsync method error:" + ex);
                return null;
            }
        }

        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<T> QueryInfo<T>(Expression<Func<T, bool>> query) where T : class
        {
            try
            {
                return _context.Set<T>().Where(query);
            }
            catch (Exception ex)
            {
                _log.Error("QueryInfo method error:" + ex);
                return null;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <param name="updateQuery">更新条件</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync<T>(Expression<Func<T, bool>> selQuery, Expression<Func<T, T>> updateQuery) where T : class
        {
            try
            {
                using (_context)
                {
                    return await _context.Set<T>().Where(selQuery).UpdateFromQueryAsync(updateQuery);
                }
            }
            catch (Exception ex)
            {
                _log.Error("UpdateAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 在SQL语句使用EF后进行释放连接
        /// </summary>
        public void ReleaseConText()
        {
            _context.Dispose();
        }
    }
}
