using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VeterinariaCoreMVC.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VeterinariaCoreMVC.Controllers
{
    public class LoginController : Controller
    {
        public async Task<IActionResult> Login()
        {
            Users user = new Users();
            ViewBag.tipoDoc =
                new SelectList(await getTipoDoc(), "idtipodoc", "description");

            return View();
        }

        public async Task<List<TipoDoc>> getTipoDoc()
        {
            // permite realizar una solicitud al servicio web api
            using (var httpcliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta =
                    await httpcliente.GetAsync(
                        "http://localhost:5050/api/Login/GetTipoDoc");
                // convertimos el contenido de la variable respuesta a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<TipoDoc>>(respuestaAPI)!;
            }
        }
        public async Task<Users> logeo(int tipodoc, string ndoc, string pasw)
        {
            Users user = new Users();

            // permite realizar una solicitud al servicio web api
            using (var httpcliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta =
                    await httpcliente.GetAsync(
                        $"http://localhost:5050/api/Login/Login/{ndoc}/{pasw}/{tipodoc}");
                // convertimos el contenido de la variable respuesta a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                if (respuestaAPI == "false") { 
                    return user;
                }
                else
                {
                    return JsonConvert.DeserializeObject<Users>(respuestaAPI)!;
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(int TipoDoc, string ndoc, string pasw)
        {
            Users user = await logeo(TipoDoc, ndoc, pasw);

            if (user.NroDocumento != "")
            {
                ViewBag.ErrorMessage = "";
                return RedirectToAction("Index", "Home");
            }
            else
            {             
                ViewBag.tipoDoc =
                new SelectList(await getTipoDoc(), "idtipodoc", "description");
                ViewBag.ErrorMessage = "Intento de inicio de sesión inválido.";
                return View("Login");
            }
        }
        







    }
}
