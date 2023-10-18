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
using Negocio;
namespace UI
{
    public partial class CrearChofer : Form
    {
        public CrearChofer()
        {
            InitializeComponent();
            oLog = new BLLUsuario();
            oBit = new BLLBitacora();
            oUsuario = new BEUsuario();
            buscar(null, 1);
            
        }
        BLLUsuario oLog;
        BLLBitacora oBit;
        validaciones validar = new validaciones();
        BLL.BLLDv OVd = new BLL.BLLDv();
        BEUsuario oUsuario;
        int pag;
        string nombre;
        IList<BEUsuario> usuarios;
        private void CrearChofer_Load(object sender, EventArgs e)
        {
            pag = 1;
            metroButton2.Enabled = false;
        }
        public void buscar(string nombre, int pag)
        {
            try
            {
                usuarios = oLog.GetAllConductores(nombre, pag);
                if (usuarios.Count == 0) { metroButton3.Enabled = false; }
                else { metroButton3.Enabled = true; }
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
        void listar()
        {
            //hacer una query que me traiga los usuario solo con el rol de chofer
        }
        private void metroButton4_Click(object sender, EventArgs e)
        {
            try
            {

                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(textBox2, "");
                errorProvider1.SetError(textBox3, "");
                errorProvider1.SetError(textBox4, "");
                errorProvider1.SetError(textBox5, "");
                if (textBox1.Text == string.Empty || !validar.usuario(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "You should enter a name without special characters");
                    error++;

                }
                if (textBox2.Text == string.Empty || !validar.contrasena(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "You should enter a password with at least 1 number and 5 letters");
                    error++;
                }

                if (textBox3.Text == string.Empty || !validar.id(textBox3.Text))
                {
                    errorProvider1.SetError(textBox3, "You should enter an id with 1 to 9 numbers");
                    error++;
                }
                if (textBox4.Text == string.Empty || !validar.calle(textBox4.Text))
                {
                    errorProvider1.SetError(textBox4, "You should enter a street name wirhout special characters");
                    error++;
                }
                if (textBox5.Text == string.Empty || !validar.id(textBox5.Text))
                {
                    errorProvider1.SetError(textBox5, "You should enter an street number  with 1 to 9 numbers");
                    error++;
                }
                if (metroDateTime2.Value == null || metroDateTime2.Value > DateTime.Now)
                {
                    errorProvider1.SetError(metroDateTime2, "You should enter a date that is not later than todaymatias");
                    error++;
                }

                if (error == 0)
                {
                    if (oLog.usuario_existente(Convert.ToInt32(textBox3.Text))) MessageBox.Show("There is a user with that id already", "ERROR");
                    else
                    {
                        string adress = textBox4.Text + " " + textBox5.Text;
                        oUsuario = new BEUsuario(textBox1.Text, textBox2.Text, Convert.ToInt32(textBox3.Text), metroDateTime2.Value.ToString(), adress);

                        if (oLog.crear_conductor(oUsuario))
                        {
                            var accion = "creo el usuario conductor" + textBox1.Text;
                            oBit.guardar_accion(accion, 2);
                            MetroMessageBox.Show(this, "conductor user created");
                          //  this.Hide();
                            List<string> ListaDVU = OVd.BuscarDVUsuarios();
                            string DVS = servicios.GenerarVD.generarDigitoVS(ListaDVU);
                            OVd.actualizarDV(DVS);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "There has been an error, try changing the username");
                        }
                    }

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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            BEUsuario UsuarioSelect =(BEUsuario)dataGridView1.CurrentRow.DataBoundItem;

            if (UsuarioSelect != null)
            {
                oLog.eliminar_usuario(UsuarioSelect.id);
            }

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            pag -= 1;
            metroButton2.Enabled = true;
            if (pag <= 1) metroButton2.Enabled = false;
            if (pag > 0) buscar(nombre, pag);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroButton2.Enabled = true;
            pag += 1;
            buscar(nombre, pag);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
