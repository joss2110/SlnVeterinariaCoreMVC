using VeterinariaCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace VeterinariaCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        Users userActual = new Users();

        Users? RecuperarUsuario()
        {
            var userJson = HttpContext.Session.GetString("usuarioActual");

            if (!string.IsNullOrEmpty(userJson))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Users>(userJson);
                }
                catch
                {
                    HttpContext.Session.Remove("usuarioActual");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public IActionResult Index()
        {
            var userActual = RecuperarUsuario();

            if (userActual == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.userActual = userActual.Nombres;

            return View();
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
