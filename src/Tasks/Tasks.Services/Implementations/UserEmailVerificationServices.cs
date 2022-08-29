using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations
{
    public class UserEmailVerificationServices : IUserEmailVerificationServices
    {
        #region Private members
        private readonly IUserServices _userServices;
        private readonly IUserEmailVerificationRepository _userEmailVerificationRepository;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userServices"></param>
        public UserEmailVerificationServices(IUserServices userServices, IUserEmailVerificationRepository userEmailVerificationRepository)
        {
            _userServices = userServices;
            _userEmailVerificationRepository = userEmailVerificationRepository;
        }

        /// <summary>
        /// Create a new email verification record in the database for the speicfied user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserEmailVerification?> CreateNewAsync(Guid userId)
        {
            // need to the get the user's email address
            User? user = await _userServices.GetUserAsync(userId);

            if (user == null) return null;

            UserEmailVerification newUserEmailVerification = BuildNewVerificationObjectFromUser(user);

            // save the record to the database
            int numRecords = await _userEmailVerificationRepository.InsertAsync(newUserEmailVerification);

            if (numRecords < 0)
            {
                throw new Exception("InsertAsync returned less than 0 records...");
            }

            return newUserEmailVerification;
        }

        /// <summary>
        /// Build a brand new UserEmailVerification object with values from the specifed user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static UserEmailVerification BuildNewVerificationObjectFromUser(User user)
        {
            UserEmailVerification emailVerification = new()
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                UserId = user.Id,
                CreatedOn = DateTime.Now,
                ConfirmedOn = null,
            };

            return emailVerification;
        }
    }
}
