namespace CacheSurvy.Models
{
    public class AppSettings
    {
        public LoggingSettings Logging { get; set; }

        public string AllowedHosts { get; set; }

        public ConnectionSettings ConnectionStrings { get; set; }

        public class LogLevels
        {
            public string Default { get; set; }

            public string Microsoft { get; set; }
        }

        public class LoggingSettings
        {
            public LogLevels LogLevel { get; set; }
        }

        public class ConnectionSettings
        {
            public string Cache { get; set; }

            public string DB { get; set; }

            public string Table { get; set; }

            public string Collection { get; set; }
        }
    }
}