using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.CliArgs.Cli.Contracts;

public class LabelCliContracts
{
    public interface ILabelCliName
    {
        public string? Name { get; set; }
    }

    public interface ILabelCliColor
    {
        public string? Color { get; set; }
    }

    public interface ILabelCliIndex
    {
        public uint? Index { get; set; }
    }
}
