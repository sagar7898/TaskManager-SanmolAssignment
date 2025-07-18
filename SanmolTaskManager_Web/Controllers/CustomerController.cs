using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_BLL.Services;
using SanmolTaskManager_Models;
using SanmolTaskManager_Models.ViewModels;
using SanmolTaskManager_Web.Models;

namespace SanmolTaskManager_Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IExcelExportService _excelExportService;
        private readonly ISearchService _searchService;
        private readonly ITaskService _taskService;
        private const int PageSize = 10;

        public CustomerController(ICustomerService customerService, IExcelExportService excelExportService, ISearchService searchService, ITaskService taskService)
        {
            _customerService = customerService;
            _excelExportService = excelExportService;
            _searchService = searchService;
            _taskService = taskService;
        }

        // -------------------- List & Search --------------------

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            try
            {
                var viewModel = await _customerService.GetCustomerIndexAsync(search, page, PageSize);
                ViewBag.ShowSearchBox = true;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null || customer.IsDeleted) return NotFound();

                var tasks = await _taskService.GetTasksByCustomerIdAsync(id);
                ViewBag.Tasks = tasks;

                return View(customer);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        // -------------------- Add or Edit --------------------

        public async Task<IActionResult> AddOrEdit(int? id)
        {
            try
            {
                if (id == null || id == 0)
                    return View(new Customer());

                var customer = await _customerService.GetByIdAsync(id.Value);
                if (customer == null)
                    return NotFound();

                return View(customer);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Customer customer, int Page = 1)
        {
            try
            {
                ModelState.Remove("Page");

                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Validation failed.",
                        errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    });
                }

                var result = await _customerService.AddOrUpdateCustomerAsync(customer, Page);
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // -------------------- Delete --------------------

        public async Task<IActionResult> Delete(int? id, int Page = 1)
        {
            ViewBag.Page = Page;
            try
            {
                if (id == null) return NotFound();

                var customer = await _customerService.GetByIdAsync(id.Value);
                if (customer == null || customer.IsDeleted) return NotFound();

                return View(customer);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int page = 1)
        {
            try
            {
                await _customerService.DeleteAsync(id);
                TempData["Success"] = "Customer deleted successfully!";
                return RedirectToAction(nameof(Index), new { page = page });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        // -------------------- Export --------------------

        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var (customers, _) = await _customerService.GetAllPagedAsync(1, 100);
                var stream = await _excelExportService.ExportCustomersAsync(customers);

                return File(stream,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Customers_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        // -------------------- Validation --------------------

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsPhoneUnique(string phone, int id)
        {
            try
            {
                var isUnique = await _customerService.IsPhoneUniqueAsync(phone, id);
                return Json(isUnique);
            }
            catch
            {
                return Json(false);
            }
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsEmailUnique(string email, int id)
        {
            try
            {
                var isUnique = await _customerService.IsEmailUniqueAsync(email, id);
                return Json(isUnique);
            }
            catch
            {
                return Json(false);
            }
        }
    }
}
