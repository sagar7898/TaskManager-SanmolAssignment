using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    public interface IExcelExportService
    {
        Task<MemoryStream> ExportTasksAsync(IEnumerable<SanmolTaskManager_Models.TaskItem> tasks);
        Task<MemoryStream> ExportCustomersAsync(IEnumerable<SanmolTaskManager_Models.Customer> customers);
    }
}
