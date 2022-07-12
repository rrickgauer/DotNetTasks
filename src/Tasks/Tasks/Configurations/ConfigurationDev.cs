namespace Tasks.Configurations
{
    public class ConfigurationDev : ConfigurationProduction, IConfigs
    {
        public new string DB_DATABASE => "Tasks_Dev";
    }
}
