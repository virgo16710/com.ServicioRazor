using com.ServicioRazor.mvc.Models;
using Newtonsoft.Json;
using System.Text;
using System.Xml;

namespace com.ServicioRazor.mvc.Repositorio
{
    public class RegionesRepository : IRegionesRepository
    {
        public RegionesRepository()
        {
               /*
                * Para esta tarea solo utilizare el httpclient
                * Utilizando solo los recursos creados en mi API como se solicito
                */
        }

        public async Task<PresentComuna> GetComunas(int IdRegion)
        {
            try
            {
                string url = "http://localhost:44673/Region/" + IdRegion+"/Comunas";
                PresentComuna comunas = new PresentComuna();
                comunas.Comunas = new List<PresentComuna.FormComuna>();
                comunas.Ocomuna = new PresentComuna.FormComuna();
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonresponse = await response.Content.ReadAsStringAsync();
                        var dato = JsonConvert.DeserializeObject<List<Comunas>>(jsonresponse);
                        foreach(var item in dato)
                        {
                            comunas.Comunas.Add(ProcesarComuna(item));
                        }
                        comunas.Regiones = await GetRegiones();
                    }
                    else
                    {
                        Console.WriteLine($"Error en la consulta: {response.StatusCode}");
                        return null;
                    }
                    return comunas;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        private PresentComuna.FormComuna ProcesarComuna(Comunas item)
        {
            /*
             * En esta parte del proyecto no se si la intencion era que en la web reflejara los datos adiciones, pero en cualquier caso lo hice
             */
            var e = new PresentComuna.FormComuna();
            e.IdRegion = item.IdRegion;
            e.IdComuna = item.IdComuna;
            e.Comuna = item.Comuna;
            if (!String.IsNullOrEmpty(item.xml))
            {
                try
                {
                    string rawXml = item.xml;
                    rawXml = rawXml.Replace("”", "\"").Replace("“", "\"").Replace(".",",").ToLower();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(rawXml);
                    e.superficie = double.Parse(doc.SelectSingleNode("//superficie")?.InnerText);
                    e.poblacion = long.Parse(doc.SelectSingleNode("//poblacion")?.InnerText);
                    e.densidad = double.Parse(doc.SelectSingleNode("//poblacion")?.Attributes["densidad"].Value);
                }
                catch(XmlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return e;
        }
        private string RetornarXml(PresentComuna.FormComuna comuna)
        {
            /*
             * Sabran comprender que el tiempo es un recurso limitado en esta prueba por lo cual me fui forzado a hacerlo en bruto
             */
            return "<info>" +
                "<superficie>" + comuna.superficie + "</superficie>" +
                "<poblacion Densidad='" + comuna.densidad + "'>" + comuna.poblacion + "</poblacion>" +
                "</info>";
        }
        public async Task<Regiones> GetRegion(int IdRegion)
        {
            try
            {
                string url = "http://localhost:44673/Region/" + IdRegion;
                Regiones region;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonresponse = await response.Content.ReadAsStringAsync();
                        region = JsonConvert.DeserializeObject<Regiones>(jsonresponse);
                    }
                    else
                    {
                        Console.WriteLine($"Error en la consulta: {response.StatusCode}");
                        return null;
                    }
                    return region;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message );
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Regiones>> GetRegiones()
        {
            try 
            {
                string url = "http://localhost:44673/region";
                List<Regiones> regiones;
                using (HttpClient client = new HttpClient()) 
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        regiones = JsonConvert.DeserializeObject<List<Regiones>>(jsonResponse);
                    }
                    else
                    {
                        Console.WriteLine($"Error en la consulta: {response.StatusCode}");
                           return null;
                    }
                    return regiones;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<bool> MergeComuna(PresentComuna.FormComuna comuna)
        {
            try
            {
                string url = "http://localhost:44673/region/" + comuna.IdRegion + "/Comuna";
                List<Regiones> regiones;
                using (HttpClient client = new HttpClient())
                {
                    Comunas Send = new Comunas()
                    {
                      Comuna = comuna.Comuna,
                      IdRegion = comuna.IdRegion,
                      IdComuna = comuna.IdComuna,
                      xml = RetornarXml(comuna)
                    };
                    var json = System.Text.Json.JsonSerializer.Serialize(Send);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url,content);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Error en la consulta: {response.StatusCode}");
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<PresentComuna.FormComuna> GetComuna(int IdRegion, int IdComuna)
        {
            try
            {
                string url = "http://localhost:44673/region/"+IdRegion+"/Comuna/"+IdComuna;
                PresentComuna.FormComuna regiones;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        var dato = JsonConvert.DeserializeObject<Comunas>(jsonResponse);
                        return ProcesarComuna(dato);
                    }
                    else
                    {
                        Console.WriteLine($"Error en la consulta: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
