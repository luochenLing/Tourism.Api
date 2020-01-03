using System.Collections.Generic;
using Tourism.Model;
using Tourism.QueryModel;

namespace Tourism.IServer
{
    public interface IMediaInfoService
    {
        /// <summary>
        /// 获取媒体列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<MediaInfo> GetMediaInfoList(MediaQuery query);
    }
}
