
using Entidades.Utils;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Servicios.Servicios
{
    public class InicializarWebApp
    {
        #region Methods
       

        public static async Task InicializarWeb(string id, Uri baseAddreess)
        {
            try
            {
                  //var sistema = await ObtenerHostSistema(id, baseAddreess);
                //WebApp.BaseAddress = sistema.AdstHost;
                //   WebApp.BaseAddress = Convert.ToString(baseAddreess);
                WebApp.BaseAddress = "http://localhost:49494";
            }
            catch (Exception ex)
            {

            }

        }

        public static async Task InicializarSeguridad(string id, Uri baseAddreess)
        {
            try
            {
                 ////var sistema = await ObtenerHostSistema(id, baseAddreess);
                ////WebApp.BaseAddressSeguridad = sistema.AdstHost;
                WebApp.BaseAddressSeguridad = "http://localhost:53317";
                // WebApp.BaseAddressSeguridad = Convert.ToString(baseAddreess);
            }
            catch (Exception ex)
            {

            }

        }

        public static async Task InicializarWebRecursosMateriales(string id, Uri baseAddreess)
        {
            try
            {
               //var sistema = await ObtenerHostSistema(id, baseAddreess);
                //WebApp.BaseAddressRM = sistema.AdstHost;
                WebApp.BaseAddressRM = "http://192.168.100.21/swRecursosMateriales";
            }
            catch (Exception ex)
            {

            }

        }        


        #endregion
    }

}