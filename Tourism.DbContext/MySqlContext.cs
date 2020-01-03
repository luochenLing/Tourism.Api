using Microsoft.EntityFrameworkCore;
using Tourism.Util;

namespace Tourism.DBContext
{
    public class MySqlContext : DbContext
    {
        protected string ConStr { get; set; }

        private ConfigurationManager _config;

        public MySqlContext()
        {
            _config = new ConfigurationManager();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_config.GetConnectionString(ConStr), options => options.EnableRetryOnFailure());
        }
    }
}
