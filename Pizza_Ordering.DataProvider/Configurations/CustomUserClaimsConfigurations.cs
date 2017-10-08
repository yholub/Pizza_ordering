using Pizza_Ordering.Domain.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Pizza_Ordering.DataProvider.Configurations
{
    internal class CustomUserClaimsConfigurations : EntityTypeConfiguration<CustomUserClaim>
    {
        public CustomUserClaimsConfigurations()
        {
            ToTable("UserClaims");

            HasKey(x => x.Id);
        }
    }
}
