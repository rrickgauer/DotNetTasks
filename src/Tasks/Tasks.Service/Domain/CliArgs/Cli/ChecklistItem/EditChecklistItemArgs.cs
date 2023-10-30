using Tasks.Service.Domain.Enums;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.CommonCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;

public class EditChecklistItemArgs : IChecklistItemCliChecklistId, IChecklistItemCliIndex, IChecklistItemCliContent, ICliNoPromptsFlag, IChecklistItemCliStatus
{
    public uint ChecklistCommandLineId { get; set; }
    public string? Content { get; set; }                = null;
    public uint? ItemCommandLineId { get; set; }        = null;
    public CliChecklistItemStatus? Status { get; set; } = null;
    public bool NoPromptsFlag { get; set; }             = false;
    public bool NoItemPromptFlag { get; set; }          = false;
    public bool NoContentPromptFlag { get; set; }       = false;
    public bool NoStatusPromptFlag { get; set; }        = false;



    public bool ShouldPromptForItem
    {
        get
        {

            if (ItemCommandLineId.HasValue)
            {
                return false;
            }
            else if (NoPromptsFlag)
            {
                return false;
            }
            else if (NoItemPromptFlag)
            {
                return false;
            }

            return true;
        }
    }

    public bool ShouldPromptForStatus
    {
        get
        {
            if (Status.HasValue)
            {
                return false;
            }
            else if (NoPromptsFlag || NoStatusPromptFlag)
            {
                return false;
            }

            return true;
        }
    }

    public bool ShouldPromptForContent
    {
        get
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return false;
            }
            else if (NoPromptsFlag || NoContentPromptFlag)
            {
                return false;
            }

            return true;
        }
    }




}



