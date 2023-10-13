namespace Tasks.Service.CustomAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public class AddOptionAttribute : Attribute
{
    public string OptionName { get; private set; }

    public AddOptionAttribute(string commandName)
    {
        OptionName = commandName;
    }
}
