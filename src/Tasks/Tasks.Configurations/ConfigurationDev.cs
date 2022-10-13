namespace Tasks.Configurations;


public class ConfigurationDev : ConfigurationProduction, IConfigs
{
    public bool IsProduction => false;

    public new string DbDataBase => "Tasks_Dev";

    protected new static readonly Uri _urlGui = new(@"http://127.0.0.1:5020");
    public new Uri UrlGui => _urlGui;
}
