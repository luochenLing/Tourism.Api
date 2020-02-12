using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Tourism.Util;

namespace Tourism.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class UserCheckAttribute : Attribute, IAuthorizationFilter
    {
        private readonly ILog _log;
        public UserCheckAttribute()
        {
            _log = LogManager.GetLogger(typeof(UserCheckAttribute));
        }

        private RedisHandler redis = new RedisHandler();
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var ret = UserManager.CheckUserIdentity(context.HttpContext);
                if (!ret)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            catch (Exception ex)
            {
                _log.Error("OnAuthorization method error:" + ex);
                context.Result = new UnauthorizedResult();
            }
        }

    }
}
