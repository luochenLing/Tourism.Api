using System.Threading.Tasks;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.IServer.Customer
{
    public interface IDeveloperServer
    {
        /// <summary>
        /// 根据账号查找开发者信息
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        Task<Developer> GetDeveloperByAccount(DeveloperQuery query);
    }
}
