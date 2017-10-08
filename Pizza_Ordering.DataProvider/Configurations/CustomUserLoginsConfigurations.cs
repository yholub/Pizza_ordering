using Pizza_Ordering.Domain.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Pizza_Ordering.DataProvider.Configurations
{
    internal class CustomUserLoginsConfigurations : EntityTypeConfiguration<CustomUserLogin>
    {
        public CustomUserLoginsConfigurations()
        {
            ToTable("UserLogins");

            HasKey(x => new { x.ProviderKey, x.LoginProvider, x.UserId });
        }
    }
}
