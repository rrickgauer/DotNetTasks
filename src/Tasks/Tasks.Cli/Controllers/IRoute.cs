using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Cli.Controllers;

public interface IRoute<in T>
{
    public void Route(T args);
}
