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

        public bool GuardarMensaje(int IDr,int IDd,string mensaje,int IDviaje)
        {
            return oMPPmensaje.Guardar_Mensaje(IDr,IDd,mensaje,IDviaje);
        }
        public bool EliminarMnesaje(int IDmensaje,string respuesta)
        {
            return oMPPmensaje.Escribir_Respesta(IDmensaje, respuesta);
        }
    }
}
