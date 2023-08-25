﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Collections;
using System.Data;
using Patrones.Singleton.Core;
namespace MPP
{
    public class MPPProducto
    {
        public MPPProducto()
        {
            oDatos = new Acceso();
            Hdatos = new Hashtable();
        }
        Acceso oDatos;
        Hashtable Hdatos;
        public bool Crear_Producto(BEProducto producto)
        {
            if (producto.id ==0)
            {
                string consulta = "S_Crear_Producto";
                Hdatos = new Hashtable();
                Hdatos.Add("@nombre", producto.nombre);
                Hdatos.Add("@cantPallets", producto.CantPallets);
                Hdatos.Add("@codCliente", producto.cliente.id);
                return oDatos.Escribir(consulta, Hdatos);
            }
            else
            {
                string consulta = "S_Modificar_Producto";
                Hdatos = new Hashtable();
                Hdatos.Add("@codigo", producto.id);
                Hdatos.Add("@nombre", producto.nombre);
                Hdatos.Add("@cantPallets", producto.CantPallets);
                Hdatos.Add("@codCliente", producto.cliente.id);
                return oDatos.Escribir(consulta, Hdatos);
            }
            
        }
        public bool Elimiinar_Producto(int ID)
        {
            string consulta = "S_Borrar_Producto";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigo", ID);
            return oDatos.Escribir(consulta, Hdatos);
        }

        public List<BEProducto> ListarProducto(int ID)
        {
            string consulta = "S_Listar_Productos";
            Hdatos = new Hashtable();
            Hdatos.Add("@codigo", ID);
            DataTable DT = new DataTable();
            DT = oDatos.Leer(consulta, Hdatos);
            List<BEProducto> ListaProducto = new List<BEProducto>();

            foreach (DataRow fila in DT.Rows)
            {
                BEProducto producto = new BEProducto();
                producto.id = Convert.ToInt32(fila["codigo"]);
                producto.nombre = fila["nombre"].ToString();
                producto.CantPallets = Convert.ToInt32(fila["cantidad_pallets"]);
                producto.cliente = SessionManager.GetInstance.Usuario;

                ListaProducto.Add(producto);
            }

            return ListaProducto;

        }
    }

}
