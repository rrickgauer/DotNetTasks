using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Responses.Custom;

public class SignupRequestResponse
{
    public bool Successful { get; set; }
    public string? Error { get; set; }
    public User? User { get; set; }
}
