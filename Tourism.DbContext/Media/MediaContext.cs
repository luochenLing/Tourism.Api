using Microsoft.EntityFrameworkCore;
using Tourism.Model;

namespace Tourism.DBContext.Media
{
    public class MediaContext : MySqlContext
    {
        public MediaContext() : base()
        {
            ConStr = "MediaConnStr";
        }

        public DbSet<MediaInfo> MediaInfo { get; set; }

    }
}
