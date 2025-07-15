using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_Web.Models;

namespace SanmolTaskManager_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICustomerService _customerService;

        public DashboardController(ITaskService taskService, ICustomerService customerService)
        {
            _taskService = taskService;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var stats = await _taskService.GetDashboardStatsAsync();
                var recentTasks = await _taskService.GetRecentTasksAsync();
                var totalCustomers = await _customerService.GetTotalCountAsync();

                ViewBag.RecentTasks = recentTasks;
                ViewBag.TotalCustomers = totalCustomers;

                return View(stats);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }
    }
}
