using IdentityServer4.Models;
using IdentityServer4.Stores;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.IServer;

namespace Tourism.Idp.ConfigurationStore
{
    public class ClientStore : IClientStore
    {
        private readonly ILog _log;
        private readonly IClientConfigService _clientConfigService;

        public ClientStore(IClientConfigService clientConfigService)
        {
            _log = LogManager.GetLogger(typeof(ClientStore));
            _clientConfigService = clientConfigService;
        }

        /// <summary>
        /// 根据ID找客户端信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = new Client();
            try
            {
                var res = await _clientConfigService.GetClientConfigInfo(clientId);
                if (res != null)
                {
                    client.ClientId = res.ClientId;
                    client.ClientSecrets = new List<Secret>() { new Secret(res.ClientSecret.Sha256()) };

                    client.AllowedScopes = res.AllowedScope?.Split('|');
                    client.AllowedGrantTypes = GetGrantType(res.GrantType);
                }
                return await Task.FromResult(client);
            }
            catch (Exception ex)
            {
                _log.Error("FindClientByIdAsync method error:" + ex);
                return await Task.FromResult(client);
            }
        }


        public ICollection<string> GetGrantType(string grantType)
        {
            var res = typeof(GrantTypes).GetProperty(grantType)?.GetValue(new GrantTypes());
            return (ICollection<string>)res;
        }
    }
}
