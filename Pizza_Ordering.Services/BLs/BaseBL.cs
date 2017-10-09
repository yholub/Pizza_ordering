using Pizza_Ordering.DataProvider.UnitOfWork;
using Pizza_Ordering.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.Services.BLs
{
    public abstract class BaseBL
    {
        private IUnitOfWorkFactory factory;

        public BaseBL(IUnitOfWorkFactory factory)
        {
            this.factory = factory;
        }

        protected TEntity UseDb<TEntity>(Func<IUnitOfWork, TEntity> func)
        {
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                return func(unitOfWork);
            }
        }

        protected void UseDb(Action<IUnitOfWork> func)
        {
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                func(unitOfWork);
            }
        }
    }
}
