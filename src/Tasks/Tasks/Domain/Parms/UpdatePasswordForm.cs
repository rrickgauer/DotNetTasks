using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tasks.Domain.Parms
{
    public class UpdatePasswordForm
    {
        [BindRequired]
        public string? Password { get; set; }
    }
}
