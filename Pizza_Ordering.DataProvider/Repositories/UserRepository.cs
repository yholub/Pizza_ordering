using Microsoft.AspNet.Identity;
using Pizza_Ordering.DataProvider.Contexts;
using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Repositories
{
    public class UserRepository : IRepository<User>, IUserStore<User, long>, IUserPasswordStore<User, long>,
        IUserSecurityStampStore<User, long>, IUserRoleStore<User, long>
    {
        protected AuthorizationContext db;

        private readonly DbSet<User> dbSet;

        public UserRepository(AuthorizationContext context)
        {
            this.db = context;
            this.dbSet = db.Set<User>();
        }

        #region IUserStore
        public Task CreateAsync(User user)
        {
            dbSet.Add(user);
            return Task.FromResult(1);
        }

        public Task DeleteAsync(User user)
        {
            dbSet.Remove(user);
            return Task.FromResult(1);
        }

        public Task<User> FindByIdAsync(long userId)
        {
            return dbSet.FindAsync(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return dbSet.FirstOrDefaultAsync(t => t.UserName == userName);
        }

        public Task UpdateAsync(User user)
        {
            db.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
            return Task.FromResult(1);
        }

        public void Dispose()
        {
            db.Dispose();
        }
        #endregion

        #region IUserPasswordStore
        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(1);
        }
        #endregion

        #region IUserSecurityStampStore
        public Task<string> GetSecurityStampAsync(User user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetSecurityStampAsync(User user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(1);
        }
        #endregion

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult<bool>(true);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            return Task.FromResult(1);
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            var role = db.Roles.FirstAsync(t => t.Name == roleName).Result;

            user.Roles.Add(new Domain.Identity.CustomUserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            });

            return Task.FromResult(1);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var role = db.Roles.FirstAsync(t => t.Name == roleName).Result;

            user.Roles.Remove(user.Roles.First(c => c.RoleId == role.Id && c.UserId == user.Id));

            return Task.FromResult(1);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            List<string> rolesNames = new List<string>();

            foreach (var role in user.Roles)
            {
                rolesNames.Add(db.Roles.First(c => c.Id == role.RoleId).Name);
            }

            return Task.FromResult<IList<string>>(rolesNames);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            List<string> userRoles = GetRolesAsync(user).Result.ToList();
            return Task.FromResult<bool>(userRoles.Contains(roleName));
        }

        public List<User> GetAll()
        {
            return dbSet.ToList();
        }

        public User GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Create(User item)
        {
            dbSet.Add(item);
        }

        public void Update(User item)
        {
            dbSet.Attach(item);
            db.Entry<User>(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(object id)
        {
            User item = dbSet.Find(id);
            if (item != null)
            {
                dbSet.Remove(item);
            }
        }

        public IQueryable<User> Query()
        {
            return dbSet.AsQueryable();
        }
    }
}
