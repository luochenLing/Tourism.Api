using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tourism.Eums;
using Tourism.IServer;
using Tourism.Model;
using Tourism.QueryModel;
using Tourism.Repository;

namespace Tourism.Server
{
    public class MediaInfoService : IMediaInfoService
    {
        private readonly ILog _log;
        private readonly IMySqlRespostitoryByDapper _mysqlRespository;

        public MediaInfoService()
        {
            _log = LogManager.GetLogger(typeof(MediaInfoService));
            _mysqlRespository = new MySqlRespostitoryByDapper(DbNameEnum.MediaServer.ToString());
        }

        /// <summary>
        /// 根据产品ID查找媒体信息列表
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MediaInfo>> GetMediaInfoListById(string proId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(proId))
                {
                    throw new ArgumentNullException("proId is not null");
                }

                var query = new MediaQuery
                {
                    MPid = new Guid(proId)
                };

                string sql = "SELECT * FROM MediaInfo WHERE mPId=@MPid";
                return await _mysqlRespository.QueryListAsync<MediaInfo, MediaQuery>(sql, query);
            }
            catch (Exception ex)
            {
                _log.Error("GetMediaInfoListById method error:" + ex);
                throw;
            }
        }
    }
}
