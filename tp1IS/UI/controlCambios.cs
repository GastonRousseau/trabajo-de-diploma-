using abstraccion;
using BE;
using BLL;
using MetroFramework;
using MetroFramework.Controls;
using Negocio;
using servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using servicios.ClasesMultiLenguaje;
using Patrones.Singleton.Core;

namespace UI
{
    public partial class Changes : MetroFramework.Forms.MetroForm,IdiomaObserver
    {
        public Changes()
        {
            InitializeComponent();
            buscar(null, 1);
            
        }
        validaciones validar = new validaciones();
        BLLUsuario oLog = new BLLUsuario();
        int pag;
        IList<BEUsuario> usuarios;
        BLLBitacora oBit = new BLLBitacora();
        string nombre;
        private void controlCambios_Load(object sender, EventArgs e)
        {
            pag = 1;
            button1.Enabled = false;
            ListarIdiomas();
            Traducir();
            servicios.Observer.agregarObservador(this);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Observer.eliminarObservador(this);
        }
        private void controlCambios_FormClosing(object sender, EventArgs e)
        {
            try
            {
                servicios.Observer.eliminarObservador(this);

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
        public void buscar(string nombre, int pag)
        {
            try
            {
                usuarios = oLog.GetAllHistorico(nombre, pag);
                if (usuarios.Count == 0) { button2.Enabled = false; }
                else { button2.Enabled = true; }
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
        private void Apply_Click(object sender, EventArgs e)
        {
            try
            {
                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(Apply, "");
                
                if (textBox1.Text != string.Empty)
                {
                    if (!validar.usuario(textBox1.Text))
                    {
                        errorProvider1.SetError(textBox1, "The username should not have any special characters");
                        error++;
                        if (oLog.username_existente(textBox1.Text))
                        {
                            errorProvider1.SetError(textBox1, "There are no users associated with that username");
                        }
                    }
                    else
                    {
                        nombre = textBox1.Text;
                    }
                }
                if (error == 0)
                {
                    buscar(nombre, 1);
                    pag = 1;
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

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                nombre = null;
                textBox1.Text = string.Empty;
                buscar(null, 1);
                pag = 1;
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = true;
                pag += 1;
                buscar(nombre, pag);
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pag -= 1;
                button1.Enabled = true;
                if (pag <= 1) button1.Enabled = false;
                if (pag > 0) buscar(nombre, pag);
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
            try
            {
                textBox1.Text = string.Empty;
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
        BLLDv OBLLdv = new BLLDv();

        public void actualizar_verificador()
        {
            try
            {
                string dv = servicios.GenerarVD.generarDigitoVS(oLog.BuscarUsuariosYgenerarDV());
                OBLLdv.actualizarDV(dv);
            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex){
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                if(dataGridView1.SelectedCells.Count == 1 )
                {
                    BEUsuario user = (BEUsuario)dataGridView1.CurrentRow.DataBoundItem;
                    if (user != null)
                    {
                        if (oLog.restaurar_usuario(user))
                        {
                            actualizar_verificador();
                            MetroMessageBox.Show(this, "user restored");
                        }
                        else MetroMessageBox.Show(this, "Error, try again");
                        buscar(null, 1);
                        pag = 1;
                    }
                    else
                    {
                        errorProvider1.SetError(dataGridView1, "select a user to restore");
                    }
                }
                else
                {
                    errorProvider1.SetError(dataGridView1, "select a user to restore");
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

        public void CambiarIdioma(Idioma Idioma)
        {
            Traducir();
            ListarIdiomas();
        }
        public void Traducir()
        {
            try
            {
                Idioma Idioma = null;

                if (SessionManager.TraerUsuario())
                    Idioma = SessionManager.GetInstance.idioma;
                if (Idioma.Nombre == "Ingles")
                {
                    VolverAlIdiomaOriginal();
                }
                else
                {
                    BLL.BLLTraductor Traductor = new BLL.BLLTraductor();


                    var traducciones = Traductor.obtenertraducciones(Idioma);
                    List<string> Lista = new List<string>();
                    Lista = Traductor.obtenerIdiomaOriginal();
                    if (traducciones.Values.Count != Lista.Count)
                    {

                    }
                    else
                    {
                        if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                        {
                            this.Text = traducciones[this.Tag.ToString()].texto;
                        }

                        if (metroButton1.Tag != null && traducciones.ContainsKey(metroButton1.Tag.ToString()))
                        {
                            this.metroButton1.Text = traducciones[metroButton1.Tag.ToString()].texto;
                        }
                        if (metroButton2.Tag != null && traducciones.ContainsKey(metroButton2.Tag.ToString()))
                        {
                            this.metroButton2.Text = traducciones[metroButton2.Tag.ToString()].texto;
                        }
                        if (metroButton3.Tag != null && traducciones.ContainsKey(metroButton3.Tag.ToString()))
                        {
                            this.metroButton3.Text = traducciones[metroButton3.Tag.ToString()].texto;
                        }
                        if (Apply.Tag != null && traducciones.ContainsKey(Apply.Tag.ToString()))
                        {
                            this.Apply.Text = traducciones[Apply.Tag.ToString()].texto;
                        }

                        if (metroLabel1.Tag != null && traducciones.ContainsKey(metroLabel1.Tag.ToString()))
                        {
                            this.metroLabel1.Text = traducciones[metroLabel1.Tag.ToString()].texto;
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
        public void ListarIdiomas()
        {
            try
            {
                comboBox1.Items.Clear();
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                var ListaIdiomas = Traductor.ObtenerIdiomas();

                foreach (Idioma idioma in ListaIdiomas)
                {
                    var traducciones = Traductor.obtenertraducciones(idioma);
                    List<string> Lista = new List<string>();
                    Lista = Traductor.obtenerIdiomaOriginal();
                    if (traducciones.Values.Count == Lista.Count)
                    {
                        comboBox1.Items.Add(idioma.Nombre);
                    }
                    else
                    {
                        if (idioma.Default == true)
                        {
                            comboBox1.Items.Add(idioma.Nombre);
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
        public void VolverAlIdiomaOriginal()
        {
            try
            {

                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                List<string> palabras = Traductor.obtenerIdiomaOriginal();




                if (this.Tag != null && palabras.Contains(this.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(this.Tag.ToString()));
                    this.Text = traduccion;
                }
                if (metroButton1.Tag != null && palabras.Contains(metroButton1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton1.Tag.ToString()));
                    this.metroButton1.Text = traduccion;
                }
                if (metroButton2.Tag != null && palabras.Contains(metroButton2.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton2.Tag.ToString()));
                    this.metroButton2.Text = traduccion;
                }
                if (metroButton3.Tag != null && palabras.Contains(metroButton3.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton3.Tag.ToString()));
                    this.metroButton3.Text = traduccion;
                }
                if (Apply.Tag != null && palabras.Contains(Apply.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(Apply.Tag.ToString()));
                    this.Apply.Text = traduccion;
                }

                if (metroLabel1.Tag != null && palabras.Contains(metroLabel1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel1.Tag.ToString()));
                    this.metroLabel1.Text = traduccion;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string idiomaSelec = comboBox1.SelectedItem.ToString();
                BLL.BLLTraductor traductor = new BLL.BLLTraductor();
                Idioma Oidioma = new Idioma();
                Oidioma = traductor.TraerIdioma(idiomaSelec);

                servicios.Observer.cambiarIdioma(Oidioma);

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
    }
}
