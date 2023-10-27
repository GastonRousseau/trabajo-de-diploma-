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
using servicios;
namespace UI
{
    public partial class Camiones : Form
    {
        public Camiones()
        {
            InitializeComponent();
            oLog = new BLLUsuario();
            oBLLCamion = new BLLCamion();
            buscar(null, 1);
            buscar2(null, 1);
        }
        BLLUsuario oLog;
        BLLCamion oBLLCamion;
        int PagConductores;
        int PagCamiones;
        string nombreConductor;
        string patente;
        IList<BEUsuario> usuarios;
        IList<BECamion> camiones;
        BLLBitacora oBit = new BLLBitacora();
        validaciones validacion = new validaciones();
        private void Camiones_Load(object sender, EventArgs e)
        {
            PagCamiones = 1;
            PagConductores = 1;
            metroButton3.Enabled = false;
            metroButton7.Enabled = false;
            
        }

        public void buscar(string nombre, int pag)
        {
            try
            {
                usuarios = oLog.GetAllConductores(nombre, pag);
                if (usuarios.Count == 0) { metroButton4.Enabled = false; }
                else { metroButton4.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = usuarios;
                dataGridView1.Columns["permisos"].Visible = false;
                dataGridView1.Columns["rol"].Visible = false;
                dataGridView1.Columns["DV"].Visible = false;
            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
        }

        public void buscar2(string patente,int pag)
        {
            try
            {
                camiones = oBLLCamion.TraerCamiones(patente, pag);
                if (camiones.Count == 0) { metroButton8.Enabled = false; }
                else { metroButton8.Enabled = true; }
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = camiones;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void metroButton3_Click(object sender, EventArgs e)
        {
            PagConductores -= 1;
            metroButton3.Enabled = true;
            if (PagConductores <= 1) metroButton3.Enabled = false;
            if (PagConductores > 0) buscar(nombreConductor, PagConductores);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            metroButton3.Enabled = true;
            PagConductores += 1;
            buscar(nombreConductor, PagConductores);
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                metroComboBox2.Items.Clear();
                if (metroComboBox1.SelectedItem.ToString() == "Liviano")
                {
                    metroComboBox2.Items.Add("6");
                    metroComboBox2.Items.Add("10");
                }
                if (metroComboBox1.SelectedItem.ToString() == "Mediano")
                {
                    metroComboBox2.Items.Add("12");
                    metroComboBox2.Items.Add("18");
                    metroComboBox2.Items.Add("20");
                }
                if (metroComboBox1.SelectedItem.ToString() == "Pesado")
                {
                    metroComboBox2.Items.Add("24");
                    metroComboBox2.Items.Add("30");
                    metroComboBox2.Items.Add("40");

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
            try
            {

                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                if (textBox1.Text == string.Empty || !validacion.patente(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "You should enter a name without special characters");
                    error++;

                }
                if (metroComboBox1.SelectedIndex == -1)
                {
                    errorProvider1.SetError(metroComboBox1, "you not select item");
                    error++;
                }
                if (metroComboBox2.SelectedIndex == -1)
                {
                    errorProvider1.SetError(metroComboBox2, "you not select item");
                    error++;
                }
                if (error == 0)
                {
                    if (oBLLCamion.Verificar_Patente(textBox1.Text)) MessageBox.Show("There is a user with that id already", "ERROR");
                    else
                    {
                        BECamion camion = new BECamion();
                        camion.patente = textBox1.Text;
                        camion.tipo = metroComboBox1.SelectedItem.ToString();
                        camion.capacidad_Pallets = Convert.ToInt32(metroComboBox2.SelectedItem.ToString());
                      //  BEUsuario UsuarioSelect = (BEUsuario)dataGridView1.CurrentRow.DataBoundItem;
                     //   if (UsuarioSelect != null)
                      //  {
                      //      camion.conductor = UsuarioSelect;
                            oBLLCamion.CrearCamion(camion);
                        //  }
                        /*/ else
                         {
                             errorProvider1.SetError(dataGridView1,"you not select driver");
                         }*/
                        buscar2(null,1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {
                int error2 = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(dataGridView1, "");
                errorProvider1.SetError(dataGridView2, "");
                BECamion camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                BEUsuario UsuarioSelect = (BEUsuario)dataGridView1.CurrentRow.DataBoundItem;
                if (camionSelect == null)
                {
                    errorProvider1.SetError(dataGridView2, "Select truck");
                    error2++;
                }
                if (UsuarioSelect == null)
                {
                    errorProvider1.SetError(dataGridView1, "Select driver");
                    error2++;
                }
                if (error2 == 0)
                {
                    oBLLCamion.Unicr_ConductoryCamion(camionSelect.id, UsuarioSelect.id);
                    buscar2(null, 1);
                    MessageBox.Show("the driver "+UsuarioSelect.user +"joined the truck "+camionSelect.patente);
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
            try
            {
                var error2 = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(dataGridView2, "");
                BECamion camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                if (camionSelect == null)
                {
                    errorProvider1.SetError(dataGridView2, "Select truck");
                    error2++;
                }
                if (error2 == 0)
                {
                    oBLLCamion.Desvincular_ConductoryCamion(camionSelect.id);
                    buscar2(null, 1);
                    MessageBox.Show("the driver became separated from the truck " +camionSelect.patente);
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
            PagCamiones -= 1;
            metroButton7.Enabled = true;
            if (PagCamiones <= 1) metroButton7.Enabled = false;
            if (PagCamiones > 0) buscar2(patente,PagCamiones);
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            metroButton7.Enabled = true;
            PagCamiones += 1;
            buscar2(patente,PagCamiones);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                BECamion camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
                errorProvider1.Clear();
                errorProvider1.SetError(dataGridView2, "");
                if (camionSelect != null)
                {
                    oBLLCamion.BorrarCamion(camionSelect.id);
                }
                else
                {
                    errorProvider1.SetError(dataGridView2, "Select truck");
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

        private void metroButton9_Click(object sender, EventArgs e)
        {
            try
            {
                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(metroButton9, "");

                if (textBox2.Text != string.Empty)
                {
                    if (!validacion.usuario(textBox2.Text))
                    {
                        errorProvider1.SetError(textBox2, "The username should not have any special characters");
                        error++;
                     
                    }
                    else
                    {
                        nombreConductor = textBox2.Text;
                    }
                }
                if (error == 0)
                {
                    buscar(nombreConductor, 1);
                    PagConductores = 1;
                }

            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            try
            {
                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox3, "");
                errorProvider1.SetError(metroButton10, "");

                if (textBox3.Text != string.Empty)
                {
                    if (!validacion.patente(textBox3.Text))
                    {
                        errorProvider1.SetError(textBox3, "The patent should not have any special characters");
                        error++;

                    }
                    else
                    {
                        patente = textBox3.Text;
                    }
                }
                if (error == 0)
                {
                    buscar2(patente, 1);
                    PagCamiones = 1;
                }

            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void formatingCellDataGridCamiones(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView2.Columns["conductor"].Index && e.RowIndex >= 0)
                {
                    BECamion camion = dataGridView2.Rows[e.RowIndex].DataBoundItem as BECamion;

                    // Asegúrate de que la celda tenga un valor de Viaje no nulo
                    if (camion != null && camion.conductor != null)
                    {
                        // Configura el valor de la celda para mostrar el nombre del producto
                        e.Value = camion.conductor.user;
                    }
                    else
                    {
                        e.Value = "Sin conductor";
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
    }
}

