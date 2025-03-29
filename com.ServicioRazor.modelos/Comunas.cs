using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.ServicioRazor.modelos
{
    public class Comunas
    {
        public int IdComuna { get; set; }
        public int IdRegion { get; set; }
        public string Comuna { get; set; } = "";
        public string xml { get; set; } = "";
    }
}
