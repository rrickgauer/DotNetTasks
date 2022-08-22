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
    }
}
