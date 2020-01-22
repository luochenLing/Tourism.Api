using Dapper;
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
    public class TravelInfoService : ITravelInfoService
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;
        public TravelInfoService()
        {
            _log = LogManager.GetLogger(typeof(TravelInfoService));

            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.TravelService.ToString());
        }

        /// <summary>
        /// 根据条件查询产品列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TravelInfo>> GetTravelInfoListAsync(TravelInfoQuery query)
        {
            try
            {
                string sql = "SELECT * FROM TravelInfo WHERE 1=1";

                #region 条件
                if (query.ProId != null)
                {
                    sql += " AND proId = @ProId";
                }

                if (!string.IsNullOrWhiteSpace(query.ProType))
                {
                    query.ProType = SqlHandler.ReplaceSQLChar(query.ProType);
                    query.ProType = $"%{query.ProType}%";
                    sql += " AND proType LIKE @ProType";
                }

                if (!string.IsNullOrWhiteSpace(query.Country
                    ))
                {
                    if (query.Country.StartsWith("!"))
                    {
                        query.Country = query.Country.TrimStart('!');
                        sql += " AND country <> @Country";
                    }
                    else
                    {
                        sql += " AND country = @Country";
                    }

                }

                if (!string.IsNullOrWhiteSpace(query.City
                    ))
                {
                    sql += " AND city = @City";
                }

                if (!string.IsNullOrWhiteSpace(query.Area
                    ))
                {
                    sql += " AND area = @Area";
                }

                if (query.StartTime != null)
                {
                    sql += " AND startTime >= @StartTime";
                }

                if (query.Month?.Length > 0)
                {
                    sql += " AND month IN @Month";
                }

                if (query.EndTime != null)
                {
                    sql += " AND endTime <= @EndTime";
                }


                if (query.ProDays?.Length > 0)
                {
                    if (query.ProDays.Length > 1)
                    {
                        sql += " AND proDays IN @ProDays";
                    }
                    else
                    {
                        sql += " AND proDays = @ProDays";
                    }
                }

                if (query.Them?.Length > 0)
                {
                    for (var i = 0; i < query.Them.Length; i++)
                    {
                        if (i == 0)
                        {
                            sql += " AND them LIKE '%" + query.Them[i] + "%'";
                        }
                        else
                        {
                            sql += " OR them LIKE '%" + query.Them[i] + "%'";
                        }
                    }

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
                #endregion

                if (query.PageIndex != 0 && query.PageSize != 0)
                {
                    query.PageIndex = (query.PageIndex - 1) * query.PageSize;
                    sql += $" LIMIT {query.PageIndex},{query.PageSize}";
                }
                return await _mysqlRespository.QueryListAsync<TravelInfo, TravelInfoQuery>(sql, query);

            }
            catch (Exception ex)
            {
                _log.Error("GetTravelInfoListAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 根据ID查找产品详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TravelInfo> GetTravelInfoAsync(TravelInfoQuery query)
        {
            try
            {
                string sql = "SELECT * FROM TravelInfo WHERE 1=1";

                if (query.ProId != null)
                {
                    sql += " AND proId = @ProId";
                }

                var info = MapperManager.SetMapper<TravelInfo, TravelInfoQuery>(query);
                return await _mysqlRespository.QueryInfoAsync(sql, info);

            }
            catch (Exception ex)
            {
                _log.Error("GetTravelInfoAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 根据条件搜索活动列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TravelActivity>> GetTravelActivityListByQuery(TravelActivityQuery query)
        {
            try
            {
                string sql = "SELECT * FROM TravelActivity WHERE 1=1";
                if (query.Id != null)
                {
                    sql += " AND id = @Id";
                }

                if (query.PId != null)
                {
                    sql += " AND pId = @PId";
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
                var info = MapperManager.SetMapper<TravelActivity, TravelActivityQuery>(query);
                return await _mysqlRespository.QueryListAsync(sql, info);
            }
            catch (Exception ex)
            {
                _log.Error("GetTravelActivityListByQuery method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 根据地名查找产品详细信息
        /// </summary>
        /// <param name="areaCondition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TravelInfo>> GetTravelListByAreaAsync(string areaCondition,int pageIndex,int pageSize)
        {
            try
            {
                areaCondition = SqlHandler.ReplaceSQLChar(areaCondition);
                areaCondition = $"%{areaCondition}%";
                string sql = "SELECT * FROM TravelInfo WHERE country LIKE @areaCondition OR city LIKE @areaCondition OR area LIKE @areaCondition";
                if (pageIndex != 0 && pageSize != 0)
                {
                    pageIndex = (pageIndex - 1) * pageSize;
                    sql += $" LIMIT {pageIndex},{pageSize}";
                }
                var parameters = new DynamicParameters();

                parameters.Add("@areaCondition", areaCondition);
                return await _mysqlRespository.QueryListAsync<TravelInfo, DynamicParameters>(sql, parameters);
            }
            catch (Exception ex)
            {
                _log.Error("GetTravelListByAreaAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 获取筛选条件
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TravelCondition>> GetTravelConditionListAsync()
        {
            try
            {
                string sql = "SELECT * FROM TravelCondition";

                return await _mysqlRespository.QueryListAsync<TravelCondition>(sql, null);
            }
            catch (Exception ex)
            {
                _log.Error("GetTravelConditionListAsync method error:" + ex);
                throw;
            }
        }

        /// <summary>
        /// 构建ef查询条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private Expression<Func<TravelInfo, bool>> BuildQuery(TravelInfoQuery query)
        {
            var oLamadaExtention = new LamadaExtention<TravelInfo>();

            if (query.ProId != null)
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.ProId), query.ProId, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.PlaceOfDeparture))
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.PlaceOfDeparture), query.PlaceOfDeparture, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.City))
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.City), query.City, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.Country))
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.Country), query.Country, ExpressionTypeEnum.Equal);
            }

            if (query.EndTime != null)
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.EndTime), query.EndTime, ExpressionTypeEnum.LessThanOrEqual);
            }

            if (query.StartTime != null)
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.StartTime), query.StartTime, ExpressionTypeEnum.GreaterThanOrEqual);
            }

            if (!string.IsNullOrWhiteSpace(query.ProDestination))
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.ProDestination), query.ProDestination, ExpressionTypeEnum.Equal);
            }

            if (!string.IsNullOrWhiteSpace(query.ProTitle))
            {
                oLamadaExtention.GetExpression(nameof(TravelInfo.ProTitle), query.ProTitle, ExpressionTypeEnum.Equal);
            }

            return oLamadaExtention.GetLambda();
        }

    }

}
