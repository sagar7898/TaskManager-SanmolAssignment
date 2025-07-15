using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    // IEmailReminderJob.cs
    public interface IEmailReminderJob
    {
        Task SendDailyOverdueSummary();
    }

}
