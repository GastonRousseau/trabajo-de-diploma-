using abstraccion;
using BE;
using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP
{
    public class MPPBitacora
    {
        Acceso oDatos;
        Hashtable Hdatos;
        public bool cargar_bitacora(BEUsuario usuario, DateTime date, string accion, int id_tipo)
        {
            try
            {
                Hdatos = new Hashtable();
                string Consulta = "s_bitacora_crear";
                Hdatos.Add("@user", usuario.user);
                Hdatos.Add("@time", date);
                Hdatos.Add("@action", accion);
                Hdatos.Add("@tipo", id_tipo);
                oDatos = new Acceso();
                return oDatos.Escribir(Consulta, Hdatos);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex) {  throw ex; }
        }
        public IList<IBitacora> GetAll(IBitacoraFilters filters, int pag)
        {
            try
            {
                oDatos = new Acceso();
                return oDatos.GetAll(filters, pag);

            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
