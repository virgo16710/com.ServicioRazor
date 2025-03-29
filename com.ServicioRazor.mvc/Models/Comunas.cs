using System.ComponentModel.DataAnnotations;

namespace com.ServicioRazor.mvc.Models
{
    public class Comunas
    {
        public int IdComuna { get; set; }
        public int IdRegion { get; set; }
        public string Comuna { get; set; } 
        public string xml { get; set; }
    }
    public class Regiones
    {
        public int IdRegion { get; set; }
        public string Region { get; set; }
    }
    //public class FormComuna
    //{
    //    public int IdComuna { get; set; }
    //    public int IdRegion { get; set; }
    //    public string Comuna { get; set; }
    //    public double densidad { get; set; }
    //    public double superficie { get; set; }
    //    public long poblacion { get; set; }
    //}
    
    public class PresentComuna
    {
        public List<FormComuna> Comunas { get; set; }
        public IEnumerable<Regiones> Regiones { get; set; }
        public FormComuna Ocomuna { get; set; }
        public class FormComuna
        {
            public int IdComuna { get; set; }
            public int IdRegion { get; set; }
            public string Comuna { get; set; }
            public double densidad { get; set; }
            public double superficie { get; set; }
            public long poblacion { get; set; }
        }
    }

}
