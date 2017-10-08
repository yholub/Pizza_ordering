using Pizza_Ordering.Domain.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Pizza_Ordering.DataProvider.Configurations
{
    internal class CustomRoleConfigurations : EntityTypeConfiguration<CustomRole>
    {
        public CustomRoleConfigurations()
        {
            ToTable("Roles");

            HasKey(x => x.Id);
        }
    }
}
