using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;

namespace Tourism.IServer
{
    public interface IAdministrativeAreaService
    {
        /// <summary>
        /// 获取热门城市
        /// </summary>
        /// <returns></returns>
        Task<List<HotCity>> GetHotCityListAsync();
    }
}
