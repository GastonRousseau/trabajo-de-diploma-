using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections; 
using BE;
using abstraccion;
using servicios;
using Patrones.Singleton.Core;

namespace DAL
{
    public class Acceso
    {
        public SqlConnection oCnn = new SqlConnection(System.Configuration.ConfigurationManager.
    ConnectionStrings["ConnectionString"].ConnectionString);
        public SqlTransaction Tranx;
        public SqlCommand Cmd;


        public bool TestConnection()
        {
            oCnn.Open();
            if (oCnn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int LeerCantidad(string Consulta, Hashtable Hdatos)
        {
            oCnn.Open();
            Cmd = new SqlCommand(Consulta, oCnn);
            Cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if ((Hdatos != null))
                {
                    foreach (string dato in Hdatos.Keys)
                    {
                        Cmd.Parameters.AddWithValue(dato, Hdatos[dato]);
                    }
                }

                int Respuesta = Convert.ToInt32(Cmd.ExecuteScalar());
                oCnn.Close();
                return Respuesta;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
        }

        public bool LeerScalar(string Consulta, Hashtable Hdatos)
        {
            oCnn.Open();
            Cmd = new SqlCommand(Consulta, oCnn);
            Cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if ((Hdatos != null))
                {
                    foreach (string dato in Hdatos.Keys)
                    {
                        Cmd.Parameters.AddWithValue(dato, Hdatos[dato]);
                    }
                }

                int Respuesta = Convert.ToInt32(Cmd.ExecuteScalar());
                oCnn.Close();
                if (Respuesta > 0)
                { return true; }
                else
                { return false; }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
        }
        public DataTable Leer(string Consulta, Hashtable Hdatos)
        {
            if (oCnn.State == ConnectionState.Closed)
            {
                oCnn.Open();
            }
            DataTable Dt = new DataTable();
            SqlDataAdapter Da;
            Cmd = new SqlCommand(Consulta, oCnn);
            Cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                Da = new SqlDataAdapter(Cmd);

                if ((Hdatos != null))
                {
                    foreach (string dato in Hdatos.Keys)
                    {
                        Cmd.Parameters.AddWithValue(dato, Hdatos[dato]);
                    }
                }

            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }
            Da.Fill(Dt);
            return Dt;



        }
        public bool Escribir(string consulta, Hashtable Hdatos)
        {

            if (oCnn.State == ConnectionState.Closed)
            {
                oCnn.Open();
            }

            try
            {
                Tranx = oCnn.BeginTransaction();
                Cmd = new SqlCommand(consulta, oCnn, Tranx);
                Cmd.CommandType = CommandType.StoredProcedure;

                if ((Hdatos != null))
                {
                    foreach (string dato in Hdatos.Keys)
                    {
                        Cmd.Parameters.AddWithValue(dato, Hdatos[dato]);
                    }
                }

                int respuesta = Cmd.ExecuteNonQuery();
                Tranx.Commit();
                return true;

            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            {
                Tranx.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                Tranx.Rollback();
                return false;
            }
            finally
            { oCnn.Close(); }

        }

        public bool Escribir_con_respuesta(string consulta, Hashtable Hdatos)
        {

            if (oCnn.State == ConnectionState.Closed)
            {
                oCnn.Open();
            }

            try
            {
                Tranx = oCnn.BeginTransaction();
                Cmd = new SqlCommand(consulta, oCnn, Tranx);
                Cmd.CommandType = CommandType.StoredProcedure;

                if ((Hdatos != null))
                {
                    foreach (string dato in Hdatos.Keys)
                    {
                        Cmd.Parameters.AddWithValue(dato, Hdatos[dato]);
                    }
                }

                int respuesta = Convert.ToInt32(Cmd.ExecuteScalar());
                Tranx.Commit();
                if (respuesta != 0) return true;
                return false;

            }
            catch (NullReferenceException ex)
            {
                Tranx.Rollback();
                throw ex;
            }
            catch (SqlException ex)
            {
                Tranx.Rollback();
                return false;
            }
            catch (Exception ex)
            {
                Tranx.Rollback();
                return false;
            }
            finally
            { oCnn.Close(); }

        }

        public IList<Componente> GetAll(int familia)
        {


            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "s_composite_obtener";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                int where = 0;
                if (familia != 0)
                {
                    where = familia;
                }
                Hashtable Hdatos = new Hashtable();
                Hdatos.Add("@where", where);
                if ((Hdatos != null))
                {
                    foreach (string dato in Hdatos.Keys)
                    {
                        Cmd.Parameters.AddWithValue(dato, Hdatos[dato]);
                    }
                }

                var reader = Cmd.ExecuteReader();

                var lista = new List<Componente>();

                while (reader.Read())
                {
                    int id_padre = 0;
                    if (reader["id_permiso_padre"] != DBNull.Value)
                    {
                        id_padre = reader.GetInt32(reader.GetOrdinal("id_permiso_padre"));
                    }

                    var id = reader.GetInt32(reader.GetOrdinal("id"));
                    var nombre = reader.GetString(reader.GetOrdinal("nombre"));
                    var patente = reader.GetBoolean(reader.GetOrdinal("es_patente"));


                    Componente c;

                    if (!patente)
                        c = new Familia();
                    else
                        c = new Patente();

                    c.Id = id;
                    c.Nombre = nombre;

                    var padre = GetComponent(id_padre, lista);

                    if (padre == null)
                    {
                        lista.Add(c);
                    }
                    else
                    {
                        padre.AgregarHijo(c);
                    }
                }
                reader.Close();
                oCnn.Close();
                return lista;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }
        }

        public bool IsInRole(int id)
        {
            var lista = GetAll(0);

            var c = GetComponent(id, lista);

            return c != null;
        }


        private Componente GetComponent(int id, IList<Componente> lista)
        {
            Componente component = null;
            try
            {
                 component = lista != null ? lista.Where(i => i.Id.Equals(id)).FirstOrDefault() : null;

                if (component == null && lista != null)
                {
                    foreach (var c in lista)
                    {

                        var l = GetComponent(id, c.Hijos);
                        if (l != null && l.Id == id) return l;
                        else
                        if (l != null)
                            return GetComponent(id, l.Hijos);

                    }
                }


            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            { throw ex; }
            return component;



        }

        public IList<BEUsuario> TraerConductores(string nombre,int pag)
        {
            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "S_Traer_Conductores";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("username", nombre == null ? (object)DBNull.Value : nombre);
                Cmd.Parameters.AddWithValue("PageNumber", pag);

                var reader = Cmd.ExecuteReader();
                IList<BEUsuario> usuarios = new List<BEUsuario>();
                while (reader.Read())
                {
                    BEUsuario user = new BEUsuario();
                    user.user = reader["username"].ToString();
                    user.password = reader["password"].ToString();
                    user.id = Convert.ToInt32(reader["id"]);
                    user.birthDate = reader["birthdate"].ToString();
                    user.active = Convert.ToInt32(reader["active"]);
                    string Dencrip = reader["Direccion"].ToString();
                    user.Direccion = Convert.ToString(encriptar.Desencriptar(Dencrip));
                    usuarios.Add(user);
                }
                reader.Close();

                return usuarios;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }


        }

        public IList<BECamion> TraerCamiones(string patente, int pag)
        {
            
            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "S_Traer_Camiones";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("patente", patente == null ? (object)DBNull.Value : patente);
                Cmd.Parameters.AddWithValue("PageNumber", pag);

                var reader = Cmd.ExecuteReader();
                // IList<BEUsuario> usuarios = new List<BEUsuario>();
                IList<BECamion> camiones = new List<BECamion>();
                while (reader.Read())
                {
                    BECamion camion = new BECamion();
                    camion.id = Convert.ToInt32(reader["codigo"]);
                    camion.patente = reader["patente"].ToString();
                    camion.tipo=reader["tipo"].ToString();
                    camion.capacidad_Pallets = Convert.ToInt32(reader["capacidad_pallets"]);
                //    int codigoConductor = Convert.ToInt32(reader["codigo_Conductor"]);
                    if (reader["codigo_Conductor"]is DBNull)
                    {
                        camiones.Add(camion);
                    }
                    else
                    {
                        BEUsuario usuario = new BEUsuario();
                        usuario.id= Convert.ToInt32(reader["codigo_Conductor"]);
                        camion.conductor = usuario;
                        camiones.Add(camion);
                    }
                  
                 
                   
                }
                reader.Close();
                
                foreach(BECamion camion in camiones)
                {
                    if (camion.conductor!=null)
                    {
                        var sql2 = "S_Traer_Conductor";
                        SqlCommand Cmd2 = new SqlCommand(sql2, oCnn);
                        Cmd2.CommandType = CommandType.StoredProcedure;
                        Cmd2.Parameters.AddWithValue("@codigo", camion.conductor.id);
                        var reader2 = Cmd2.ExecuteReader();
                        while (reader2.Read())
                        {
                            BEUsuario user = new BEUsuario();
                            user.id = camion.conductor.id;
                            user.user = reader2["username"].ToString();
                            user.password = reader2["password"].ToString();
                            user.birthDate = reader2["birthdate"].ToString();
                            user.active = Convert.ToInt32(reader2["active"]);
                            string Dencrip = reader2["Direccion"].ToString();
                            user.Direccion = Convert.ToString(encriptar.Desencriptar(Dencrip));
                            camion.conductor = user;

                        }
                        reader2.Close();
                    }
                }
                return camiones;
                
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }


        }
        public IList<IBitacora> GetAll(IBitacoraFilters filters, int pag)
        {
            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "s_bitacora_obtener";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("Username", filters.Username == null ? (object)DBNull.Value : filters.Username);
                Cmd.Parameters.AddWithValue("From", filters.From.ToString() == "" ? (object)DBNull.Value : filters.From);
                Cmd.Parameters.AddWithValue("To", filters.To.ToString() == "" ? (object)DBNull.Value : filters.To);
                Cmd.Parameters.AddWithValue("idBitacoraType", filters.Type == null ? (object)DBNull.Value : filters.Type);
                Cmd.Parameters.AddWithValue("Like", filters.Like == null ? (object)DBNull.Value : filters.Like);
                Cmd.Parameters.AddWithValue("PageNumber", pag);

                var reader = Cmd.ExecuteReader();
                IList<IBitacora> listBitacora = new List<IBitacora>();
                    while (reader.Read())
                    {
                        IBitacora bitacora = new BEBitacora();
                        bitacora.IdBitacora = Convert.ToInt32(reader["id"].ToString());
                        bitacora.Username = reader["username"].ToString();
                        bitacora.Date = Convert.ToDateTime(reader["time"].ToString());
                        bitacora.Type = Convert.ToInt32(reader["tipo"]);
                        bitacora.Message = reader["action"].ToString();
                        listBitacora.Add(bitacora);
                    }
                reader.Close();

                return listBitacora;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }

        }
        public IList<BEViaje> getAll_Historial_viajes_clientes(int id,int pag,string NombreProducto)
        {
            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "S_Listar_Historial_Viajes_cliente";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("nombreProducto", NombreProducto == null ? (object)DBNull.Value : NombreProducto);
                Cmd.Parameters.AddWithValue("PageNumber", pag);
                Cmd.Parameters.AddWithValue("codigoCliente", id);

                var fila = Cmd.ExecuteReader();
                IList<BEViaje> viajes = new List<BEViaje>();

                while (fila.Read())
                {
                    BEViaje viaje = new BEViaje();///////////////////////////////////////
                    viaje.id = Convert.ToInt32(fila["codigo_viaje"]);
                    viaje.cantidad_Pallets = Convert.ToInt32(fila["viajes_palets"]);
                    viaje.cantidad_KM = Convert.ToInt32(fila["distancia"]);
                    viaje.fecha = Convert.ToDateTime(fila["fecha"]);
                    viaje.estado = fila["estado"].ToString();
                    viaje.Km_Recorridos = Convert.ToInt32(fila["KM_recorridos"]);
                    BECamion camion = new BECamion();////////////////////////////////////
                    camion.id = Convert.ToInt32(fila["codigo_camion"]);
                    camion.capacidad_Pallets = Convert.ToInt32(fila["capacidad_pallets"]);
                    camion.patente = fila["patente"].ToString();
                    camion.tipo = fila["tipo"].ToString();
                    BEUsuario conuctor = new BEUsuario();////////////////////////////////
                    conuctor.id = Convert.ToInt32(fila["codigo_conductor"]);
                    conuctor.user = fila["username"].ToString();
                    BEProducto producto = new BEProducto();//////////////////////////////
                    producto.id = Convert.ToInt32(fila["codigo_producto"]);
                    producto.nombre = fila["nombre"].ToString();
                    producto.CantPallets = Convert.ToInt32(fila["cantidad_pallets"]);
                    camion.conductor = conuctor;
                    viaje.camion = camion;
                    producto.cliente = SessionManager.GetInstance.Usuario;
                    viaje.producto = producto;
                    viajes.Add(viaje);
                }
                fila.Close();
                return viajes;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }
        }
        public IList<BEViaje> getAll_Historial_viajes_(int pag, string NombreCliente,DateTime fecha)
        {
            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "S_Listar_Historial_Viajes";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("nombreCliente", NombreCliente == null ? (object)DBNull.Value : NombreCliente);
                Cmd.Parameters.AddWithValue("fecha", fecha == null ? (object)DBNull.Value : fecha);
                Cmd.Parameters.AddWithValue("PageNumber", pag);
             //   Cmd.Parameters.AddWithValue("codigoCliente", id);

                var fila = Cmd.ExecuteReader();
                IList<BEViaje> viajes = new List<BEViaje>();

                while (fila.Read())
                {
                    BEViaje viaje = new BEViaje();///////////////////////////////////////
                    viaje.id = Convert.ToInt32(fila["codigo_viaje"]);
                    viaje.cantidad_Pallets = Convert.ToInt32(fila["viajes_palets"]);
                    viaje.cantidad_KM = Convert.ToInt32(fila["distancia"]);
                    viaje.fecha = Convert.ToDateTime(fila["fecha"]);
                    viaje.estado = fila["estado"].ToString();
                    viaje.Km_Recorridos = Convert.ToInt32(fila["KM_recorridos"]);
                    BECamion camion = new BECamion();////////////////////////////////////
                    camion.id = Convert.ToInt32(fila["codigo_camion"]);
                    camion.capacidad_Pallets = Convert.ToInt32(fila["capacidad_pallets"]);
                    camion.patente = fila["patente"].ToString();
                    camion.tipo = fila["tipo"].ToString();
                    BEUsuario conuctor = new BEUsuario();////////////////////////////////
                    conuctor.id = Convert.ToInt32(fila["codigo_conductor"]);
                    conuctor.user = fila["username"].ToString();
                    BEProducto producto = new BEProducto();//////////////////////////////
                    producto.id = Convert.ToInt32(fila["codigo_producto"]);
                    producto.nombre = fila["nombre"].ToString();
                    producto.CantPallets = Convert.ToInt32(fila["cantidad_pallets"]);
                    camion.conductor = conuctor;
                    viaje.camion = camion;
                    producto.cliente = SessionManager.GetInstance.Usuario;
                    viaje.producto = producto;
                    viajes.Add(viaje);
                }
                fila.Close();
                return viajes;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }
        }
        public IList<BEViaje> GetAll_Viajes_Conuctores(int id, int pag,string usernameCliente)
        {
            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "S_Listar_Viajes_Chofer";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("usernameCliente", usernameCliente == null ? (object)DBNull.Value : usernameCliente);
                Cmd.Parameters.AddWithValue("PageNumber", pag);
                Cmd.Parameters.AddWithValue("codigoChofer", id);

                var fila = Cmd.ExecuteReader();
                IList<BEViaje> viajes = new List<BEViaje>();
               // IList<IBitacora> listBitacora = new List<IBitacora>();
                while (fila.Read())
                {
                    BEViaje viaje = new BEViaje();///////////////////////////////////////
                    viaje.id = Convert.ToInt32(fila["codigo_viaje"]);
                    viaje.cantidad_Pallets = Convert.ToInt32(fila["viajes_palets"]);
                    viaje.cantidad_KM = Convert.ToInt32(fila["distancia"]);
                    viaje.fecha = Convert.ToDateTime(fila["fecha"]);
                    viaje.estado = fila["estado"].ToString(); 
                    if(viaje.estado=="En proceso")
                    {
                        viaje.Km_Recorridos = Convert.ToInt32(fila["KM_recorridos"]);
                    }
                    
                    BECamion camion = new BECamion();////////////////////////////////////
                    camion.id = Convert.ToInt32(fila["codigo_camion"]);
                    camion.capacidad_Pallets = Convert.ToInt32(fila["capacidad_pallets"]);
                    camion.patente = fila["patente"].ToString();
                    camion.tipo = fila["tipo"].ToString();
                    BEUsuario conuctor = new BEUsuario();////////////////////////////////
                    conuctor.id = Convert.ToInt32(fila["codigo_conductor"]);
                    conuctor.user = fila["username"].ToString();
                    // conuctor.password = fila[""].ToString();
                    BEUsuario cliente = new BEUsuario();/////////////////////////////////
                    cliente.id = Convert.ToInt32(fila["codigo_usuario"]);
                    cliente.user = fila["username"].ToString();
                    cliente.password = fila["password"].ToString();
                    BEProducto producto = new BEProducto();//////////////////////////////
                    producto.id = Convert.ToInt32(fila["codigo_producto"]);
                    producto.nombre = fila["nombre"].ToString();
                    producto.CantPallets = Convert.ToInt32(fila["cantidad_pallets"]);

                    /////////////////////////////////////////////////////////////////////
                    camion.conductor = conuctor;
                    viaje.camion = camion;
                    producto.cliente = cliente;
                    viaje.producto = producto;
                    viajes.Add(viaje);

                }
                fila.Close();

                return viajes;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }

        }



        public IList<BEUsuario> GetAllHistorico(string nombre, int pag)
        {
            try
            {
                if (oCnn.State == ConnectionState.Closed)
                {
                    oCnn.Open();
                }
                var sql = "s_historico_obtener";
                Cmd = new SqlCommand(sql, oCnn);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("username", nombre == null ? (object)DBNull.Value : nombre);
                Cmd.Parameters.AddWithValue("PageNumber", pag);

                var reader = Cmd.ExecuteReader();
                IList<BEUsuario> usuarios = new List<BEUsuario>();
                    while (reader.Read())
                    {
                        BEUsuario user = new BEUsuario();
                        user.user = reader["username"].ToString();
                        user.password = reader["password"].ToString();
                        user.id = Convert.ToInt32(reader["id"]);
                        user.birthDate = reader["birthdate"].ToString();
                        user.active = Convert.ToInt32(reader["active"]);
                        string Dencrip = reader["Direccion"].ToString();
                        user.Direccion = Convert.ToString(encriptar.Desencriptar(Dencrip));
                    usuarios.Add(user);
                    }
                reader.Close();

                return usuarios;
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (SqlException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            { oCnn.Close(); }

        }
    }
}




