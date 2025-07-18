using SanmolTaskManager_Models;
using SanmolTaskManager_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    public interface ICustomerService
    {
        // --------------------- GET ---------------------
        Task<(IEnumerable<Customer> Customers, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<Customer> GetByIdAsync(int id);
        Task<CustomerIndexViewModel> GetCustomerIndexAsync(string search, int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();

        // --------------------- ADD / UPDATE / DELETE ---------------------
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<object> AddOrUpdateCustomerAsync(Customer customer, int currentPage);

        // --------------------- VALIDATION ---------------------
        Task<bool> IsPhoneUniqueAsync(string phone, int id);
        Task<bool> IsEmailUniqueAsync(string email, int id);
    }
}
