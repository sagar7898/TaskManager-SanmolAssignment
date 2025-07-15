using Microsoft.Extensions.Options;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Services
{
    // EmailReminderJob.cs
    public class EmailReminderJob : IEmailReminderJob
    {
        private readonly ITaskService _taskService;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _settings;

        public EmailReminderJob(ITaskService taskService, IEmailService emailService, IOptions<EmailSettings> options)
        {
            _taskService = taskService;
            _emailService = emailService;
            _settings = options.Value;
        }

        public async Task SendDailyOverdueSummary()
        {
            try
            {
                var (allTasks, _) = await _taskService.FindPagedAsync("All", 1, 1000);
                var overdueTasks = allTasks
                    .Where(t => t.Status == "Pending" && t.DueDate.Date < DateTime.Today && !t.IsDeleted)
                    .ToList();

                if (!overdueTasks.Any())
                    return;

                var receiver = new EmailReceiver
                {
                    EmailAddress = _settings.AdminReceiver,
                    UserName = "Admin",
                    Subject = $"⏰ Daily Overdue Task Summary ({DateTime.Today:dd-MMM-yyyy})",
                    Tasks = overdueTasks.Select(t => new EmailTaskItem
                    {
                        Title = t.Title,
                        CustomerName = t.Customer?.Name ?? "Unknown",
                        DueDate = t.DueDate,
                        Priority = t.Priority,
                        Status = t.Status
                    }).ToList()
                };

                await _emailService.SendEmailAsync(receiver);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while sending daily overdue summary email", ex);
            }
        }
    }
}
