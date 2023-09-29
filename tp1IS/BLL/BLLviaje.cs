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
        public IList<BEViaje> Historial_viajes_clientes(int ID, int numberpag, string NombreProducto)
        {
            return oMPPviaje.Historial_viajes_clientes(ID, numberpag, NombreProducto);
        }
        public bool actualizar_KM_recorridos(int ID, int KM)
        {
           return oMPPviaje.actualizar_KM_recorridos(ID, KM);
        }
        public List<BEViaje> Traer_Viajes_Viajes_con_Mensajes_Choferes(int ID)
        {
            return oMPPviaje.Traer_Viajes_Viajes_con_Mensajes_Choferes(ID);
        }
        public IList<BEViaje> getAll_Historial_viajes_(int pag, string NombreCliente,Nullable<DateTime> from,Nullable<DateTime>to)
        {
            return oMPPviaje.getAll_Historial_viajes_(pag, NombreCliente,from,to);
        }
        public IList<BEViaje> getAll_Historial_viajes_SF(string NombreCliente, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            return oMPPviaje.getAll_Historial_viajes_SF(NombreCliente, from, to);
        }
        public IList<BEViaje> Viajes_pendientes_sistema(string NombreCliente, string NombreConductor, string PatenteCamiones, Nullable<DateTime> from, Nullable<DateTime> to, int pag)
        {
            return oMPPviaje.Viajes_pendientes_sistema(NombreCliente, NombreConductor, PatenteCamiones, from, to, pag);
        }
        public bool Modifica_Viaje(BEViaje viaje)
        {
            return oMPPviaje.Modifica_Viaje(viaje);
        }
    }
}
