namespace SubscriptionApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public ICollection<UserSubscription> Subscriptions { get; set; } = new List<UserSubscription>();
}