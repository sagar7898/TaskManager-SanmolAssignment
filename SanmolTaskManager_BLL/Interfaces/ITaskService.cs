using SanmolTaskManager_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    public interface ITaskService
    {
        Task<(IEnumerable<TaskItem> Tasks, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize, string sortOrder = "desc");
        Task<(IEnumerable<TaskItem> Tasks, int TotalCount)> FindPagedAsync(string status, int page, int pageSize, string sortOrder = "desc");

        Task<TaskItem> GetByIdAsync(int id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(int id);

        Task<DashboardStats> GetDashboardStatsAsync();
        Task<List<TaskItem>> GetRecentTasksAsync(int count = 5);
        Task<List<TaskItem>> GetTasksByCustomerIdAsync(int customerId);
    }
}
