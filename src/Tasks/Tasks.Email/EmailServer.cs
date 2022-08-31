using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;

namespace Tasks.Email
{
    public class EmailServer
    {
        #region Symbolic constants
        private const int SMTP_PORT = 587;
        #endregion

        #region Private memebers
        private readonly SmtpClient _smtpClient;
        private readonly IConfigs _configs;
        private readonly NetworkCredential _credentials;
        private bool _isConnected = false;
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        public EmailServer(IConfigs configs)
        {
            _configs = configs;
            _smtpClient = new SmtpClient();
            _credentials = new NetworkCredential(_configs.EMAIL_USERNAME, _configs.EMAIL_PASSWORD);
        }   

        /// <summary>
        /// Setup connection
        /// </summary>
        public void Connect()
        {
            _smtpClient.Host = _configs.EMAIL_SMTP_CLIENT;
            _smtpClient.Port = SMTP_PORT;
            _smtpClient.EnableSsl = true;
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = _credentials;
            
            _isConnected = true;
        }


        /// <summary>
        /// Send message async
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(IEmailMessage emailMessage)
        {
            EmailContent content = emailMessage.GetEmailContent();

            await SendMessageAsync(content);
        }

        /// <summary>
        /// Send message async
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(EmailContent content)
        {
            EnsureConnected();

            await _smtpClient.SendMailAsync(_configs.EMAIL_ADDRESS, content.Recipient, content.Subject, content.Body);
        }

        /// <summary>
        /// Make sure that the smtp server is connected
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void EnsureConnected()
        {
            if (!_isConnected)
            {
                throw new Exception("Server is not connected yet!");
            }
        }

        /// <summary>
        /// Close the connection
        /// </summary>
        public void CloseConnection()
        {
            _smtpClient.Dispose();
        }
    }
}
