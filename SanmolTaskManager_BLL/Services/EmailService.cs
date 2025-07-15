using Microsoft.Extensions.Options;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System;

namespace SanmolTaskManager_BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(EmailReceiver receiver)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.Name, _settings.EmailId));
                message.To.Add(new MailboxAddress(receiver.UserName, receiver.EmailAddress));
                message.Subject = receiver.Subject;

                // Load HTML template
                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "Emails", "ReminderTemplate.html");
                var htmlBody = await File.ReadAllTextAsync(templatePath);

                // Build HTML table for tasks
                var taskTable = new StringBuilder();
                taskTable.Append(@"
                    <table style='width:100%; border-collapse:collapse; font-family:Segoe UI, sans-serif; font-size:14px;'>
                        <thead>
                            <tr style='background-color:#4e73df; color:#fff;'>
                                <th style='padding:10px; border:1px solid #ddd;'>Title</th>
                                <th style='padding:10px; border:1px solid #ddd;'>Customer</th>
                                <th style='padding:10px; border:1px solid #ddd;'>Due Date</th>
                                <th style='padding:10px; border:1px solid #ddd;'>Priority</th>
                                <th style='padding:10px; border:1px solid #ddd;'>Status</th>
                            </tr>
                        </thead>
                        <tbody>");

                foreach (var task in receiver.Tasks)
                {
                    taskTable.Append($@"
                            <tr>
                                <td style='padding:10px; border:1px solid #ddd;'>{task.Title}</td>
                                <td style='padding:10px; border:1px solid #ddd;'>{task.CustomerName}</td>
                                <td style='padding:10px; border:1px solid #ddd;'>{task.DueDate:dd-MMM-yyyy}</td>
                                <td style='padding:10px; border:1px solid #ddd;'>{task.Priority}</td>
                                <td style='padding:10px; border:1px solid #ddd;'>{task.Status}</td>
                            </tr>");
                }

                taskTable.Append("</tbody></table>");

                // Replace placeholders
                htmlBody = htmlBody.Replace("{{UserName}}", receiver.UserName)
                                   .Replace("{{TaskCount}}", receiver.Tasks.Count.ToString())
                                   .Replace("{{TaskCards}}", taskTable.ToString());

                // Build body
                var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
                message.Body = bodyBuilder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Timeout = 200000;
                await smtp.ConnectAsync(_settings.Host, _settings.Port, _settings.UseSSL);
                await smtp.AuthenticateAsync(_settings.EmailId, _settings.Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while sending email", ex);
            }
        }
    }
}
