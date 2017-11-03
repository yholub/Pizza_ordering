using Pizza_Ordering.DataProvider.Contexts;
using Pizza_Ordering.DataProvider.Repositories;
using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private AuthorizationContext authorizationDb = new AuthorizationContext();
        private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository<Address> addressesRepository;

        private IRepository<CapacityPlan> capacityPlansRepository;

        private IRepository<FixPizza> fixPizzasRepository;

        private IRepository<Ingredient> ingredientsRepository;

        private IRepository<IngredientItem> ingredientItemRepository;

        private IRepository<ModifiedPizza> modifiedPizzasRepository;

        private UserRepository usersRepository;

        private IRepository<Order> ordersRepository;

        private IRepository<OrderItem> orderItemsRepository;

        private IRepository<PizzaHouse> pizzaHousesRepository;

        private IRepository<SavedPizza> savedPizzasRepository;

        private IRepository<UserBonus> userBonusRepository;

        private IRepository<IngredientAmount> ingregientAmountsRepository;

        private RoleRepository rolesRepository;

        public UserRepository Users
        {
            get
            {
                if (usersRepository == null)
                {
                    usersRepository = new UserRepository(authorizationDb);
                }

                return usersRepository;
            }
        }

        public RoleRepository Roles
        {
            get
            {
                if (rolesRepository == null)
                {
                    rolesRepository = new RoleRepository(authorizationDb);
                }

                return rolesRepository;
            }
        }

        public IRepository<SavedPizza> SavedPizzas
        {
            get
            {
                if (savedPizzasRepository == null)
                {
                    savedPizzasRepository = new Repository<SavedPizza>(db);
                }

                return savedPizzasRepository;
            }
        }

        public IRepository<CapacityPlan> CapacityPlanes
        {
            get
            {
                if (capacityPlansRepository == null)
                {
                    capacityPlansRepository = new Repository<CapacityPlan>(db);
                }

                return capacityPlansRepository;
            }
        }

        public IRepository<FixPizza> FixPizzas
        {
            get
            {
                if (fixPizzasRepository == null)
                {
                    fixPizzasRepository = new Repository<FixPizza>(db);
                }

                return fixPizzasRepository;
            }
        }

        public IRepository<Ingredient> Ingredients
        {
            get
            {
                if (ingredientsRepository == null)
                {
                    ingredientsRepository = new Repository<Ingredient>(db);
                }

                return ingredientsRepository;
            }
        }

        public IRepository<IngredientItem> IngredientItems
        {
            get
            {
                if (ingredientItemRepository == null)
                {
                    ingredientItemRepository = new Repository<IngredientItem>(db);
                }

                return ingredientItemRepository;
            }
        }

        public IRepository<ModifiedPizza> ModifiedPizzas
        {
            get
            {
                if (modifiedPizzasRepository == null)
                {
                    modifiedPizzasRepository = new Repository<ModifiedPizza>(db);
                }

                return modifiedPizzasRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (ordersRepository == null)
                {
                    ordersRepository = new Repository<Order>(db);
                }

                return ordersRepository;
            }
        }

        public IRepository<OrderItem> OrderItems
        {
            get
            {
                if (orderItemsRepository == null)
                {
                    orderItemsRepository = new Repository<OrderItem>(db);
                }

                return orderItemsRepository;
            }
        }

        public IRepository<PizzaHouse> PizzaHouses
        {
            get
            {
                if (pizzaHousesRepository == null)
                {
                    pizzaHousesRepository = new Repository<PizzaHouse>(db);
                }

                return pizzaHousesRepository;
            }
        }

        public IRepository<UserBonus> UserBonuses
        {
            get
            {
                if (userBonusRepository == null)
                {
                    userBonusRepository = new Repository<UserBonus>(db);
                }

                return userBonusRepository;
            }
        }

        public IRepository<Address> Addresses
        {
            get
            {
                if (addressesRepository == null)
                {
                    addressesRepository = new Repository<Address>(db);
                }

                return addressesRepository;
            }
        }

        public IRepository<IngredientAmount> IngredientAmounts
        {
            get
            {
                if (ingregientAmountsRepository == null)
                {
                    ingregientAmountsRepository = new Repository<IngredientAmount>(db);
                }

                return ingregientAmountsRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
