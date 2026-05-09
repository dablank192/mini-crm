using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mini_crm.Model;

namespace mini_crm.Config;

public class VendorDetailConfig : IEntityTypeConfiguration<VendorDetail>
{
    public void Configure (EntityTypeBuilder<VendorDetail> builder)
    {
        builder.ToTable("VendorDetail");
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Customer)
        .WithOne(t => t.VendorDetail)
        .HasForeignKey<VendorDetail>(t => t.CustomerId);
    }
}
