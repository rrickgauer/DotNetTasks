using System.Runtime.CompilerServices;

namespace Tasks.Service.CustomAttributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class CliTableColumnAttribute : Attribute
{
    public string Header { get; private set; }
    public int Position { get; set; } = -1;

    public string ToStringMethodName { get; set; } = string.Empty;

    public CliTableColumnAttribute([CallerMemberName] string header = "")
    {
        Header = header;
    }
}
