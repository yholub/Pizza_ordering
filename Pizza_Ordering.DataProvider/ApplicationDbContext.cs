using Pizza_ordering.Domain;
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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<PizzaRecipe> PizzaRecipes { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<PizzaHouse> Companies { get; set; }
        public virtual DbSet<Place> Places { get; set; }
    }
}
