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
        public bool actualizar_Cantidad_Pallets(int numero, int codProducto)
        {
            return OMPPproducto.actualizar_Cantidad_Pallets(numero, codProducto);
        }
        public List<string> Producto_asignado_viaje(int usuario)
        {
            return OMPPproducto.Producto_asignado_viaje(usuario);
        }

        public bool Sumar_cantidad_pallets(int numero, int codProducto)
        {
            return OMPPproducto.Sumar_cantidad_pallets(numero, codProducto);
        }
    }
}

