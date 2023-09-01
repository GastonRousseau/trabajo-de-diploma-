using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BEMensaje

    {
        public int id { get; set; }
        public BEUsuario remitente { get; set; }
        public BEUsuario destinatario { get; set; }
        public string mensaje { get; set; }
        public string respuesta { get; set; }

        public DateTime fecha  { get; set;  }

     //   public BEViaje viaje { get; set; }
    }
}
