using Microsoft.Owin.BuilderProperties;
using Pizza_Ordering.DataProvider.Repositories;
using Pizza_Ordering.Domain;
using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Pizza_Ordering.Domain.Entities.Address> Addresses { get; }

        IRepository<CapacityPlan> CapacityPlanes { get; }

        IRepository<FixPizza> FixPizzas { get; }

        IRepository<Ingredient> Ingredients { get; }

        IRepository<IngredientItem> IngredientItems { get; }

        IRepository<ModifiedPizza> ModifiedPizzas { get; }

        UserRepository Users { get; }

        IRepository<Order> Orders { get; }

        IRepository<OrderItem> OrderItems { get; }

        IRepository<PizzaHouse> PizzaHouses { get; }

        IRepository<SavedPizza> SavedPizzas { get; }

        IRepository<UserBonus> UserBonuses { get; }

        IRepository<IngredientAmount> IngredientAmounts { get; }

        RoleRepository Roles { get; }

        void Save();

        void Dispose(bool disposing);
    }
}
