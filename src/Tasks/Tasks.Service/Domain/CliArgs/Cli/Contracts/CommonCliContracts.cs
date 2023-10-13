using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.CliArgs.Cli.Contracts;

public class CommonCliContracts
{
    public interface ICliDeleteFlag
    {
        public bool Force { get; set; }
    }

}
