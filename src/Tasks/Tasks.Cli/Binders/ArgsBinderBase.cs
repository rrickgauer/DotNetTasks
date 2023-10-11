using System.CommandLine.Binding;

namespace Tasks.Cli.Binders;

public abstract class ArgsBinderBase<T> : BinderBase<T> where T : new()
{

}
