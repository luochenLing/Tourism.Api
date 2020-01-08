using Dapper;
using Dapper.Contrib.Extensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Repository.MySql;

namespace Tourism.Repository
{
    public class MySqlRespostitoryByDapper : IMySqlRespostitoryByDapper
    {
        private string _dbName;
        private readonly ILog _log;
        public MySqlRespostitoryByDapper(string dbName)
        {
            _log = LogManager.GetLogger(typeof(MySqlRespostitoryByDapper));
            _dbName = dbName;
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
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    var res = await context.InsertAsync(info);

                    return res;
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
        public async Task<int> BatchAddAsync<T>(string sql, List<T> list) where T : class
        {
            try
            {
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    var res = await context.ExecuteAsync(sql, list);
                    return res;
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
        public async Task<int> DelAsync<T>(string sql, T info = null) where T : class
        {
            try
            {
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    var res = await context.ExecuteAsync(sql, info);
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
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <param name="updateQuery">更新条件</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync<T>(string sql, T info = null) where T : class
        {
            try
            {
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    return await context.ExecuteAsync(sql, info);
                }
            }
            catch (Exception ex)
            {
                _log.Error("UpdateAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selQuery">查询条件</param>
        /// <param name="updateQuery">更新条件</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync<T>(string sql, List<T> info = null) where T : class
        {
            try
            {
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    return await context.ExecuteAsync(sql, info);
                }
            }
            catch (Exception ex)
            {
                _log.Error("UpdateAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据条件查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<T> QueryInfoAsync<T>(string sql, T query = null) where T : class
        {
            try
            {
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    return await context.QueryFirstOrDefaultAsync<T>(sql, query);
                }
            }
            catch (Exception ex)
            {
                _log.Error("QueryInfoAsync method error:" + ex);
                return null;
            }
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, T info = null) where T : class
        {
            try
            {
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    return await context.QueryAsync<T>(sql, info);
                }
            }
            catch (Exception ex)
            {
                _log.Error("QueryListAsync method error:" + ex);
                return null;
            }
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="sql"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> QueryListAsync<TModel, TQuery>(string sql, TQuery info = null) where TModel : class where TQuery : class
        {
            try
            {
                using (var context = ConnectionFactory.MySqlConnection(_dbName))
                {
                    return await context.QueryAsync<TModel>(sql, info);
                }
            }
            catch (Exception ex)
            {
                _log.Error("QueryListAsync method error:" + ex);
                return null;
            }
        }

    }
}
