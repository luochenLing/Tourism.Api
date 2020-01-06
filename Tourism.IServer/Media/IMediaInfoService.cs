using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Model;

namespace Tourism.IServer
{
    public interface IMediaInfoService
    {
        /// <summary>
        /// 根据产品ID查找媒体信息列表
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        Task<IEnumerable<MediaInfo>> GetMediaInfoListById(string proId);
    }
}
