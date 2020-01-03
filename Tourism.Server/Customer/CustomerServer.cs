﻿using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tourism.Eums;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Repository;
using Tourism.Util;

namespace Tourism.Server
{
    public class CustomerServer : ICustomerServer
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;
        public CustomerServer()
        {
            _log = LogManager.GetLogger(typeof(CustomerServer));
            _mysqlRespository = new MySqlRespostitoryByDapper(nameof(CustomerServer));
        }

        /// <summary>
        /// 添加一条用户信息
        /// </summary>
        /// <param name="info">要添加的用户数据</param>
        /// <returns></returns>
        public async Task<int> AddCustomerInfoAsync(User info)
        {
            try
            {
                return await _mysqlRespository.AddAsync(info);
            }
            catch (Exception ex)
            {
                _log.Error("AddCustomerInfoAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 批量添加用户信息
        /// </summary>
        /// <param name="customers">要添加的用户列表</param>
        /// <returns></returns>
        public async Task<int> BatchAddCustomerInfoAsync(List<User> customers)
        {
            try
            {
                string sql = @"INSERT INTO `User`
                                (`cId`,`cName`, `cSex`, `cAge`, `cAddress`, `cPhone`, `cIdNum`, `cIdentity`, `cNickName`, `cEmail`, `cPasswd`, `cPic`) 
                                VALUES(@CId,@cName,@cSex,@cAge,@cAddress,@cPhone,@cIdNum,@cIdentity,@cNickName,@cEmail,@cPasswd,@cPic)";
                return await _mysqlRespository.BatchAddAsync(sql, customers);
            }
            catch (Exception ex)
            {
                _log.Error("BatchAddCustomerInfoAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据参数删除用户信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public async Task<int> DelCustomerInfoByIdAsync(string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                {
                    _log.Error("DelCustomerInfoByQueryAsync's error：id is not null");
                    throw new Exception("DelCustomerInfoByQueryAsync's error：id is not null");
                }
                string sql = "DELETE FROM `User` WHERE cId=@id";
                return await _mysqlRespository.DelAsync(sql, Id);
            }
            catch (Exception ex)
            {
                _log.Error("DelCustomerInfoByQueryAsync method error:" + ex);
                return -1;
            }
        }

        /// <summary>
        /// 根据参数获取用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetCustomerInfoByQueryAsync(UserQuery query)
        {
            try
            {
                Expression<Func<User, bool>> selQuery = BuildQuery(query);

                if (selQuery == null)
                {
                    _log.Error("GetCustomerInfoByQueryAsync's error：selQuery is not null");
                    throw new Exception("GetCustomerInfoByQueryAsync's error：selQuery is not null");
                }
                string sql = "SELECT * FROM `User` WHERE cId=@cId ORDER BY cId DESC LIMIT 0,10 ";
                var info = SetMapper(query);
                var res = await _mysqlRespository.QueryListAsync(sql, info);
                return res;
            }
            catch (Exception ex)
            {
                _log.Error("GetCustomerInfoByQueryAsync method error:" + ex);
                throw;
            }
        }

        ///// <summary>
        ///// 根据参数获取用户信息（分页）
        ///// </summary>
        ///// <param name="query">查询实体</param>
        ///// <param name="sort">排序字段</param>
        ///// <param name="order">排序方式</param>
        ///// <param name="totalCount">数据总数</param>
        ///// <param name="pageIndex">页码</param>
        ///// <param name="pageSize">每页数据条数</param>
        ///// <param name="showCount">是否返回数据总数</param>
        ///// <returns></returns>
        //public async Task<List<User>> GetCustomerPageListByQueryAsync(UserQuery query, string sort, string order, int pageIndex = 0, int pageSize = 0, bool showCount = false)
        //{
        //    try
        //    {
        //        Expression<Func<User, bool>> selQuery = BuildQuery(query);
        //        if (selQuery == null)
        //        {
        //            _log.Error("GetCustomerPageListByQueryAsync's error：selQuery is not null");
        //            throw new Exception("GetCustomerPageListByQueryAsync's error：selQuery is not null");
        //        }
        //        return await _mysqlRespository.PageListByQueryAsync(selQuery, sort, order, pageIndex, pageSize, showCount).ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error("GetCustomerPageListByQueryAsync method error:" + ex);
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// 根据参数修改用户信息
        ///// </summary>
        ///// <param name="query">查询参数</param>
        ///// <param name="updateData">修改信息</param>
        ///// <returns></returns>
        //public async Task<int> UpdateCustomerInfoByQueryAsync(UserQuery query, User updateData)
        //{
        //    try
        //    {
        //        Expression<Func<User, bool>> selQuery = BuildQuery(query);


        //        if (selQuery == null)
        //        {
        //            _log.Error("UpdateCustomerInfoByQueryAsync's error：selQuery is not null");
        //            throw new Exception("UpdateCustomerInfoByQueryAsync's error：selQuery is not null");
        //        }
        //        //不修改密码和用户身份标识
        //        return await _mysqlRespository.UpdateAsync(selQuery, x => new User { CName = updateData.CName, CSex = updateData.CSex, CAge = updateData.CAge, CAddress = updateData.CAddress, CPhone = updateData.CPhone, CIdNum = updateData.CIdNum, CIdentity = updateData.CIdentity, CNickName = updateData.CNickName, CEmail = updateData.CEmail, CPic = updateData.CPic });
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error("UpdateCustomerInfoByQueryAsync method error:" + ex);
        //        return -1;
        //    }
        //}

        ///// <summary>
        ///// 根据sql查询用户数据
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sql">sql</param>
        ///// <param name="sqlParams">sql的参数</param>
        ///// <returns></returns>
        //public async Task<List<User>> CustomerListBySqlQueryAsync(string sql, params object[] sqlParams)
        //{
        //    try
        //    {
        //        var res = await _mysqlRespository.OperatingDbBySqlQueryAsync<User>(sql, sqlParams);
        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error("CustomerListBySqlQueryAsync method error:" + ex);
        //        throw;
        //    }
        //}

        /// <summary>
        /// 构建ef查询条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private Expression<Func<User, bool>> BuildQuery(UserQuery query)
        {
            var oLamadaExtention = new LamadaExtention<User>();

            if (query.CId != null)
            {
                oLamadaExtention.GetExpression(nameof(User.CId), query.CId, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CName))
            {
                oLamadaExtention.GetExpression(nameof(User.CName), query.CName, ExpressionTypeEnum.Equal);
            }

            if (query.CSex != null)
            {
                oLamadaExtention.GetExpression(nameof(User.CSex), query.CSex, ExpressionTypeEnum.Equal);
            }

            if (query.CAge != null)
            {
                oLamadaExtention.GetExpression(nameof(User.CAge), query.CAge, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CAddress))
            {
                oLamadaExtention.GetExpression(nameof(User.CAddress), query.CAddress, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CPhone))
            {
                oLamadaExtention.GetExpression(nameof(User.CPhone), query.CPhone, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CIdNum))
            {
                oLamadaExtention.GetExpression(nameof(User.CIdNum), query.CIdNum, ExpressionTypeEnum.Equal);
            }

            if (query.CIdentity != null)
            {
                oLamadaExtention.GetExpression(nameof(User.CIdentity), query.CIdentity, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CNickName))
            {
                oLamadaExtention.GetExpression(nameof(User.CNickName), query.CNickName, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CEmail))
            {
                oLamadaExtention.GetExpression(nameof(User.CEmail), query.CEmail, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CPasswd))
            {
                oLamadaExtention.GetExpression(nameof(User.CPasswd), query.CPasswd, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.CPic))
            {
                oLamadaExtention.GetExpression(nameof(User.CPic), query.CPic, ExpressionTypeEnum.Equal);
            }

            return oLamadaExtention.GetLambda();
        }

        private User SetMapper(UserQuery query)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserQuery, User>());
            var mapper = config.CreateMapper();
            User info = mapper.Map<UserQuery, User>(query);
            return info;
        }
    }
}
