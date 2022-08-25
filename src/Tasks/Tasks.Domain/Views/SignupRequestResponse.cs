using Tasks.Domain.Models;

namespace Tasks.Domain.Views
{
    public class SignupRequestResponse
    {
        public bool Successful { get; set; }
        public string? Error { get; set; }
        public User? User { get; set; }
    }
}
