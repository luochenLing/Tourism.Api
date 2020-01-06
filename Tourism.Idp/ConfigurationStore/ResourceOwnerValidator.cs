using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using log4net;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Util;

namespace Tourism.Idp.ConfigurationStore
{
    public class ResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        private readonly IDeveloperServer _developerServer;
        private readonly ILog _log;
        public ResourceOwnerValidator(IDeveloperServer developerServer)
        {
            _log = LogManager.GetLogger(typeof(ResourceOwnerValidator));
            _developerServer = developerServer;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(context.UserName) || string.IsNullOrWhiteSpace(context.Password))
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "user name and password must fill");
                }
                var password = PwdHandler.MD5EncryptTo32(context.Password);
                var res = _developerServer.GetDeveloperByAccount(new DeveloperQuery { Name = context.UserName, Secret = password }).GetAwaiter().GetResult();
                if (res.Name == context.UserName && res.Secret == password)
                {
                    context.Result = new GrantValidationResult(
                        subject: context.UserName,
                        authenticationMethod: OidcConstants.AuthenticationMethods.Password,
                        claims: GetClaims(res));
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Wrong user name or password");
                }
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                _log.Error("ValidateAsync method error:" + ex);
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, ex.Message);
                return Task.FromResult(0);
            }
        }

        public IEnumerable<Claim> GetClaims(Developer info)
        {
            return new List<Claim>()
            {
                new Claim(nameof(info.Name),info.Name),
                new Claim(nameof(info.Organ),info.Organ),
                new Claim(nameof(info.Email),info.Email),
                new Claim(nameof(info.Phone),info.Phone),
            };
        }
    }
}
