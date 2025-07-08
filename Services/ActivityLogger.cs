using ScrumApp__Juro_.Data;
using ScrumApp__Juro_.Models.Entities;
using Task = System.Threading.Tasks.Task;

namespace ScrumApp__Juro_.Services
{
    public class ActivityLogger
    {
        private readonly ScrumDbContext _context;

        public ActivityLogger(ScrumDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string role, string username, string email, string action, string details = "")
        {
            var log = new ActivityLog
            {
                Role = $"[{role}]",
                Username = username,
                Email = email,
                Action = action,
                Details = details,
                Timestamp = DateTime.Now
            };

            _context.ActivityLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }

}
