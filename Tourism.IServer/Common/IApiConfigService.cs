using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;

namespace Tourism.IServer
{
    public interface IApiConfigService
    {

        /// <summary>
        /// 获取ApiConfig列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ApiConfig>> GetApiConfigList();

        /// <summary>
        /// 获取ApiConfig信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<ApiConfig> GetApiConfigInfo(string name);

        /// <summary>
        /// 获取ApiConfig信息根据scope
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        Task<IEnumerable<ApiConfig>> GetApiConfigsByScope(string[] scope);
    }
}
