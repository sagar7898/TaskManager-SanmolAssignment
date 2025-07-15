using SanmolTaskManager_Models;
using System.Threading.Tasks;

namespace SanmolTaskManager_BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailReceiver receiver);
    }
}
