using Cyber_Security_App.Models;
using Cyber_Security_App.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cyber_Security_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string input = "Your input string here";
            string hashedInput = HashUtility.ComputeSha256Hash(input);

            return View();
        }

        public IActionResult HashData()
        {
            return View();
        }

        [HttpPost]
        public string HashData(string input) 
        { 
            return HashUtility.ComputeSha256Hash(input);
        }


        public IActionResult HtmlEncode()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
