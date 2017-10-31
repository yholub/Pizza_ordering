using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Pizza_Ordering.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering.DataProvider.Contexts
{
    public class ApplicationRoleManager : RoleManager<CustomRole, long>
    {
        public ApplicationRoleManager(CustomRoleStore store)
            : base(store)
        {
        }
    }
}
