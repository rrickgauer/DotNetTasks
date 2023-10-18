using Spectre.Console;
using System.Reflection;
using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Models;

public class Checklist : ITableViewModel<ChecklistView, Checklist>, ITableViewModel<ChecklistLabelView, Checklist>, ICliTable
{
    [SqlColumn("id")]
    [CliTableColumn]
    public Guid? Id { get; set; }

    [JsonIgnore]
    [SqlColumn("user_id")]
    [CliTableColumn("User fucking id")]
    public Guid? UserId { get; set; }

    [SqlColumn("title")]
    [CliTableColumn]
    public string? Title { get; set; }

    [SqlColumn("checklist_type_id")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("type")]
    [CliTableColumn]
    public ChecklistType ListType { get; set; } = ChecklistType.List;

    [SqlColumn("created_on")]
    [CliTableColumn]
    public DateTime CreatedOn { get; set; } = DateTime.Now;



    #region - ITableViewModel -

    public static explicit operator Checklist(ChecklistView other)
    {
        Checklist checklist = new()
        {
            Id = other.Id,
            Title = other.Title,
            UserId = other.UserId,
            ListType = other.ListType,
            CreatedOn = other.CreatedOn
        };

        return checklist;

    }

    public static explicit operator Checklist(ChecklistLabelView other)
    {
        Checklist checklist = new()
        {
            Id = other.ChecklistId,
            Title = other.ChecklistTitle,
            UserId = other.ChecklistUserId,
            ListType = other.ChecklistTypeId.Value,
            CreatedOn = other.ChecklistCreatedOn.Value,
        };

        return checklist;
    }

    #endregion


    public static void AddTableColumns(Table table)
    {
        var properties = AttributeUtilities.GetPropertiesWithAttribute<CliTableColumnAttribute>(typeof(Checklist));
        var headers = properties.Select(p => p.GetCustomAttribute<CliTableColumnAttribute>().Header);

        foreach(var header in headers)
        {
            table.AddColumn(header);
        }
    }



}



