using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Tourism.Eums;

namespace Tourism.Util
{
    public class UserManager
    {
        private static ILog _log;

        public static bool CheckUserIdentity(HttpContext context)
        {
            if (_log == null)
            {
                _log = LogManager.GetLogger(typeof(UserManager));
            }

            RedisHandler redis = new RedisHandler();
            try
            {

                var tokenName = context.Request.Cookies.Where(t => t.Key == SystemCodeEnum.P00002.ToString()).FirstOrDefault().Value;
                var userId = context.Request.Cookies.Where(t => t.Key == SystemCodeEnum.uid.ToString()).FirstOrDefault().Value;
                string val = string.Empty;
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    val = redis.GetAsync(userId).GetAwaiter().GetResult();
                }
                else
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(tokenName))
                {
                    return false;
                }
                //只有userid和token都一致的情况下，才判断为本人，否则不通过验证
                if (!string.IsNullOrWhiteSpace(val) && val == tokenName)
                {
                    return true;
                }
                else
                {
                    _log.Warn($"CheckUserIdentity method warn,tokenName:{tokenName},userId:{userId}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _log.Error("CheckUserIdentity method error:" + ex);
                return false;
            }
        }
    }
}
