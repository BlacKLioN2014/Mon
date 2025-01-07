using Microsoft.AspNetCore.Mvc;
using Mon.Models;
using System.Diagnostics;
using System.Web;

namespace Mon.Controllers
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
            try
            {
                //AppUsuario User = 

                //if (User != null)
                //{
                    //ViewBag.User = User.UserName;
                    return View();
                //}
                //else
                //{
                //    return View("~/Views/Home/Index.cshtml");
                //}

            }
            catch (Exception x)
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public IActionResult Privacy()
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
