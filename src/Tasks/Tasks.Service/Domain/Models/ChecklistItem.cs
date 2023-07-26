using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;

namespace Tasks.Service.Domain.Models;

public class ChecklistItem
{
    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [SqlColumn("checklist_id")]
    public Guid? ChecklistId { get; set; }

    [SqlColumn("content")]
    public string? Content { get; set; }

    [SqlColumn("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [SqlColumn("completed_on")]
    public DateTime? CompletedOn { get; set; } = null;

    [SqlColumn("position")]
    public uint Position { get; set; } = 0;


    /// <summary>
    /// Flag to check if item is complete.
    /// If CompletedOn has a value (is not null), then the item is complete.
    /// Otherwise, it is incomplete.
    /// </summary>
    [JsonIgnore]
    public bool IsComplete
    {
        get => CompletedOn != null;
        set => CompletedOn = value ? DateTime.Now : null;
    }
}
