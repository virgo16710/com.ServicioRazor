using com.ServicioRazor.modelos;

namespace com.ServicioRazor.datos.Interfaz
{
    public interface IRegionRepository
    {
        /// <summary>
        /// Todas las regiones
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Regiones>> GetAll();
        /// <summary>
        /// Trae una region
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Regiones> Get(int id);
        /// <summary>
        /// Todas las comunas de una region
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Comunas>> GetAllComunas(int id);
        /// <summary>
        /// Trae la comuna de una region
        /// </summary>
        /// <param name="region"></param>
        /// <param name="comuna"></param>
        /// <returns></returns>
        Task<Comunas> GetComuna(int region, int comuna);
        /// <summary>
        /// Actualiza la comuna
        /// </summary>
        /// <param name="comuna"></param>
        Task<bool> Post(int IdRegion,Comunas comuna);

    }
}
