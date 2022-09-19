using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using profile_service.Core.Domain;

namespace profile_service.Infrastructure
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile");
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(255);
        }
    }
}

