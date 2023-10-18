using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.CliArgs.Cli.Contracts;

public class ChecklistCliContracts
{
    public interface IChecklistCliIndex
    {
        public uint? CommandLineId { get; set; }
    }

    public interface IChecklistCliTitle
    {
        public string? Title { get; set; }
    }
}


