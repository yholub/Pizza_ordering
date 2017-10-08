using Pizza_Ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected ApplicationDbContext db;

        public Repository(ApplicationDbContext context)
        {
            this.db = context;
        }

        public List<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public TEntity Get(object id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public void Create(TEntity item)
        {
            db.Set<TEntity>().Add(item);
        }

        public void Update(TEntity item)
        {
            db.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(object id)
        {
            TEntity item = db.Set<TEntity>().Find(id);
            if (item != null)
            {
                db.Set<TEntity>().Remove(item);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
