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

        public List<string> Todos_los_usuarios_a_conectar()
        {
            return oMPPmensaje.Todos_los_usuarios_a_conectar();
        }
        public List<BEUsuario> obtenerchats(int codigo)
        {
            return oMPPmensaje.obtenerchats(codigo);
        }
        public bool Eliminar_Chat(int codigoU, int codigoE)
        {
            return oMPPmensaje.Eliminar_Chat(codigoU, codigoE);
        }
        public Dictionary<string, int> Mensajes_Nuevos(int ID)
        {
            return oMPPmensaje.Mensajes_Nuevos(ID);
        }
        public List<string> Usuarios_con_quien_conectar(int ID)
        {
            return oMPPmensaje.Usuarios_con_quien_conectar(ID);
        }
        public string Buscar_ServicioTecnico()
        {
            return oMPPmensaje.Buscar_ServicioTecnico();
        }
    }
}
