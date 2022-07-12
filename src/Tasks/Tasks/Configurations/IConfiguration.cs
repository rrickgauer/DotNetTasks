namespace Tasks.Configurations
{
    public interface IConfiguration
    {
        public string DB_SERVER { get; }
        public string DB_DATABASE { get; }
        public string DB_USER { get; }
        public string DB_PASSWORD { get; }
    }
}
