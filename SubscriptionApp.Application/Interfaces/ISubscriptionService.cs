using SubscriptionApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionApp.Application.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<SubscriptionPlan>> GetPlansAsync();
    Task<UserSubscription> ActivateAsync(int userId, int planId);
    Task<bool> DeactivateAsync(int userId, int subscriptionId);
    Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int userId);
}