using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_Models
{
    public class EmailReceiver
    {
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }

        public List<EmailTaskItem> Tasks { get; set; } = new();
    }

    public class EmailTaskItem
    {
        public string Title { get; set; }
        public string CustomerName { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }

}
