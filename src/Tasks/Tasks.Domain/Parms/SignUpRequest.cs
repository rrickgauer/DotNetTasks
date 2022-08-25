using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain.Parms
{
    public class SignUpRequest
    {
        [BindRequired]
        public string? Email { get; set; }

        [BindRequired]
        public string? Password { get; set; }
    }
}
