using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_Models
{
    public class DashboardStats
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
        public int Overdue { get; set; }
        public int Upcoming { get; set; }
    }
}
