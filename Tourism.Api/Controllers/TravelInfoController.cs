using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tourism.Api.Model;
using Tourism.Eums;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Util;

namespace Tourism.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TravelInfoController : ControllerBase
    {
        private readonly ITravelInfoService _travelInfoService;
        private readonly ISettingService _settingService;
        private readonly ICouponService _couponService;
        private readonly IAdministrativeAreaService _administrativeAreaService;
        private readonly IMediaInfoService _mediaInfoService;

        private readonly ILog _log;
        private readonly ConfigurationManager _configurationManager;
        private dynamic _result;
        public TravelInfoController(ITravelInfoService travelInfoService, ISettingService settingService, ICouponService couponService, IAdministrativeAreaService administrativeAreaService, IMediaInfoService mediaInfoService)
        {
            _log = LogManager.GetLogger(typeof(TravelInfoController));
            _configurationManager = new ConfigurationManager();
            _travelInfoService = travelInfoService;
            _settingService = settingService;
            _couponService = couponService;
            _administrativeAreaService = administrativeAreaService;
            _mediaInfoService = mediaInfoService;
        }

        [HttpGet]
        public string Index()
        {
            return "TravelInfo Page";
        }

        /// <summary>
        /// 获取首页轮播图
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSwpierList")]
        public async Task<IActionResult> GetSwpierList()
        {
            _result = new ResultObject<Setting>();
            try
            {
                var url = _configurationManager.GetSection(ConfigEnum.MediaUrl.ToString());
                var query = new SettingQuery
                {
                    SystemCode = SystemCodeEnum.Travel.ToString().ToLower(),
                    TypeCode = (int)TypeCodeEnum.IndexSwipper
                };
                var res = await _settingService.GetSettingsAsync(query);
                res.ToList().ForEach(x => x.Content = url + "\\" + (x.Content));

                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res.ToList();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetSwpierList method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据地名查找商品列表
        /// </summary>
        /// <param name="areaName"></param>
        [HttpPost("GetTravelListByArea")]
        public async Task<IActionResult> GetTravelListByArea(string areaName)
        {
            _result = new ResultObject<TravelInfo>();
            try
            {
                if (string.IsNullOrWhiteSpace(areaName))
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "areaName is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var url = _configurationManager.GetSection(ConfigEnum.MediaUrl.ToString());
                var res = await _travelInfoService.GetTravelListByAreaAsync(areaName);
                res.ToList().ForEach(x => x.Cover = url + "\\" + (x.Cover));
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res.ToList();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetTravelListByArea method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据产品类型获取列表
        /// </summary>
        /// <param name="proType"></param>
        /// <returns></returns>
        [HttpPost("GetTravelInfoListByProType")]
        public async Task<IActionResult> GetTravelInfoListByProType(string proType)
        {
            _result = new ResultObject<TravelInfo>();

            try
            {
                if (string.IsNullOrWhiteSpace(proType))
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "proType is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var url = _configurationManager.GetSection(ConfigEnum.MediaUrl.ToString());
                var query = new TravelInfoQuery
                {
                    ProType = proType
                };
                var res = await _travelInfoService.GetTravelInfoListAsync(query);
                res.ToList().ForEach(x => x.Cover = url + "\\" + (x.Cover));
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                if (proType == "4")
                {
                    _result.resultData = res.Take(2);
                }
                else
                {
                    _result.resultData = res;
                }

                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetTravelInfoListByProType method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据国境标识获取产品列表
        /// </summary>
        /// <param name="frontier">0 境内 1 境外</param>
        /// <returns></returns>
        [HttpPost("GetTravelInfoListByFrontier")]
        public async Task<IActionResult> GetTravelInfoListByFrontier(string frontier)
        {
            _result = new ResultObject<TravelInfo>();
            try
            {
                if (string.IsNullOrWhiteSpace(frontier))
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "frontier is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var url = _configurationManager.GetSection(ConfigEnum.MediaUrl.ToString());
                var query = new TravelInfoQuery();
                if (frontier == "0")
                {
                    query.Country = "中国";
                }
                else if (frontier == "1")
                {
                    query.Country = "!中国";
                }
                else
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "faild";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _travelInfoService.GetTravelInfoListAsync(query);
                res.ToList().ForEach(x => x.Cover = url + "\\" + (x.Cover));
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetTravelInfoListByFrontier method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据筛选条件获取列表
        /// </summary>
        /// <param name="conditionQuery"></param>
        /// <returns></returns>
        [HttpPost("GetTravelInfoListByFilter")]
        public async Task<IActionResult> GetTravelInfoListByFilter(TravelConditionQuery conditionQuery)
        {
            _result = new ResultObject<TravelInfo>();

            var travelQuery = new TravelInfoQuery();
            try
            {
                if (conditionQuery == null)
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "conditionQuery is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var url = _configurationManager.GetSection(ConfigEnum.MediaUrl.ToString());
                if (conditionQuery.dateList?.Length > 0)
                {
                    travelQuery.ProDays = conditionQuery.dateList;
                }

                if (conditionQuery.travelTimeList?.Length > 0)
                {
                    travelQuery.Month = conditionQuery.travelTimeList;
                }

                if (conditionQuery.specialList?.Length > 0)
                {
                    travelQuery.Them = conditionQuery.specialList;
                }

                if (!string.IsNullOrWhiteSpace(conditionQuery.orderFiled))
                {
                    //除了价格从低到高是正序，其他条件都是倒序
                    if (conditionQuery.orderFiled == ConditionEnum.priceDown.ToString())
                    {
                        travelQuery.Order = "asc";
                        travelQuery.Sort = "proPrice";
                    }
                    else
                    {
                        travelQuery.Order = "desc";
                        travelQuery.Sort = conditionQuery.orderFiled;
                    }

                }

                var res = await _travelInfoService.GetTravelInfoListAsync(travelQuery);
                res.ToList().ForEach(x => x.Cover = url + "\\" + (x.Cover));
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res.ToList();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetTravelInfoListByFilter method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 获取相关产品列表
        /// </summary>
        /// <param name="areaName"></param>
        /// <returns></returns>
        [HttpPost("RelatedProducts")]
        public async Task<IActionResult> RelatedProducts(string areaName)
        {
            try
            {
                return await GetTravelListByArea(areaName);
            }
            catch (Exception ex)
            {
                _log.Error("RelatedProducts method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据用户ID获取优惠券列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("GetCouponListByUserId")]
        public async Task<IActionResult> GetCouponListByUserId(string userId)
        {
            _result = new ResultObject<Coupon>();
            if (string.IsNullOrWhiteSpace(userId))
            {
                _result.code = (int)HttpStatusCode.BadRequest;
                _result.msg = "userId is not null";
                _result.resultData = null;
                return BadRequest(_result);
            }

            try
            {
                var query = new CouponQuery
                {
                    UserId = new Guid(userId)
                };
                var res = await _couponService.GetCouponsAsync(query);
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res.ToList();
                return Ok(_result);

            }
            catch (Exception ex)
            {
                _log.Error("GetCouponListByUserId method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据ID获取产品信息
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        [HttpPost("GetTravelInfoById")]
        public async Task<IActionResult> GetTravelInfoById(string proId)
        {
            _result = new ResultObject<TravelInfo>();

            if (string.IsNullOrWhiteSpace(proId))
            {
                _result.code = (int)HttpStatusCode.BadRequest;
                _result.msg = "proId is not null";
                _result.resultData = null;
                return BadRequest(_result);
            }
            var url = _configurationManager.GetSection(ConfigEnum.MediaUrl.ToString());
            try
            {
                var res = await _travelInfoService.GetTravelInfoAsync(new TravelInfoQuery
                {
                    ProId = new Guid(proId)
                });
                if (!string.IsNullOrWhiteSpace(res.Cover))
                {
                    res.Cover = url + res.Cover;
                }
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);

            }
            catch (Exception ex)
            {
                _log.Error("GetTravelInfoById method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }


        /// <summary>
        /// 根据产品ID获取产品活动列表
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        [HttpPost("GetTravelActivityListByProId")]
        public async Task<IActionResult> GetTravelActivityListByProId(string proId)
        {
            _result = new ResultObject<TravelInfo>();
            if (string.IsNullOrWhiteSpace(proId))
            {
                _result.code = (int)HttpStatusCode.BadRequest;
                _result.msg = "proId is not null";
                _result.resultData = null;
                return BadRequest(_result);
            }
            try
            {
                var res = await _travelInfoService.GetTravelActivityListByQuery(new TravelActivityQuery
                {
                    PId = new Guid(proId)
                });
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);

            }
            catch (Exception ex)
            {
                _log.Error("GetTravelActivityListByProId method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 获取热门城市列表
        /// </summary>
        /// <param name="proid"></param>
        /// <returns></returns>
        [HttpGet("GetHotCityList")]
        public async Task<IActionResult> GetHotCityList()
        {
            _result = new ResultObject<HotCity>();
            try
            {
                var res = await _administrativeAreaService.GetHotCityListAsync();
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);

            }
            catch (Exception ex)
            {
                _log.Error("GetHotCityList method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据产品ID查找媒体信息列表
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        [HttpPost("GetMediaInfoListById")]
        public async Task<IActionResult> GetMediaInfoListById(string proId)
        {
            _result = new ResultObject<MediaInfo>();
            try
            {
                var url = _configurationManager.GetSection(ConfigEnum.MediaUrl.ToString());
                var res = await _mediaInfoService.GetMediaInfoListById(proId);
                res.ToList().ForEach(x => x.MUrl = url + "\\" + (x.MUrl));
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetMediaInfoListById method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }
    }
}