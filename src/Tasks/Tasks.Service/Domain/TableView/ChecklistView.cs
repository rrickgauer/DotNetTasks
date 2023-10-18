using Spectre.Console;
using System.Reflection;
using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.TableView;

public class ChecklistView : ITableView, ICliTable
{
    // ITableView
    [JsonIgnore]
    public string ViewName => "View_Checklists";


    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [JsonIgnore]
    [SqlColumn("command_line_reference")]
    [CliTableColumn("Id")]
    public uint? CommandLineReferenceId { get; set; }


    [JsonIgnore]
    [SqlColumn("user_id")]
    public Guid? UserId { get; set; }

    [SqlColumn("title")]
    [CliTableColumn]
    public string? Title { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    [JsonPropertyName("type")]
    [SqlColumn("checklist_type_id")]
    [CliTableColumn("Type")]
    public ChecklistType ListType { get; set; } = ChecklistType.List;

    [SqlColumn("created_on")]
    [CliTableColumn("Created On", ToStringMethodName = nameof(GetCreatedOnCliOutput))]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [SqlColumn("count_items")]
    public long CountItems { get; set; } = 0;


    #region - ICliTable -

    public static void AddTableColumns(Table table)
    {
        var properties = AttributeUtilities.GetPropertiesWithAttribute<CliTableColumnAttribute, ChecklistView>();
        var headers = properties.Select(p => p.GetCustomAttribute<CliTableColumnAttribute>()?.Header);

        foreach (var header in headers)
        {
            if (header != null)
            {
                table.AddColumn(header);
            }
        }
    }

    public string GetCreatedOnCliOutput()
    {
        return CreatedOn.ToString("MM/dd/yyyy hh:mm tt");
    }

    #endregion


}
