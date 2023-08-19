using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using BE;
using servicios;
using System.Collections;
using abstraccion;

namespace MPP
{
    public class MPPUsuario
    {
        Acceso oDatos;
        Hashtable Hdatos;
        public bool cambiar_contrasena(int id, string contrasena)
        {
            try
            {

                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_cambiar_contrasena";
                Hdatos.Add("@id", id);
                Hdatos.Add("@password", contrasena);

                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool borrar_rol(int id, int rol)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_sacar_rol";
                Hdatos.Add("@id", id);
                Hdatos.Add("@rol", rol);
                oDatos = new Acceso();
                return oDatos.Escribir_con_respuesta(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool validar(BEUsuario usuario)
        {
            try
            {
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_login";
                Hdatos.Add("@username", usuario.user);
                Hdatos.Add("@password", usuario.password);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (usuario.user == fila["username"].ToString() && usuario.password == fila["password"].ToString()) return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BEUsuario buscar_usuario(string username)
        {
            try
            {
                BEUsuario user = new BEUsuario();
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_buscar";
                Hdatos.Add("@username", username);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    user.user = username;
                    user.password = fila["password"].ToString();
                    user.id = Convert.ToInt32(fila["id"]);
                    user.birthDate = fila["birthdate"].ToString();
                  
                    string Dencrip = fila["Direccion"].ToString();
                    user.Direccion = encriptar.Desencriptar(Dencrip);
                }
                return user;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool es_activo(string username)
        {
            try
            {
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_activo";
                Hdatos.Add("@username", username);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (username == fila["username"].ToString() && true == Convert.ToBoolean(fila["active"])) return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool usuario_existente(int id)
        {
            try
            {
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_porid";
                Hdatos.Add("@id", id);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (id == Convert.ToInt32(fila["id"])) return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool username_existente(string username)
        {
            try
            {
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_pori_nombre";
                Hdatos.Add("@user", username);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (username == fila["username"].ToString()) return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool cargar_usuario(BEUsuario usuario)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_crear";
                Hdatos.Add("@id", usuario.id);
                Hdatos.Add("@user", usuario.user);
                Hdatos.Add("@password", usuario.password);
                Hdatos.Add("@active", true);
                Hdatos.Add("@birthdate", usuario.birthDate);
                Hdatos.Add("@dv", usuario.DV);

                string DEncript = servicios.encriptar.Encriptar(Convert.ToString(usuario.Direccion));
                Hdatos.Add("Direccion", DEncript);
                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool eliminar_usuario(int id)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_eliminar";
                Hdatos.Add("@id", id);
                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool es_admin(string username)
        {
            try
            {
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_buscar";
                Hdatos.Add("@username", username);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (username == fila["username"].ToString() && Convert.ToInt32(fila["rol"]) == 5) return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool dar_admin(int id)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_dar_admin";
                Hdatos.Add("@id", id);
                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool crear_admin(BEUsuario usuario)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_crear_admin";
                Hdatos.Add("@id", usuario.id);
                Hdatos.Add("@user", usuario.user);
                Hdatos.Add("@password", usuario.password);
                Hdatos.Add("@active", true);
                Hdatos.Add("@birthdate", usuario.birthDate);
                Hdatos.Add("@dv", usuario.DV);
                string DEncript = servicios.encriptar.Encriptar(Convert.ToString(usuario.Direccion));
                Hdatos.Add("Direccion", DEncript);
                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool agregar_rol(int id, int rol)
        {
            try
            {

                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_agregar_rol";
                Hdatos.Add("@id", id);
                Hdatos.Add("@rol", rol);

                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public bool restaurar_usuario(BEUsuario user)
        {
            try
            {

                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_restaurar";
                Hdatos.Add("@id", user.id);
                Hdatos.Add("@username", user.user);
                Hdatos.Add("@password", user.password);
                Hdatos.Add("@active", user.active);
                Hdatos.Add("@birthdate", user.birthDate);
                string DEncript = servicios.encriptar.Encriptar(Convert.ToString(user.Direccion));
                Hdatos.Add("Direccion", DEncript);
                string DV = servicios.GenerarVD.generarDigitoVU(user);
                Hdatos.Add("@dv", DV);
                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public bool tiene_rol(int id, int rol)
        {
            try
            {
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_rol_buscar";
                Hdatos.Add("@id", id);
                Hdatos.Add("@rol", rol);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (Convert.ToInt32(fila["id_rol"]) == rol && Convert.ToInt32(fila["id_usuario"]) == id) return true;
                }
                return false;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<BEUsuario> GetAllHistorico(string nombre, int pag)
        {
            try
            {
                oDatos = new Acceso();
                return oDatos.GetAllHistorico(nombre, pag);

            }
            catch (Exception ex) { throw ex; }
        }

        public BEUsuario buscar_usuarioporID(int id)
        {
            try
            {
                BEUsuario user = new BEUsuario();
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_buscarxid";
                Hdatos.Add("@id", id);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    user.user = fila["username"].ToString();
                    user.password = fila["password"].ToString();
                    user.id = Convert.ToInt32(fila["id"]);
                     user.birthDate = fila["birthdate"].ToString();
                    string Dencrip = fila["Direccion"].ToString();
                    user.Direccion = Convert.ToString(encriptar.Desencriptar(Dencrip));
                }
                return user;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool actualizarDVxUsuario(int ID,string DV)
        {

            try
            {

                Hdatos = new Hashtable();
                string Consulta = "s_Usuario_ActualizarDV";
                Hdatos.Add("@id", ID);
                Hdatos.Add("@dv", DV);

                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool testConnection()
        {

            try
            {

                oDatos = new Acceso();
                return oDatos.TestConnection();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> BuscarUsuariosYgenerarDV()
        {
            try
            {
                List<string> DVNUsers = new List<string>();
                BEUsuario user = new BEUsuario();
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "S_Traer_Usuarios";
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    user.user = fila["username"].ToString();
                    user.password = fila["password"].ToString();
                    user.id = Convert.ToInt32(fila["id"]);
                    string encriptado = fila["birthdate"].ToString();
                     user.birthDate = fila["birthdate"].ToString();
                     string DNIencrip =(fila["Direccion"].ToString());
                    user.Direccion = encriptar.Desencriptar((DNIencrip));
             
                    string dvV = fila["DV"].ToString();
                    string DV = servicios.GenerarVD.generarDigitoVU(user);
                    DVNUsers.Add(DV);
                    actualizarDVxUsuario(user.id, DV);
                }
                return DVNUsers;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
