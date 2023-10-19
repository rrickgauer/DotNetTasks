
using Spectre.Console;
using System.Reflection;
using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Constants;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.TableView;

public class ChecklistItemView : ITableView, ICliTable
{

    #region - ITableView -
    [JsonIgnore]
    public string ViewName => "View_Checklist_Items";
    #endregion


    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [SqlColumn("command_line_reference")]
    [CliTableColumn("ID")]
    public uint? CommandLineReference { get; set; }

    [SqlColumn("checklist_id")]
    public Guid? ChecklistId { get; set; }

    [SqlColumn("checklist_command_line_reference")]
    public uint? ChecklistCommandLineReference { get; set; }

    [SqlColumn("content")]
    [CliTableColumn]
    public string? Content { get; set; }

    [CliTableColumn("Completed")]
    public bool IsComplete
    {
        get => CompletedOn != null;
        set => CompletedOn = value ? DateTime.Now : null;
    }


    [SqlColumn("position")]
    public uint Position { get; set; } = 0;

    [SqlColumn("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [CliTableColumn("Created On")]
    [JsonIgnore]
    public string CreatedOnCliTable => CreatedOn.ToString(DateFormatTokens.CliOutput);


    [SqlColumn("completed_on")]
    public DateTime? CompletedOn { get; set; }


    #region - ICliTable -

    public static void AddTableColumns(Table table)
    {
        GetColumns().ForEach(c => table.AddColumn(c));
    }


    public static List<string> GetColumns()
    {
        var props = AttributeUtilities.GetPropertiesWithAttributeDict<CliTableColumnAttribute>(typeof(ChecklistItemView));

        var columns = props.Select(p => p.Attribute.Header);

        return columns.ToList();
    }



    #endregion
}
