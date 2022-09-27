using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Domain.Models;

#pragma warning disable CS8601 // Possible null reference assignment.

namespace Tasks.Email.Messages
{
    public class UserEmailVerificationMessage : IEmailMessage
    {
        #region Private members
        private readonly UserEmailVerification _userEmailVerification;
        private readonly IConfigs _configs;
        #endregion

        private string _confirmationUrl => $"{_configs.UrlGui}/email-verifications/{_userEmailVerification.Id}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userEmailVerification"></param>
        /// <param name="configs"></param>
        public UserEmailVerificationMessage(UserEmailVerification userEmailVerification, IConfigs configs)
        {
            _userEmailVerification = userEmailVerification;
            _configs = configs; 
        }
        
        /// <summary>
        /// Get the email content
        /// </summary>
        /// <returns></returns>
        public EmailContent GetEmailContent()
        {
            EmailContent emailContent = new()
            {
                Recipient = _userEmailVerification.Email,
                Subject = "Confirm email address",
                Body = GetBody(),
            };

            return emailContent;
        }


        private string GetBody()
        {
            string body = $"{_confirmationUrl}";

            return body;
        }
    }
}
