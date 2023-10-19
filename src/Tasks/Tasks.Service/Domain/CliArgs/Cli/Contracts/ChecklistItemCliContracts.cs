using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
