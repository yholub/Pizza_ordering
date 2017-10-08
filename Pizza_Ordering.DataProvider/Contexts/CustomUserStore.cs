using Microsoft.AspNet.Identity.EntityFramework;
using Pizza_Ordering.Domain;
using Pizza_Ordering.Domain.Entities;
using Pizza_Ordering.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Contexts
{
    public class CustomUserStore : UserStore<User, CustomRole, long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(AuthorizationContext context)
            : base(context)
        {
        }
    }
}
