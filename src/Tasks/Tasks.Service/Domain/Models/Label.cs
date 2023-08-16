using System.Text.Json.Serialization;
using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Domain.Models;

public class Label : ITableViewModel<ChecklistLabelView, Label>
{
    public Guid? Id { get; set; }

    [JsonIgnore]
    public Guid? UserId { get; set; }
    
    public string? Name { get; set; }
    
    public string? Color { get; set; }
    
    public DateTime? CreatedOn { get; set; }

    public static explicit operator Label(ChecklistLabelView other)
    {
        Label l = new()
        {
            Id = other.LabelId,
            UserId = other.LabelUserId,
            Name = other.LabelName,
            Color = other.LabelColor,
            CreatedOn = other.LabelCreatedOn,
        };


        return l;
    }
}
