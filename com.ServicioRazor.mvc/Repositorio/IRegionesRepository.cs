using com.ServicioRazor.mvc.Models;

namespace com.ServicioRazor.mvc.Repositorio
{
    //Este Interfaz se va a encargar de hacer las consultas a la API
    public interface IRegionesRepository
    {
        Task<IEnumerable<Regiones>> GetRegiones();
        Task<Regiones> GetRegion(int IdRegion); 
        Task<PresentComuna> GetComunas(int IdRegion);
        Task<PresentComuna.FormComuna> GetComuna(int IdRegion, int IdComuna);
        Task<bool> MergeComuna(PresentComuna.FormComuna comuna);
    }
}
