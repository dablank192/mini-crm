using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mini_crm.Model;

namespace mini_crm.Config;

public class PartnerDetailConfig : IEntityTypeConfiguration<PartnerDetail>
{
    public void Configure (EntityTypeBuilder<PartnerDetail> builder)
    {
        builder.ToTable("PartnerDetail");
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Customer)
        .WithOne(t => t.PartnerDetail)
        .HasForeignKey<PartnerDetail>(t => t.CustomerId);
    }
}
