namespace Tasks.Configurations
{
    public class ConfigurationDev : ConfigurationProduction, IConfigs
    {
        public new string DB_DATABASE => "Tasks_Dev";

        protected new static readonly Uri _urlGui = new(@"http://127.0.0.1:5020");
        public new Uri URL_GUI => _urlGui;
    }
}
