namespace Tasks.Service.Domain.CliArgs.Cli.Contracts;

public class CommonCliContracts
{
    public interface ICliDeleteFlag
    {
        public bool Force { get; set; }
    }

}
