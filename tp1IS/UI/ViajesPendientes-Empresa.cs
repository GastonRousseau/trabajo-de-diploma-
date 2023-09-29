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
using Negocio;
using Patrones.Singleton.Core;
namespace UI
{
    public partial class ViajesPendientes_Empresa : MetroFramework.Forms.MetroForm
    {
        public ViajesPendientes_Empresa()
        {
            
            InitializeComponent();
            buscarViajes(nombreCliente, NombreCOnductor, PatenteCamion, from, to, 1);//null, null, null, null, null, 1);
         //   oBLLviaje = new BLLviaje();
            cargarDatos();
            panel1.Visible = false;
            this.Size = new Size(695, 431);

        }
        int pag;
        string nombreCliente;
        string NombreCOnductor;
        string PatenteCamion;
        Nullable<DateTime> from;
        Nullable<DateTime> to;
        BLLUsuario oBLLusuario = new BLLUsuario();
        BLLCamion oBLLcamion=new BLLCamion();
        BLLviaje oBLLviaje=new BLLviaje();
        BLLProducto oBLLproducto = new BLLProducto();
        BLLMensaje oBLLmensaje = new BLLMensaje();
        BEViaje viajeSelect = new BEViaje();
        private void ViajesPendientes_Empresa_Load(object sender, EventArgs e)
        {

        }
        void cargarDatos()
        {
            metroComboBox3.DataSource = null;
            metroComboBox3.DataSource= oBLLcamion.ListaPatentes();
            metroComboBox1.DataSource = null;
            metroComboBox1.DataSource = oBLLusuario.Username_Clientes();
            metroComboBox2.DataSource = null;
            metroComboBox2.DataSource = oBLLusuario.Username_Conductor();
            metroComboBox1.SelectedItem = null;
            metroComboBox2.SelectedItem = null;
            metroComboBox3.SelectedItem = null;


            if (panel1.Visible == true)
            {
                if (viajeSelect != null)
                {
                    List<BECamion> camiones = new List<BECamion>();
                    camiones = oBLLcamion.Camiones_Disponibles(viajeSelect.fecha, viajeSelect.cantidad_Pallets);
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = camiones;
                }
            }
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            pag -= 1;
            metroButton1.Enabled = true;
            if (pag <= 1) metroButton1.Enabled = false;
          //  if (pag > 0) buscar(nombreCliente, pag, from, to);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            metroButton1.Enabled = true;
            pag += 1;
           // buscar(nombreCliente, pag, from, to);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem != null)
            {
                nombreCliente = metroComboBox1.SelectedItem.ToString();
            }
            if (metroComboBox2.SelectedItem != null)
            {
                NombreCOnductor = metroComboBox2.SelectedItem.ToString();
            }
            if (metroComboBox3.SelectedItem != null)
            {
                PatenteCamion = metroComboBox3.SelectedItem.ToString();
            }
            if (metroDateTime1.Value != null)
            {
                from = metroDateTime1.Value;
            }

            if (metroDateTime2.Value != null)
             {
                to = metroDateTime2.Value;
             }

            buscarViajes(nombreCliente, NombreCOnductor, PatenteCamion, from, to, 1);
            pag = 1;
        }

        void buscarViajes(string Ncliente,string Nconductor,string Pcamion,Nullable<DateTime> from,Nullable<DateTime> to,int pag)
        {
            IList<BEViaje> viajes = new List<BEViaje>();
            viajes = oBLLviaje.Viajes_pendientes_sistema(Ncliente,Nconductor,Pcamion,from,to,pag);
            if (viajes.Count == 0) { metroButton2.Enabled = false; }
            else { metroButton2.Enabled = true; }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = viajes;
        }
        private void metroButton4_Click(object sender, EventArgs e)
        {
            nombreCliente = null;
            NombreCOnductor = null;
            PatenteCamion = null;
            metroComboBox1.SelectedItem = null;
            metroComboBox2.SelectedItem = null;
            metroComboBox3.SelectedItem = null;
            metroDateTime1.Checked = false;
            metroDateTime2.Checked = false;
            from = null;
            to = null;
            buscarViajes(nombreCliente, NombreCOnductor, PatenteCamion, from, to, 1);
            pag = 1;

        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            
            if (viajeSelect != null)
            {
                List<BECamion> camiones = new List<BECamion>();
                camiones = oBLLcamion.Camiones_Disponibles(viajeSelect.fecha, viajeSelect.cantidad_Pallets);
                this.Size = new Size(695, 701);
                panel1.Visible = true;
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = camiones;
            }
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            BECamion camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
            // panel1.Visible = true;
            if (viajeSelect != null)
            {
                if (camionSelect != null)
                {
                    //OpenFileDialog Dialogo1 = new OpenFileDialog();
                 
                   //     Dialogo1.Title = "Mensajes para el conductor antiguo del viaje " + viajeSelect.camion.conductor.user;
                        BEMensaje mensaje1 = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor,"Dejas de ser el conductor del viaje :"+viajeSelect.id,DateTime.Now,2);
                    oBLLmensaje.GuardarMensaje(mensaje1);
                    BEMensaje mensaje2 = new BEMensaje(SessionManager.GetInstance.Usuario, camionSelect.conductor, "Sos el nuevo conductor del viaje :" + viajeSelect.id, DateTime.Now,2);
                    oBLLmensaje.GuardarMensaje(mensaje2);
                    BEMensaje mensaje3 = new BEMensaje(SessionManager.GetInstance.Usuario,viajeSelect.producto.cliente, "El nuevo conductor asignado a tu viaje: "+viajeSelect.id+" es el conductor: "+camionSelect.conductor.user, DateTime.Now,2);
                    oBLLmensaje.GuardarMensaje(mensaje3);
                    viajeSelect.camion = camionSelect;
                    oBLLviaje.Modifica_Viaje(viajeSelect);
                    MessageBox.Show("Se modifico el camion del viaje");
                    //   cargarDatos();
                    //  this.Size = new Size(695,701);
                }
            }
          
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (panel1.Visible == true)
            {
                cargarDatos();
            }
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            this.Size=new Size(695,431);
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            if (viajeSelect != null)
            {
                if (viajeSelect.estado == "pendiente")
                {
                    oBLLproducto.Sumar_cantidad_pallets(viajeSelect.cantidad_Pallets, viajeSelect.producto.id);

                    BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario,viajeSelect.producto.cliente,"Tu viaje se cancelo",DateTime.Now,2);
                    oBLLmensaje.GuardarMensaje(mensaje);
                    Chat.usuarioAconectar = viajeSelect.producto.cliente;
                    Chat form = new Chat();
                    
                    form.button2.Visible = true;
                    form.userControl11.Texts = "Se cancelo debido a que:";
                    // form.label1.Text = viajeSelect.camion.conductor.user;
                    form.Show();
                    oBLLviaje.ActualizarEstado(viajeSelect.id, "Cancelado");

                }
            }
        }
    }
}
