using IdentityModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
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
    public class CustomerController : ControllerBase
    {
        private readonly ILog _log;
        private readonly ICustomerServer _customerServer;
        private ResultObject _result;
        private IConfiguration _configuration;
        private RedisHandler _redis;
        public CustomerController(ICustomerServer customerServer, IConfiguration configuration)
        {
            try
            {
                _log = LogManager.GetLogger(typeof(CustomerController));
                _customerServer = customerServer;
                _configuration = configuration;
                _redis = new RedisHandler();
                _result = new ResultObject();
            }
            catch (Exception ex)
            {
                _log.Error("CustomerController method error:" + ex);
            }
        }

        /// <summary>
        /// 注册用户信息
        /// </summary>
        /// <param name="info">注册用户信息</param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(User info)
        {
            try
            {
                if (info == null)
                {
                    _result.status = (int)HttpStatusCode.BadRequest;
                    _result.msg = "info is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.AddCustomerInfoAsync(info);
                _result.status = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("RegisterUser method error:" + ex);
                _result.status = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.status, _result);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="query">登录条件，多半为用户名、密码和手机号</param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserQuery query)
        {
            try
            {
                if (query == null)
                {
                    _result.status = (int)HttpStatusCode.BadRequest;
                    _result.msg = "query is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                if (string.IsNullOrWhiteSpace(query.CName))
                {
                    _result.status = (int)HttpStatusCode.BadRequest;
                    _result.msg = "CName is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                if (string.IsNullOrWhiteSpace(query.CPasswd))
                {
                    _result.status = (int)HttpStatusCode.BadRequest;
                    _result.msg = "CPasswd is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.UserLogin(new UserQuery()
                {
                    CName = query.CName,
                    CPasswd = query.CPasswd
                });

                if (res == null)
                {
                    _result.status = (int)HttpStatusCode.Forbidden;
                    _result.msg = "fail";
                    _result.resultData = res;
                    return NotFound(_result);
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("clientJwt")?.Value);
                var authTime = DateTime.UtcNow;
                var expiresAt = authTime.AddSeconds(Convert.ToDouble(_configuration.GetSection("loginTimeSpan")?.Value));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtClaimTypes.Audience,"api"),
                        new Claim(JwtClaimTypes.Id, res?.CId.ToString()),
                        new Claim(JwtClaimTypes.Name, res?.CName)
                    }),
                    Expires = expiresAt,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                //token
                HttpContext.Response.Cookies.Append(SystemCodeEnum.P00002.ToString(), tokenString, new CookieOptions
                {
                    HttpOnly = true
                });
                HttpContext.Response.Cookies.Append(SystemCodeEnum.uid.ToString(), res?.CId.Value.ToString("N"), new CookieOptions
                {
                    HttpOnly = false
                });
                //存入redis。操作需要登陆的界面时，需要用token+userid验证
                var ret = _redis.Set(res?.CId.Value.ToString("N"), tokenString, TimeSpan.FromSeconds(Convert.ToDouble(_configuration.GetSection("loginTimeSpan")?.Value)));
                _result.status = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("UserLogin method error:" + ex);
                _result.status = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.status, _result);
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
            try
            {
                if (query == null)
                {
                    _result.status = (int)HttpStatusCode.BadRequest;
                    _result.msg = "query is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.GetCustomerInfoByQueryAsync(query);
                _result.status = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res.ToList();
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("GetUserListByQuery method error:" + ex);
                _result.status = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.status, _result);
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
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    _result.status = (int)HttpStatusCode.BadRequest;
                    _result.msg = "id is not null";
                    _result.resultData = null;
                    return BadRequest(_result);
                }
                var res = await _customerServer.DelCustomerInfoByIdAsync(id);
                _result.status = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = res;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("DelUserListByQuery method error:" + ex);
                _result.status = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.status, _result);
            }
        }

        /// <summary>
        /// 注销，删除cookie
        /// </summary>
        /// <returns></returns>
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                HttpContext.Response.Cookies.Delete(SystemCodeEnum.P00002.ToString());
                var userId = HttpContext.Request.Cookies.Where(t => t.Key == SystemCodeEnum.uid.ToString()).FirstOrDefault().Value;
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    await _redis.DelAsync(userId);
                }
                HttpContext.Response.Cookies.Delete(SystemCodeEnum.uid.ToString());
                _result.status = (int)HttpStatusCode.OK;
                _result.msg = "success";
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("LogOut method error:" + ex);
                _result.status = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.status, _result);
            }
        }

        /// <summary>
        /// 从redis缓存检索信息是否正确
        /// </summary>
        /// <returns></returns>
        [HttpPost("CheckUserIdentity")]
        public IActionResult CheckUserIdentity()
        {
            try
            {
                _log.Info("CheckUserIdentity is running");
                var ret = UserManager.CheckUserIdentity(HttpContext);
                _result.status = (int)HttpStatusCode.OK;
                _result.msg = "success";
                _result.resultData = ret;
                return Ok(_result);
            }
            catch (Exception ex)
            {
                _log.Error("CheckUserIdentity method error:" + ex);
                _result.status = (int)HttpStatusCode.InternalServerError;
                _result.msg = "fail";
                _result.resultData = null;
                return StatusCode(_result.status, _result);
            }
        }

    }
}