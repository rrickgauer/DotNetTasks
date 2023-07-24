using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Views;

public class SignupRequestResponse
{
    public bool Successful { get; set; }
    public string? Error { get; set; }
    public User? User { get; set; }
}
