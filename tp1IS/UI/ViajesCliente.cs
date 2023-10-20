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
    public partial class ViajesCliente : Form
    {
        public ViajesCliente()
        {
            InitializeComponent();
            oBLLviaje = new BLLviaje();
            oBLLmensaje = new BLLMensaje();
            panel3.Visible = false;
            //  dateTimePicker1.Value = null;
            this.Size = new Size(826, 311);
            Listar(null,1);
        }
        BLLviaje oBLLviaje;
        BLLMensaje oBLLmensaje;
        BLLCamion oBLLcamion = new BLLCamion();
        BEViaje viajeSelect = new BEViaje();
        int pag = 0;
        string NombreProducto;
        List<BEViaje> viajes = new List<BEViaje>();
        BLLProducto oBLLproducto = new BLLProducto();
        private void ViajesCliente_Load(object sender, EventArgs e)
        {
            label1.Visible =false;
            barrarProgreso1.Visible = false;
            metroLabel2.Visible = false;
            cargarCombo();
            //Listar();
        }
        void Listar(string producto, int pag)
        {
            
            viajes = oBLLviaje.Traer_Viajes_Clientes(SessionManager.GetInstance.Usuario.id,pag,NombreProducto);
            if (viajes.Count == 0) { metroButton2.Enabled = false; }
            else { metroButton2.Enabled = true; }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = viajes;
            /*   dataGridView1.DataMember ="producto";
               dataGridView1.Columns["nombre"].HeaderText ="producto";
              // dataGridView1.Columns["Nombre"].HeaderText = "Productos";*/
            /* dataGridView1.Columns["producto"].DisplayMember = "nombre";
             dataGridView1.Columns["producto"].DataPropertyName = "producto";
             dataGridView1.Columns["producto"].HeaderText = "Producto";*/


        }
        void cargarCombo()
        {
            //metroComboBox1.Items.AddR(oBLLproducto.Producto_asignado_viaje(SessionManager.GetInstance.Usuario.id));
            List<string> productos = oBLLproducto.Producto_asignado_viaje(SessionManager.GetInstance.Usuario.id);
            foreach (string nombre in productos)
            {
                metroComboBox1.Items.Add(nombre);
            }
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
                e.Value = camion.patente+" "+camion.conductor.user;
            }

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
          /*  int error = 0;
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (viajeSelect == null)
            {
                error++;
            }
            if (error == 0)
            {
                BEMensaje mensaje = new BEMensaje();
             //   mensaje.mensaje = userControl11.Texts;//textBox1.Text;
                mensaje.destinatario = SessionManager.GetInstance.Usuario;
                mensaje.remitente = viajeSelect.camion.conductor;
            //    oBLLmensaje.GuardarMensaje(mensaje,viajeSelect.id);
                MessageBox.Show("se escribio el mensaje");

            }
            else
            {
                MessageBox.Show("hubo un error");
            }*/
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            BEViaje viajeSelect = ((BEViaje)dataGridView1.CurrentRow.DataBoundItem);
            if (viajeSelect.id != 0)
            {
                if(viajeSelect.estado=="En proceso")
                {
                     label1.Visible = true;
                barrarProgreso1.Visible = true;
                //progressBar1.Minimum = 0;
                barrarProgreso1.MaximumValue =viajeSelect.cantidad_KM;
                    barrarProgreso1.ProgressValue = viajeSelect.Km_Recorridos;
                    metroLabel2.Visible = true;
                    metroLabel2.Text = (viajeSelect.Km_Recorridos + "/" + viajeSelect.cantidad_KM);
                }
                else
                {
                    MessageBox.Show("El estado del viaje tiene que ser pendiente, de lo contrario no se podra realizar la accion");
                }

            }
            else
            {
                MessageBox.Show("No hay ningun viaje seleccionado");
            } 
           
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
                if (viajeSelect.id != 0)
                {
                    if (viajeSelect.estado == "pendiente")
                    {
                        oBLLviaje.ActualizarEstado(viajeSelect.id, "Cancelado",null);
                        BEMensaje mensaja = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "El viaje con id:" + viajeSelect.id, DateTime.Now, 2);
                        oBLLmensaje.GuardarMensaje(mensaja);
                        Chat.usuarioAconectar = viajeSelect.camion.conductor;
                        Chat form = new Chat();
                        form.button2.Visible = true;
                        form.userControl11.Texts = "Solicite la cancelacion del viaje devido a que";
                        form.Show();
                        Listar(null, 1);

                    }
                    else
                    {
                        MessageBox.Show("El estado del viaje tiene que ser pendiente, de lo contrario no se podra realizar la accion");
                    }
                }
                else
                {
                    MessageBox.Show("No hay ningun viaje seleccionado");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (viajeSelect.id != 0)
            {
                
                Chat.usuarioAconectar = viajeSelect.camion.conductor;
                
                // form.label1.Text = viajeSelect.camion.conductor.user;
                Chat form = new Chat();
                form.button2.Visible = true;
                form.Show();

            }
            else
            {
                MessageBox.Show("No hay ningun viaje seleccionado");
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
           
            if(viajeSelect.id != 0 && viajeSelect.estado == "pendiente")
            {
                this.Size = new Size(1081, 311);
                panel3.Visible = true;

            }
            else
            {
                MessageBox.Show("No hay ningun viaje seleccion o el viaje que selecciono no tiende estado pendiente");
            }
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            this.Size= new Size(826,311);
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value != null&& viajeSelect!=null&&viajeSelect.estado=="pendiente")
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = oBLLcamion.Camiones_Disponibles(dateTimePicker1.Value, viajeSelect.cantidad_Pallets);
            }
            else 
            {
                MessageBox.Show("Ocurrio un error,puede ser que el estado del viaje no sea pendiente");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            BECamion camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
            if(camionSelect != null&&viajeSelect!=null&&dateTimePicker1.Value!=null)
            {
                if (viajeSelect.camion != camionSelect)
                {
                    BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "Dejaras del ser el conductor del viaje con id: " + viajeSelect.id, DateTime.Now, 2);
                    oBLLmensaje.GuardarMensaje(mensaje);
                    viajeSelect.camion = camionSelect;
                    BEMensaje mensaje2 = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "Eres el nuevo conductor del viaje con id: " + viajeSelect.id, DateTime.Now, 2);
                    oBLLmensaje.GuardarMensaje(mensaje2);
                }
                else
                {
                    BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario,viajeSelect.camion.conductor,"Se postargo el viaje"+viajeSelect.id +"a la fecha " + dateTimePicker1.Value,DateTime.Now,2);
                    oBLLmensaje.GuardarMensaje(mensaje);
                }
               
                viajeSelect.fecha = dateTimePicker1.Value;
                oBLLviaje.Modifica_Viaje(viajeSelect);


                MessageBox.Show("Se postergo la fecha del viaje al dia" +dateTimePicker1.Value);
                panel3.Visible = false;
                this.Size = new Size(826, 311);
                Listar(null,1);
            }
            else
            {
                MessageBox.Show("hubo un error al intentar postergar el viaje");
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroButton1.Enabled = true;
            pag += 1;
            Listar(NombreProducto, pag);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            pag -= 1;
            metroButton1.Enabled = true;
            if (pag <= 1) metroButton1.Enabled = false;
            if (pag > 0) Listar(NombreProducto, pag);
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem != null)
            {
                NombreProducto = metroComboBox1.SelectedItem.ToString();
                Listar(NombreProducto, 1);
            }
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            metroComboBox1.SelectedIndex = -1;
            NombreProducto = null;
            Listar(null, 1);
        }
    }
}
