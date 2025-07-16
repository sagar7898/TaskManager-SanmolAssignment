using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_BLL.Services;
using SanmolTaskManager_Models;

namespace SanmolTaskManager_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICustomerService _customerService;
        private readonly IExcelExportService _excelExportService;
        private readonly ISearchService _searchService;

        public TaskController(ITaskService taskService, ICustomerService customerService, IExcelExportService excelExportService, ISearchService searchService)
        {
            _taskService = taskService;
            _customerService = customerService;
            _excelExportService = excelExportService;
            _searchService = searchService;
        }

        public async Task<IActionResult> Index(string search, string status = "Pending", int page = 1, string sortOrder = "asc", int pageSize = 5)
        {
            IEnumerable<TaskItem> tasks;
            int totalCount;
            ViewBag.SortOrder = sortOrder;
            ViewBag.PageSize = pageSize;


            if (!string.IsNullOrWhiteSpace(search))
            {
                tasks = await _searchService.SearchTasksAsync(search);
                totalCount = tasks.Count();

                // Optional: filter search results by status if needed
                if (status != "All")
                {
                    tasks = tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
                    totalCount = tasks.Count();
                }

                ViewBag.TotalPages = 1; // or calculate based on filtered results if needed
                ViewBag.CurrentPage = 1;
            }
            else
            {
                if (status == "All")
                {
                    // Fetch without filtering status
                    (tasks, totalCount) = await _taskService.GetAllPagedAsync(page, pageSize, sortOrder);
                }
                else
                {
                    (tasks, totalCount) = await _taskService.FindPagedAsync(status, page, pageSize, sortOrder);
                }
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                ViewBag.CurrentPage = page;
            }
            ViewBag.TotalFilteredCount = totalCount; // 👈 Add this line
            ViewBag.ShowSearchBox = true;
            ViewBag.StatusFilter = status;
            return View(tasks);
        }

        public async Task<IActionResult> AddOrEdit(int? id)
        {
            try
            {
                var (customers, _) = await _customerService.GetAllPagedAsync(1, 100);
                ViewBag.Customers = customers;

                if (id == null || id == 0)
                {
                    var task = new TaskItem { CustomerId = null };
                    return View(task);
                }

                var taskItem = await _taskService.GetByIdAsync(id.Value);
                if (taskItem == null)
                    return NotFound();

                return View(taskItem);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the task.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(TaskItem task)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var (customers, _) = await _customerService.GetAllPagedAsync(1, 100);
                    ViewBag.Customers = customers;
                    return View(task);
                }

                if (task.Id == 0)
                {
                    await _taskService.AddAsync(task);
                    TempData["Success"] = "Task added successfully!";
                }
                else
                {
                    await _taskService.UpdateAsync(task);
                    TempData["Success"] = "Task updated successfully!";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while saving the task.";
                return View(task);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarkComplete(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);
                if (task == null) return NotFound();

                task.Status = "Completed";
                await _taskService.UpdateAsync(task);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Failed to mark task as completed." });
            }
        }

        public async Task<IActionResult> ExportToExcel(string status = "Pending")
        {
            try
            {
                IEnumerable<TaskItem> tasks;

                if (status == "All")
                    (tasks, _) = await _taskService.GetAllPagedAsync(1, 100);
                else
                    (tasks, _) = await _taskService.FindPagedAsync(status, 1, 100);

                foreach (var task in tasks)
                    task.Customer ??= await _customerService.GetByIdAsync(task.CustomerId.Value);

                var stream = await _excelExportService.ExportTasksAsync(tasks);

                return File(stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Tasks_{status}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while exporting to Excel.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> LoadTable(string status = "Pending", int page = 1, int pageSize = 5, string sortOrder = "desc")
        {
            try
            {
                var (tasks, totalCount) = await _taskService.FindPagedAsync(status, page, pageSize, sortOrder);
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                ViewBag.StatusFilter = status;
                ViewBag.PageSize = pageSize;
                ViewBag.SortOrder = sortOrder;

                return PartialView("_TaskTable", tasks);
            }
            catch (Exception ex)
            {
                return PartialView("_TaskTable", Enumerable.Empty<TaskItem>());
            }
        }
    }
}
