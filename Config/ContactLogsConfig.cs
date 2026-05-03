using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mini_crm.Model;

namespace mini_crm.Config;

public class ContactLogsConfig : IEntityTypeConfiguration<ContactLogs>
{
    public void Configure (EntityTypeBuilder<ContactLogs> builder)
    {
        builder.ToTable("ContactLogs");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.CustomerId);
        builder.Property(t => t.CustomerId)
        .IsRequired();

        builder.Property(t => t.ContactMethod)
        .IsRequired();

        builder.Property(t => t.ContactDate)
        .IsRequired();

        builder.HasOne(t => t.Customer)
        .WithMany(t => t.ContactLogs)
        .HasForeignKey(t => t.CustomerId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
