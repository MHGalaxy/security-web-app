using Microsoft.AspNetCore.Mvc;

namespace Cyber_Security_App.Controllers
{
    public class ToolController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(string data)
        {
            // پردازش داده‌ها
            return View();
        }
    }
}
