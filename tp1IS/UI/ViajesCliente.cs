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
    public partial class ViajesCliente : MetroFramework.Forms.MetroForm
    {
        public ViajesCliente()
        {
            InitializeComponent();
            oBLLviaje = new BLLviaje();
            oBLLmensaje = new BLLMensaje();
        }
        BLLviaje oBLLviaje;
        BLLMensaje oBLLmensaje;
        private void ViajesCliente_Load(object sender, EventArgs e)
        {
            label1.Visible =false;
            barrarProgreso1.Visible = false;
            Listar();
        }
        void Listar()
        {
            List<BEViaje> viajes = new List<BEViaje>();
            viajes = oBLLviaje.Traer_Viajes_Clientes(SessionManager.GetInstance.Usuario.id);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = viajes;
         /*   dataGridView1.DataMember ="producto";
            dataGridView1.Columns["nombre"].HeaderText ="producto";
           // dataGridView1.Columns["Nombre"].HeaderText = "Productos";*/
           /* dataGridView1.Columns["producto"].DisplayMember = "nombre";
            dataGridView1.Columns["producto"].DataPropertyName = "producto";
            dataGridView1.Columns["producto"].HeaderText = "Producto";*/


        }

        private void formating(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void metroButton3_Click(object sender, EventArgs e)
        {
            int error = 0;
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (viajeSelect == null)
            {
                error++;
            }
            if (error == 0)
            {
                BEMensaje mensaje = new BEMensaje();
                mensaje.mensaje = userControl11.Texts;//textBox1.Text;
                mensaje.destinatario = SessionManager.GetInstance.Usuario;
                mensaje.remitente = viajeSelect.camion.conductor;
            //    oBLLmensaje.GuardarMensaje(mensaje,viajeSelect.id);
                MessageBox.Show("se escribio el mensaje");

            }
            else
            {
                MessageBox.Show("hubo un error");
            }
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            BEViaje viajeSelect = ((BEViaje)dataGridView1.CurrentRow.DataBoundItem);
            if (viajeSelect != null) 
                {
                label1.Visible = true;
                barrarProgreso1.Visible = true;
                if (viajeSelect.estado == "Finalizado")
                {
                    barrarProgreso1.ProgressValue = 3;
                }
                if (viajeSelect.estado == "En progreso")
                {
                    barrarProgreso1.ProgressValue = 2;
                }
                if (viajeSelect.estado == "Pendiente")
                {
                    barrarProgreso1.ProgressValue = 1;
                }
            } 
           
            
        }
    }
}
