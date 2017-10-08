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

        public RoleRepository(AuthorizationContext context)
        {
            this.db = context;
        }

        public Task CreateAsync(CustomRole role)
        {
            db.Roles.Add(role);
            return Task.FromResult(1);
        }

        public Task DeleteAsync(CustomRole role)
        {
            db.Set<CustomRole>().Remove(role);
            return Task.FromResult(1);
        }

        public void Dispose()
        {
        }

        public Task<CustomRole> FindByIdAsync(long roleId)
        {
            return db.Set<CustomRole>().FindAsync(roleId);
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
            return db.Set<CustomRole>().ToList();
        }

        public CustomRole Get(object id)
        {
            return db.Set<CustomRole>().Find(id);
        }

        public void Create(CustomRole item)
        {
            db.Set<CustomRole>().Add(item);
        }

        public void Update(CustomRole item)
        {
            db.Entry<CustomRole>(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(object id)
        {
            CustomRole item = db.Set<CustomRole>().Find(id);
            if (item != null)
            {
                db.Set<CustomRole>().Remove(item);
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
