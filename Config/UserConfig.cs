using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mini_crm.Model;

namespace mini_crm.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure (EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Username);
    }
}
