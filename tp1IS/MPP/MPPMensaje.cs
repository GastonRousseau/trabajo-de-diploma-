using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Collections;
using System.Data;
namespace MPP
{
  public class MPPMensaje
    {
        public MPPMensaje()
        {
            oDatos = new Acceso();
            hdatos = new Hashtable();
        }
        Acceso oDatos;
        Hashtable hdatos;

        public bool Guardar_Mensaje(BEMensaje mensaje)//?
        {
            string consulta = "S_Guardar_Mensaje";
            hdatos = new Hashtable();
            hdatos.Add("@codRemitente",mensaje.remitente.id);
            hdatos.Add("@codDestinatario",mensaje.destinatario.id);
            hdatos.Add("@mensaje",mensaje.mensaje);
            hdatos.Add("@fecha",mensaje.fecha);
            return oDatos.Escribir(consulta, hdatos);
        }

        public List<BEUsuario> obtenerchats(int codigo)
        {
            List<BEUsuario> usuarios = new List<BEUsuario>();
            string consulta = "S_Obtener_Chats";
            hdatos.Add("@codigoUsuario", codigo);
            DataTable DT = oDatos.Leer(consulta, hdatos);
            foreach (DataRow fila in DT.Rows)
            {
                //if (codigo!= Convert.ToInt32(fila["id_"]))
                // {
                BEUsuario usuario = new BEUsuario();
                usuario.id = Convert.ToInt32(fila["id"]);
                usuario.user = fila["username"].ToString();
                usuarios.Add(usuario);
                // }

            }
            return usuarios;

        }
        public List<BEMensaje> ObtenerMensajes(int codigoUsuario, BEUsuario codigoChat)
        {
            List<BEMensaje> Mensajes = new List<BEMensaje>();
            string consulta = "S_Lista_Mensajes";
            hdatos.Add("@id_usuario_remitente", codigoUsuario);
            hdatos.Add("@nombre_usuario_destinatario", codigoChat.user);
            DataTable DT = oDatos.Leer(consulta, hdatos);
            foreach (DataRow fila in DT.Rows)
            {
                /*  BEMensaje mensaje = new BEMensaje();
                  mensaje.coido = Convert.ToInt32(fila["codigo_mensaje"]);
                  mensaje.mensaje=fila["mensaje"].ToString();
                  BEusuario usuarioRemitente = new BEusuario();
                  usuarioRemitente.username= fila["username_remitente"].ToString();
                  usuarioRemitente.codigo = Convert.ToInt32(fila["codigo_remitente"]);
                  BEusuario usuarioDestinatario = new BEusuario();
                  usuarioDestinatario.username= fila["username_destinatario"].ToString();
                  usuarioDestinatario.codigo= Convert.ToInt32(fila["codigo_destinatario"]);
                  mensaje.destinatario = usuarioDestinatario;
                  mensaje.remitente = usuarioRemitente;*/
                BEMensaje mensaje = new BEMensaje();
                mensaje.id = Convert.ToInt32(fila["codigo_mensaje"]);
                mensaje.mensaje = fila["mensaje"].ToString();
                BEUsuario usuarioRemitente = new BEUsuario();
                usuarioRemitente.id = Convert.ToInt32(fila["codigo_remitente"]);
                mensaje.remitente = usuarioRemitente;
                Mensajes.Add(mensaje);
            }
            return Mensajes;
        }
        public bool Escribir_Respesta(int IDmensaje,string repuesta)
        {
            string consulta = "S_Responder_Mensaje";
            hdatos = new Hashtable();
            hdatos.Add("@codMensaje",IDmensaje);
            hdatos.Add("@respuesta",repuesta);
            return oDatos.Escribir(consulta, hdatos);
        }
    }
}
