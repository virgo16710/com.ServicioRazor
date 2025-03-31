using com.ServicioRazor.mvc.Models;
using com.ServicioRazor.mvc.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace com.ServicioRazor.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRegionesRepository _regionesRepository;
        public HomeController(ILogger<HomeController> logger, HttpClient client)
        {
            _logger = logger;
           _regionesRepository = new RegionesRepository(client);
        }
        /// <summary>
        /// Trae todas las regiones
        /// </summary>
        /// <returns></returns>
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            return View(await _regionesRepository.GetRegiones());
        }
        /// <summary>
        /// Esto despliego un Modal para vero los datos de la region
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/GetRegion")]
        public async Task<JsonResult> GetRegion(int id)
        {
              return new JsonResult(await _regionesRepository.GetRegion(id));
        }
        /// <summary>
        /// Trae las comunas de dicha region
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Comunas")]
        public async Task<IActionResult> Comunas(int id)
        {
            return View(await _regionesRepository.GetComunas(id));
        }
        [HttpGet("/GetComuna")]
        public async Task<JsonResult> GetComuna(int IdRegion, int IdComuna)
        {
            return new JsonResult(await _regionesRepository.GetComuna(IdRegion, IdComuna));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Post(PresentComuna c)
        {
            if (await _regionesRepository.MergeComuna(c.Ocomuna))
                return RedirectToAction("Comunas","Home",new { id = c.Ocomuna.IdRegion });
            else
                return RedirectToAction("/Comunas");
        }
    }
}
