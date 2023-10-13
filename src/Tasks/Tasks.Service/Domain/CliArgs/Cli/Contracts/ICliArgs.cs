using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.CliArgs.Cli.Contracts;

public interface ICliArgs<T>
{
    public static T FromArgs(IEnumerable<string> args)
    {
        return CliArgUtilities.ParseArgs<T>(args.ToArray());
    }

    public string ToArgs()
    {
        return CliArgUtilities.ToArgs(this);
    }
}
