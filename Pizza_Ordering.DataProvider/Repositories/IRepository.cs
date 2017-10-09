using Pizza_Ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Repositories
{
    public interface IRepository<TEntity>
    {
        List<TEntity> GetAll();

        TEntity GetById(object id);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(object id);

        IQueryable<TEntity> Query();
    }
}
