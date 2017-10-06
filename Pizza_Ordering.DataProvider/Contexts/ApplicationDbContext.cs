using Pizza_ordering.Domain;
using Pizza_Ordering.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_ordering.Domain.Entities;

namespace Pizza_Ordering.DataProvider
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            :base("DefaultConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<CapacityPlan> CapacityPlans { get; set; }
        public virtual DbSet<FixPizza> FixPizzas { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<IngredientItem> IngredientItems { get; set; }
        public virtual DbSet<ModifiedPizza> ModifiedPizzas { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<PizzaHouse> PizzaHouses { get; set; }
        public virtual DbSet<SavedPizza> SavedPizzas { get; set; }
        public virtual DbSet<UserBonus> UserBonuses { get; set; }
    }
}
