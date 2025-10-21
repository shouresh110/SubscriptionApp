﻿namespace SubscriptionApp.Domain.Entities;

public class SubscriptionPlan
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int DurationInDays { get; set; }
}