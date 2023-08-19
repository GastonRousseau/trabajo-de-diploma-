using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace MPP
{
    public class MPPComposite
    {
        Acceso oDatos;
        Hashtable Hdatos;
        public bool escribir (Componente componente)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_componente_agregar";
                Hdatos.Add("@nombre", componente.Nombre);
                Hdatos.Add("@patente", false);
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
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

         public int escribir_retorno_id (string componente)
         {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_componente_agregar_con_id";
                Hdatos.Add("@nombre", componente);
                Hdatos.Add("@patente", false);
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (Convert.ToInt32(fila["MaxValor"]) != 0) return Convert.ToInt32(fila["MaxValor"]);
                }
                return 0;
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
        public bool hacer_patente(Componente componente, bool patente)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_componente_modificar";
                Hdatos.Add("@id", componente.Id);
                Hdatos.Add("@patente", patente);
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
        public bool escribir_relacion(int hijo, int padre)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_componente_agregar_relacion";
                Hdatos.Add("@id_permiso_padre", padre);
                Hdatos.Add("@id_permiso_hijo", hijo);
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

        public IList< Componente> GetAll(int familia)
        {
            try
            {
                oDatos = new Acceso();
                return oDatos.GetAll(familia);
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
        public IList<Componente> GetFamilias()
        {
            try
            {
                IList<Componente> lista = new List<Componente>();
                Hdatos = new Hashtable();
                string Consulta = "s_buscar_familia";
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                Componente c;
                foreach (DataRow fila in Ds2.Rows)
                {
                    c = new Familia();
                    c.Nombre = fila["nombre"].ToString();
                    c.Id = Convert.ToInt32(fila["id"]);
                    lista.Add(c);
                }
                return lista;
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
        public IList<Componente> GetPermisos()
        {
            try
            {
                IList<Componente> lista = new List<Componente>();
                Hdatos = new Hashtable();
                string Consulta = "s_buscar_permisos";
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                Componente c;
                foreach (DataRow fila in Ds2.Rows)
                {
                    c = new Familia();
                    c.Nombre = fila["nombre"].ToString();
                    c.Id = Convert.ToInt32(fila["id"]);
                    lista.Add(c);
                }
                return lista;
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
        public List<int> get_permisos(int rol)
        {
            try
            {
                List<int> lista = new List<int>();
                Hdatos = new Hashtable();
                string Consulta = "s_buscar_permisos";
                IList<Componente> componentes = GetAll(rol);
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                int c;
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (Convert.ToBoolean(fila["es_patente"]) == true)
                    {
                        c = Convert.ToInt32(fila["id"]);
                        lista.Add(c);
                    }
                }
                List<int> objetosFiltrados = componentes.Where(obj => lista.Contains(obj.Id)).Select(obj => obj.Id).ToList();
                return objetosFiltrados;
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
        public IList<Componente> get_permisos_usuario(int usuario)
        {
            try
            {
                IList<Componente> lista = new List<Componente>();
                Hdatos = new Hashtable();
                Hdatos.Add("@id", usuario);
                string Consulta = "s_buscar_permisos_user";
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                Componente c;
                foreach (DataRow fila in Ds2.Rows)
                {
                    c = new Familia();
                    c.Nombre = fila["nombre"].ToString();
                    c.Id = Convert.ToInt32(fila["id"]);
                    lista.Add(c);
                }
                
                return lista;
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
        public bool borrar(int id)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_componente_borrar";
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

        public int buscar_id(string nombre) 
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_id_buscar";
                Hdatos.Add("@nombre", nombre);
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (nombre == fila["nombre"].ToString() && Convert.ToInt32(fila["id"]) != 0) return Convert.ToInt32(fila["id"]);
                }
                return 0;
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
        public bool buscar_rol_usado(int rol)
        {
            try
            {
                DataTable Ds2 = new DataTable();
                Acceso oDatos = new Acceso();
                Hdatos = new Hashtable();
                string Consulta = "s_rol_usado";
                Hdatos.Add("@rol", rol);
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                foreach (DataRow fila in Ds2.Rows)
                {

                    if (rol == Convert.ToInt32(fila["id_rol"])) return true;
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
        public bool es_patente(string nombre)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_buscar_permiso";
                Hdatos.Add("@nombre", nombre);
                oDatos = new Acceso();
                DataTable Ds2 = new DataTable();
                Ds2 = oDatos.Leer(Consulta, Hdatos);
                Componente componente = new Patente();
                foreach (DataRow fila in Ds2.Rows)
                {
                    if (nombre == fila["nombre"].ToString() && Convert.ToBoolean(fila["es_patente"]) == true)
                    {
                        return true;
                    }
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





    }



}

