using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Pizza_Ordering.Domain;
using Pizza_Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Contexts
{
    public class ApplicationUserManager : UserManager<User, long>
    {
        public ApplicationUserManager(IUserStore<User, long> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new CustomUserStore(context.Get<AuthorizationContext>()));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User, long>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
#if DEBUG
                RequiredLength = 2
#else
                RequiredLength = 6,
                RequireNonLetterOrDigit = true
#endif
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, long>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}
