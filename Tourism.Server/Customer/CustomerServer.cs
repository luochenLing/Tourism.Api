using AutoMapper;
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
            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.CustomerService.ToString());
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
        /// 根据ID删除用户信息
        /// </summary>
        /// <param name="Id">用户ID</param>
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
        /// 根据参数获取用户信息(可分页)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetCustomerInfoByQueryAsync(UserQuery query)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("GetCustomerInfoByQueryAsync's error：selQuery is not null");
                    throw new Exception("GetCustomerInfoByQueryAsync's error：selQuery is not null");
                }
                string sql = "SELECT * FROM `User` WHERE 1=1 ";

                if (query.CId != null)
                {
                    sql += " AND cId=@cId";
                }

                if (query.CIdentity != null)
                {
                    sql += " AND cIdentity=@CIdentity";
                }

                if (!string.IsNullOrWhiteSpace(query.CName))
                {
                    sql += " AND cName=@CName";
                }

                if (!string.IsNullOrWhiteSpace(query.CEmail))
                {
                    sql += " AND cEmail=@CEmail";
                }

                if (!string.IsNullOrWhiteSpace(query.CPhone))
                {
                    sql += " AND cPhone=@CPhone";
                }

                if (query.CIdentity != null)
                {
                    sql += " AND cIdNum=@CIdentity";
                }

                if (!string.IsNullOrWhiteSpace(query.CNickName))
                {
                    sql += " AND cNickName=@CNickName";
                }

                if (!string.IsNullOrWhiteSpace(query.CPasswd))
                {
                    sql += " AND cPasswd=@cPasswd";
                }
                if (!string.IsNullOrWhiteSpace(query.Sort))
                {
                    query.Sort = SqlHandler.ReplaceSQLChar(query.Sort);
                    sql += $" ORDER BY {query.Sort}";
                    if (!string.IsNullOrWhiteSpace(query.Order))
                    {
                        query.Order = SqlHandler.ReplaceSQLChar(query.Order);
                        sql += $" {query.Order}";
                    }
                }

                if (query.PageIndex != 0 && query.PageSize != 0)
                {
                    query.PageIndex = (query.PageIndex - 1) * query.PageSize;
                    sql += $" LIMIT {query.PageIndex},{query.PageSize}";
                }
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

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="query">查询条件，只包括账号密码即可</param>
        /// <returns></returns>
        public async Task<User> UserLogin(UserQuery query)
        {
            try
            {
                if (query == null)
                {
                    _log.Error("UserLogin's error：selQuery is not null");
                    throw new Exception("UserLogin's error：selQuery is not null");
                }
                string sql = "SELECT cId,cName,cSex,cAge,cPhone,cNickName,cPic FROM `User` WHERE 1=1 ";

                if (!string.IsNullOrWhiteSpace(query.CName))
                {
                    sql += " AND cName=@CName OR cEmail=@CName OR cPhone=@CName";
                }

                if (!string.IsNullOrWhiteSpace(query.CPasswd))
                {
                    sql += " AND cPasswd=@cPasswd";
                }

                var info = SetMapper(query);
                var res = await _mysqlRespository.QueryInfoAsync(sql, info);
                return res;
            }
            catch (Exception ex)
            {
                _log.Error("UserLogin method error:" + ex);
                throw;
            }
        }

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
