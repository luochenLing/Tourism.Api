using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.IServer
{
    public interface ICustomerServer
    {
        /// <summary>
        /// 添加一条用户信息
        /// </summary>
        /// <param name="info">要添加的用户数据</param>
        /// <returns></returns>
        Task<int> AddCustomerInfoAsync(User info);

        /// <summary>
        /// 批量添加用户信息
        /// </summary>
        /// <param name="customers">要添加的用户列表</param>
        /// <returns></returns>
        Task<int> BatchAddCustomerInfoAsync(List<User> customers);

        /// <summary>
        /// 根据ID删除用户信息
        /// </summary>
        /// <param name="Id">用户ID</param>
        /// <returns></returns>
        Task<int> DelCustomerInfoByIdAsync(string Id);
   
        /// <summary>
        /// 根据参数获取用户信息(可分页)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<User>> GetCustomerInfoByQueryAsync(UserQuery query);
        
    }
}
