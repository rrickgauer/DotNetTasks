using System.ComponentModel;

namespace Tasks.Service.Domain.Enums;

public enum CliShowChecklistItemsOption
{

    [Description("Show all the items")]
    All,

    [Description("Show only completed items")]
    Complete,

    [Description("Only show incomplete items")]
    Incomplete,
}
