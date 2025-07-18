using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_Models.ViewModels
{
    public class CustomerIndexViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; }
        public int TotalCustomers { get; set; }
        public Dictionary<int, int> TaskCounts { get; set; } = new(); // 👈 For # of Tasks per customer
    }
}
