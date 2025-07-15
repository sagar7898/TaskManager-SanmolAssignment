using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_BLL.Services;
using SanmolTaskManager_Models;
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

        public async Task<IActionResult> Index(string search, int page = 1)
        {
            try
            {
                ViewBag.ShowSearchBox = true;
                ViewBag.CurrentPage = page;
                ViewBag.PageSize = PageSize;


                if (string.IsNullOrWhiteSpace(search))
                {
                    var (customers, totalCount) = await _customerService.GetAllPagedAsync(page, PageSize);
                    ViewBag.TotalCustomers = totalCount;
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / PageSize);

                    // Load task counts using existing method
                    var taskCounts = new Dictionary<int, int>();
                    foreach (var customer in customers)
                    {
                        var tasks = await _taskService.GetTasksByCustomerIdAsync(customer.Id);
                        taskCounts[customer.Id] = tasks.Count;
                    }

                    ViewBag.TaskCounts = taskCounts;

                    return View(customers);
                }
                else
                {
                    var results = await _searchService.SearchCustomersAsync(search);
                    ViewBag.TotalCustomers = results.Count();
                    ViewBag.TotalPages = 1;

                    var taskCounts = new Dictionary<int, int>();
                    foreach (var customer in results)
                    {
                        var tasks = await _taskService.GetTasksByCustomerIdAsync(customer.Id);
                        taskCounts[customer.Id] = tasks.Count;
                    }

                    ViewBag.TaskCounts = taskCounts;

                    return View(results);
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

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
        public async Task<IActionResult> AddOrEdit(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(customer);

                // Phone uniqueness check
                bool isPhoneUnique = await _customerService.IsPhoneUniqueAsync(customer.Phone, customer.Id);
                if (!isPhoneUnique)
                {
                    ModelState.AddModelError("Phone", "Phone number already exists.");
                    return View(customer);
                }

                // Email uniqueness check
                bool isEmailUnique = await _customerService.IsEmailUniqueAsync(customer.Email, customer.Id);
                if (!isEmailUnique)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(customer);
                }

                if (customer.Id == 0)
                {
                    await _customerService.AddAsync(customer);
                    TempData["Success"] = "Customer added successfully!";

                    // ✅ Redirect to last page
                    int totalCustomers = await _customerService.GetTotalCountAsync();
                    int lastPage = (int)Math.Ceiling((double)totalCustomers / PageSize);

                    return RedirectToAction(nameof(Index), new { page = lastPage });
                }
                else
                {
                    await _customerService.UpdateAsync(customer);
                    TempData["Success"] = "Customer updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }



        public async Task<IActionResult> Delete(int? id)
        {
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _customerService.DeleteAsync(id);
                TempData["Success"] = "Customer deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

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
