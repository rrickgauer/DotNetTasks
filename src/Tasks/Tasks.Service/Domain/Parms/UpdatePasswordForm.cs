using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tasks.Service.Domain.Parms;

public class UpdatePasswordForm
{
    [BindRequired]
    public string Password { get; set; }
}
