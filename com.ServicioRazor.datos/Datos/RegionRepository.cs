using com.ServicioRazor.datos.Interfaz;
using com.ServicioRazor.modelos;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace com.ServicioRazor.datos.Datos
{
    
    public class RegionRepository : IRegionRepository
    {
        private string _conn;
        public RegionRepository()
        {
            _conn = Conection.GetConection();
        }
        public async Task<Regiones> Get(int id)
        {
            try 
            {
                Regiones retorno = new Regiones();
                using (SqlConnection client = new SqlConnection(_conn))
                {
                    client.Open();
                    using (SqlCommand cmd = new SqlCommand("GetRegion", client))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                  retorno = new Regiones() 
                                  { 
                                      IdRegion = (Int32)reader[0] ,
                                      Region = reader[1].ToString()
                                  };
                            }
                        }
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al Obtener la region: " + ex.Message);
                return new Regiones();
            }
        }

        public async Task<IEnumerable<Regiones>> GetAll()
        {
            try
            {
                List<Regiones> Retorno =  new List<Regiones>();
                using (SqlConnection client = new SqlConnection(_conn))
                {
                    client.Open();
                    using (SqlCommand cmd = new SqlCommand("GETREGIONES", client))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Retorno.Add(new Regiones()
                                {
                                    IdRegion = (Int32)reader[0],
                                    Region = reader[1].ToString()
                                });
                            }
                        }
                    }
                }
                if (Retorno.Count > 0)
                    return Retorno;
                else
                    return new List<Regiones>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al Obtener las regiones: " + ex.Message);
                return new List<Regiones>();
            }
        }

        public async Task<IEnumerable<Comunas>> GetAllComunas(int id)
        {
            try
            {
                List<Comunas> retorno = new List<Comunas>();
                using (SqlConnection client = new SqlConnection(_conn))
                {
                    client.Open();
                    using (SqlCommand cmd = new SqlCommand("GETCOMUNAS", client))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retorno.Add(new Comunas()
                                {
                                    IdComuna = (Int32)reader[0],
                                    IdRegion = (Int32)reader[1],
                                    Comuna = reader[2].ToString(),
                                    xml = reader[3].ToString()
                                });
                            }
                        }
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al Obtener las comunas: " + ex.Message);
                return new List<Comunas>();
            }
        }

        public async Task<Comunas> GetComuna(int region, int comuna)
        {
            try
            {
                Comunas retorno = new Comunas();
                using (SqlConnection client = new SqlConnection(_conn))
                {
                    client.Open();
                    using (SqlCommand cmd = new SqlCommand("GETCOMUNA", client))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@idRegion",region));
                        cmd.Parameters.Add(new SqlParameter("@IdComuna", comuna));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retorno = new Comunas()
                                {
                                  IdComuna = (Int32)reader[0],
                                  IdRegion = (Int32)reader[1],
                                  Comuna = reader[2].ToString(),
                                  xml = reader[3].ToString()
                                };
                            }
                        }
                    }
                }
                return retorno;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al Obtener comuna: "+ ex.Message);
                return new Comunas();
            }
        }

        public async Task<bool> Post(int IdRegion, Comunas comuna)
        {
            try
            {
                Comunas retorno = new Comunas();
                using (SqlConnection client = new SqlConnection(_conn))
                {
                    client.Open();
                    using (SqlCommand cmd = new SqlCommand("MERGCOMUNA", client))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@idRegion", IdRegion));
                        cmd.Parameters.Add(new SqlParameter("@IdComuna", comuna.IdComuna));
                        cmd.Parameters.Add(new SqlParameter("@Comuna", comuna.Comuna));
                        cmd.Parameters.Add(new SqlParameter("@info", comuna.xml));
                        var dato = cmd.ExecuteNonQuery();
                        if (dato == 0) return false;
                        else return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar comuna: " + ex.Message);
                return false;
            }
        }
    }
}
