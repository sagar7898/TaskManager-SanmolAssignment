using SanmolTaskManager_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    public interface ISearchService
    {
        Task<IEnumerable<Customer>> SearchCustomersAsync(string keyword);
        Task<IEnumerable<TaskItem>> SearchTasksAsync(string keyword);
    }
}
