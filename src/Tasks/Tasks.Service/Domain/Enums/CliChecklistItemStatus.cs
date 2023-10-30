using System.ComponentModel;

namespace Tasks.Service.Domain.Enums;

public enum CliChecklistItemStatus
{
    [Description("Switch the status")]
    Toggle,

    [Description("Mark the item as complete")]
    Complete,

    [Description("Mark the item as incomplete")]
    Incomplete,
}
