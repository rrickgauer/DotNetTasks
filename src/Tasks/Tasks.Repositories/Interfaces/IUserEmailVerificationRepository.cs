using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Repositories.Interfaces
{
    public interface IUserEmailVerificationRepository
    {
        Task<int> InsertAsync(UserEmailVerification userEmailVerification);
        Task<int> UpdateAsync(UserEmailVerification userEmailVerification);
        Task<DataRow?> GetAsync(Guid id);
    }
}
