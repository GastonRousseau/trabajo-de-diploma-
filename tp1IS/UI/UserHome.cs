using Negocio;
using Patrones.Singleton.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using servicios.ClasesMultiLenguaje;
using BLL;
using BE;
namespace UI
{
    public partial class UserHome : Form,IdiomaObserver
    {
        public UserHome()
        {
            InitializeComponent();
            mensajes_nuevos();
        }
        BLL.BLLTraductor Otraductor = new BLL.BLLTraductor();
        BLL.BLLDv ODV = new BLL.BLLDv();
        BLLMensaje oBLLmensaje = new BLLMensaje();
        BLLUsuario oBLLusuario = new BLLUsuario();
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();

        private void UserHome_Load(object sender, EventArgs e)
        {
            try
            {
                servicios.Observer.agregarObservador(this);
                ListarIdiomas();
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

            servicios.Observer.eliminarObservador(this);
        }
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
                if (formulario is InterfazMensajes)
                {
                    formulario.Show();
                }
               
                else
                {

                    if (formulario is Chat)
                    {
                        // form.label1.Text = ServicioTecnico.user;
                        // formulario.userControl11.Texts = "Tengo el siguiente problema:";
                        formulario.Show();
                    }
                    else
                    {
                        formulario.TopLevel = false;
                        formulario.FormBorderStyle = FormBorderStyle.None;
                        formulario.Dock = DockStyle.Fill;
                        panel2.Controls.Add(formulario);
                        panel2.Tag = formulario;
                        formulario.Show();
                        if (metroButton1.Visible == false)
                        {
                            metroButton1.Visible = true;
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

        private void UserHome_FormClosing(object sender, FormClosingEventArgs e)
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

        BLLBitacora oBit = new BLLBitacora();
        private void metroButton4_Click(object sender, EventArgs e)
        {

            try
            {
                ODV.actualizarDV(servicios.GenerarVD.generarDigitoVS(ODV.BuscarDVUsuarios()));
                oBit.guardar_logOut();
                SessionManager.Logout();
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

        public void CambiarIdioma(Idioma Idioma)
        {
      
                ListarIdiomas();
                traducir();

            
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
        private void VolverAidiomaOriginal()
        {
            try
            {
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                 palabras = Traductor.obtenerIdiomaOriginal();

                RecorrerPanel(panel1, 2);
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
                    traducciones = Traductor.obtenertraducciones(Idioma);
                    List<string> Lista = new List<string>();
                    Lista = Traductor.obtenerIdiomaOriginal();
                    if (traducciones.Values.Count != Lista.Count)
                    {
                      //  MessageBox.Show("The lenguaje change is not complete for " + Idioma.Nombre);
                    }
                    else
                    {
                        RecorrerPanel(panel1, 1);
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

        
        private void button2_Click(object sender, EventArgs e)
        {
            CrearProducto form = new CrearProducto();
            AbrirFormulario(form);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Historial_de_viajes_Clinte form = new Historial_de_viajes_Clinte();
            AbrirFormulario(form);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Crear_viaje form = new Crear_viaje();
            AbrirFormulario(form);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViajesCliente form = new ViajesCliente();
            AbrirFormulario(form);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InterfazMensajes form = new InterfazMensajes();
            AbrirFormulario(form);
        }

        FlowLayoutPanel panelMensajes = new FlowLayoutPanel();
        void mensajes_nuevos()
        {
            Dictionary<string, int> datos = new Dictionary<string, int>();
            datos = oBLLmensaje.Mensajes_Nuevos(SessionManager.GetInstance.Usuario.id);
            if (datos.Count > 0||datos!=null)
            {

                // panelMensajes.FlowDirection = FlowDirection.TopDown;
                // panelMensajes.Dock = DockStyle.Right;
                //  panelMensajes.AutoScroll = true;
                panelMensajes.Location = new Point(545,245);
                panelMensajes.Size = new Size(199, 147);
                panelMensajes.BackColor = Color.AliceBlue;
                int contador = 0;
                foreach (var kvp in datos)
                {
                    if (contador < 5)
                    {
                        Label label = new Label();
                        label.BackColor = Color.Transparent;
                        label.ForeColor = Color.Black;
                        label.Text = $"{kvp.Key}: {kvp.Value} mensajes nuevos";
                        panelMensajes.Controls.Add(label);
                    }
                    else
                    {
                        Label label = new Label();
                        label.BackColor = Color.Black;
                        label.Text = "Y muchos mensajes más...";
                        panelMensajes.Controls.Add(label);
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
                panelMensajes.Controls.Add(boton_Borrar);
                boton_Borrar.Click += btnBorrarChat_click;
                this.Controls.Add(panelMensajes);
                panelMensajes.Visible = true;
            }
            else
            {
                panelMensajes.Visible = false;
            }

        }

        private void btnBorrarChat_click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            panelMensajes.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string nombre = oBLLmensaje.Buscar_ServicioTecnico();
            BEUsuario ServicioTecnico = oBLLusuario.buscar_usuario(nombre);
            Chat.usuarioAconectar = ServicioTecnico;
            Chat form = new Chat();
            form.label1.Text = ServicioTecnico.user;
            form.userControl11.Texts = "Tengo el siguiente problema:";
            form.Show();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            Historial_de_viajes_Clinte form = new Historial_de_viajes_Clinte();
            AbrirFormulario(form);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            ViajesCliente form = new ViajesCliente();
            AbrirFormulario(form);
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            Crear_viaje form = new Crear_viaje();
            AbrirFormulario(form);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            CrearProducto form = new CrearProducto();
            AbrirFormulario(form);
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            InterfazMensajes form = new InterfazMensajes();
            AbrirFormulario(form);
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            string nombre = oBLLmensaje.Buscar_ServicioTecnico();
            BEUsuario ServicioTecnico = oBLLusuario.buscar_usuario(nombre);
            Chat.usuarioAconectar = ServicioTecnico;
            Chat form = new Chat();
            AbrirFormulario(form);
           
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (formularioAbierto != null)
            {
                
                formularioAbierto.Close();
               // servicios.Observer.eliminarObservador(formularioAbierto);
                metroButton1.Visible = false;
            }
        }
    }
}
