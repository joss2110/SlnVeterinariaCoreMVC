using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VeterinariaCoreMVC.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace VeterinariaCoreMVC.Controllers
{
    public class LoginController : Controller
    {
        

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

        Users userActual = new Users();
        void GrabarUsuario()
        {
            HttpContext.Session.SetString("usuarioActual",
                    JsonConvert.SerializeObject(userActual));
        }

        public async Task<IActionResult> Login()
        {
            userActual = null;
            GrabarUsuario();
            ViewBag.tipoDoc =
                new SelectList(await getTipoDoc(), "idtipodoc", "description");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(int TipoDoc, string ndoc, string pasw)
        {
            List<TipoDoc> listTipoDoc = await getTipoDoc();


            TipoDoc tdoc = listTipoDoc.FirstOrDefault(t => t.idtipodoc == TipoDoc);

            if (tdoc.tdigits == "Alfanumerico")
            {
                if (!Regex.IsMatch(ndoc, $@"^[a-zA-Z0-9]{{{tdoc.ndigits}}}$"))
                {
                    ViewBag.ErrorMessage = "Debe completar correctamente sus credenciales.";
                    return await RetornarVistaLogin();
                }
            }
            else
            {
                if (!Regex.IsMatch(ndoc, $@"^\d{{{tdoc.ndigits}}}$"))
                {
                    ViewBag.ErrorMessage = "Debe completar correctamente sus credenciales.";
                    return await RetornarVistaLogin();
                }
            }

            var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");
            if (!passwordRegex.IsMatch(pasw))
            {
                ViewBag.ErrorMessage = "Debe completar correctamente sus credenciales.";
                return await RetornarVistaLogin();
            }

            Users user = await logeo(TipoDoc, ndoc, pasw);

            if (!string.IsNullOrEmpty(user.NroDocumento))
            {
                ViewBag.ErrorMessage = "";
                userActual = user;
                GrabarUsuario();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Intento de inicio de sesión inválido.";
                return await RetornarVistaLogin();
            }
        }

        // Método auxiliar para retornar la vista del Login con la lista de tipos de documento
        private async Task<IActionResult> RetornarVistaLogin()
        {
            ViewBag.tipoDoc = new SelectList(await getTipoDoc(), "idtipodoc", "description");
            return View("Login");
        }









    }
}
