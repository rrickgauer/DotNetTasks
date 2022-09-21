﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Domain.Views;


#pragma warning disable CS8601 // Possible null reference assignment.

namespace Tasks.Email.Messages
{
    public class DailyRecurrencesMessage : IEmailMessage
    {
        #region Symbolic constants
        private const string EMAIL_SUBJECT = "Your daily recurrences";
        #endregion

        #region Private members
        private readonly IConfiguration _configs;
        private readonly UserRecurrences _userRecurrences;
        #endregion

        #region Exceptions
        private readonly ArgumentNullException NULL_EMAIL_EXCEPTION = new("User email needs to have a value");
        #endregion

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="userRecurrences"></param>
        public DailyRecurrencesMessage(IConfiguration configs, UserRecurrences userRecurrences)
        {
            _configs = configs;
            _userRecurrences = userRecurrences;

            CheckUserEmail();
        }

        /// <summary>
        /// Ensure that user's email has a value
        /// </summary>
        private void CheckUserEmail()
        {
            if (_userRecurrences.User.Email is null)
            {
                throw NULL_EMAIL_EXCEPTION;
            }
        }

        public EmailContent GetEmailContent()
        {
            var content = GetBaseEmailContent();

            string body = string.Empty;

            foreach (var recurrence in _userRecurrences.Recurrences)
            {
                body += GetRecurrenceBodySection(recurrence) + "\n";
            }

            content.Body = body;

            return content;
        }

        /// <summary>
        /// Setup the EmailContent object that we are going to return
        /// </summary>
        /// <returns></returns>
        private EmailContent GetBaseEmailContent()
        {
            EmailContent emailContent = new()
            {
                Recipient = _userRecurrences.User.Email,
                Subject = EMAIL_SUBJECT,
            };

            return emailContent;
        }

        private string GetRecurrenceBodySection(Recurrence recurrence)
        {
            string result = $"    * {recurrence.Name}";

            return result;
        }




    }
}
