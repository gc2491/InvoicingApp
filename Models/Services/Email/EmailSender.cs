using System.Threading.Tasks;
using AspNET.Models.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace AspNET.Models.Services.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptionsMonitor<SmtpOptions> SmtpOptions;

        public EmailSender(IOptionsMonitor<SmtpOptions> smtpOptions)
        {
            this.SmtpOptions = smtpOptions;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var options = this.SmtpOptions.CurrentValue;
                using var client = new SmtpClient();
                await client.ConnectAsync(options.Host, options.Port, options.Security);
                if(!string.IsNullOrEmpty(options.Username))
                {
                    await client.AuthenticateAsync(options.Username, options.Password);
                }

                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(options.Sender));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = htmlMessage
                };

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
            catch(Exception exc)
            {
                // do something
            }
        }
    }
}
