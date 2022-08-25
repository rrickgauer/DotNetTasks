using Tasks.Domain.Constants;
using Tasks.Domain.Enums;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations
{
    public class UserServices : IUserServices
    {
        #region Private members
        private readonly IUserRepository _userRepository;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Update the user password
        /// </summary>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePasswordAsync(Guid userId, string password)
        {
            var result = await _userRepository.UpdateUserPasswordAsync(userId, password);

            return result >= 0;
        }


        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="signUpRequest"></param>
        /// <returns></returns>
        public async Task<User?> CreateUserAsync(SignUpRequest signUpRequest)
        {
            // setup a new User object with the email/password values given in the argument
            User user = new()
            {
                Id = Guid.NewGuid(),
                Email = signUpRequest.Email,
                Password = signUpRequest.Password,
                CreatedOn = DateTime.Now,
            };

            // send it to the repository
            int numRecords = await _userRepository.InsertUserAsync(user);

            return numRecords < 0 ? null : user;
        }

        #region Validate new user

        /// <summary>
        /// Validate the new user signup request
        /// </summary>
        /// <param name="signUpRequest"></param>
        /// <returns></returns>
        public async Task<ValidateUserResult> ValidateNewUserAsync(SignUpRequest signUpRequest)
        {
            var result = ValidateUserResult.Valid;

            if (!ValidateNewUserPasswordLength(signUpRequest))
            {
                return ValidateUserResult.InvalidPasswordLength;
            }

            if (signUpRequest.Email.Length > SignUpRequestValueLimits.EmailMaxLength)
            {
                return ValidateUserResult.InvalidEmailLength;
            }

            if (await IsEmailTaken(signUpRequest.Email))
            {
                return ValidateUserResult.EmailIsTaken;
            }

            return result;
        }

        /// <summary>
        /// Checks if the password value is within the limits
        /// </summary>
        /// <param name="signUpRequest"></param>
        /// <returns></returns>
        private bool ValidateNewUserPasswordLength(SignUpRequest signUpRequest)
        {
            if (signUpRequest.Password.Length < SignUpRequestValueLimits.PasswordMinLength)
            {
                return false;
            }
            else if (signUpRequest.Password.Length > SignUpRequestValueLimits.PasswordMaxLength)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the given email address is registered to a user's account.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<bool> IsEmailTaken(string email)
        {
            User? user = await _userRepository.GetUserAsync(email);

            return user != null;
        }

        /// <summary>
        /// Get the appropriate SignupRequestResponse object for an invalid sign up attempt
        /// </summary>
        /// <param name="validateUserResult"></param>
        /// <returns></returns>
        public SignupRequestResponse GetInvalidSignUpRequestResponse(ValidateUserResult validateUserResult)
        {
            SignupRequestResponse result = new()
            {
                Successful = false,
                Error = GetInvalidNewUserRequestErrorMessage(validateUserResult),
            };

            return result;
        }

        /// <summary>
        /// Get the appropriate error message for the specified ValidateUserResult
        /// </summary>
        /// <param name="validateUserResult"></param>
        /// <returns></returns>
        private string GetInvalidNewUserRequestErrorMessage(ValidateUserResult validateUserResult)
        {
            string errorMessage = string.Empty;

            switch (validateUserResult)
            { 
                case ValidateUserResult.InvalidPasswordLength:
                    errorMessage = InvalidSignUpRequestErrorMessages.InvalidPasswordLengthMessage;
                    break;
                
                case ValidateUserResult.InvalidEmailLength:
                    errorMessage = InvalidSignUpRequestErrorMessages.InvalidEmailLengthMessage;
                    break;

                case ValidateUserResult.EmailIsTaken:
                    errorMessage = InvalidSignUpRequestErrorMessages.EmailIsTakenMessage;
                    break;
            }

            return errorMessage;
        }

        #endregion

    }
}
