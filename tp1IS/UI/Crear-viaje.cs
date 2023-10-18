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
namespace UI
{
    public partial class Crear_viaje : Form
    {
        public Crear_viaje()
        {
            InitializeComponent();
            oBLLcamion = new BLLCamion();
            oBLLproducto = new BLLProducto();
            oBLLviaje = new BLLviaje();
            this.Size = new Size(702, 432);
            panel3.Visible = false;

        }
        //int costo = 0;
        BLLCamion oBLLcamion;
        BLLProducto oBLLproducto;
        BLLviaje oBLLviaje;
        validaciones validar = new validaciones();
        //BEViaje viaje = new BEViaje();
        BEViaje viaje=new BEViaje();
        
        int costo;
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
            if (camiones.Count == 0)
            {
                MessageBox.Show("There are no trucks available according to the data you entered"); 
            }
        }
       // Pago form = new Pago();
        private void metroButton1_Click(object sender, EventArgs e)
        {
            int error = 0;
            BEProducto productoSelect=(BEProducto)dataGridView1.CurrentRow.DataBoundItem;
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
            if (textBox1.Text == string.Empty||!validar.usuario(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "");
                error++;   
            }
            if (textBox6.Text == string.Empty||!validar.numero(textBox6.Text))
            {
                errorProvider1.SetError(textBox6, "");
                error++;
            }
            if (textBox7.Text == string.Empty||!validar.numero(textBox7.Text))
            {
                errorProvider1.SetError(textBox7, "");
                error++;
            }

            if (textBox2.Text == string.Empty||!validar.usuario(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "");
                error++;
            }
            if (textBox3.Text == string.Empty||Convert.ToInt32(textBox3.Text)>4000||!validar.numero(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "");
                error++;
            }
            if (textBox4.Text == string.Empty||Convert.ToInt32(textBox4.Text)>40||!validar.numero(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "");
                error++;
            }
            if (dateTimePicker1.Value == null || dateTimePicker1.Value > DateTime.Now.AddYears(1))
            {
                errorProvider1.SetError(dateTimePicker1, "The date is null or greater than one year");
            }
            if ((BECamion)dataGridView2.CurrentRow.DataBoundItem == null)
            {
                errorProvider1.SetError(dataGridView2, "");
                error++;
            }
            if ((BEProducto)dataGridView1.CurrentRow.DataBoundItem == null || productoSelect.CantPallets<=0 ||productoSelect.CantPallets-Convert.ToInt32(textBox4.Text)<=0)
            {
                errorProvider1.SetError(dataGridView1, "");
                error++;
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
             //   oBLLviaje.guardarViaje(viaje);
              //  MessageBox.Show("Se creo el viaje");
                oBLLproducto.actualizar_Cantidad_Pallets(viaje.cantidad_Pallets, viaje.producto.id);
                Listar();
                costo = oBLLviaje.calcularCoste(viaje.cantidad_KM, viaje.cantidad_Pallets);
                textBox5.Text = Convert.ToString(costo);
                // Pago.viajeCliente = viaje;
                // Pago form = new Pago(viaje);
                //  form.Show();
                this.Size = new Size(936, 432);
                panel3.Visible = true;
               /* form.PagoRealizado += (sender2, args) =>
                {
                    // Manejar el evento aquí, por ejemplo, instanciar el viaje
                    //InstanciarViaje();
                    oBLLproducto.actualizar_Cantidad_Pallets(viaje.cantidad_Pallets, viaje.producto.id);
                    oBLLviaje.guardarViaje(viaje);
                };*/
                //form.Show();
                //actualizar cantidad de pallets;
            }
            else
            {
                MessageBox.Show("Hubo un error La puta madre");
            }
          
        }
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            int error = 0;
            errorProvider1.Clear();
            errorProvider1.SetError(textBox3, "");
            errorProvider1.SetError(textBox4, "");
            if (textBox3.Text == string.Empty || !validar.id(textBox3.Text))
            {
                errorProvider1.SetError(textBox3,"");
            }
            if (textBox4.Text == string.Empty || !validar.id(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "");
            }
            if (error == 0)
            {
                //  int KM = Convert.ToInt32(textBox3.Text);
                //  int CantPalets = Convert.ToInt32(textBox4.Text);
                MessageBox.Show("El costo del viaje sera de $" +oBLLviaje.calcularCoste(Convert.ToInt32(textBox3.Text),Convert.ToInt32(textBox4.Text)));
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            int pago;DateTime fechaVencimiento;int codigoSeguridad;string NombreTitularTarjeta;
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
            if (textBox11.Text == string.Empty||!validar.numero(textBox11.Text))
            {
                errorProvider1.SetError(textBox11, "Error when entering data");
                error++;
            }
            if (textBox10.Text == string.Empty||!validar.usuario(textBox10.Text))
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
    }
}
