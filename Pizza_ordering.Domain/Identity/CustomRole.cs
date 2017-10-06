using Microsoft.AspNet.Identity.EntityFramework;

namespace Pizza_Ordering.Domain.Identity
{
    public class CustomRole : IdentityRole<long, CustomUserRole>
    {
        public CustomRole()
        {
        }

        public CustomRole(string name)
        {
            Name = name;
        }
    }
}
