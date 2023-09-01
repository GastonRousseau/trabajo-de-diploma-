using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;
namespace BLL
{
   public class BLLMensaje
    {
        public BLLMensaje()
        {
            oMPPmensaje = new MPPMensaje();
        }
        MPPMensaje oMPPmensaje;

        public bool GuardarMensaje(BEMensaje mensaje)
        {
            return oMPPmensaje.Guardar_Mensaje(mensaje);
        }
        public bool escribir_Respuesta(int IDmensaje,string respuesta)
        {
            return oMPPmensaje.Escribir_Respesta(IDmensaje, respuesta);
        }
        public List<BEMensaje> ObtenerMensajes(int codigoUsuario, BEUsuario codigoChat)
        {
            return oMPPmensaje.ObtenerMensajes(codigoUsuario, codigoChat);
        }
        public List<BEUsuario> obtenerchats(int codigo)
        {
            return oMPPmensaje.obtenerchats(codigo);
        }
    }
}
