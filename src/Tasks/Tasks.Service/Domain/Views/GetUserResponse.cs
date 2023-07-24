using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;

namespace Tasks.Service.Domain.Views;

public class GetUserResponse
{
    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [SqlColumn("email")]
    public string? Email { get; set; }

    [JsonIgnore]
    [SqlColumn("password")]
    public string? Password { get; set; }

    [SqlColumn("created_on")]
    public DateTime? CreatedOn { get; set; }

    [SqlColumn("email_confirmed_on")]
    public DateTime? EmailConfirmedOn { get; set; }

    [SqlColumn("deliver_reminders")]
    public bool? DeliverReminders { get; set; }

    [JsonIgnore]
    public bool IsConfirmed => EmailConfirmedOn != null;
}
