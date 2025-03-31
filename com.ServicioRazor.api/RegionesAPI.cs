using com.ServicioRazor.datos.Datos;
using com.ServicioRazor.datos.Interfaz;

namespace com.ServicioRazor.api
{
    
    public class RegionesAPI
    {
        private IRegionRepository _regionRepository;
        public RegionesAPI()
        {
            _regionRepository = new RegionRepository();
        }
        
        public async Task<IEnumerable<com.ServicioRazor.modelos.Regiones>> GetRegiones() 
        {
             return await _regionRepository.GetAll();
        }
        public async Task<com.ServicioRazor.modelos.Regiones> GetRegion(int id)
        {
            return await _regionRepository.Get(id);
        }
        public async Task<IEnumerable<com.ServicioRazor.modelos.Comunas>> GetComunas(int id)
        {
            return await _regionRepository.GetAllComunas(id);
        }
        public async Task<com.ServicioRazor.modelos.Comunas> GetComuna(int IdRegion, int IdComuna)
        {
            return await _regionRepository.GetComuna(IdRegion, IdComuna);
        }
        public async Task<bool> PostComunas(int IdRegion, com.ServicioRazor.modelos.Comunas comunas)
        {
            return await _regionRepository.Post(IdRegion, comunas);
        }
    }
}
