using System;
using Microsoft.EntityFrameworkCore;
using mini_crm.Model;


namespace mini_crm.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> option) : base(option) {}

    public DbSet<User> User {get; set;}
    public DbSet<Customer> Customer {get; set;}
    public DbSet<ContactLogs> ContactLogs {get; set;}

    public void Configure (ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
