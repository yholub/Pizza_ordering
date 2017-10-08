using Pizza_Ordering.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Pizza_Ordering.DataProvider.Configurations
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasKey(x => x.Id);
        }
    }
}
