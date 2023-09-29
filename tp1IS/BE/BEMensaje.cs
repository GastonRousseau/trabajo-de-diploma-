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

        public int tipo { get; set; }
        public DateTime fecha  { get; set;  }

        public BEMensaje()
        {

        }

        public BEMensaje(BEUsuario _remitente,BEUsuario _destinatario,string _mensaje,DateTime _fecha,int _tipo)
        {
            remitente = _remitente;
            destinatario = _destinatario;
            mensaje = _mensaje;
            fecha = _fecha;
            tipo = _tipo;
        }
     //   public BEViaje viaje { get; set; }
    }
}
