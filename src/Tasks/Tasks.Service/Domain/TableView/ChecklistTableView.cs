using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Domain.TableView;

public class ChecklistTableView : ITableView
{
    // ITableView
    [JsonIgnore]
    public string ViewName => "View_Checklists";


    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [JsonIgnore]
    [SqlColumn("user_id")]
    public Guid? UserId { get; set; }

    [SqlColumn("title")]
    public string? Title { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("type")]
    [SqlColumn("checklist_type_id")]
    public ChecklistType ListType { get; set; } = ChecklistType.List;


    [SqlColumn("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [SqlColumn("count_items")]
    public long CountItems { get; set; } = 0;


}
