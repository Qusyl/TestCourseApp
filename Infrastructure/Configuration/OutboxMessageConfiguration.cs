
using Infrastructure.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
           builder.HasKey(o => o.Id);
            builder.Property(o => o.Type).IsRequired();
            builder.Property(o => o.Payload).IsRequired();
            builder.Property(o => o.OccurredOn).IsRequired();
            builder.Property(o => o.ProcessedOn).IsRequired();

            builder.Property(o => o.Erorr).IsRequired();
        }
    }
}
