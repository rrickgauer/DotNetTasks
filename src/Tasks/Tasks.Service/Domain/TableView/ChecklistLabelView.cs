using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Domain.TableView;

public class ChecklistLabelView : ITableView
{
    // ITableView
    public string ViewName => "View_Checklist_Labels";


    [SqlColumn("checklist_id")]
    public Guid? ChecklistId { get; set; }

    [SqlColumn("checklist_user_id")]
    public Guid? ChecklistUserId { get; set; }

    [SqlColumn("checklist_title")]
    public string? ChecklistTitle { get; set; }

    [SqlColumn("checklist_type_id")]
    public ChecklistType? ChecklistTypeId { get; set; }

    [SqlColumn("checklist_created_on")]
    public DateTime? ChecklistCreatedOn { get; set; }

    [SqlColumn("label_id")]
    public Guid? LabelId { get; set; }

    [SqlColumn("label_user_id")]
    public Guid? LabelUserId { get; set; }

    [SqlColumn("label_name")]
    public string? LabelName { get; set; }

    [SqlColumn("label_color")]
    public string? LabelColor { get; set; }

    [SqlColumn("label_created_on")]
    public DateTime? LabelCreatedOn { get; set; }

    [SqlColumn("created_on")]
    public DateTime? CreatedOn { get; set; }
}
