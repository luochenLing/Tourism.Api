using Microsoft.EntityFrameworkCore;
using Tourism.Model;


namespace Tourism.DBContext.Travel
{
    public class TravelContext : MySqlContext
    {

        public TravelContext() : base()
        {
            ConStr = "TravelConnStr";
        }
        public DbSet<TravelInfo> TravelInfo { get; set; }
        public DbSet<TravelActivity> TravelActivity { get; set; }
    }
}
