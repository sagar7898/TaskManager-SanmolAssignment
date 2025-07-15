using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_DAL.Repositories;
using SanmolTaskManager_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SanmolTaskManager_BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IGenericRepository<Customer> _customerRepo;
        private readonly IGenericRepository<TaskItem> _taskRepo;

        public SearchService(IGenericRepository<Customer> customerRepo, IGenericRepository<TaskItem> taskRepo)
        {
            _customerRepo = customerRepo;
            _taskRepo = taskRepo;
        }

        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string keyword)
        {
            try
            {
                return await _customerRepo.Query()
                    .Where(c => !c.IsDeleted &&
                                (c.Name.Contains(keyword) ||
                                 c.Phone.Contains(keyword) ||
                                 c.Email.Contains(keyword)))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while searching customers", ex);
            }
        }

        public async Task<IEnumerable<TaskItem>> SearchTasksAsync(string keyword)
        {
            try
            {
                return await _taskRepo.Query()
                    .Where(t => t.Description.Contains(keyword) ||
                                t.Customer.Name.Contains(keyword))
                    .Include(t => t.Customer)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while searching tasks", ex);
            }
        }
    }
}
