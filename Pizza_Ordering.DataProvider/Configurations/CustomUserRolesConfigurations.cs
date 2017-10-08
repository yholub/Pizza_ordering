using Pizza_Ordering.Domain.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Pizza_Ordering.DataProvider.Configurations
{
    internal class CustomUserRolesConfigurations : EntityTypeConfiguration<CustomUserRole>
    {
        public CustomUserRolesConfigurations()
        {
            ToTable("UserRoles");

            HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
