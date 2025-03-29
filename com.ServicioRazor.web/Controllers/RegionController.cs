/*
 * Si leen esto, les deseo un buen día.
 * atte: El programador
 */

using com.ServicioRazor.api;
using com.ServicioRazor.modelos;
using Microsoft.AspNetCore.Mvc;

namespace com.ServicioRazor.Web.Controllers
{
    [Route("api/Valuetech")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private string _conection;
        private readonly IConfiguration _configuration;
        private RegionesAPI _regiones;
        public RegionController(IConfiguration configuration)
        {
            _conection = configuration.GetConnectionString("DefaultConnection");
            _regiones = new RegionesAPI(_conection);

        }
        /// <summary>
        /// Listado de Regiones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Region")]
        public async Task<JsonResult> Get()
        {
            return new JsonResult(await _regiones.GetRegiones());
        }

        [HttpGet]
        [Route("/Region/{IdRegion}")]
        public async Task<JsonResult>GetId(int IdRegion)
        {
            return new JsonResult(await _regiones.GetRegion(IdRegion));
        }
        [HttpGet]
        [Route("/Region/{IdRegion}/Comunas")]
        public async Task<JsonResult> GetComunasIdRegion(int IdRegion) 
        {
            return new JsonResult(await _regiones.GetComunas(IdRegion));
        }
        [HttpGet]
        [Route("/Region/{IdRegion}/Comuna/{IdComuna}")]
        public async Task<JsonResult> GetComuna(int IdRegion, int IdComuna)
        {
            return new JsonResult(await _regiones.GetComuna(IdRegion, IdComuna));
        }
        [HttpPost]
        [Route("/Region/{IdRegion}/Comuna")]
        public async Task<JsonResult>PostComuna(int IdRegion, Comunas data)
        {
            var value =  await _regiones.PostComunas(IdRegion, data);
            if (value)
                return new JsonResult("OK");
            else
                return new JsonResult("No Actualizo");
        }
    }
}
