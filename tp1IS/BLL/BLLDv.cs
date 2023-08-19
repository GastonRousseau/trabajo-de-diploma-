using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPP;
using servicios;
namespace BLL
{
   public class BLLDv
    {

        public BLLDv()
        {
            Odv = new MPPDv();
        }
        MPPDv Odv;

        public bool actualizarDV(string DV)
        {
            try
            {
                return Odv.actualizarDV(DV);
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

        public string BuscarDVS()
        {
            try
            {
                return Odv.BuscarDVS();
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

        public List<string> BuscarDVUsuarios()
        {
            try
            {
                return Odv.BuscarDVUsuarios();
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
