using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using abstraccion;
using MPP;
namespace BLL
{
   public class BLLProducto
    {
        public BLLProducto()
        {
            OMPPproducto = new MPPProducto();
        }
        MPPProducto OMPPproducto;

        public bool CrearProdcto(BEProducto producto)
        {
            return OMPPproducto.Crear_Producto(producto);
        }
        public bool EliminarProducto(int ID)
        {
            return OMPPproducto.Elimiinar_Producto(ID);
        }
        public List<BEProducto> ListarProductos(int ID)
        {
            return OMPPproducto.ListarProducto(ID);
        }
    }
}

