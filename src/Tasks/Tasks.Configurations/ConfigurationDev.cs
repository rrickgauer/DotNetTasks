namespace Tasks.Configurations
{
    public class ConfigurationDev : ConfigurationProduction, IConfigs
    {
        public new string DB_DATABASE => "Tasks_Dev";
        public new string URL_GUI => "http://127.0.0.1:5020";
    }
}
