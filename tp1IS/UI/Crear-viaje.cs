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
using servicios;
using servicios.ClasesMultiLenguaje;
namespace UI
{
    public partial class Crear_viaje : Form,IdiomaObserver
    {
        public Crear_viaje()
        {
            InitializeComponent();
            oBLLcamion = new BLLCamion();
            oBLLproducto = new BLLProducto();
            oBLLviaje = new BLLviaje();
            this.Size = new Size(702, 432);
            panel3.Visible = false;
            label6.Visible = false;
            
        }
        //int costo = 0;
        BLLCamion oBLLcamion;
        BLLProducto oBLLproducto;
        BLLviaje oBLLviaje;
        validaciones validar = new validaciones();
        //BEViaje viaje = new BEViaje();
        BEViaje viaje=new BEViaje();
        BEProducto productoSelect = new BEProducto();
        BECamion camionSelect = new BECamion();
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();

        int costo;
        private void Crear_viaje_Load(object sender, EventArgs e)
        {
            Listar();
            Observer.agregarObservador(this);
            traducir();
        }
        void Listar()
        {
            List<BEProducto> Productos = oBLLproducto.ListarProductos(SessionManager.GetInstance.Usuario.id);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Productos;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Cliente"].Visible = false;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {

            try
            {
                int error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(dateTimePicker1, "");
                errorProvider1.SetError(textBox4, "");
                if (metroDateTime1.Value == null)
                {
                    error++;
                    errorProvider1.SetError(dateTimePicker1, "no date was chosen");
                }
                if (textBox4.Text == string.Empty || !validar.id(textBox4.Text))
                {
                    error++;
                    errorProvider1.SetError(textBox4, "the number of pallets was not written");
                }
                if (error == 0)
                {
                    DateTime fecha = metroDateTime1.Value;
                    int pallets = Convert.ToInt32(textBox4.Text);
                    List<BECamion> camiones = oBLLcamion.Camiones_Disponibles(fecha, pallets);
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = camiones;
                    dataGridView2.Columns["id"].Visible = false;
                    dataGridView2.Columns["Conductor"].Visible = false;
                    if (camiones.Count == 0)
                    {
                        MessageBox.Show("There are no trucks available according to the data you entered");
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
       // Pago form = new Pago();
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(textBox2, "");
                errorProvider1.SetError(textBox3, "");
                errorProvider1.SetError(textBox4, "");
                errorProvider1.SetError(textBox6, "");
                errorProvider1.SetError(textBox7, "");
                errorProvider1.SetError(dataGridView1, "");
                errorProvider1.SetError(dataGridView2, "");
                errorProvider1.SetError(dateTimePicker1, "");
                if (dataGridView1.RowCount > 0)
                {
                    productoSelect = (BEProducto)dataGridView1.CurrentRow.DataBoundItem;

                }
                else
                {
                    MessageBox.Show("must have products to take a trip");
                    errorProvider1.SetError(dataGridView1, "error");
                    error++;
                }
                if (dataGridView2.RowCount > 0)
                {
                    camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                }
                else
                {
                    MessageBox.Show("you must look for available trucks first");
                    errorProvider1.SetError(dataGridView2, "error");
                    error++;
                }
                if (textBox1.Text == string.Empty || !validar.usuario(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "data is entered incorrectly");
                    error++;
                }
                if (textBox6.Text == string.Empty || !validar.numero(textBox6.Text))
                {
                    errorProvider1.SetError(textBox6, "data is entered incorrectly");
                    error++;
                }
                if (textBox7.Text == string.Empty || !validar.numero(textBox7.Text))
                {
                    errorProvider1.SetError(textBox7, "data is entered incorrectly");
                    error++;
                }

                if (textBox2.Text == string.Empty || !validar.usuario(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "data is entered incorrectly");
                    error++;
                }
                if (textBox3.Text == string.Empty || !validar.numero(textBox3.Text))
                {
                    errorProvider1.SetError(textBox3, "data is entered incorrectly");
                    error++;
                }
                else
                {
                    if (Convert.ToInt32(textBox3.Text) > 4000)
                    {
                        MessageBox.Show("trips of more than 4000 km are not made");
                    }
                }
                if (textBox4.Text == string.Empty || Convert.ToInt32(textBox4.Text) > 40 || !validar.numero(textBox4.Text))
                {
                    errorProvider1.SetError(textBox4, "data is entered incorrectly");
                    error++;
                }
                if (dateTimePicker1.Value == null || dateTimePicker1.Value > DateTime.Now.AddYears(1))
                {
                    errorProvider1.SetError(dateTimePicker1, "The date is null or greater than one year");
                }
                if (camionSelect == null)
                {
                    errorProvider1.SetError(dataGridView2, "data is entered incorrectly");
                    error++;
                }
                if (productoSelect == null)
                {
                    errorProvider1.SetError(dataGridView1, "data is entered incorrectly");
                    error++;
                }
                if (productoSelect.Estado == "desactivado")
                {
                    error++;
                    MessageBox.Show("You cannot request a travel with products that have a deactivated status");
                }
                else
                {
                    if (productoSelect.CantPallets < 0)
                    {
                        error++;
                        errorProvider1.SetError(dataGridView1, "data is entered incorrectly");
                    }
                    else
                    {
                        if (!validar.id(textBox4.Text))
                        {
                            MessageBox.Show("The number of pallets was not written correctly");
                            error++;
                        }
                        else
                        {
                            if (productoSelect.CantPallets - Convert.ToInt32(textBox4.Text) < 0)
                            {

                                MessageBox.Show("you need more pallets to request the trip");
                                error++;
                            }
                        }
                    }
                }
                if (error == 0)
                {

                    viaje.partida = textBox1.Text;
                    viaje.destino = textBox2.Text;
                    viaje.cantidad_KM = Convert.ToInt32(textBox3.Text);
                    viaje.fecha = metroDateTime1.Value;
                    viaje.camion = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                    viaje.producto = (BEProducto)dataGridView1.CurrentRow.DataBoundItem;
                    viaje.cantidad_Pallets = Convert.ToInt32(textBox4.Text);
                    viaje.estado = "pendiente";
                    oBLLproducto.actualizar_Cantidad_Pallets(viaje.cantidad_Pallets, viaje.producto.id);
                    Listar();
                    costo = oBLLviaje.calcularCoste(viaje.cantidad_KM, viaje.cantidad_Pallets);
                    textBox5.Text = Convert.ToString(costo);
                    this.Size = new Size(936, 432);
                    panel3.Visible = true;
                
                }
                else
                {
                    MessageBox.Show("there was a problem");
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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "");
                errorProvider1.SetError(textBox4, "");
                if (textBox3.Text == string.Empty || !validar.id(textBox3.Text))
                {
                    errorProvider1.SetError(textBox3, "data is entered incorrectly");
                    error++;
                }
                if (textBox4.Text == string.Empty || !validar.id(textBox4.Text))
                {
                    errorProvider1.SetError(textBox4, "data is entered incorrectly");
                    error++;
                }
                if (error == 0)
                {
                    MessageBox.Show("El costo del viaje sera de $" + oBLLviaje.calcularCoste(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text)));
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            try
            {
                int pago; DateTime fechaVencimiento; int codigoSeguridad; string NombreTitularTarjeta;
                int error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox10, "");
                errorProvider1.SetError(textBox11, "");
                errorProvider1.SetError(textBox12, "");
                errorProvider1.SetError(dateTimePicker1, "");
                if (textBox12.Text == string.Empty || !validar.numero(textBox12.Text))
                {
                    errorProvider1.SetError(textBox12, "Error when entering data");
                    error++;
                }
                if (textBox11.Text == string.Empty || !validar.numero(textBox11.Text))
                {
                    errorProvider1.SetError(textBox11, "Error when entering data");
                    error++;
                }
                if (textBox10.Text == string.Empty || !validar.NombreYapellido(textBox10.Text))
                {
                    errorProvider1.SetError(textBox10, "Error when entering data");
                    error++;
                }
                if (dateTimePicker1.Value == null || dateTimePicker1.Value < DateTime.Now)
                {
                    errorProvider1.SetError(dateTimePicker1, "Expired card");
                    error++;
                }

                if (error == 0)
                {
                    MessageBox.Show("The trip was paid correctly");
                    oBLLviaje.guardarViaje(viaje);
                    this.Size = new Size(702, 432);
                    panel3.Visible = false;
                    costo = 0;
                    LimpiarText();

                }
                else
                {
                    MessageBox.Show("There was an error when paying, the trip was not saved");
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

        void LimpiarText()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            LimpiarText();
            this.Size = new Size(702, 432);
            panel3.Visible = false;
            costo = 0;
            viaje = null;
            MessageBox.Show("the operation was canceled");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
            label6.Text = "the truck with the patent was selected " + camionSelect.patente;
            label6.Visible = true;
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
                        RecorrerPanel(panel3, 1);
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
                RecorrerPanel(panel3, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
