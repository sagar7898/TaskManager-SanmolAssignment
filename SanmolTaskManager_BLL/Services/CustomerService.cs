using Microsoft.EntityFrameworkCore;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_DAL.Repositories;
using SanmolTaskManager_Models;
using SanmolTaskManager_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepo;
        private readonly ISearchService _searchService;
        private readonly ITaskService _taskService;
        private const int PageSize = 10;

        public CustomerService(IGenericRepository<Customer> customerRepo, ISearchService searchService, ITaskService taskService)
        {
            _customerRepo = customerRepo;
            _searchService = searchService;
            _taskService = taskService;
        }

        // ------------------- Basic CRUD -------------------

        public async Task<Customer> GetByIdAsync(int id)
        {
            try
            {
                return await _customerRepo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching customer by ID", ex);
            }
        }

        public async Task AddAsync(Customer customer)
        {
            try
            {
                await _customerRepo.AddAsync(customer);
                await _customerRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding customer", ex);
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                _customerRepo.Update(customer);
                await _customerRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating customer", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var customer = await _customerRepo.GetByIdAsync(id);
                if (customer != null)
                {
                    customer.IsDeleted = true;
                    _customerRepo.Update(customer);
                    await _customerRepo.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting customer", ex);
            }
        }

        // ------------------- Validation -------------------

        public async Task<bool> IsPhoneUniqueAsync(string phone, int id)
        {
            try
            {
                return !await _customerRepo.Query()
                    .AnyAsync(c => c.Phone == phone && c.Id != id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while checking phone uniqueness", ex);
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email, int id)
        {
            try
            {
                return !await _customerRepo.Query()
                    .AnyAsync(c => c.Email == email && c.Id != id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while checking email uniqueness", ex);
            }
        }

        // ------------------- Paging & ViewModel -------------------

        public async Task<(IEnumerable<Customer> Customers, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                return await _customerRepo.GetPagedAsync(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while fetching paged customers", ex);
            }
        }

        public async Task<CustomerIndexViewModel> GetCustomerIndexAsync(string search, int pageNumber, int pageSize)
        {
            IEnumerable<Customer> customers;
            int totalCount;

            if (string.IsNullOrWhiteSpace(search))
            {
                (customers, totalCount) = await _customerRepo.GetPagedAsync(pageNumber, pageSize);
            }
            else
            {
                customers = await _searchService.SearchCustomersAsync(search);
                totalCount = customers.Count();
            }

            var taskCounts = new Dictionary<int, int>();
            foreach (var customer in customers)
            {
                var tasks = await _taskService.GetTasksByCustomerIdAsync(customer.Id);
                taskCounts[customer.Id] = tasks.Count;
            }

            return new CustomerIndexViewModel
            {
                Customers = customers,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                PageSize = pageSize,
                SearchTerm = search,
                TotalCustomers = totalCount,
                TaskCounts = taskCounts
            };
        }

        public async Task<int> GetTotalCountAsync()
        {
            try
            {
                return await _customerRepo.Query().CountAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while counting customers", ex);
            }
        }

        // ------------------- Add or Update (AJAX Handler) -------------------

        public async Task<object> AddOrUpdateCustomerAsync(Customer customer, int currentPage)
        {
            if (!await IsPhoneUniqueAsync(customer.Phone, customer.Id))
                return new { success = false, message = "Phone number already exists." };

            if (!await IsEmailUniqueAsync(customer.Email, customer.Id))
                return new { success = false, message = "Email already exists." };

            if (customer.Id == 0)
            {
                await AddAsync(customer);
                int totalCustomers = await GetTotalCountAsync();
                int lastPage = (int)Math.Ceiling((double)totalCustomers / PageSize);

                return new
                {
                    success = true,
                    message = "Customer added successfully!",
                    redirectUrl = $"/Customer/Index?page={lastPage}"
                };
            }
            else
            {
                await UpdateAsync(customer);

                return new
                {
                    success = true,
                    message = "Customer updated successfully!",
                    redirectUrl = $"/Customer/Index?page={currentPage}"
                };
            }
        }
    }
}
