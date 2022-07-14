namespace Tasks.Domain.Enums
{
    public enum Frequency
    {
        ONCE,
        DAILY,
        WEEKLY,
        MONTHLY,
        YEARLY,
    }

    public sealed class FrequencyStrings
    {
        public const string ONCE    = "ONCE";
        public const string DAILY   = "DAILY";
        public const string WEEKLY  = "WEEKLY";
        public const string MONTHLY = "MONTHLY";
        public const string YEARLY  = "YEARLY";
    }
}
