using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tracking.Models;

namespace Tracking.Controllers
{
    /// <summary>
    /// Controller for handling home-related actions.
    /// </summary>
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Displays the home page.
        /// </summary>

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Displays the privacy page.
        /// </summary>

        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Displays the error page.
        /// </summary>

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
