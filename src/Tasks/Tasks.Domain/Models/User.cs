namespace Tasks.Domain.Models
{
    /// <summary>
    /// User domain model
    /// </summary>
    public class User
    {
        public Guid? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
