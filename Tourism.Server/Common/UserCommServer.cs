using log4net;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tourism.Eums;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Repository.MongoDb;
using Tourism.Util;

namespace Tourism.Server
{
    public class UserCommServer : IUserCommServer
    {
        private readonly ILog _log;
        private readonly MongoRepository _mongoRespository;
        public UserCommServer()
        {
            if (_log == null)
            {
                _log = LogManager.GetLogger(typeof(UserCommServer));
            }

            if (_mongoRespository == null)
            {
                _mongoRespository = new MongoRepository("Commont");
            }
        }

        /// <summary>
        /// 添加一条评论记录
        /// </summary>
        /// <param name="info">数据</param>
        /// <returns></returns>
        public async Task<int> AddUserCommAsync(UserComm info)
        {
            try
            {
                return await _mongoRespository.AddAsync(info);
            }
            catch (Exception ex)
            {
                _log.Error("AddUserCommAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 批量添加评论记录
        /// </summary>
        /// <param name="infos">数据集合</param>
        /// <returns></returns>
        public async Task<int> BatchAddUserCommAsync(List<UserComm> infos)
        {
            try
            {
                return await _mongoRespository.BatchAddAsync(infos);
            }
            catch (Exception ex)
            {
                _log.Error("BatchAddUserCommAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 删除符合条件的一条评论记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public async Task<int> DelOneUserCommAsync(UserCommQuery query)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("query is not null");
                    return -1;
                }
                var express = BuildQuery(query);

                if (express == null)
                {
                    _log.Error("query's attribute is not null");
                    return -1;
                }

                return await _mongoRespository.DelOneAsync(express);
            }
            catch (Exception ex)
            {
                _log.Error("DelOneUserCommAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 删除所有符合条件的评论记录
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public async Task<int> DelManyUserCommAsync(UserCommQuery query)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("query is not null");
                    return -1;
                }
                var express = BuildQuery(query);
                if (express == null)
                {
                    _log.Error("query's attribute is not null");
                    return -1;
                }

                return await _mongoRespository.DelManayAsync(express);
            }
            catch (Exception ex)
            {
                _log.Error("DelManyUserCommAsync method error:" + ex);
                throw;
            }
        }

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
        public async Task<(IFindFluent<UserComm, UserComm> linq, long totalCount)> PageUserCommListByQuery(Dictionary<string, string> sortQuery, UserCommQuery query, long totalCount, int pageIndex = 0, int pageSize = 0, bool showCount = false)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("query is not null");
                    return (null, 0);
                }

                var express = BuildQuery(query);

                if (express == null)
                {
                    _log.Error("query's attribute is not null");
                    return (null, 0);
                }

                var result = await _mongoRespository.PageListByQuery(sortQuery, express, totalCount, pageIndex, pageSize, showCount);
                return result;
            }
            catch (Exception ex)
            {
                _log.Error("DelOneUserCommAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 根据条件返回评论列表（不分页）
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public async Task<List<UserComm>> QueryUserCommListAsync(UserCommQuery query)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("query is not null");
                    return null;
                }
                var express = BuildQuery(query);

                var ret = await _mongoRespository.QueryListAsync(express);
                var resultList = await IAsyncCursorExtensions.ToListAsync(ret);
                return resultList;
            }
            catch (Exception ex)
            {
                _log.Error("QueryUserCommListAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 根据条件更新所有符合条件的数据
        /// </summary>
        /// <param name="query">查询信息</param>
        /// <param name="info">更新字段</param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateManyUserCommAsync(UserCommQuery query, UserComm info)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("query is not null");
                    return null;
                }
                var express = BuildQuery(query);

                if (express == null)
                {
                    _log.Error("query's attribute is not null");
                    return null;
                }

                var result = await _mongoRespository.UpdateManyAsync(info, express);

                return result;
            }
            catch (Exception ex)
            {
                _log.Error("UpdateManyUserCommAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 根据条件更新一条符合条件的数据
        /// </summary>
        /// <param name="query">查询信息</param>
        /// <param name="info">更新字段</param>
        /// <returns></returns>
        public async Task<UpdateResult> UpdateOneUserCommAsync(UserCommQuery query, UserComm info)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("query is not null");
                    return null;
                }
                var express = BuildQuery(query);

                if (express == null)
                {
                    _log.Error("query's attribute is not null");
                    return null;
                }

                var result = await _mongoRespository.UpdateOneAsync(info, express);

                return result;
            }
            catch (Exception ex)
            {
                _log.Error("UpdateOneUserCommAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        /// <param name="query">查询字段</param>
        /// <returns></returns>
        public Expression<Func<UserComm, bool>> BuildQuery(UserCommQuery query)
        {
            var oLamadaExtention = new LamadaExtention<UserComm>();

            if (!string.IsNullOrWhiteSpace(query.Id))
            {
                var Id = ObjectId.Parse(query.Id);
                oLamadaExtention.GetExpression(nameof(Id), Id, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.NickName))
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.NickName), query.NickName, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CommentTimeBegin))
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.CommentTimeBegin), query.CommentTimeBegin, ExpressionTypeEnum.GreaterThanOrEqual);
            }

            if (!string.IsNullOrWhiteSpace(query.CommentTimeEnd))
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.CommentTimeEnd), query.CommentTimeEnd, ExpressionTypeEnum.LessThanOrEqual);
            }

            if (query.CommentScore != null)
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.CommentScore), query.CommentScore, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CommentMediaUrl))
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.CommentMediaUrl), query.CommentMediaUrl, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.Content))
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.Content), query.Content, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.AdditionalComments))
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.AdditionalComments), query.AdditionalComments, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CustomerServiceReply))
            {
                oLamadaExtention.GetExpression(nameof(UserCommQuery.CustomerServiceReply), query.CustomerServiceReply, ExpressionTypeEnum.Equal);
            }

            return oLamadaExtention.GetLambda();
        }
    }
}
