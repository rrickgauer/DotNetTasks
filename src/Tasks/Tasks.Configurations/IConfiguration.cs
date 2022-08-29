namespace Tasks.Configurations
{
    public interface IConfigs
    {
        public string DB_SERVER { get; }
        public string DB_DATABASE { get; }
        public string DB_USER { get; }
        public string DB_PASSWORD { get; }

        public string EMAIL_ADDRESS { get; }
        public string EMAIL_SMTP_CLIENT { get; }
        public string EMAIL_USERNAME { get; }
        public string EMAIL_PASSWORD { get; }

        public string URL_GUI { get; } 
    }
}
