using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tourism.Api.Model;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILog _log;
        private readonly ICustomerServer _customerServer;
        private dynamic _result;
        public CustomerController(ICustomerServer customerServer)
        {
            _log = LogManager.GetLogger(typeof(CustomerController));
            _customerServer = customerServer;
        }

        /// <summary>
        /// 注册用户信息
        /// </summary>
        /// <param name="info">注册用户信息</param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(User info)
        {
            _result = new ResultObject<User>();
            try
            {
                if (info==null)
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "info is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.AddCustomerInfoAsync(info);
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("RegisterUser method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="query">登录条件，多半为用户名、密码和手机号</param>
        /// <returns></returns>
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(UserQuery query)
        {
            _result = new ResultObject<User>();
            try
            {
                if (query == null)
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "query is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.GetCustomerInfoByQueryAsync(query);
                var resultData = res.FirstOrDefault();
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = resultData;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("UserLogin method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据用户条件查询用户信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        [HttpPost("GetUserListByQuery")]
        public async Task<IActionResult> GetUserListByQuery(UserQuery query)
        {
            _result = new ResultObject<User>();
            try
            {
                if (query == null)
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "query is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.GetCustomerInfoByQueryAsync(query);
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res.ToList();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetUserListByQuery method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

        /// <summary>
        /// 根据用户ID删除用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost("DelUserListByQuery")]
        public async Task<IActionResult> DelUserListByQuery(string id)
        {
            _result = new ResultObject<User>();
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _result.code = (int)HttpStatusCode.BadRequest;
                    _result.msg = "id is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.DelCustomerInfoByIdAsync(id);
                _result.code = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("DelUserListByQuery method error:" + ex);
                _result.code = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.code, _result);
            }
        }

    }
}