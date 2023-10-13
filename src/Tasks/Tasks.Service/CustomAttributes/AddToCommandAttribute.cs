using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.CustomAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public class AddToCommandAttribute : Attribute
{
    public string CommandName { get; private set; }

    public AddToCommandAttribute(string commandName)
    {
        CommandName = commandName;
    }
}
