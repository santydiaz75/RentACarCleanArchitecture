using RentACarCleanArchitecture.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RentACarCleanArchitecture.Infrastructure.Configurations;

internal sealed class OutboxMessageConfiguration
: IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        builder.HasKey(outboxMessage => outboxMessage.Id);

        builder.Property(OutboxMessage => OutboxMessage.Content)
        .HasColumnType("jsonb");

    }
}