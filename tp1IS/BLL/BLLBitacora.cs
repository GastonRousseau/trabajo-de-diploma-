using abstraccion;
using BE;
using Patrones.Singleton.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class BLLBitacora
    {
        MPP.MPPBitacora oBit = new MPP.MPPBitacora();

        public void guardar_accion(string accion, int id_tipo)
        {
            try
            {
            SessionManager u = SessionManager.GetInstance;
                if (u.Usuario != null)
                {
                    BEUsuario user = u.Usuario;
                    DateTime fecha = DateTime.Now;

                    oBit.cargar_bitacora(user, fecha, accion, id_tipo);
                }

                
          
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
        public void guardar_logIn()
        {
           try
           {
            SessionManager u = SessionManager.GetInstance;
                if (u.Usuario != null)
                {
                   
                    BEUsuario user = u.Usuario;
                    DateTime fecha = u.FechaInicio;
                    int id_tipo = 2;
                    string accion = "logged in";
                    oBit.cargar_bitacora(user, fecha, accion, id_tipo);
                }
            
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex) { throw ex; }
            }
        public void guardar_logOut()
        {
         try
          {
            SessionManager u = SessionManager.GetInstance;
                if (u.Usuario != null)
                {
                    BEUsuario user = u.Usuario;
                    DateTime fecha = DateTime.Now;
                    string accion = "logged out";
                    int id_tipo = 2;
                    oBit.cargar_bitacora(user, fecha, accion, id_tipo);
                }
               
          }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex) { throw ex; }
        }
        public IList<IBitacora> GetAll(IBitacoraFilters filters, int pag)
        {
            try
            {
                    return oBit.GetAll(filters, pag);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
