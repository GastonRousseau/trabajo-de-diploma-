using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using BE;
using servicios;
using abstraccion;

namespace Negocio
{
    public class BLLUsuario
    {
        BLLBitacora oBit = new BLLBitacora();
        public BLLUsuario()
        {
            oUsuario = new MPPUsuario();
        }
        MPPUsuario oUsuario;
        
        public bool cambiar_contrasena(int id, string contra)
        {
            try
            {
               
                string contraseña = encriptar.EncriptarConHash(contra);
              
                return oUsuario.cambiar_contrasena(id, contraseña);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }

        public BEUsuario buscar_usuarioxid(int id)
        {
            try
            {
                return oUsuario.buscar_usuarioporID(id);
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

        public bool ActualizarDVxU(int ID,string DV)
        {
            try
            {
                return oUsuario.actualizarDVxUsuario(ID, DV);
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
                return oUsuario.BuscarUsuariosYgenerarDV();
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
                return oUsuario.borrar_rol(id, rol);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
        public bool validar(BEUsuario usuario)
        {
            try
            {
               
                usuario.password = encriptar.EncriptarConHash(usuario.password);
                return oUsuario.validar(usuario);
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
                return oUsuario.buscar_usuario(username);
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
                return oUsuario.usuario_existente(id);
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
                return oUsuario.username_existente(username);
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
                return oUsuario.eliminar_usuario(id);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
        public bool cargar_usuario(BEUsuario usuario)
        {
            try
            {
                usuario.password = encriptar.EncriptarConHash(usuario.password);
         
                usuario.DV = GenerarVD.generarDigitoVU(usuario);
                return oUsuario.cargar_usuario(usuario);
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
                return oUsuario.es_admin(username);
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
                return oUsuario.dar_admin(id);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
        public bool crear_admin(BEUsuario usuario)
        {
            try
            {
    
                usuario.password = encriptar.EncriptarConHash(usuario.password);
                usuario.DV = GenerarVD.generarDigitoVU(usuario);
                return oUsuario.crear_admin(usuario);
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
                return oUsuario.es_activo(username);
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
                return oUsuario.agregar_rol(id, rol);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
        public bool tiene_rol(int id, int rol)
        {
            try
            {
                return oUsuario.tiene_rol(id, rol);
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

                return oUsuario.restaurar_usuario(user);
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

                return oUsuario.testConnection();
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
                return oUsuario.GetAllHistorico(nombre, pag);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                oBit.guardar_accion(ex.Message, 1);
                throw ex;
            }
        }
    }
    
}
