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

        private string _confirmationUrl => $"{_configs.URL_GUI}/email-verifications/{_userEmailVerification.Id}";
        #endregion

        public UserEmailVerificationMessage(UserEmailVerification userEmailVerification, IConfigs configs)
        {
            _userEmailVerification = userEmailVerification;
            _configs = configs; 
        }


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
            string body = string.Empty;

            body = $"{_confirmationUrl}";

            return body;
        }
    }
}
