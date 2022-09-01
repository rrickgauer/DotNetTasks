using Tasks.CustomAttributes;

namespace Tasks.Domain.Models;


/// <summary>
/// User domain model
/// </summary>
public class User
{
    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [SqlColumn("email")]
    public string? Email { get; set; }

    [SqlColumn("password")]
    public string? Password { get; set; }

    [SqlColumn("created_on")]
    public DateTime? CreatedOn { get; set; } = DateTime.Now;

    [SqlColumn("deliver_reminders")]
    public bool? DeliverReminders { get; set; } = false;
}
