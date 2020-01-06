using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;

namespace Tourism.IServer
{
    public interface IClientConfigService
    {
        /// <summary>
        /// 获取ClientConfig列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ClientConfig>> GetClientConfigList();

        /// <summary>
        /// 获取ClientConfig一条信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        Task<ClientConfig> GetClientConfigInfo(string clientId);
    }
}
