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
using servicios;
using servicios.ClasesMultiLenguaje;
namespace UI
{
    public partial class ViajesPendientes_Empresa : Form,IdiomaObserver
    {
        public ViajesPendientes_Empresa()
        {
            
            InitializeComponent();
            buscarViajes(nombreCliente, NombreCOnductor, PatenteCamion, from, to, 1);//null, null, null, null, null, 1);
         //   oBLLviaje = new BLLviaje();
            cargarDatos();
            panel1.Visible = false;
            this.Size = new Size(695, 431);
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;

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
        BECamion camionSelect = new BECamion();
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();
        private void ViajesPendientes_Empresa_Load(object sender, EventArgs e)
        {
            if (SessionManager.tiene_permiso(61) == true)
            {
                metroButton5.Enabled = false;
                metroButton6.Enabled = false;
            }
            Observer.agregarObservador(this);
            traducir();
        }
        void cargarDatos()
        {
            try
            {
                metroComboBox3.DataSource = null;
                metroComboBox3.DataSource = oBLLcamion.ListaPatentes();
                metroComboBox1.DataSource = null;
                metroComboBox1.DataSource = oBLLusuario.Username_Clientes();
                metroComboBox2.DataSource = null;
                metroComboBox2.DataSource = oBLLusuario.Username_Conductor();
                metroComboBox1.SelectedItem = null;
                metroComboBox2.SelectedItem = null;
                metroComboBox3.SelectedItem = null;


                if (panel1.Visible == true)
                {
                    if (viajeSelect.id != 0)
                    {
                        List<BECamion> camiones = new List<BECamion>();
                        camiones = oBLLcamion.Camiones_Disponibles(viajeSelect.fecha, viajeSelect.cantidad_Pallets);
                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = camiones;
                    }
                    else
                    {
                        MessageBox.Show("no hay ningun camion seleccionado");

                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            pag -= 1;
            metroButton1.Enabled = true;
            if (pag <= 1) metroButton1.Enabled = false;
            if (pag > 0) buscarViajes(nombreCliente, nombreCliente, PatenteCamion, from, to, pag);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////
            metroButton1.Enabled = true;
            pag += 1;
            buscarViajes(nombreCliente,nombreCliente,PatenteCamion, from, to,pag);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
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
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void buscarViajes(string Ncliente,string Nconductor,string Pcamion,Nullable<DateTime> from,Nullable<DateTime> to,int pag)
        {
            try
            {
                IList<BEViaje> viajes = new List<BEViaje>();
                viajes = oBLLviaje.Viajes_pendientes_sistema(Ncliente, Nconductor, Pcamion, from, to, pag);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;


                dataGridView1.DataSource = viajes;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

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
            try
            {
                if (viajeSelect.id != 0)
                {
                    List<BECamion> camiones = new List<BECamion>();
                    camiones = oBLLcamion.Camiones_Disponibles(viajeSelect.fecha, viajeSelect.cantidad_Pallets);
                    this.Size = new Size(695, 701);
                    panel1.Visible = true;
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = camiones;
                }
                else
                {
                    MessageBox.Show("no hay ningun camion seleccionado");
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            try
            {
                // camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                // panel1.Visible = true;
                if (viajeSelect.id != 0)
                {
                    if (camionSelect.id != 0)
                    {
                        //OpenFileDialog Dialogo1 = new OpenFileDialog();

                        //     Dialogo1.Title = "Mensajes para el conductor antiguo del viaje " + viajeSelect.camion.conductor.user;
                        BEMensaje mensaje1 = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "Dejas de ser el conductor del viaje :" + viajeSelect.id, DateTime.Now, 2);
                        oBLLmensaje.GuardarMensaje(mensaje1);
                        BEMensaje mensaje2 = new BEMensaje(SessionManager.GetInstance.Usuario, camionSelect.conductor, "Sos el nuevo conductor del viaje :" + viajeSelect.id, DateTime.Now, 2);
                        oBLLmensaje.GuardarMensaje(mensaje2);
                        BEMensaje mensaje3 = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.producto.cliente, "El nuevo conductor asignado a tu viaje: " + viajeSelect.id + " es el conductor: " + camionSelect.conductor.user, DateTime.Now, 2);
                        oBLLmensaje.GuardarMensaje(mensaje3);
                        viajeSelect.camion = camionSelect;
                        oBLLviaje.Modifica_Viaje(viajeSelect);
                        MessageBox.Show("Se modifico el camion del viaje");
                        //   cargarDatos();
                        //  this.Size = new Size(695,701);
                    }
                }
                else
                {
                    MessageBox.Show("there is no trip selected");

                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

            try
            {
                if (viajeSelect.id != 0)
                {
                    if (viajeSelect.estado == "pendiente")
                    {
                        oBLLproducto.Sumar_cantidad_pallets(viajeSelect.cantidad_Pallets, viajeSelect.producto.id);

                        BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.producto.cliente, "Se cancelo el viaje con id: "+ viajeSelect.id+ " se cancelo", DateTime.Now, 2);
                        oBLLmensaje.GuardarMensaje(mensaje);
                        Chat.usuarioAconectar = viajeSelect.producto.cliente;
                        Chat form = new Chat();

                        form.button2.Visible = true;
                        form.userControl11.Texts = "Se cancelo debido a que:";
                        // form.label1.Text = viajeSelect.camion.conductor.user;
                        form.Show();
                        oBLLviaje.ActualizarEstado(viajeSelect.id, "Cancelado", null);

                    }
                }
                else
                {
                    MessageBox.Show("there is no trip selected");

                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void cellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;


                    if (viaje != null && viaje.producto != null)
                    {

                        e.Value = viaje.producto.nombre;
                    }
                }
                if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;


                    if (viaje != null && viaje.camion != null)
                    {

                        e.Value = viaje.camion.patente + " " + viaje.camion.conductor.user;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void cellFormatingDataGrid2(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView2.Columns["conductor"].Index && e.RowIndex >= 0)
                {
                    BECamion camion = dataGridView2.Rows[e.RowIndex].DataBoundItem as BECamion;


                    if (camion != null && camion.conductor != null)
                    {

                        e.Value = camion.conductor.user;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cellFormattingDatagrid1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;


                    if (viaje != null && viaje.producto != null)
                    {

                        e.Value = viaje.producto.nombre;
                    }
                }
                if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;


                    if (viaje != null && viaje.camion != null)
                    {

                        e.Value = viaje.camion.patente + " " + viaje.camion.conductor.user;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cellFormatingDatagrid2(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
               
                if (e.ColumnIndex == dataGridView2.Columns["conductor"].Index && e.RowIndex >= 0)
                {
                    BEUsuario conductor = dataGridView2.Rows[e.RowIndex].DataBoundItem as BEUsuario;


                    if (conductor != null)
                    {

                        e.Value = conductor.user;
                   }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (label5.Visible == false)
            {
                label5.Visible = true;
            }
            if (label6.Visible == false)
            {
                label6.Visible = true;
            }
            label6.Text = viajeSelect.id + " " + viajeSelect.producto.nombre;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
            if (label7.Visible == false)
            {
                label7.Visible = true;
            }
            if (label8.Visible == false)
            {
                label8.Visible = true;
            }
            label7.Text = camionSelect.patente;
        }

        public void CambiarIdioma(Idioma Idioma)
        {
            //  throw new NotImplementedException();
            traducir();
        }

        void traducir()
        {
            try
            {
                Idioma Idioma = null;

                if (SessionManager.TraerUsuario())
                    Idioma = SessionManager.GetInstance.idioma;
                if (Idioma.Nombre == "Ingles")
                {
                    VolverAidiomaOriginal();
                }
                else
                {
                    BLL.BLLTraductor Traductor = new BLL.BLLTraductor();


                    traducciones = Traductor.obtenertraducciones(Idioma);
                    List<string> Lista = new List<string>();
                    Lista = Traductor.obtenerIdiomaOriginal();
                    if (traducciones.Values.Count != Lista.Count)
                    {

                    }
                    else
                    {
                        RecorrerPanel(panel1, 1);
                        RecorrerPanel(this, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void RecorrerPanel(Control panel, int v)
        {
            foreach (Control control in panel.Controls)
            {
                if (v == 1)
                {

                    if (control.Tag != null && traducciones.ContainsKey(control.Tag.ToString()))
                    {
                        control.Text = traducciones[control.Tag.ToString()].texto;
                    }
                }
                else
                {
                    if (control.Tag != null && palabras.Contains(control.Tag.ToString()))
                    {
                        string traduccion = palabras.Find(p => p.Equals(control.Tag.ToString()));
                        control.Text = traduccion;
                    }
                }

            }
        }

        void VolverAidiomaOriginal()
        {
            try
            {
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                palabras = Traductor.obtenerIdiomaOriginal();

                RecorrerPanel(panel1, 2);
                RecorrerPanel(this, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
