using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.CustomAttributes;

[AttributeUsage(AttributeTargets.Property)]
public class CopyToAttribute : Attribute
{
    public string Name { get; private set; }

    public string ParseFunctionName { get; set; } = string.Empty;

    public CopyToAttribute([CallerMemberName] string name = "")
    {
        Name = name;
    }


    public bool HasParseFunction => !string.IsNullOrWhiteSpace(ParseFunctionName);
}
