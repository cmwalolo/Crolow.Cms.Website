using Crolows.Cms.Generic.WebDesigns.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Crolows.Cms.Generic.WebDesigns.Controllers
{
    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;

        public PagesController(ILogger<PagesController> logger)
        {
            _logger = logger;
        }


        [Route("Pages")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Pages/{*page}")]
        public IActionResult OtherPages(string page)
        {
            return View(page);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
