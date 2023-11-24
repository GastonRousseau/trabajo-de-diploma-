using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Data;
using System.Collections;
namespace MPP
{
    public class MPPviaje
    {
        public MPPviaje()
        {
            oDatos = new Acceso();
            Hdatos = new Hashtable();
        }
        Acceso oDatos;
        Hashtable Hdatos;
        public bool Guardar_Viaje(BEViaje viaje)
        {
            if (viaje.id == 0)
            {
                string consulta = "S_Crear_Viaje";
                Hdatos = new Hashtable();
                Hdatos.Add("@codCamion", viaje.camion.id);
                Hdatos.Add("@codProducto", viaje.producto.id);
                Hdatos.Add("@cantidadPallets", viaje.cantidad_Pallets);
                Hdatos.Add("@distancia", viaje.cantidad_KM);
                Hdatos.Add("@fecha", viaje.fecha);
                Hdatos.Add("@estado", viaje.estado);
                Hdatos.Add("@partida", viaje.partida);
                Hdatos.Add("@destino", viaje.destino);
                return oDatos.Escribir(consulta, Hdatos);
            }
            else
            {
                string consulta = "S_Modificar_Viaje";
                Hdatos = new Hashtable();
                Hdatos.Add("codigo", viaje.id);
                Hdatos.Add("@codCamion", viaje.camion);
                Hdatos.Add("@codProoducto", viaje.producto);
                Hdatos.Add("@cantidadPallets", viaje.cantidad_Pallets);
                Hdatos.Add("@distancia", viaje.cantidad_KM);
                Hdatos.Add("@fecha", viaje.fecha);
                Hdatos.Add("@estado", viaje.estado);
                return oDatos.Escribir(consulta, Hdatos);
            }
        }

        public IList<BEViaje> Historial_viajes_clientes(int ID,int numberpag,string NombreProducto)
        {
            return oDatos.getAll_Historial_viajes_clientes(ID, numberpag, NombreProducto);
        }
        public List<BEViaje> Traer_Viajes_Clientes(int ID,int numberpag,string NombreProducto)
        {
            string consulta = "S_Listar_Vuelos_Clientes";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigocliente", ID);
            
                Hdatos.Add("nombreProducto", NombreProducto == null ? (object)DBNull.Value : NombreProducto);
                Hdatos.Add("PageNumber",numberpag);
              //  Hdatos.Add("codigoCliente", ID);
            
            DataTable DT = oDatos.Leer(consulta, Hdatos);
            List<BEViaje> viajes = new List<BEViaje>();
            foreach (DataRow fila in DT.Rows)
            {
                BEViaje viaje = new BEViaje();///////////////////////////////////////
                viaje.id = Convert.ToInt32(fila["codigo_viaje"]);
                viaje.cantidad_Pallets = Convert.ToInt32(fila["viajes_palets"]);
                viaje.cantidad_KM = Convert.ToInt32(fila["distancia"]);
                viaje.fecha = Convert.ToDateTime(fila["fecha"]);
              //  int KM= Convert.ToInt32(fila["KM_recorridos"]);
                if (fila["KM_recorridos"] is DBNull)
                {

                }
                else
                {
                    viaje.Km_Recorridos = Convert.ToInt32(fila["KM_recorridos"]);
                }
               
                viaje.estado = fila["estado"].ToString();
                viaje.partida = fila["partida"].ToString();
                viaje.destino = fila["destino"].ToString();
                BECamion camion = new BECamion();////////////////////////////////////
                camion.id= Convert.ToInt32(fila["codigo_camion"]);
                camion.capacidad_Pallets = Convert.ToInt32(fila["capacidad_pallets"]);
                camion.patente = fila["patente"].ToString();
                camion.tipo = fila["tipo"].ToString();
                BEUsuario conuctor = new BEUsuario();////////////////////////////////
                conuctor.id= Convert.ToInt32(fila["codigo_conductor"]);
                conuctor.user = fila["username_conductor"].ToString();
               // conuctor.password = fila[""].ToString();
                BEUsuario cliente = new BEUsuario();/////////////////////////////////
                cliente.id= Convert.ToInt32(fila["codigo_usuario"]);
                cliente.user = fila["username"].ToString();
                cliente.password = fila["password"].ToString();
                BEProducto producto = new BEProducto();//////////////////////////////
                producto.id= Convert.ToInt32(fila["codigo_producto"]);
                producto.nombre = fila["nombre"].ToString();
                producto.CantPallets = Convert.ToInt32(fila["cantidad_pallets"]);

                /////////////////////////////////////////////////////////////////////
                camion.conductor = conuctor;
                viaje.camion = camion;
                producto.cliente = cliente;
                viaje.producto = producto;
                viajes.Add(viaje);
            }
            return viajes;
        }
        public IList<BEViaje> Traer_Viajes_Chofer(int ID,int pag,string username)
        {
            return oDatos.GetAll_Viajes_Conuctores(ID, pag, username);
           /* string consulta = "S_Listar_Viajes_Chofer";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigoChofer", ID);
            DataTable DT = oDatos.Leer(consulta, Hdatos);
            List<BEViaje> viajes = new List<BEViaje>();
            foreach (DataRow fila in DT.Rows)
            {
                BEViaje viaje = new BEViaje();///////////////////////////////////////
                viaje.id = Convert.ToInt32(fila["codigo_viaje"]);
                viaje.cantidad_Pallets = Convert.ToInt32(fila["viajes_palets"]);
                viaje.cantidad_KM = Convert.ToInt32(fila["distancia"]);
                viaje.fecha = Convert.ToDateTime(fila["fecha"]);
                viaje.estado = fila["estado"].ToString();
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
            return viajes;*/
        }

        public List<BEViaje> Traer_Viajes_Viajes_con_Mensajes_Choferes(int ID)
        {
            string consulta = "S_Traer_Viajes_con_Mensajes";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigoChofer", ID);
            DataTable DT = oDatos.Leer(consulta, Hdatos);
             List<BEViaje> viajes = new List<BEViaje>();
            List<BEMensaje> Mensajes = new List<BEMensaje>();
            foreach (DataRow fila in DT.Rows)
            {
                BEViaje viaje = new BEViaje();///////////////////////////////////////
                viaje.id = Convert.ToInt32(fila["codigo_viaje"]);
                viaje.cantidad_Pallets = Convert.ToInt32(fila["viajes_palets"]);
                viaje.cantidad_KM = Convert.ToInt32(fila["distancia"]);
                viaje.fecha = Convert.ToDateTime(fila["fecha"]);
                viaje.estado = fila["estado"].ToString();
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
                foreach(DataRow fila2 in DT.Rows)
                {
                    BEMensaje mensaje = new BEMensaje();
                    mensaje.id = Convert.ToInt32(fila["codigo_mensaje"]);
                    mensaje.destinatario = conuctor;
                    mensaje.remitente = cliente;
                    mensaje.mensaje = fila["mensaje"].ToString();
                    Mensajes.Add(mensaje);
                }
               
                
                /////////////////////////////////////////////////////////////////////

                camion.conductor = conuctor;
                viaje.camion = camion;
                producto.cliente = cliente;
                viaje.producto = producto;
               // viaje.mensajes = Mensajes;
                viajes.Add(viaje);
                //mensaje.viaje=viaje;
              //  Mensajes.Add(mensaje);
            }
            return viajes;
        }
        public bool Eliminar_Viaje(int ID)
        {
            string consulta = "S_Eliminar_Viaje";
            Hdatos = new Hashtable();
            Hdatos.Add("codigo", ID);
            return oDatos.Escribir(consulta, Hdatos);
        }

        public bool Modifica_Viaje(BEViaje viaje)
        {
            string consulta = "S_Modificar_Viaje";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigo",viaje.id);
            Hdatos.Add("@codCamion",viaje.camion.id);
            Hdatos.Add("@codProducto",viaje.producto.id);
            Hdatos.Add("@cantidadPallets",viaje.producto.CantPallets);
            Hdatos.Add("@distancia",viaje.cantidad_KM);
            Hdatos.Add("@fecha",viaje.fecha);
            Hdatos.Add("@estado",viaje.estado);
            return oDatos.Escribir(consulta, Hdatos);
        }

        public bool actualizar_Estado(int ID,string estado,Nullable<DateTime> fecha)
        {
            string consulta = "S_Actualizar_Estado_Viaje";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigo", ID);
            Hdatos.Add("@estado", estado);
            Hdatos.Add("@fecha_finalizado", fecha == null ? (object)DBNull.Value : fecha);

            return oDatos.Escribir(consulta,Hdatos);
        }
        public bool actualizar_KM_recorridos(int ID,int KM)////////////////////////////////////////////////////////
        {
            string consulta = "S_Actualizar_KM_del_viaje";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigo", ID);
            Hdatos.Add("@KM", KM);
            return oDatos.Escribir(consulta, Hdatos);
        }

        public IList<BEViaje> getAll_Historial_viajes_(int pag, string NombreCliente, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            return oDatos.getAll_Historial_viajes_(pag,NombreCliente,from,to);
        }
        public IList<BEViaje> getAll_Historial_viajes_SF(string NombreCliente, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            return oDatos.getAll_Historial_viajes_SF(NombreCliente, from, to);
        }

        public IList<BEViaje> Viajes_pendientes_sistema(string NombreCliente,string NombreConductor,string PatenteCamiones, Nullable<DateTime> from, Nullable<DateTime> to,int pag)
        {
            return oDatos.Viajes_pendientes_sistema(NombreCliente, NombreConductor, PatenteCamiones, from, to, pag);
        }

        public List<BEViaje> TraerViajesDelConductor(string nombreConductor, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            string consulta = "S_Traer_Viajes_Finalizados_Por_Conductor";
            Hdatos = new Hashtable();
            Hdatos.Add("@username", nombreConductor);
            Hdatos.Add("@from", from == null ? (object)DBNull.Value : from);
            Hdatos.Add("@to", to == null ? (object)DBNull.Value : to);
            DataTable DT = oDatos.Leer(consulta, Hdatos);
            List<BEViaje> viajes = new List<BEViaje>();
            foreach(DataRow fila in DT.Rows)
            {
                BEViaje viaje = new BEViaje();
                viaje.cantidad_KM = Convert.ToInt32(fila["distancia"]);
                viaje.fecha = Convert.ToDateTime(fila["fecha"]);
                viaje.fechaFinalizacion = Convert.ToDateTime(fila["fecha_finalizacion"]);
                viaje.cantidad_Pallets = Convert.ToInt32(fila["cantidad_palets"]);
                viaje.partida = fila["partida"].ToString();
                viaje.destino = fila["destino"].ToString();
                viajes.Add(viaje);
            }
            return viajes;
        }

        public Dictionary<string,int> Conductores_viajes_realizados(Nullable<DateTime> from, Nullable<DateTime> to)
        {
            string consulta = "S_Traer_Conductor_y_Viajes_Realizados";
            Hdatos = new Hashtable();
            Hdatos.Add("@from", from == null ? (object)DBNull.Value : from);
            Hdatos.Add("@to", to == null ? (object)DBNull.Value : to);
            DataTable DT = oDatos.Leer(consulta, Hdatos);
            Dictionary<string, int> Conductores = new Dictionary<string, int>();
            foreach(DataRow fila in DT.Rows)
            {
                string username = fila["UsernameConductor"].ToString();
                int cantidad = Convert.ToInt32(fila["CantidadViajes"]);
                Conductores.Add(username, cantidad);
            }
            return Conductores;
        }
        

        
    }
}
