using BE;
using MetroFramework;
using Negocio;
using Patrones.Singleton.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using servicios.ClasesMultiLenguaje;
using servicios;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BLL;
using abstraccion;
using MetroFramework.Forms;
using MetroFramework.Components;

namespace UI
{
    public partial class AdminHome : MetroFramework.Forms.MetroForm, IdiomaObserver
    {
        public AdminHome()
        {
         //   panel2.Visible = false;
            InitializeComponent();
            groupBox1.Hide();
            es_traductor();
            ajustarControles();
            es_chofer();
            //  es_monitor();
            es_admin();
            mensajes_nuevos();
            this.MinimumSize = new System.Drawing.Size(1373, 540);
            esconderSubPaneles();
            panel1.Size = new Size(502, 528);


        }
        SessionManager session = SessionManager.GetInstance;
        BLLMensaje oBLLmensaje = new BLLMensaje();
        public void es_traductor()
        {
            if (!SessionManager.tiene_permiso(22) && !SessionManager.tiene_permiso(21)) metroButton11.Hide(); //metroButton12.Hide();
        }
        public void es_chofer()
        {
            if (SessionManager.tiene_permiso(60)) metroButton16.Hide(); metroButton17.Hide(); metroButton18.Hide(); metroButton15.Show();
        }
        public void es_admin()
        {
            if (SessionManager.tiene_permiso(5)) metroButton13.Hide();/* metroButton15.Hide();*/metroButton17.Show(); metroButton18.Show();
        }

        /*  public void es_monitor()
          {
              if (!SessionManager.tiene_permiso(7)) metroButton14.Hide();metroButton16.Hide();
          }*/
        BLLUsuario oLog = new BLLUsuario();
        BEUsuario oUsuraio;
        BLL.BLLDv ODV = new BLLDv();

        private Form formularioAbierto = null;
        private void AbrirFormulario(Form formulario)
        {
            try
            {
                if (formularioAbierto != null)
                {

                    formularioAbierto.Close();
                }

                formularioAbierto = formulario;
                //    formularioAbierto.BringToFront();
                //    formularioAbierto.TopLevel = false;

                //  formularioAbierto.Dock= DockStyle.Fill;

                //    Control topLevelControl = panel9.TopLevelControl;
                //     panel9.Controls.Add(formularioAbierto);
                formulario.Location = new Point(513,25);
                formularioAbierto.Show();
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
        BLLTraductor Otraductor = new BLLTraductor();
        private void AdminHome_Load(object sender, EventArgs e)
        {
            try
            {

                metroButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;//////////////////////////////////////////////////////////////////////////////////////////
                metroButton2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton6.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton7.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton9.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton11.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                metroButton12.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                comboBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                textBox1.Dock = DockStyle.Top;//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                metroLabel1.Dock = DockStyle.Top;
                metroLabel2.Dock = DockStyle.Top;

                ListarIdiomas();

                servicios.Observer.agregarObservador(this);
                SessionManager.GetInstance.idioma = Otraductor.ObtenerIdiomaBase();
                traducir();

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
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Observer.eliminarObservador(this);
        }


        private void AdminHome_FormClosing(object sender, EventArgs e)
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
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MetroMessageBox.Show(this, "Yes/No", "¿Do you wish to give admin privileges to a user that already exist?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    darAdmin formadmin = new darAdmin();
                    AbrirFormulario(formadmin);
                }
                else
                {
                    crearAdmin formcrearAdmin = new crearAdmin();
                    AbrirFormulario(formcrearAdmin);

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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            groupBox1.Show();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                if (textBox1.Text == string.Empty || !Regex.IsMatch(textBox1.Text, "^([0-9]{1,9}$)"))
                {
                    errorProvider1.SetError(textBox1, "You should enter an id from 1 to 9 numbers");
                }
                else
                {
                    if (oLog.usuario_existente(Convert.ToInt32(textBox1.Text)))
                    {
                        if (oLog.eliminar_usuario(Convert.ToInt32(textBox1.Text)))
                        {
                            var accion = " elimino el usuario " + textBox1.Text;
                            oBit.guardar_accion(accion, 2);
                            MetroMessageBox.Show(this, "User deleted");
                            if (Convert.ToInt32(textBox1.Text) == session.Usuario.id)
                            {
                                MetroMessageBox.Show(this, "Your user is disabled, you are going to be redirected to log in page");
                                this.Hide();
                            }
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "There has been an error deleting the user");
                        }
                    }

                    else { MetroMessageBox.Show(this, "There are no users registered with this id"); }
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
        BLLBitacora oBit = new BLLBitacora();
        private void metroButton4_Click(object sender, EventArgs e)
        {
            try
            {


                oBit.guardar_logOut();
                SessionManager.Logout();
                this.Close();

                servicios.Observer.eliminarObservador(this);

                var formularios = Application.OpenForms;

                var copiaFormularios = new List<Form>(formularios.OfType<Form>());

                foreach (Form formulario in copiaFormularios)
                {
                    if (formulario.Text != "Welcome!")
                    {
                        formulario.Close();
                    }
                }

                SignIn form = new SignIn();
                form.Show();
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
            servicios.Observer.eliminarObservador(this);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {
                borrarPassword passForm = new borrarPassword();
                AbrirFormulario(passForm);
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

        private void metroButton6_Click(object sender, EventArgs e)
        {
            try
            {
                crearRol rol = new crearRol();
                AbrirFormulario(rol);
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

        private void metroButton7_Click(object sender, EventArgs e)
        {
            darRol darRol = new darRol();
            AbrirFormulario(darRol);
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

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
            try
            {
                ListarIdiomas();
                traducir();
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

        private void traducir()
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
                        if (metroButton4.Tag != null && traducciones.ContainsKey(metroButton4.Tag.ToString()))
                        {
                            this.metroButton4.Text = traducciones[metroButton4.Tag.ToString()].texto;
                        }
                        if (metroButton5.Tag != null && traducciones.ContainsKey(metroButton5.Tag.ToString()))
                        {
                            this.metroButton5.Text = traducciones[metroButton5.Tag.ToString()].texto;
                        }
                        if (metroButton6.Tag != null && traducciones.ContainsKey(metroButton6.Tag.ToString()))
                        {
                            this.metroButton6.Text = traducciones[metroButton6.Tag.ToString()].texto;
                        }
                        if (metroButton8.Tag != null && traducciones.ContainsKey(metroButton8.Tag.ToString()))
                        {
                            this.metroButton8.Text = traducciones[metroButton8.Tag.ToString()].texto;
                        }
                        if (metroLabel1.Tag != null && traducciones.ContainsKey(metroLabel1.Tag.ToString()))
                        {
                            this.metroLabel1.Text = traducciones[metroLabel1.Tag.ToString()].texto;
                        }
                        if (metroLabel2.Tag != null && traducciones.ContainsKey(metroLabel2.Tag.ToString()))
                        {
                            this.metroLabel2.Text = traducciones[metroLabel2.Tag.ToString()].texto;
                        }
                        if (groupBox1.Tag != null && traducciones.ContainsKey(groupBox1.Tag.ToString()))
                        {
                            this.groupBox1.Text = traducciones[groupBox1.Tag.ToString()].texto;
                        }
                        if (metroButton7.Tag != null && traducciones.ContainsKey(metroButton7.Tag.ToString()))
                        {
                            this.metroButton7.Text = traducciones[metroButton7.Tag.ToString()].texto;
                        }
                        if (metroButton11.Tag != null && traducciones.ContainsKey(metroButton11.Tag.ToString()))
                        {
                            this.metroButton11.Text = traducciones[metroButton11.Tag.ToString()].texto;
                        }
                        if (metroButton9.Tag != null && traducciones.ContainsKey(metroButton9.Tag.ToString()))
                        {
                            this.metroButton9.Text = traducciones[metroButton9.Tag.ToString()].texto;
                        }
                        if (metroButton10.Tag != null && traducciones.ContainsKey(metroButton10.Tag.ToString()))
                        {
                            this.metroButton10.Text = traducciones[metroButton10.Tag.ToString()].texto;
                        }
                        if (metroButton12.Tag != null && traducciones.ContainsKey(metroButton12.Tag.ToString()))
                        {
                            this.metroButton12.Text = traducciones[metroButton12.Tag.ToString()].texto;
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
                if (metroButton4.Tag != null && palabras.Contains(metroButton4.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton4.Tag.ToString()));
                    this.metroButton4.Text = traduccion;
                }
                if (metroButton5.Tag != null && palabras.Contains(metroButton5.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton5.Tag.ToString()));
                    this.metroButton5.Text = traduccion;
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
                if (groupBox1.Tag != null && palabras.Contains(groupBox1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(groupBox1.Tag.ToString()));
                    this.groupBox1.Text = traduccion;
                }
                if (metroButton10.Tag != null && palabras.Contains(metroButton10.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton10.Tag.ToString()));
                    this.metroButton10.Text = traduccion;
                }
                if (metroButton11.Tag != null && palabras.Contains(metroButton1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton11.Tag.ToString()));
                    this.metroButton11.Text = traduccion;
                }
                if (metroButton9.Tag != null && palabras.Contains(metroButton9.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton9.Tag.ToString()));
                    this.metroButton9.Text = traduccion;
                }
                if (metroButton7.Tag != null && palabras.Contains(metroButton7.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton7.Tag.ToString()));
                    this.metroButton7.Text = traduccion;
                }
                if (metroButton6.Tag != null && palabras.Contains(metroButton6.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton6.Tag.ToString()));
                    this.metroButton6.Text = traduccion;
                }
                if (metroButton8.Tag != null && palabras.Contains(metroButton8.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton8.Tag.ToString()));
                    this.metroButton8.Text = traduccion;
                }
                if (metroButton12.Tag != null && palabras.Contains(metroButton12.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton12.Tag.ToString()));
                    this.metroButton12.Text = traduccion;
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

        private void metroButton8_Click(object sender, EventArgs e)
        {
            AddLenguaje from = new AddLenguaje();
            AbrirFormulario(from);
        }

        private void metroButton9_Click(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            AbrirFormulario(bitacora);
        }

        private void metroButton10_Click(object sender, EventArgs e)
        {
            Changes cambios = new Changes();
            AbrirFormulario(cambios);


        }

        private void metroButton11_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionManager.tiene_permiso(21) || SessionManager.tiene_permiso(22))
                {
                    AddLenguaje from = new AddLenguaje();
                    AbrirFormulario(from);
                }
                else
                {
                    MessageBox.Show("you do not have the necessary permissions");
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

        private void metroButton8_Click_1(object sender, EventArgs e)
        {
            eliminarRol from = new eliminarRol();
            AbrirFormulario(from);
        }

        private void metroButton12_Click(object sender, EventArgs e)
        {

        }

        private void metroButton12_Click_1(object sender, EventArgs e)
        {
            modificarTranslation from = new modificarTranslation();
            AbrirFormulario(from);
        }

        void ajustarControles()
        {
            /* TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
             tableLayoutPanel.Dock = DockStyle.Fill;
             tableLayoutPanel.ColumnCount = 2;
             tableLayoutPanel.Controls.Add(metroButton1, 0, 0);
             this.Controls.Add(tableLayoutPanel);*/
            metroButton1.Anchor = AnchorStyles.Right;
            // metroButton1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        }

        private void metroButton15_Click(object sender, EventArgs e)
        {
            //  Mensajes_Chof form = new Mensajes_Chof();
            InterfazMensajes form = new InterfazMensajes();
            AbrirFormulario(form);
        }

        private void metroButton13_Click(object sender, EventArgs e)
        {
            Viajes_chofer form = new Viajes_chofer();
            AbrirFormulario(form);
        }

        private void metroButton16_Click(object sender, EventArgs e)
        {
            CrearChofer from = new CrearChofer();
            AbrirFormulario(from);
        }

        private void metroButton17_Click(object sender, EventArgs e)
        {
            ViajesPendientes_Empresa form = new ViajesPendientes_Empresa();
            AbrirFormulario(form);
        }

        private void metroButton18_Click(object sender, EventArgs e)
        {
            Camiones form = new Camiones();
            AbrirFormulario(form);
        }
        FlowLayoutPanel panelMensajes = new FlowLayoutPanel();
        void mensajes_nuevos()
        {
            Dictionary<string, int> datos = new Dictionary<string, int>();
            datos = oBLLmensaje.Mensajes_Nuevos(SessionManager.GetInstance.Usuario.id);
            if (datos != null)
            {

                // panelMensajes.FlowDirection = FlowDirection.TopDown;
                // panelMensajes.Dock = DockStyle.Right;
                //  panelMensajes.AutoScroll = true;
                panelMensajes.Location = new Point(1222, 375);
                panelMensajes.Size = new Size(199, 147);
                panelMensajes.BackColor = Color.NavajoWhite;
                int contador = 0;
                foreach (var kvp in datos)
                {
                    if (contador < 5)
                    {
                        Label label = new Label();
                        label.BackColor = Color.White;
                        label.ForeColor = Color.Black;
                        label.Text = $"{kvp.Key}: {kvp.Value} mensajes nuevos";
                       // panelMensajes.Controls.Add(label);
                        panel2.Controls.Add(label);
                    }
                    else
                    {
                        Label label = new Label();
                        label.BackColor = Color.Black;
                        label.Text = "Y muchos mensajes más...";
                        //panelMensajes.Controls.Add(label);
                        panel2.Controls.Add(label);
                        break;  // Salir del bucle si hemos mostrado 5 etiquetas
                    }
                    contador++;

                }
                //    RadioButton boton_Borrar = new System.Windows.Forms.RadioButton();
                Label boton_Borrar = new Label();
                boton_Borrar.Text = "X";
                boton_Borrar.Size = new Size(16, 16);
                boton_Borrar.Location = new Point(1, 1);
                boton_Borrar.BackColor = Color.Black;
                boton_Borrar.ForeColor = Color.White;
                //panelMensajes.Controls.Add(boton_Borrar);
                panel2.Controls.Add(boton_Borrar);
                boton_Borrar.Click += btnBorrarChat_click;
                //  this.Controls.Add(panelMensajes);
                // panelMensajes.Visible = true;
                panel2.Visible = true;
            }
            else
            {
                panelMensajes.Visible = false;
                panel2.Visible = false;
            }

        }

        private void btnBorrarChat_click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            panelMensajes.Visible = false;

        }

     /*   private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            bool isOn;
            isOn = metroToggle1.Checked;

            if (metroToggle1.Checked == true)
            {
                MetroStyleManager styleManager = new MetroStyleManager();

                // Establecer el tema y el estilo
                styleManager.Theme = MetroFramework.MetroThemeStyle.Dark; // O MetroThemeStyle.Light
                styleManager.Style = MetroFramework.MetroColorStyle.Blue; // O cualquier otro estilo disponible

                // Asignar el administrador de estilo al formulario
                styleManager.Owner = this;
            }
            if (metroToggle1.Checked == false)
            {
                MetroStyleManager styleManager = new MetroStyleManager();

                // Establecer el tema y el estilo
                styleManager.Theme = MetroFramework.MetroThemeStyle.Light; // O MetroThemeStyle.Light
                styleManager.Style = MetroFramework.MetroColorStyle.Blue; // O cualquier otro estilo disponible

                // Asignar el administrador de estilo al formulario
                styleManager.Owner = this;
            }
        }*/

        private void metroButton19_Click(object sender, EventArgs e)
        {
           
            string nombre = oBLLmensaje.Buscar_ServicioTecnico();
            BEUsuario ServicioTecnico = oLog.buscar_usuario(nombre);
            Chat.usuarioAconectar = ServicioTecnico;
            Chat form = new Chat();
            form.label1.Text = ServicioTecnico.user;
            form.userControl11.Texts = "Tengo el siguiente problema:";
            form.Show();
        }

        private void metroButton14_Click(object sender, EventArgs e)
        {
            Historial_Viajes_Empresa form = new Historial_Viajes_Empresa();
            AbrirFormulario(form);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GenerarPDF form = new GenerarPDF();
            AbrirFormulario(form);
        }

        private void metroButton20_Click(object sender, EventArgs e)
        {
            Viajes_chofer viajes =new Viajes_chofer();
            if (SessionManager.GetInstance.Usuario.rol == 61)
            {
                AbrirFormulario(viajes);
            }
        }


        void esconderSubPaneles()
        {
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
        }

        void hidePaneles()
        {
            if (panel3.Visible == true)
            {
                panel3.Visible = false;
            }
            if (panel4.Visible == true)
            {
                panel4.Visible = false;
            }
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            if (panel6.Visible == true)
            {
                panel6.Visible = false;
            }
            if (panel7.Visible == true)
            {
                panel7.Visible = false;
            }
        }

        void abrirSubMenu(Panel SubMenu)
        {
            if (SubMenu.Visible == false)
            {
                hidePaneles();
                SubMenu.Visible = true;
            }
            else
            {
                SubMenu.Visible = false;
            }
        }

        private void metroButton23_Click(object sender, EventArgs e)
        {
            abrirSubMenu(panel5);
        }

        private void metroButton22_Click(object sender, EventArgs e)
        {
            abrirSubMenu(panel4);
        }

        private void metroButton25_Click(object sender, EventArgs e)
        {
            abrirSubMenu(panel3);
        }

        private void metroButton24_Click(object sender, EventArgs e)
        {
            abrirSubMenu(panel7);
        }

        private void metroButton21_Click(object sender, EventArgs e)
        {
            abrirSubMenu(panel6);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton26_Click(object sender, EventArgs e)
        {
            GenerarPDF form = new GenerarPDF();
            AbrirFormulario(form);
        }
    }
    }

