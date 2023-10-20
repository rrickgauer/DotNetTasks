using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Domain.CliArgs.Cli.Contracts;

public class ChecklistItemCliContracts
{
    public interface IChecklistItemCliIndex
    {
        public uint? ItemCommandLineId { get; set; }
    }

    public interface IChecklistItemCliContent
    {
        public string? Content { get; set; }
    }

    public interface IChecklistItemCliChecklistId
    {
        public uint ChecklistCommandLineId { get; set; }
    }

    public interface IChecklistItemCliStatus
    {
        public CliChecklistItemStatus? Status { get; set; }
    }

}
