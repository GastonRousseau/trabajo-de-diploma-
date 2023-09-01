using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BEViaje
    {
        public int id { get; set; }
        public BECamion camion { get; set; }
        public BEProducto producto { get; set; }
        public string partida { get; set; }
        public string destino { get; set; }
        public int cantidad_KM { get; set; }
        public DateTime fecha { get; set; }
        public string estado { get; set; }
        public int cantidad_Pallets { get; set; }

       public List<BEMensaje> mensajes { get; set; }
    }
}
