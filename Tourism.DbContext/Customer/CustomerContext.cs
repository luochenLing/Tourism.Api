using Microsoft.EntityFrameworkCore;
using Tourism.Model;

namespace Tourism.DBContext.Customer
{
    public class CustomerContext : MySqlContext
    {
        public CustomerContext() : base()
        {
            ConStr = "CustomerConnStr";
        }

        public DbSet<User> User { get; set; }

        public DbSet<Developer> Developer { get; set; }
    }
}
