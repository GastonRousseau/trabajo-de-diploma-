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
    public partial class Mensajes_Chof : MetroFramework.Forms.MetroForm
    {
        public Mensajes_Chof()
        {
            InitializeComponent();
            oBLLviaje = new BLLviaje();
            oBLLmensaje = new BLLMensaje();
        }
        BLLviaje oBLLviaje;
        List<BEViaje> viajes = new List<BEViaje>();
        List<BEMensaje> Mensajes = new List<BEMensaje>();
        BEMensaje mensaje_ = new BEMensaje();
        BLLMensaje oBLLmensaje;
        private void Mensajes_Chof_Load(object sender, EventArgs e)
        {
            listar();
        }
        void listar()
        {
            dataGridView1.DataSource = null;
            viajes = oBLLviaje.Traer_Viajes_Viajes_con_Mensajes_Choferes(SessionManager.GetInstance.Usuario.id);
            foreach(BEMensaje mensaje in Mensajes)
            {
         //       viajes.Add(mensaje.viaje);
            }
            dataGridView1.DataSource = viajes;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            int error = 0;
            if (error == 0)
            {
            /*    foreach(BEMensaje mensaje in Mensajes)
                {
                    if (mensaje.viaje == viajeSelect)
                    {
                        mensaje_ = mensaje;

                        metroTextBox1.Text = mensaje_.mensaje;
                    }
                }*/
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            int error = 0;
            if (mensaje_ != null && error == 0)
            {
                mensaje_.respuesta = metroTextBox2.Text;
                oBLLmensaje.escribir_Respuesta(mensaje_.id, mensaje_.respuesta);
                MessageBox.Show("Se escribio la respuesta");
            }
        }
    }
}
