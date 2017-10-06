using Microsoft.AspNet.Identity.EntityFramework;
using Pizza_Ordering.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Contexts
{
    public class CustomRoleStore : RoleStore<CustomRole, long, CustomUserRole>
    {
        public CustomRoleStore(AuthorizationContext context)
            : base(context)
        {
        }
    }
}
