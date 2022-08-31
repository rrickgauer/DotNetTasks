namespace Tasks.Validation
{
    public interface IValidDateRange
    {
        public DateTime StartsOn { get; set; }
        public DateTime EndsOn { get; set; }
        public bool IsValid();
    }

    public class ValidDateRange : IValidDateRange
    {
        public DateTime StartsOn { get; set; } = DateTime.Now;
        public DateTime EndsOn { get; set; } = DateTime.Now;
        public bool IsValid() => EndsOn >= StartsOn;
    }

}
