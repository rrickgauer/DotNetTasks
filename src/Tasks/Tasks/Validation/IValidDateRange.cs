namespace Tasks.Validation
{
    public interface IValidDateRange
    {
        public DateTime StartsOn { get; set; }
        public DateTime EndsOn { get; set; }
        public bool IsValid();
    }
}
