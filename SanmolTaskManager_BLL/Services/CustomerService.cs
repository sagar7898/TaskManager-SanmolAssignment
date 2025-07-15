using Microsoft.EntityFrameworkCore;
using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_DAL.Repositories;
using SanmolTaskManager_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepo;



        public CustomerService(IGenericRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

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

        public async Task<int> GetTotalCountAsync()
        {
            try
            {
                return await _customerRepo.Query().CountAsync(c => !c.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while counting customers", ex);
            }
        }

        public async Task<bool> IsPhoneUniqueAsync(string phone, int id)
        {
            try
            {
                return !await _customerRepo.Query()
                    .AnyAsync(c => c.Phone == phone && c.Id != id && !c.IsDeleted);
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
                    .AnyAsync(c => c.Email == email && c.Id != id && !c.IsDeleted);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while checking email uniqueness", ex);
            }
        }

        
    }
}
