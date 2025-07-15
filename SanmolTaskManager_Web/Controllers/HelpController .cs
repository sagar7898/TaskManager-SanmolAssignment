using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SanmolTaskManager_Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class HelpController : Controller
    {
        public IActionResult HelpIndex()
        {
            return View();
        }
    }
}
