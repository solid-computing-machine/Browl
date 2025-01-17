﻿using Browl.Service.MarketDataCollector.Domain.Entities;
using Browl.Service.MarketDataCollector.Domain.Interfaces.Services;
using Browl.Service.MarketDataCollector.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Browl.Service.MarketDataCollector.Infrastructure.Data.Contexts;
public class BrowlDbContext : DbContext
{
    private readonly ITenantService _tenantService;
    public BrowlDbContext(DbContextOptions options, ITenantService service) : base(options) => _tenantService = service;
    public string TenantName
    {
        get => _tenantService.GetTenant()?.TenantName ?? string.Empty;
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Habit>? Habits { get; set; }
    public DbSet<Progress>? Progress { get; set; }
    public DbSet<Reminder>? Reminders { get; set; }
    public DbSet<Goal>? Goals { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Telephone> Telephones { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenantConnectionString = _tenantService.GetConnectionString();
        if (!string.IsNullOrEmpty(tenantConnectionString))
        {
            optionsBuilder.UseSqlServer(_tenantService.GetConnectionString());
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Habit>().HasQueryFilter(a => a.TenantName == TenantName);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new TelephoneConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ChangeTracker.Entries<IHasTenant>()
            .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified)
            .ToList().ForEach(entry => entry.Entity.TenantName = TenantName);
        return await base.SaveChangesAsync(cancellationToken);
    }
}