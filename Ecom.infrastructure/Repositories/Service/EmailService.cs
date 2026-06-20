using Ecom.Core.DTO;
using Ecom.Core.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories.Service
{
    public class EmailService: IEmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendEmail(EmailDTO emailDTO)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("My Ecom", configuration["EmailSetting:from"]));
            message.Subject = emailDTO.Subject;
            message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDTO.Content

            };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(
                    host: configuration["EmailSetting:Smtp"],
                    port: int.Parse(configuration["EmailSetting:Port"]),
                    useSsl: true);

                await smtp.AuthenticateAsync(
                    userName: configuration["EmailSetting:Username"],
                    password: configuration["EmailSetting:Password"]);

                await smtp.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                smtp.Disconnect(quit: true);
                smtp.Dispose();
            }

        }
    }
}
