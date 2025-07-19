using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_Models.ViewModels
{
    public class TaskIndexViewModel
    {
        public IEnumerable<TaskItem> Tasks { get; set; }

        public string Search { get; set; }
        public string Status { get; set; }
        public string SortOrder { get; set; }

        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool ShowSearchBox => true;
        //public int TotalPages { get; set; }

        public string StatusFilter { get; set; }

        public int TotalFilteredCount { get; set; }
    }

}
