using Microsoft.AspNetCore.Mvc.Filters;
using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;
using ScrumApp__Juro_.Services;
using System.Security.Claims;
using Task = System.Threading.Tasks.Task;

public class ActivityLogFilter : IAsyncActionFilter
{
    private readonly ActivityLogger _logger;
    private readonly ScrumDbContext _context;

    public ActivityLogFilter(ActivityLogger logger, ScrumDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Extract Username
        var username = context.HttpContext.User.Identity?.Name ?? "Anonymous";
        var role = "Unknown";
        var email = "Unknown";

        // Check if user exists in Manager table
        var manager = _context.Managers.FirstOrDefault(m => m.Username == username);
        if (manager != null)
        {
            role = "Manager";
            email = manager.Email;
        }
        else
        {
            var dev = _context.Developers.FirstOrDefault(d => d.Username == username);
            if (dev != null)
            {
                role = "Developer";
                email = dev.Email;
            }
        }

        var actionName = context.ActionDescriptor.DisplayName;

        await _logger.LogAsync(role, username, email, $"Accessed {actionName}");

        await next(); // Execute the action
    }
}
