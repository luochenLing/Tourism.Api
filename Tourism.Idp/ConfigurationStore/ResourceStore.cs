using IdentityServer4.Models;
using IdentityServer4.Stores;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tourism.IServer;

namespace Tourism.Idp.ConfigurationStore
{
    public class ResourceStore : IResourceStore
    {
        private readonly ILog _log;
        private readonly IApiConfigService _apiConfigService;

        public ResourceStore(IApiConfigService apiConfigService)
        {
            _log = LogManager.GetLogger(typeof(ClientStore));
            _apiConfigService = apiConfigService;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResource = new ApiResource();
            try
            {
                var res = await _apiConfigService.GetApiConfigInfo(name);

                if (res != null)
                {
                    apiResource.Name = res.Key;
                    apiResource.DisplayName = res.Value;
                }
                return Task.FromResult(apiResource).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                _log.Error("FindApiResourceAsync method error:" + ex);
                return Task.FromResult(apiResource).GetAwaiter().GetResult();
            }
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var apiResources = new List<ApiResource>();
            try
            {
                var res = await _apiConfigService.GetApiConfigsByScope(scopeNames.ToArray());
                
                if (res != null)
                {
                    foreach (var item in res)
                    {
                        apiResources.Add(new ApiResource(item.Key, item.Value));
                    }
                }
                return apiResources;
            }
            catch (Exception ex)
            {
                _log.Error("FindApiResourcesByScopeAsync method error:" + ex);
                return Task.FromResult(apiResources).GetAwaiter().GetResult();
            }
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {

            var identityResourceList = new List<IdentityResource>
                    {
                            new IdentityResources.OpenId()
                    };
            return Task.FromResult(identityResourceList.AsEnumerable());
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var apiResources = new List<ApiResource>();

            try
            {
                var res = await _apiConfigService.GetApiConfigList();
                if (res != null)
                {
                    foreach (var item in res)
                    {
                        apiResources.Add(new ApiResource(item.Key, item.Value));
                    }
                }
                var identityResourceList = new List<IdentityResource>
                    {
                            new IdentityResources.OpenId()
                    };
                var result = new Resources(identityResourceList, apiResources);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _log.Error("GetAllResourcesAsync method error:" + ex);
                return await Task.FromResult(new Resources());
            }
        }
    }
}
