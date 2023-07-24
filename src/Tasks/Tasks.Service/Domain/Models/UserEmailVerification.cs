using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;

namespace Tasks.Service.Domain.Models;

public class UserEmailVerification
{
    [JsonIgnore]
    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [SqlColumn("user_id")]
    public Guid? UserId { get; set; }

    [SqlColumn("email")]
    public string? Email { get; set; }

    [SqlColumn("confirmed_on")]
    public DateTime? ConfirmedOn { get; set; }

    [SqlColumn("created_on")]
    public DateTime? CreatedOn { get; set; }

    [JsonIgnore]
    public bool IsConfirmed => ConfirmedOn != null;
}
