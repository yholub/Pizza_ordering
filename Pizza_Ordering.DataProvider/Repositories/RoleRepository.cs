using Microsoft.AspNet.Identity;
using Pizza_Ordering.DataProvider.Contexts;
using Pizza_Ordering.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Repositories
{
    public class RoleRepository : IRepository<CustomRole>, IRoleStore<CustomRole, long>
    {
        protected AuthorizationContext db;

        private readonly DbSet<CustomRole> dbSet;

        public RoleRepository(AuthorizationContext context)
        {
            this.db = context;
            this.dbSet = db.Set<CustomRole>();
        }

        public Task CreateAsync(CustomRole role)
        {
            db.Roles.Add(role);
            return Task.FromResult(1);
        }

        public Task DeleteAsync(CustomRole role)
        {
            dbSet.Remove(role);
            return Task.FromResult(1);
        }

        public void Dispose()
        {
        }

        public Task<CustomRole> FindByIdAsync(long roleId)
        {
            return dbSet.FindAsync(roleId);
        }

        public Task<CustomRole> FindByNameAsync(string roleName)
        {
            return db.Roles.FirstOrDefaultAsync(t => t.Name == roleName);
        }

        public Task UpdateAsync(CustomRole role)
        {
            db.Entry<CustomRole>(role).State = System.Data.Entity.EntityState.Modified;
            return Task.FromResult(1);
        }

        public List<CustomRole> GetAll()
        {
            return dbSet.ToList();
        }

        public CustomRole GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Create(CustomRole item)
        {
            dbSet.Add(item);
        }

        public void Update(CustomRole item)
        {
            dbSet.Attach(item);
            db.Entry<CustomRole>(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(object id)
        {
            CustomRole item = db.Set<CustomRole>().Find(id);
            if (item != null)
            {
                dbSet.Remove(item);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IQueryable<CustomRole> Query()
        {
            return dbSet.AsQueryable();
        }
    }
}
