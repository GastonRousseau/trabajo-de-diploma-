using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using MPP;
namespace BLL
{
  public  class BLLCamion
    {
        public BLLCamion()
        {
            OMPPCamion = new MPPCamion();
        }
        MPPCamion OMPPCamion;

        public bool CrearCamion(BECamion camion)
        {
            return OMPPCamion.CrearCamion(camion);
        }
        public bool Unicr_ConductoryCamion(int camion, int conductor)
        {
            return OMPPCamion.Unicr_ConductoryCamion(camion, conductor);
        }
        public IList<BECamion> TraerCamiones(string patente, int pag)
        {
            return OMPPCamion.TraerCamiones(patente, pag);
        }
        public bool Desvincular_ConductoryCamion(int camion)
        {
            return OMPPCamion.Desvincular_ConductoryCamion(camion);
        }
        public bool Verificar_Patente(string patente)
        {
            return OMPPCamion.Verificar_Patente(patente);
        }
        public bool BorrarCamion(int ID)
        {
            return OMPPCamion.BorrarCamion(ID);
        }
    }
}
