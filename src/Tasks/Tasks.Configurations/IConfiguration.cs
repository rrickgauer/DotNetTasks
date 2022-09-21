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

        public Uri URL_GUI { get; }

        public string REQUEST_HEADER_KEY { get; }
        public string REQUEST_HEADER_VALUE { get; }

        public string IP_ADDRESS { get; }
    }
}
