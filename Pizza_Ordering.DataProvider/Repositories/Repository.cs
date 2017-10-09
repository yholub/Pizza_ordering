using Pizza_Ordering.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected ApplicationDbContext db;

        private readonly DbSet<TEntity> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this.db = context;
            dbSet = context.Set<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
        }

        public void Update(TEntity item)
        {
            dbSet.Attach(item);
            db.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(object id)
        {
            TEntity item = db.Set<TEntity>().Find(id);
            if (item != null)
            {
                dbSet.Remove(item);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IQueryable<TEntity> Query()
        {
            return dbSet.AsQueryable();
        }
    }
}
