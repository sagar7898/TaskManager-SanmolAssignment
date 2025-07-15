using SanmolTaskManager_BLL.Interfaces;
using SanmolTaskManager_Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SanmolTaskManager_BLL.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public ExcelExportService()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Sagar Nachankar");
        }

        public async Task<MemoryStream> ExportTasksAsync(IEnumerable<TaskItem> tasks)
        {
            try
            {
                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Tasks");

                ws.Cells[1, 1].Value = "Title";
                ws.Cells[1, 2].Value = "Description";
                ws.Cells[1, 3].Value = "Customer";
                ws.Cells[1, 4].Value = "Due Date";
                ws.Cells[1, 5].Value = "Status";
                ws.Cells[1, 6].Value = "Priority";

                int row = 2;
                foreach (var task in tasks)
                {
                    ws.Cells[row, 1].Value = task.Title;
                    ws.Cells[row, 2].Value = task.Description;
                    ws.Cells[row, 3].Value = task.Customer?.Name;
                    ws.Cells[row, 4].Value = task.DueDate.ToString("yyyy-MM-dd");
                    ws.Cells[row, 5].Value = task.Status;
                    ws.Cells[row, 6].Value = task.Priority;
                    row++;
                }

                ws.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exporting task data to Excel", ex);
            }
        }

        public async Task<MemoryStream> ExportCustomersAsync(IEnumerable<Customer> customers)
        {
            try
            {
                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Customers");

                ws.Cells[1, 1].Value = "Name";
                ws.Cells[1, 2].Value = "Phone";
                ws.Cells[1, 3].Value = "Email";

                int row = 2;
                foreach (var customer in customers)
                {
                    ws.Cells[row, 1].Value = customer.Name;
                    ws.Cells[row, 2].Value = customer.Phone;
                    ws.Cells[row, 3].Value = customer.Email;
                    row++;
                }

                ws.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception("Error exporting customer data to Excel", ex);
            }
        }
    }
}
