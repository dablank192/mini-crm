using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mini_crm.Model;

namespace mini_crm.Config;

public class ClientDetailConfig : IEntityTypeConfiguration<ClientDetail>
{
    public void Configure (EntityTypeBuilder<ClientDetail> builder)
    {
        builder.ToTable("ClientDetail");
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Customer)
        .WithOne(t => t.ClientDetail)
        .HasForeignKey<ClientDetail>(t => t.CustomerId);
    }
}
