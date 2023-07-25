using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Domain.Models;

public class Checklist
{
    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [JsonIgnore]
    [SqlColumn("user_id")]
    public Guid? UserId { get; set; }

    [SqlColumn("title")]
    public string? Title { get; set; }

    [SqlColumn("checklist_type_id")]
    public ChecklistType ListType { get; set; } = ChecklistType.List;

    [SqlColumn("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.Now;
}




#region - DotnetLists Original -

//public class Checklist : ITableViewModel<ChecklistTableView, Checklist>, ITableViewModel<LabelAssignmentTableView, Checklist>
//{
//    public Guid? Id { get; set; } = Guid.NewGuid();
//    public string? Title { get; set; } = string.Empty;
//    public string? Description { get; set; } = string.Empty;
//    public ChecklistType ListType { get; set; } = ChecklistType.List;
//    public DateTime CreatedOn { get; set; } = DateTime.Now;

//    public List<ChecklistItem> Items { get; set; } = new();


//    #region - ITableViewModel -

//    public static Checklist FromView(ChecklistTableView view)
//    {
//        Checklist result = new()
//        {
//            Id = view.Id,
//            Title = view.Title,
//            Description = view.Description,
//            ListType = view.ListType,
//            CreatedOn = view.CreatedOn
//        };

//        return result;
//    }


//    public static Checklist FromView(LabelAssignmentTableView view)
//    {
//        Checklist result = new()
//        {
//            Id = view.ChecklistId,
//            Title = view.ChecklistTitle,
//            Description = view.ChecklistDescription,
//            ListType = view.ChecklistType,
//            CreatedOn = view.ChecklistCreatedOn,
//        };

//        return result;
//    }

//    #endregion
//}

#endregion
