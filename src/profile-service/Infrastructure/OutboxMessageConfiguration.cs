using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using profile_service.Core.Domain;

namespace profile_service.Infrastructure
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable("Outbox");
            builder.HasKey(x => x.Id);
            builder.Property(b => b.CreatedDate);
            builder.Property(b => b.IsActive);
            builder.Property(b => b.MessageContent);
            builder.Property(b => b.MessageType);
        }
    }
}

