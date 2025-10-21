using Microsoft.EntityFrameworkCore;
using Serilog;
using SubscriptionApp.Application.Interfaces;
using SubscriptionApp.Domain.Entities;
using SubscriptionApp.Infrastructure.Data;
using SubscriptionApp.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Serilog (نیازمند نصب Serilog.AspNetCore و using Serilog)
builder.Host.UseSerilog((ctx, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration)
      .WriteTo.Console());

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF InMemory (نیازمند پکیج‌های EFCore + InMemory در Infrastructure و رفرنس پروژه)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("SubscriptionDb"));

// DI
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!await db.SubscriptionPlans.AnyAsync())
    {
        db.SubscriptionPlans.AddRange(
            new SubscriptionPlan { Id = 1, Name = "Basic", Price = 9.99m, DurationInDays = 30 },
            new SubscriptionPlan { Id = 2, Name = "Pro", Price = 19.99m, DurationInDays = 90 },
            new SubscriptionPlan { Id = 3, Name = "Premium", Price = 49.99m, DurationInDays = 365 }
        );
        db.Users.AddRange(
            new User { Id = 1, Name = "Alice", Email = "alice@test.com" },
            new User { Id = 2, Name = "Bob", Email = "bob@test.com" }
        );
        await db.SaveChangesAsync();
    }
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();