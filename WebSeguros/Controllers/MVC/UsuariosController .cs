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
    public class UsuariosController : Controller
    {
        private readonly IApiServicio apiServicio;
        public UsuariosController(IApiServicio apiServicio)
        {
            this.apiServicio = apiServicio;
        }
        private void InicializarMensaje(string mensaje)
        {
            if (mensaje == null)
            {
                mensaje = "";
            }
            ViewData["Error"] = mensaje;
        }

        public async Task<IActionResult> Index(string mensaje)
        {

            var lista = new List<Usuarios>();
            try
            {
                lista = await apiServicio.Listar<Usuarios>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Usuarios/ListarUsuarios");
                InicializarMensaje(mensaje);
                return View(lista);
            }
            catch (Exception ex)
            {                
                return BadRequest();
            }
        }
        public async Task<IActionResult> Create()
        {
            ViewData["IdPerfil"] = new SelectList(await apiServicio.Listar<Perfil>(new Uri(WebApp.BaseAddress), "api/Perfils/ListarPerfiles"), "IdPerfil", "Descripcion");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Usuarios usuarios)
        {
            ViewData["IdPerfil"] = new SelectList(await apiServicio.Listar<Perfil>(new Uri(WebApp.BaseAddress), "api/Perfils/ListarPerfiles"), "IdPerfil", "Descripcion");
            Response response = new Response();
            try
            {
                response = await apiServicio.InsertarAsync(usuarios,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Usuarios/InsertarUsuaruo");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                ViewData["IdPerfil"] = new SelectList(await apiServicio.Listar<Perfil>(new Uri(WebApp.BaseAddress), "api/Perfils/ListarPerfiles"), "IdPerfil", "Descripcion");
                return View(usuarios);

            }
            catch (Exception ex)
            {

                return BadRequest();
            }
            
        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var respuesta = await apiServicio.SeleccionarAsync<Response>(id, new Uri(WebApp.BaseAddress),
                                                                  "api/Usuarios");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaPersona = JsonConvert.DeserializeObject<Usuarios>(respuesta.Resultado.ToString());
                        var persona = new Usuarios
                        {
                            IdUsuario= respuestaPersona.IdUsuario,
                            Identificacion = respuestaPersona.Identificacion,
                            Nombres = respuestaPersona.Nombres,
                            Apellido = respuestaPersona.Apellido
                        };

                        ViewData["IdPerfil"] = new SelectList(await apiServicio.Listar<Perfil>(new Uri(WebApp.BaseAddress), "api/Perfils/ListarPerfiles"), "IdPerfil", "Descripcion");

                        return View(persona);
                    }

                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Usuarios usuarios)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicio.EditarAsync(id, usuarios, new Uri(WebApp.BaseAddress),
                                                                 "api/Usuarios");
                    if (response.IsSuccess)
                    {                       

                        return RedirectToAction("Index");
                    }
                    ViewData["IdPerfil"] = new SelectList(await apiServicio.Listar<Perfil>(new Uri(WebApp.BaseAddress), "api/Perfils/ListarPerfiles"), "IdPerfil", "Descripcion");
                    return View(usuarios);

                }
                return BadRequest();
            }
            catch (Exception ex)
            {
               
                return BadRequest();
            }
        }
        public async Task<IActionResult> Delete(string id)
        {

            try
            {
                var response = await apiServicio.EliminarAsync(id, new Uri(WebApp.BaseAddress)
                                                               , "api/Usuarios");
                if (response.IsSuccess)
                {

                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index", new { mensaje = "Existe un relacion" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", new { mensaje = "Existe un relacion" });
            }
        }

        // GET: Personas

    }
}
