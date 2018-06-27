using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entidades;
using Servicios.Interfaces;
using Entidades.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WebSeguros.Controllers.MVC
{
    public class LoginsController : Controller
    {
        private readonly IApiServicio apiServicio;
        public LoginsController(IApiServicio apiServicio)
        {
            this.apiServicio = apiServicio;
        }

        public async Task<IActionResult> Index()
        {

            var lista = new List<Login>();
            try
            {
                lista = await apiServicio.Listar<Login>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Logins/ListaLogin");
                return View(lista);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        public async Task<IActionResult> Login()
        {
            var ba = new Login();
            return View(ba);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            Response response = new Response();
            try
            {
                response = await apiServicio.ObtenerElementoAsync(login, new Uri(WebApp.BaseAddress),
                                                             "api/Logins/ObtenerUser");
                if (response.IsSuccess)
                {

                    return RedirectToAction("Index");
                }

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        // GET: Personas

    }
}
