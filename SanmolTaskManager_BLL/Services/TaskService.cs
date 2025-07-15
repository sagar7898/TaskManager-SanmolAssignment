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
    public class TaskService : ITaskService
    {
        private readonly IGenericRepository<TaskItem> _taskRepo;

        public TaskService(IGenericRepository<TaskItem> taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task<(IEnumerable<TaskItem> Tasks, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize, string sortOrder = "desc")
        {
            try
            {
                var query = _taskRepo.Query()
                    .Include(t => t.Customer)
                    .Where(t => !t.IsDeleted && t.Customer != null && !t.Customer.IsDeleted);

                var totalCount = await query.CountAsync();

                query = sortOrder == "asc"
                    ? query.OrderBy(t => t.DueDate)
                    : query.OrderByDescending(t => t.DueDate);

                var tasks = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (tasks, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetAllPagedAsync", ex);
            }
        }

        public async Task<(IEnumerable<TaskItem> Tasks, int TotalCount)> FindPagedAsync(string status, int page, int pageSize, string sortOrder = "desc")
        {
            try
            {
                var query = _taskRepo.Query()
                    .Include(t => t.Customer)
                    .Where(t => !t.IsDeleted && t.Customer != null && !t.Customer.IsDeleted);

                if (status != "All")
                    query = query.Where(t => t.Status == status);

                var totalCount = await query.CountAsync();

                query = sortOrder == "asc"
                    ? query.OrderBy(t => t.DueDate)
                    : query.OrderByDescending(t => t.DueDate);

                var tasks = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return (tasks, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in FindPagedAsync", ex);
            }
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            try
            {
                return await _taskRepo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetByIdAsync", ex);
            }
        }

        public async Task AddAsync(TaskItem task)
        {
            try
            {
                task.CreatedAt = DateTime.Now;
                task.UpdatedAt = DateTime.Now;
                await _taskRepo.AddAsync(task);
                await _taskRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddAsync", ex);
            }
        }

        public async Task UpdateAsync(TaskItem task)
        {
            try
            {
                task.UpdatedAt = DateTime.Now;
                _taskRepo.Update(task);
                await _taskRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateAsync", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var task = await _taskRepo.GetByIdAsync(id);
                if (task != null)
                {
                    task.IsDeleted = true;
                    _taskRepo.Update(task);
                    await _taskRepo.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteAsync", ex);
            }
        }

        public async Task<DashboardStats> GetDashboardStatsAsync()
        {
            try
            {
                var tasks = await _taskRepo.Query()
                    .Where(t => !t.IsDeleted && t.Customer != null && !t.Customer.IsDeleted)
                    .ToListAsync();

                var today = DateTime.Today;

                var stats = new DashboardStats
                {
                    Total = tasks.Count,
                    Pending = tasks.Count(t => t.Status == "Pending"),
                    Completed = tasks.Count(t => t.Status == "Completed"),
                    Overdue = tasks.Count(t => t.Status == "Pending" && t.DueDate.Date < today),
                    Upcoming = tasks.Count(t =>
                        t.Status == "Pending" &&
                        t.DueDate.Date >= today &&
                        t.DueDate.Date <= today.AddDays(3))
                };

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetDashboardStatsAsync", ex);
            }
        }

        public async Task<List<TaskItem>> GetRecentTasksAsync(int count = 5)
        {
            try
            {
                return await _taskRepo.Query()
                    .Include(t => t.Customer)
                    .Where(t => !t.IsDeleted)
                    .OrderByDescending(t => t.UpdatedAt)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetRecentTasksAsync", ex);
            }
        }

        public async Task<List<TaskItem>> GetTasksByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _taskRepo.Query()
                    .Include(t => t.Customer)
                    .Where(t => !t.IsDeleted && t.CustomerId == customerId && !t.Customer.IsDeleted)
                    .OrderByDescending(t => t.DueDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetTasksByCustomerIdAsync", ex);
            }
        }
    }
}
