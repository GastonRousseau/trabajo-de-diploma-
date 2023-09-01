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
        public List<BEViaje> Traer_Viajes_Clientes(int ID)
        {
            return oMPPviaje.Traer_Viajes_Clientes(ID);
        }
        public IList<BEViaje> Traer_Viajes_Chofer(int ID, int pag,string username)
        {
            return oMPPviaje.Traer_Viajes_Chofer(ID,pag,username);
        }

        public List<BEViaje> Traer_Viajes_Viajes_con_Mensajes_Choferes(int ID)
        {
            return oMPPviaje.Traer_Viajes_Viajes_con_Mensajes_Choferes(ID);
        }
    }
}
