namespace Tasks.Configurations;


public class ConfigurationDev : ConfigurationProduction, IConfigs
{
    public override bool IsProduction => false;

    public override string DbDataBase => "Tasks_Dev";

    protected static readonly Uri _urlGui = new(@"http://127.0.0.1:5020");
    public new Uri UrlGui => _urlGui;
}
