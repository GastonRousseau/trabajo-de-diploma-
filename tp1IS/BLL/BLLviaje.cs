using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;
namespace BLL
{
    public class BLLviaje
    {
        public BLLviaje()
        {
            oMPPviaje = new MPPviaje();
        }
        MPPviaje oMPPviaje;

        public bool guardarViaje(BEViaje viaje)
        {
            return oMPPviaje.Guardar_Viaje(viaje);
        }
        public bool EliminarViaje(int ID)
        {
            return oMPPviaje.Eliminar_Viaje(ID);
        }
        public bool ActualizarEstado(int ID,string estado)
        {
            return oMPPviaje.actualizar_Estado(ID,estado);
        }

    }
}
