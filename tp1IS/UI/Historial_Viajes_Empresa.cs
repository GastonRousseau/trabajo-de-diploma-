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
using BE;
using BLL;
namespace UI
{
    public partial class Historial_Viajes_Empresa : Form
    {
        public Historial_Viajes_Empresa()
        {
            InitializeComponent();
            buscar(null, 1, null,null);//    arreglar para que sea de una fecha a otra
        }
        IList<BEViaje> viajes = new List<BEViaje>();
        BLLviaje oBLLviajes = new BLLviaje();
        int pag;
        string nombreCliente;
        Nullable<DateTime> from;
        Nullable<DateTime> to;
        private void Historial_Viajes_Empresa_Load(object sender, EventArgs e)
        {

        }

        void buscar(string nombreCliente,int pag,Nullable<DateTime> from,Nullable<DateTime>to)
        {
            try
            {

                viajes = oBLLviajes.getAll_Historial_viajes_(pag,nombreCliente,from,to);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                //  dataGridView1.ReadOnly = true;
                // dataGridView1.Columns["viajes_palets"].ReadOnly = true;
                //   dataGridView1.Columns["distancia"].ReadOnly = true;
                //  dataGridView1.Columns["estado"].ReadOnly = true;
                // dataGridView1.Columns["fecha"].ReadOnly = true;

                //               dataGridView1.Columns["Km_Recorridos"].ReadOnly = false;

            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                //  oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                var accion = ex.Message;
                //  oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

            pag -= 1;
            metroButton1.Enabled = true;
            if (pag <= 1) metroButton1.Enabled = false;
            if (pag > 0) buscar(nombreCliente,pag,from,to);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroButton1.Enabled = true;
            pag += 1;
            buscar(nombreCliente, pag, from,to);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            from = null;
            to = null;
            nombreCliente = null;
            if (textBox1.Text != string.Empty)
            {
                nombreCliente = textBox1.Text;
            }
            if (metroDateTime1.Value != null)
            {
                from = metroDateTime1.Value;
            }
            if (metroDateTime2.Value != null)
            {
                to = metroDateTime2.Value;
            }
            buscar(nombreCliente, 1, from, to);
            pag = 1;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            from = null;
            to = null;
            nombreCliente = null;
            buscar(nombreCliente, 1, from, to);
            pag = 1;
        }

        private void cellFormattingDataGrid(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.RowIndex >= 0)
            {
                BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;

                // Asegúrate de que la celda tenga un valor de Viaje no nulo
                if (viaje != null && viaje.producto != null)
                {
                    // Configura el valor de la celda para mostrar el nombre del producto
                    e.Value = viaje.producto.nombre;
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.RowIndex >= 0)
            {
                BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;

                // Asegúrate de que la celda tenga un valor de Viaje no nulo
                if (viaje != null && viaje.camion != null)
                {
                    // Configura el valor de la celda para mostrar el nombre del producto
                    e.Value = viaje.camion.patente + " " + viaje.camion.conductor.user;
                }
            }
        }
    }
}
