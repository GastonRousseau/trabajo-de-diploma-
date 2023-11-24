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
using servicios.ClasesMultiLenguaje;
namespace UI
{
    public partial class ViajesCliente : Form,IdiomaObserver
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
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();
        BECamion camionSelect = new BECamion();
        private void ViajesCliente_Load(object sender, EventArgs e)
        {
            label1.Visible =false;
            barrarProgreso1.Visible = false;
            metroLabel2.Visible = false;
            cargarCombo();
            Observer.agregarObservador(this);
            //Listar();
            traducir();
        }
        void Listar(string producto, int pag)
        {

            try
            {
                viajes = oBLLviaje.Traer_Viajes_Clientes(SessionManager.GetInstance.Usuario.id, pag, NombreProducto);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                dataGridView1.Columns["id"].Visible = false;
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
        void cargarCombo()
        {
            try
            {
                List<string> productos = oBLLproducto.Producto_asignado_viaje(SessionManager.GetInstance.Usuario.id);
                foreach (string nombre in productos)
                {
                    metroComboBox1.Items.Add(nombre);
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

        private void formating(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.Value != null)
                {
                    BEProducto producto = (BEProducto)e.Value;
                    e.Value = producto.nombre;
                }
                if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.Value != null)
                {
                    BECamion camion = (BECamion)e.Value;
                    e.Value = camion.patente + " " + camion.conductor.user;
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

        private void metroButton3_Click(object sender, EventArgs e)
        {
          
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                BEViaje viajeSelect = ((BEViaje)dataGridView1.CurrentRow.DataBoundItem);
                if (viajeSelect.id != 0)
                {
                    if (viajeSelect.estado == "En proceso")
                    {
                        label1.Visible = true;
                        barrarProgreso1.Visible = true;
                        //progressBar1.Minimum = 0;
                        barrarProgreso1.MaximumValue = viajeSelect.cantidad_KM;
                        barrarProgreso1.ProgressValue = viajeSelect.Km_Recorridos;
                        metroLabel2.Visible = true;
                        metroLabel2.Text = (viajeSelect.Km_Recorridos + "/" + viajeSelect.cantidad_KM);
                    }
                    else
                    {
                        MessageBox.Show("The status of the travel must be pending, otherwise the action cannot be carried out.");
                    }

                }
                else
                {
                    MessageBox.Show("There is no travel selected");
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
                    if (viajeSelect.id != 0 && viajeSelect != null)
                    {
                        if (viajeSelect.estado == "pendiente")
                        {
                            oBLLviaje.ActualizarEstado(viajeSelect.id, "Cancelado", null);
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
                            MessageBox.Show("The status of the travel must be pending, otherwise the action cannot be carried out.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("There is no travel selected");
                    }
                }
                else
                {
                    MessageBox.Show("there are no trips to accomplish this task");
                }
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        private Chat chatAbierto = null;
        private void metroButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
                    if (viajeSelect.id != 0)
                    {

                        Chat.usuarioAconectar = viajeSelect.camion.conductor;

                        // form.label1.Text = viajeSelect.camion.conductor.user;
                        Chat form = new Chat();
                        if (chatAbierto != null)
                        {
                            chatAbierto.Close();
                        }
                        chatAbierto = form;
                        chatAbierto.button2.Visible = true;
                        chatAbierto.Show();

                    }
                    else
                    {
                        MessageBox.Show("There is no travel selected");
                    }
                }
                else
                {
                    MessageBox.Show("does not have any travel");
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

        private void metroButton5_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;

                    if (viajeSelect.id != 0 && viajeSelect.estado == "pendiente" && viajeSelect != null)
                    {
                        this.Size = new Size(1081, 311);
                        panel3.Visible = true;

                    }
                    else
                    {
                        MessageBox.Show("There is no travel selected or the trip you selected does not have a pending status");
                    }
                }
                else
                {
                    MessageBox.Show("there are no trips to accomplish this task");
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
            if (dateTimePicker1.Value != null&& viajeSelect!=null&&viajeSelect.estado=="pendiente"&&dateTimePicker1.Value>DateTime.Now)
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = oBLLcamion.Camiones_Disponibles(dateTimePicker1.Value, viajeSelect.cantidad_Pallets);
            }
            else 
            {
                MessageBox.Show("An error occurred, it may be that the trip status is not pending");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
            //viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            try
            {
                viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
                
                if (camionSelect.patente!=null)
                {
                    // camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                    if (camionSelect != null && viajeSelect != null && dateTimePicker1.Value != null &&dateTimePicker1.Value>DateTime.Now)
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
                            BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "Se postargo el viaje" + viajeSelect.id + "a la fecha " + dateTimePicker1.Value, DateTime.Now, 2);
                            oBLLmensaje.GuardarMensaje(mensaje);
                        }

                        viajeSelect.fecha = dateTimePicker1.Value;
                        oBLLviaje.Modifica_Viaje(viajeSelect);


                        MessageBox.Show("The travel date was postponed to the day " + dateTimePicker1.Value);
                        panel3.Visible = false;
                        this.Size = new Size(826, 311);
                        Listar(null, 1);
                    }
                    else
                    {
                        MessageBox.Show("There was an error when trying to postpone the trip");
                    }
                }
                else
                {
                    MessageBox.Show("no truck was selected");
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

        public void CambiarIdioma(Idioma Idioma)
        {
            // throw new NotImplementedException();
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
                        RecorrerPanel(panel2, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void RecorrerPanel(Panel panel, int v)
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
                RecorrerPanel(panel2, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
