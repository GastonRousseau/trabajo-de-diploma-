using BE;
using Negocio;
using servicios;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MetroFramework;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Patrones.Singleton.Core;
using System.Drawing;
using System.ComponentModel;
using servicios.ClasesMultiLenguaje;
using BLL;
using System.Collections.Generic;

namespace UI
{
    public partial class SignIn : MetroFramework.Forms.MetroForm,IdiomaObserver
    {
        public SignIn()
        {
            InitializeComponent();
            groupBox1.Hide();
            if (!testConnection())
            {
                MetroMessageBox.Show(this, "Connection with database could not be stablished, please check with vendor");
                return;
            }
          
            this.MinimumSize = new System.Drawing.Size(472,455);
            anclado();
        }
        BLLUsuario oLog = new BLLUsuario();
        BLL.BLLDv OBLLDV = new BLLDv();
        BEUsuario oUsuraio;
        public bool ojoOpen;

        public bool testConnection()
        {
            try
            {
                if (oLog.testConnection()) return true;
                return false;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        public void limpiar()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox4.Text = null;
        }
        validaciones validar = new validaciones();
        public void SignIn_Load(object sender, EventArgs e)
        {
            ojoOpen = false;
            Bitmap imagen = new Bitmap(Application.StartupPath + @"\ojoCerrado.png");
            botonOjo.Image = imagen;
            textBox2.UseSystemPasswordChar = true;
            Observer.agregarObservador(this);
            ListarIdiomas();

        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Observer.eliminarObservador(this);
        }
        private void SigIn_FormClosing(object sender, EventArgs e)
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
        private void metroButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
           

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        BLLBitacora oBit = new BLLBitacora();
        BLLComposite oComp = new BLLComposite();
        
        public void logIn(string username)
        {
            try
            {

                if (oLog.es_activo(username))
                {
                    BEUsuario user=  oLog.buscar_usuario(username);
                    user.permisos = oComp.GetPermisosdeUser(user.id);
                    SessionManager u = SessionManager.GetInstance;
                    SessionManager.Login(user);
                    BE.DigitoV DV = new BE.DigitoV();
                    DV.DigitovBaseDeDatos = OBLLDV.BuscarDVS();
                    List<string> ListaDV = oLog.BuscarUsuariosYgenerarDV();
                    DV.DigitovActual = GenerarVD.generarDigitoVS(ListaDV);
                    oBit.guardar_logIn();
                    if (DV.DigitovBaseDeDatos == DV.DigitovActual)
                    {

                        if (SessionManager.tiene_permiso(5))
                        {
                           
                            AdminHome home = new AdminHome();
                            home.Show();
                            this.Hide();
                            servicios.Observer.eliminarObservador(this);

                        }
                        else
                        {
                           
                            UserHome home = new UserHome();
                            home.Show();
                            this.Hide();
                            servicios.Observer.eliminarObservador(this);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error de Digito Verificador");
                        if (SessionManager.tiene_permiso(5))
                        {

                            ErrorDV form = new ErrorDV();
                            form.Show();
                            this.Hide();
                            servicios.Observer.eliminarObservador(this);

                        }
                        else
                        {

                        }
                    }

                }
                else
                {
                    MetroMessageBox.Show(this, "The user is disabled");
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
        private void metroButton2_Click_1(object sender, EventArgs e)
        {
           try
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(textBox2, "");
                var error = 0;
                if (textBox1.Text == string.Empty || !validar.usuario(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "The username should not have special characters");
                    error++;

                }
                if (textBox2.Text == string.Empty || !validar.contrasena(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "You should enter a password with at least 1 number and 5 letters");
                    error++;
                }
                if(error == 0)
                {
                    oUsuraio = new BEUsuario(textBox1.Text, textBox2.Text);
                    if (oLog.validar(oUsuraio))
                    {
                        logIn(textBox1.Text);

                    }
                    else
                    {
                        MetroMessageBox.Show(this, "Incorrect user or password");
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

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            groupBox1.Show();
        }
       
        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(textBox2, "");
                errorProvider1.SetError(textBox4, "");
                errorProvider1.SetError(textBox3, "");
                errorProvider1.SetError(textBox5, "");
                if (textBox1.Text == string.Empty || !validar.usuario(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "The username should not have special character");
                    error++;

                }
                if (textBox2.Text == string.Empty || !validar.contrasena(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "You should enter a password with at least 1 number and 5 letters");
                    error++;
                }

                if (textBox4.Text == string.Empty || !validar.id(textBox4.Text))
                {
                    errorProvider1.SetError(textBox4, "You should enter an id with 1 to 9 numbers");
                    error++;
                }
                if (textBox3.Text == string.Empty || !validar.calle(textBox3.Text))
                {
                    errorProvider1.SetError(textBox3, "The street name should not have special character");
                    error++;
                }
                if (textBox5.Text == string.Empty || !validar.id(textBox5.Text))
                {
                    errorProvider1.SetError(textBox5, "You should enter an street number with 1 to 9 numbers");
                    error++;
                }

                if (metroDateTime1.Value == null || metroDateTime1.Value > DateTime.Now)
                {
                    errorProvider1.SetError(metroDateTime1, "You should enter a date that is not later than today");
                    error++;
                }

                if(error == 0)
                {
                    if (oLog.usuario_existente(Convert.ToInt32(textBox4.Text))) MessageBox.Show("There is an user with that id already", "ERROR");
                    else
                    {
                        
                        BE.DigitoV DV = new BE.DigitoV();
                        DV.DigitovBaseDeDatos = OBLLDV.BuscarDVS();
                        List<string> ListaDV = oLog.BuscarUsuariosYgenerarDV();
                        DV.DigitovActual = GenerarVD.generarDigitoVS(ListaDV);
                        if (DV.DigitovBaseDeDatos == DV.DigitovActual || DV.DigitovBaseDeDatos == string.Empty)
                        {
                            string adreess = textBox3.Text + " " + textBox5.Text;
                            oUsuraio = new BEUsuario(textBox1.Text, textBox2.Text, Convert.ToInt32(textBox4.Text), metroDateTime1.Value.ToString(),adreess);
                            oUsuraio.DV = GenerarVD.generarDigitoVU(oUsuraio);
                            if (oLog.cargar_usuario(oUsuraio))
                            {

                                actualizarDVSxnewUser(oUsuraio);
                                MetroMessageBox.Show(this, "User created");
                                limpiar();
                                logIn(oUsuraio.user);
                            }
                            else
                            {
                                limpiar();
                                MetroMessageBox.Show(this, "There has been an error, try changing the username");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Verifier digit error, user cannot be created");
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
       public void actualizarDVSxnewUser(BEUsuario Ousuario)
        {
          
            List<string> ListaDVU = oLog.BuscarUsuariosYgenerarDV();

            OBLLDV.actualizarDV(servicios.GenerarVD.generarDigitoVS(ListaDVU));
          
        }
      
        public void CambiarIdioma(Idioma Idioma)
        {
            traducir();
            ListarIdiomas();
        }
        private void botonOjo_Click(object sender, EventArgs e)
        {
            try
            {

            if (ojoOpen == true)
            {
                textBox2.UseSystemPasswordChar = true;
                Bitmap imagen = new Bitmap(Application.StartupPath + @"\ojoCerrado.png");
                botonOjo.Image = imagen;
                ojoOpen = false;

            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
                Bitmap imagen = new Bitmap(Application.StartupPath + @"\ojoabierto.png");
                botonOjo.Image = imagen;
                ojoOpen = true;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
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

        private void traducir()
        {
            try
            {
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                Idioma Idioma = null;

               
                Idioma=Traductor.TraerIdioma(comboBox1.SelectedItem.ToString());
                if (Idioma.Nombre == "Ingles")
                {
                    VolverAidiomaOriginal();
                }
                else
                {
                    


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
                        if (metroLabel2.Tag != null && traducciones.ContainsKey(metroLabel2.Tag.ToString()))
                        {
                            this.metroLabel2.Text = traducciones[metroLabel2.Tag.ToString()].texto;
                        }
                        if (groupBox1.Tag != null && traducciones.ContainsKey(groupBox1.Tag.ToString()))
                        {
                            this.groupBox1.Text = traducciones[groupBox1.Tag.ToString()].texto;
                        }
                        if (metroLabel4.Tag != null && traducciones.ContainsKey(metroLabel4.Tag.ToString()))
                        {
                            this.metroLabel4.Text = traducciones[metroLabel4.Tag.ToString()].texto;
                        }
                        if (metroLabel5.Tag != null && traducciones.ContainsKey(metroLabel5.Tag.ToString()))
                        {
                            this.metroLabel5.Text = traducciones[metroLabel5.Tag.ToString()].texto;
                        }
                        if (metroLabel3.Tag != null && traducciones.ContainsKey(metroLabel3.Tag.ToString()))
                        {
                            this.metroLabel3.Text = traducciones[metroLabel3.Tag.ToString()].texto;
                        }
                        if (metroLabel1.Tag != null && traducciones.ContainsKey(metroLabel1.Tag.ToString()))
                        {
                            this.metroLabel1.Text = traducciones[metroLabel1.Tag.ToString()].texto;
                        }
                        if (metroLabel6.Tag != null && traducciones.ContainsKey(metroLabel6.Tag.ToString()))
                        {
                            this.metroLabel6.Text = traducciones[metroLabel6.Tag.ToString()].texto;
                        }
                        if (metroLabel7.Tag != null && traducciones.ContainsKey(metroLabel7.Tag.ToString()))
                        {
                            this.metroLabel7.Text = traducciones[metroLabel7.Tag.ToString()].texto;
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

        private void VolverAidiomaOriginal()
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
               
                if (metroLabel3.Tag != null && palabras.Contains(metroLabel3.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel3.Tag.ToString()));
                    this.metroLabel3.Text = traduccion;
                }
                if (metroLabel2.Tag != null && palabras.Contains(metroLabel2.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel2.Tag.ToString()));
                    this.metroLabel2.Text = traduccion;
                }
                if (groupBox1.Tag != null && palabras.Contains(groupBox1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(groupBox1.Tag.ToString()));
                    this.groupBox1.Text = traduccion;
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
                if (metroLabel1.Tag != null && palabras.Contains(metroLabel1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel1.Tag.ToString()));
                    this.metroLabel1.Text = traduccion;
                }
                if (metroLabel6.Tag != null && palabras.Contains(metroLabel6.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel6.Tag.ToString()));
                    this.metroLabel6.Text = traduccion;
                }
                if (metroLabel7.Tag != null && palabras.Contains(metroLabel7.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel7.Tag.ToString()));
                    this.metroLabel7.Text = traduccion;
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

        private void ListarIdiomas()
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

        void anclado()
        {
            
        }

        private void sing_SizeChanged(object sender, EventArgs e)
        {
        
        }
    }
}