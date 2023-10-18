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
            hdatos.Add("@tipo", mensaje.tipo);
            return oDatos.Escribir(consulta, hdatos);
        }

        public List<BEUsuario> obtenerchats(int codigo)
        {
            List<BEUsuario> usuarios = new List<BEUsuario>();
            string consulta = "S_Obtener_Chats";
            hdatos = new Hashtable();
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
            try
            { 
            List<BEMensaje> Mensajes = new List<BEMensaje>();
            string consulta = "S_Lista_Mensajes";
                hdatos = new Hashtable();
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
                mensaje.fecha = Convert.ToDateTime(fila["fecha"]);
                mensaje.tipo = Convert.ToInt32(fila["tipo"]);
                Mensajes.Add(mensaje);
            }
            return Mensajes;

        }catch(Exception ex)
            {
                throw ex;
           
        }
        }

        public Dictionary<string,int> Mensajes_Nuevos(int ID)
        {
            string consulta = "S_mensajes_nuevos";
            hdatos = new Hashtable();
            hdatos.Add("@codigoUsuario",ID);
            DataTable DT = new DataTable();
            Dictionary<string, int> datos = new Dictionary<string, int>();
               DT= oDatos.Leer(consulta, hdatos);
            if(DT.Rows.Count > 0)
            {
                foreach(DataRow fila in DT.Rows)
                {
                    string nombre = fila["username_remitente"].ToString();
                    int cantidad = Convert.ToInt32(fila["cantidad_mensajes_no_leidos"]);
                    datos.Add(nombre, cantidad);

                }
                return datos;
            }
            else
            {
                return null;
            }
        }

        public string Buscar_ServicioTecnico()
        {
            string consulta2 = "S_Traer_Usuarios_por_Rol";
            hdatos = new Hashtable();
            hdatos.Add("@rol", 61);
            DataTable DT2 = new DataTable();
            DT2 = oDatos.Leer(consulta2, hdatos);
            string nombre="";
            foreach (DataRow fila2 in DT2.Rows)
            {
                nombre = fila2["username"].ToString();
                
            }
            return nombre;
        }
        public List<string> Usuarios_con_quien_conectar(int ID)
        {
           
            
            string consulta = "S_Traer_Conductores_Relacionados_a_cliente";
            hdatos = new Hashtable();
            hdatos.Add("@codigo_Cliente", ID);
            DataTable DT = new DataTable();
            List<string> Usuarios = new List<string>();
            DT = oDatos.Leer(consulta, hdatos);
            foreach(DataRow fila in DT.Rows)
            {
                string nombre = fila["username"].ToString();
                Usuarios.Add(nombre);
            }
            string consulta2 = "S_Traer_Usuarios_por_Rol";
            hdatos = new Hashtable();
            hdatos.Add("@rol",5);
            DataTable DT2 = new DataTable();
            DT2 = oDatos.Leer(consulta2, hdatos);
            foreach(DataRow fila2 in DT2.Rows)
            {
                string nombre = fila2["username"].ToString();
                Usuarios.Add(nombre);
            }
        
            return Usuarios;
        }

        public List<string> Todos_los_usuarios_a_conectar()
        {
            string consulta = "S_Traer_Todos_Los_Usuarios";
            hdatos = new Hashtable();
            DataTable DT = new DataTable();
            List<string> Usuarios = new List<string>();
            DT = oDatos.Leer(consulta, null);
            foreach(DataRow fila in DT.Rows)
            {
                string nombre = fila["username"].ToString();
                Usuarios.Add(nombre);
            }
            return Usuarios;
        }
        public bool Escribir_Respesta(int IDmensaje,string repuesta)
        {
            string consulta = "S_Responder_Mensaje";
            hdatos = new Hashtable();
            hdatos.Add("@codMensaje",IDmensaje);
            hdatos.Add("@respuesta",repuesta);
            return oDatos.Escribir(consulta, hdatos);
        }
        public bool Eliminar_Chat(int codigoU, int codigoE)
        {
            string consulta = "S_Borrar_Chat";
            hdatos = new Hashtable();
            hdatos.Add("@codigo1", codigoU);
            hdatos.Add("@codigo2", codigoE);
            return oDatos.Escribir(consulta, hdatos);
        }
    }
}
