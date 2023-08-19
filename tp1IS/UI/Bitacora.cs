using abstraccion;
using BE;
using MetroFramework;
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
    public partial class Bitacora : MetroFramework.Forms.MetroForm,IdiomaObserver
    {
        public Bitacora()
        {
            InitializeComponent();
            inicializar_filtros();
        }
        public int pag;
        void inicializar_filtros()
        {
            textBox1.Text = string.Empty;
            List<string> tipos = new List<string>() { "error", "information"};
            metroComboBox1.DataSource = tipos;
            metroComboBox1.SelectedIndex = -1;
            filters = new BEBitacoraFilters() { From = DateTime.Now, To = DateTime.Now, Username = null, Like = null, Type = null };
        }
        BLLBitacora oBit = new BLLBitacora();
        validaciones validar = new validaciones();
        IList<IBitacora> listBitacora;
        IBitacoraFilters filters = new BEBitacoraFilters() { From = DateTime.Now, To = DateTime.Now };
        private void Bitacora_Load(object sender, EventArgs e)
        {
            try
            {
                listBitacora = oBit.GetAll(filters, 1);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listBitacora;
                pag = 1;
                button1.Enabled = false;
                ListarIdiomas();
                Traducir();
                servicios.Observer.agregarObservador(this);

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

        private void Bitacora_FormClosing(object sender, EventArgs e)
        {
            try
            {
                servicios.Observer.eliminarObservador(this);

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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Observer.eliminarObservador(this);
        }
        BLLUsuario oLog = new BLLUsuario();
        public void buscar(int pag)
        {
            try
            {
                listBitacora = oBit.GetAll(filters, pag);
                if (listBitacora.Count == 0) { button2.Enabled = false; }
                else { button2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listBitacora;

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
        private void Apply_Click(object sender, EventArgs e)
        {
            try
            {
                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(metroDateTime1, "");
                errorProvider1.SetError(metroDateTime2, "");
                errorProvider1.SetError(metroComboBox1, "");
                errorProvider1.SetError(Apply, "");
                string name = null;
                Nullable<DateTime> from = null;
                Nullable<DateTime> to = null;
                Nullable <int> type = null;
                if (metroDateTime1.Value == null && metroDateTime2.Value == null && metroComboBox1.SelectedIndex == -1 && textBox1.Text == string.Empty)
                {
                    errorProvider1.SetError(Apply, "You should pick a filter");
                    error++;
                }
                if(textBox1.Text != string.Empty)
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
                        name = textBox1.Text;
                    }
                }
                if(metroDateTime2.Value != null)
                {
                    to = metroDateTime2.Value;
                }
                if (metroDateTime1.Value != null)
                {
                    from = metroDateTime1.Value;
                }
                if (metroComboBox1.SelectedIndex != -1)
                {
                    if (metroComboBox1.SelectedItem.ToString() == "error") type = 1;
                    else type = 2;
                }
                if (error == 0)
                {
                    filters = new BEBitacoraFilters() { From = from, To = to, Username = name, Type= type };
                    buscar(1);
                    pag = 1;
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
            try
            {
            inicializar_filtros();
            buscar(1);
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
            button1.Enabled = true;
            pag += 1;
            buscar(pag);

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                pag -= 1;
                button1.Enabled = true;
                if (pag <= 1) button1.Enabled = false;
                if(pag > 0) buscar(pag);

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
            textBox1.Text = string.Empty;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroComboBox1.SelectedIndex = -1;
        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

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
                if (Idioma.Nombre=="Ingles")
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
                        if (metroLabel2.Tag != null && traducciones.ContainsKey(metroLabel2.Tag.ToString()))
                        {
                            this.metroLabel2.Text = traducciones[metroLabel2.Tag.ToString()].texto;
                        }
                        if (metroLabel3.Tag != null && traducciones.ContainsKey(metroLabel3.Tag.ToString()))
                        {
                            this.metroLabel3.Text = traducciones[metroLabel3.Tag.ToString()].texto;
                        }
                        if (metroLabel4.Tag != null && traducciones.ContainsKey(metroLabel4.Tag.ToString()))
                        {
                            this.metroLabel4.Text = traducciones[metroLabel4.Tag.ToString()].texto;
                        }
                        if (metroLabel5.Tag != null && traducciones.ContainsKey(metroLabel5.Tag.ToString()))
                        {
                            this.metroLabel5.Text = traducciones[metroLabel5.Tag.ToString()].texto;
                        }

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
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
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
                if (metroLabel2.Tag != null && palabras.Contains(metroLabel2.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel2.Tag.ToString()));
                    this.metroLabel2.Text = traduccion;
                }

                if (metroLabel3.Tag != null && palabras.Contains(metroLabel3.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel3.Tag.ToString()));
                    this.metroLabel3.Text = traduccion;
                }
                if (metroLabel4.Tag != null && palabras.Contains(metroLabel4.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel4.Tag.ToString()));
                    this.metroLabel4.Text = traduccion;
                }
                if (metroLabel5.Tag != null && palabras.Contains(metroLabel5.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel5.Tag.ToString()));
                    this.metroLabel5.Text = traduccion;
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
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
