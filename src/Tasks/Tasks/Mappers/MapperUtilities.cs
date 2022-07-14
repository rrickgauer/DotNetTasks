namespace Tasks.Mappers
{
    public class MapperUtilities
    {

        public static DateOnly? ToDateOnly(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return DateOnly.FromDateTime(dateTime.Value);
            }
            else
            {
                return null;
            }
        }
    }
}
