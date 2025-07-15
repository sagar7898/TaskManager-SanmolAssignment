using SanmolTaskManager_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<(IEnumerable<Customer> Customers, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<Customer> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<int> GetTotalCountAsync();
        Task<bool> IsPhoneUniqueAsync(string phone, int id);
        Task<bool> IsEmailUniqueAsync(string email, int id);

    }
}
