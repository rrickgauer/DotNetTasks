using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Services.Interfaces;

public interface IUserEmailVerificationServices
{
    Task<UserEmailVerification?> CreateNewAsync(Guid userId);
    Task<bool> SendEmailAsync(UserEmailVerification userEmailVerification);
    Task<UserEmailVerification?> ConfirmEmailAsync(Guid userEmailVerificationId);
    Task<UserEmailVerification?> GetUserEmailVerificationAsync(Guid userEmailVerificationId);
}
