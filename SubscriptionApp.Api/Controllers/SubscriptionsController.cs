using Microsoft.AspNetCore.Mvc;
using SubscriptionApp.Application.Interfaces;

namespace SubscriptionApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _service;

    public SubscriptionsController(ISubscriptionService service)
    {
        _service = service;
    }

    [HttpGet("plans")]
    public async Task<IActionResult> GetPlans() => Ok(await _service.GetPlansAsync());

    [HttpPost("{userId}/activate/{planId}")]
    public async Task<IActionResult> Activate(int userId, int planId)
        => Ok(await _service.ActivateAsync(userId, planId));

    [HttpDelete("{userId}/deactivate/{subscriptionId}")]
    public async Task<IActionResult> Deactivate(int userId, int subscriptionId)
        => Ok(await _service.DeactivateAsync(userId, subscriptionId));

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserSubs(int userId)
        => Ok(await _service.GetUserSubscriptionsAsync(userId));
}
