using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tasks.Service.Domain.Parms;

public class SignUpRequest
{
    [BindRequired]
    public string? Email { get; set; }

    [BindRequired]
    public string? Password { get; set; }
}
