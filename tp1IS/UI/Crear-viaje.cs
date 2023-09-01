using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BE;
using BLL;
using Patrones.Singleton.Core;
namespace UI
{
    public partial class Crear_viaje : MetroFramework.Forms.MetroForm
    {
        public Crear_viaje()
        {
            InitializeComponent();
            oBLLcamion = new BLLCamion();
            oBLLproducto = new BLLProducto();
            oBLLviaje = new BLLviaje();
        }
        BLLCamion oBLLcamion;
        BLLProducto oBLLproducto;
        BLLviaje oBLLviaje;
        private void Crear_viaje_Load(object sender, EventArgs e)
        {
            Listar();
        }
        void Listar()
        {
            List<BEProducto> Productos = oBLLproducto.ListarProductos(SessionManager.GetInstance.Usuario.id);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Productos;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            DateTime fecha = metroDateTime1.Value;
            int pallets = Convert.ToInt32(textBox4.Text);
            List<BECamion> camiones = oBLLcamion.Camiones_Disponibles(fecha, pallets);
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = camiones;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            int error = 0;
            BEProducto productoSelect=(BEProducto)dataGridView1.CurrentRow.DataBoundItem;

            if (textBox1.Text == string.Empty)
            {
                error++;
            }
            if (textBox2.Text == string.Empty)
            {
                error++;
            }
            if (textBox3.Text == string.Empty)
            {
                error++;
            }
            if (textBox4.Text == string.Empty)
            {
                error++;
            }
            if ((BECamion)dataGridView2.CurrentRow.DataBoundItem == null)
            {
                error++;
            }
            if ((BEProducto)dataGridView1.CurrentRow.DataBoundItem == null || productoSelect.CantPallets<=0 ||productoSelect.CantPallets-Convert.ToInt32(textBox4.Text)<=0)
            {
                error++;
            }
            if (error == 0)
            {
                BEViaje viaje = new BEViaje();
                viaje.partida = textBox1.Text;
                viaje.destino = textBox2.Text;
                viaje.cantidad_KM = Convert.ToInt32(textBox3.Text);
                viaje.fecha = metroDateTime1.Value;
                viaje.camion = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                viaje.producto = (BEProducto)dataGridView1.CurrentRow.DataBoundItem;
                viaje.cantidad_Pallets = Convert.ToInt32(textBox4.Text);
                viaje.estado = "pendiente";
                oBLLviaje.guardarViaje(viaje);
                MessageBox.Show("Se creo el viaje");
                oBLLproducto.actualizar_Cantidad_Pallets(viaje.cantidad_Pallets, viaje.producto.id);
                Listar();
                //actualizar cantidad de pallets;
            }
            else
            {
                MessageBox.Show("Hubo un error La puta madre");
            }
          
        }
    }
}
