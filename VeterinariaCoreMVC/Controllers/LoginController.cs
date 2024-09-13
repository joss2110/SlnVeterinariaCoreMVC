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
        //private readonly flowersshoesContext db;

        ////public LoginController(flowersshoesContext ctx)
        ////{
        //    db = ctx;
        //}

        public async Task<IActionResult> Login()
        {
            ViewBag.tipoDoc =
                new SelectList(await getTipoDoc(), "idtipodoc", "description");
            //trabajadorActual = null;

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






    }
}
