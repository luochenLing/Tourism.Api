using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.IServer
{
    public interface ISettingService
    {
        Task<IEnumerable<Setting>> GetSettingsAsync(SettingQuery query);
    }
}
