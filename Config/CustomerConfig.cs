using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mini_crm.Model;

namespace mini_crm.Config;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure (EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.UserId)
        .IsRequired();

        builder.HasIndex(t => t.FirstName);
        builder.Property(t => t.FirstName)
        .IsRequired()
        .HasMaxLength(100);

        builder.HasIndex(t => t.LastName);
        builder.Property(t => t.LastName)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(t => t.Email)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(t => t.PhoneNumber)
        .IsRequired(false);

        builder.Property(t => t.CustomerTag)
        .IsRequired();

        builder.HasOne(t => t.User)
        .WithMany(t => t.Customer)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
