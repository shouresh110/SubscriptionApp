using Microsoft.EntityFrameworkCore;
using SubscriptionApp.Domain.Entities;

namespace SubscriptionApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
    public DbSet<UserSubscription> UserSubscriptions => Set<UserSubscription>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SubscriptionPlan>().HasData(
            new SubscriptionPlan { Id = 1, Name = "Basic", Price = 9.99m, DurationInDays = 30 },
            new SubscriptionPlan { Id = 2, Name = "Pro", Price = 19.99m, DurationInDays = 90 },
            new SubscriptionPlan { Id = 3, Name = "Premium", Price = 49.99m, DurationInDays = 365 }
        );

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Alice", Email = "alice@test.com" },
            new User { Id = 2, Name = "Bob", Email = "bob@test.com" }
        );
    }
}