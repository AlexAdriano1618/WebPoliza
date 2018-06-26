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
    public class PersonasController : Controller
    {
        private readonly IApiServicio apiServicio;
        public PersonasController(IApiServicio apiServicio)
        {
            this.apiServicio = apiServicio;
        }

        public async Task<IActionResult> Index()
        {

            var lista = new List<Persona>();
            try
            {
                lista = await apiServicio.Listar<Persona>(new Uri(WebApp.BaseAddress)
                                                                    , "api/Personas/ListarPersonas");
                return View(lista);
            }
            catch (Exception ex)
            {                
                return BadRequest();
            }
        }
        public async Task<IActionResult> Create()
        {
            ViewData["IdGenero"] = new SelectList(await apiServicio.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGeneros"), "IdGenero", "Descripcion");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Persona persona)
        {
            ViewData["IdGenero"] = new SelectList(await apiServicio.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGeneros"), "IdGenero", "Descripcion");
            Response response = new Response();
            try
            {
                response = await apiServicio.InsertarAsync(persona,
                                                             new Uri(WebApp.BaseAddress),
                                                             "api/Personas/InsertarPersona");
                if (response.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                ViewData["IdGenero"] = new SelectList(await apiServicio.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGeneros"), "IdGenero", "Descripcion");
                return View(persona);

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
                                                                  "api/Personas");
                    if (respuesta.IsSuccess)
                    {

                        var respuestaPersona = JsonConvert.DeserializeObject<Persona>(respuesta.Resultado.ToString());
                        var persona = new Persona
                        {
                            IdPersona= respuestaPersona.IdPersona,
                            Identificacion = respuestaPersona.Identificacion,
                            Nombre = respuestaPersona.Nombre,
                            Apellido = respuestaPersona.Apellido
                        };

                        ViewData["IdGenero"] = new SelectList(await apiServicio.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGeneros"), "IdGenero", "Descripcion");
                        
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
        public async Task<IActionResult> Edit(string id, Persona persona)
        {
            Response response = new Response();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    response = await apiServicio.EditarAsync(id, persona, new Uri(WebApp.BaseAddress),
                                                                 "api/Personas");
                    if (response.IsSuccess)
                    {                       

                        return RedirectToAction("Index");
                    }
                    ViewData["IdGenero"] = new SelectList(await apiServicio.Listar<Genero>(new Uri(WebApp.BaseAddress), "api/Generoes/ListarGeneros"), "IdGenero", "Descripcion");
                    return View(persona);

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
                                                               , "api/Personas");
                if (response.IsSuccess)
                {
                    
                    return RedirectToAction("Index");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                

                return BadRequest();
            }
        }

        // GET: Personas

    }
}
