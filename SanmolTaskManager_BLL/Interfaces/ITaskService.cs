using SanmolTaskManager_Models;
using SanmolTaskManager_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    public interface ITaskService
    {
        // ------------------- CRUD -------------------
        Task<TaskItem> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(int id);

        // ------------------- Paging & Filtering -------------------
        Task<(IEnumerable<TaskItem> Tasks, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize, string sortOrder = "desc");
        Task<(IEnumerable<TaskItem> Tasks, int TotalCount)> FindPagedAsync(string status, int page, int pageSize, string sortOrder = "desc");

        // ------------------- Dashboard -------------------
        Task<DashboardStats> GetDashboardStatsAsync();
        Task<List<TaskItem>> GetRecentTasksAsync(int count = 5);

        // ------------------- Customer Specific -------------------
        Task<List<TaskItem>> GetTasksByCustomerIdAsync(int customerId);

        // ------------------- ViewModel Support -------------------
        Task<TaskIndexViewModel> GetTaskIndexAsync(string search, string status, int page, int pageSize, string sortOrder);

        // ------------------- Add or Update (AJAX Handler) -------------------
        Task<object> AddOrUpdateTaskAsync(TaskItem task, int currentPage, int pageSize);
    }
}
