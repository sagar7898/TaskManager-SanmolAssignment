using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SanmolTaskManager_Web.Controllers
{
    public class ReminderController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;

        public ReminderController(ITaskService taskService, IEmailService emailService, IOptions<EmailSettings> options)
        {
            _taskService = taskService;
            _emailService = emailService;
            _emailSettings = options.Value;
        }

        // GET: /Reminder/Send
        public async Task<IActionResult> Send()
        {
            try
            {
                var (allTasks, _) = await _taskService.FindPagedAsync("All", 1, 1000);
                var overdueTasks = allTasks
                    .Where(t => t.Status == "Pending" && t.DueDate.Date < DateTime.Today && !t.IsDeleted)
                    .ToList();

                if (!overdueTasks.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "No overdue tasks found."
                    });
                }

                var receiver = new EmailReceiver
                {
                    EmailAddress = _emailSettings.AdminReceiver,
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

                return Json(new
                {
                    success = true,
                    message = "✅ Overdue task summary sent to admin."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "❌ Failed to send overdue task summary.",
                    error = ex.Message
                });
            }
        }
    }
}
