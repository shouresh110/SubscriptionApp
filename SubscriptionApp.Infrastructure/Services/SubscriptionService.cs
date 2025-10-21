using Microsoft.EntityFrameworkCore;
using SubscriptionApp.Application.Interfaces;
using SubscriptionApp.Domain.Entities;
using SubscriptionApp.Infrastructure.Data;

namespace SubscriptionApp.Infrastructure.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly AppDbContext _db;

    public SubscriptionService(AppDbContext db) => _db = db;

    public async Task<IEnumerable<SubscriptionPlan>> GetPlansAsync()
        => await _db.SubscriptionPlans.AsNoTracking().ToListAsync();

    public async Task<UserSubscription> ActivateAsync(int userId, int planId)
    {
        var plan = await _db.SubscriptionPlans.FindAsync(planId);
        if (plan == null) throw new Exception("Plan not found");

        var sub = new UserSubscription
        {
            UserId = userId,
            PlanId = planId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(plan.DurationInDays),
            IsActive = true
        };

        _db.UserSubscriptions.Add(sub);
        await _db.SaveChangesAsync();
        return sub;
    }

    public async Task<bool> DeactivateAsync(int userId, int subscriptionId)
    {
        var sub = await _db.UserSubscriptions
            .FirstOrDefaultAsync(s => s.Id == subscriptionId && s.UserId == userId);
        if (sub == null) return false;

        sub.IsActive = false;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int userId)
    {
        return await _db.UserSubscriptions
            .Include(s => s.Plan)
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }
}