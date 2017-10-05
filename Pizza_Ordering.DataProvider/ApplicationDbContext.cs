using Pizza_Ordering.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            :base("DefaultConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
