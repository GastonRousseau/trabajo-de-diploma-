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
using servicios;
using Patrones.Singleton.Core;
namespace UI
{
    public partial class Viajes_chofer : MetroFramework.Forms.MetroForm
    {
        public Viajes_chofer()
        {
            InitializeComponent();
            oBLLviaje = new BLLviaje();
            buscar(null,1);
        }
        BLLviaje oBLLviaje;
        int pag = 0;
        string NombreCliente;
        IList<BEViaje> viajes = new List<BEViaje>();
        
        private void Viajes_chofer_Load(object sender, EventArgs e)
        {
            pag = 1;
        }
        void buscar(string nombre,int pag) 
        {

            try
            {
               
                viajes = oBLLviaje.Traer_Viajes_Chofer(SessionManager.GetInstance.Usuario.id,pag,nombre);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                
                
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
            if (pag > 0) buscar(NombreCliente, pag);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
           metroButton1.Enabled = true;
            pag += 1;
            buscar(NombreCliente, pag);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            NombreCliente = textBox1.Text;
            buscar(NombreCliente, 1);
        }

        private void formato(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.Value != null)
            {
                BEProducto producto = (BEProducto)e.Value;
                e.Value = producto.nombre;
            }
            if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.Value != null)
            {
                BECamion camion = (BECamion)e.Value;
                e.Value = camion.patente;
            }
        }
    }
}
