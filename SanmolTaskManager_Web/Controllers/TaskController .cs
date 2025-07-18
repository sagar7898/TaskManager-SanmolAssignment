using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_BLL.Services;
using SanmolTaskManager_Models;
using SanmolTaskManager_Models.ViewModels;
using System.Drawing.Printing;

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

        // -------------------- List & Search --------------------

        public async Task<IActionResult> Index(string search, string status = "Pending", int page = 1, string sortOrder = "asc", int pageSize = 5)
        {
            ViewBag.ShowSearchBox = true;
            if (!string.IsNullOrWhiteSpace(search))
            {
                var tasks = await _searchService.SearchTasksAsync(search);

                if (status != "All")
                    tasks = tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

                var viewModel = new TaskIndexViewModel
                {
                    Tasks = tasks,
                    Search = search,
                    Status = status,
                    SortOrder = sortOrder,
                    PageSize = pageSize,
                    CurrentPage = 1,
                    TotalCount = tasks.Count(),
                    TotalFilteredCount = tasks.Count()
                };

                return View(viewModel);
            }

            var taskViewModel = await _taskService.GetTaskIndexAsync(search, status, page, pageSize, sortOrder);
            return View(taskViewModel);
        }

        public async Task<IActionResult> LoadTable(string status = "Pending", int page = 1, int pageSize = 5, string sortOrder = "desc")
        {
            try
            {
                var viewModel = await _taskService.GetTaskIndexAsync(null, status, page, pageSize, sortOrder);
                return PartialView("_TaskTable", viewModel);
            }
            catch (Exception ex)
            {
                return PartialView("_TaskTable", new TaskIndexViewModel
                {
                    Tasks = Enumerable.Empty<TaskItem>(),
                    Status = status,
                    SortOrder = sortOrder,
                    PageSize = pageSize,
                    CurrentPage = page,
                    TotalCount = 0
                });
            }
        }

        // -------------------- Add or Edit --------------------

        public async Task<IActionResult> AddOrEdit(int? id, int page = 1, int pageSize = 10, string statusFilter = null)
        {
            try
            {
                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.StatusFilter = statusFilter;
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
        public async Task<IActionResult> AddOrEdit(TaskItem task, int page = 1, int pageSize = 5, string statusFilter = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(task); // return to form with validation errors
                }

                bool isNew = task.Id == 0;

                await _taskService.AddOrUpdateTaskAsync(task, page, pageSize);

                TempData["Success"] = isNew ? "Task added successfully!" : "Task updated successfully!";
                return RedirectToAction(nameof(Index), new { page = page, pageSize = pageSize, status = statusFilter });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong: " + ex.Message);
                return View(task);
            }
        }

        // -------------------- Mark Complete --------------------

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

        // -------------------- Export --------------------

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
    }
}
