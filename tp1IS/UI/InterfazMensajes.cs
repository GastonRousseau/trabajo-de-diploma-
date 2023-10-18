using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using Patrones.Singleton.Core;
using servicios;
using System.Text.RegularExpressions;
using Negocio;
namespace UI
{
    public partial class InterfazMensajes : Form
    {
        public InterfazMensajes()
        {
            InitializeComponent();
            oBLLmensajes = new BLLMensaje();
            oBLLusuario = new BLLUsuario();

        }
        BLLBitacora oBit = new BLLBitacora();
        private Form ChatActual;
        BLLMensaje oBLLmensajes;
        BEUsuario usuarioChat = new BEUsuario();
        BLLUsuario oBLLusuario;
        validaciones validar = new validaciones();
        private void InterfazMensajes_Load(object sender, EventArgs e)
        {
            cargarchats();
            Cargar_Usuarios_posibles();
        }
        private void abrirChat(Chat form, string titulo)//,int usuario,BEusuario usario_chatea)
        {
            if (ChatActual != null)
            {
                ChatActual.Close();
            }
            form.label1.Text = titulo;
            ChatActual = form;
        
            form.BringToFront();
            form.TopLevel = false;
            panel2.Controls.Add(form);
          
            form.Show();
        }
        private void crear_chat(string nombre,int id)//(string nombreUsuarioDElchat)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Height = 75;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(panel);
            panel.BringToFront();
            panel.BackColor= Color.LightSkyBlue;
            Label titulo = new Label();
            titulo.Font = new Font("Century Gothic", 14, FontStyle.Regular);
            titulo.Size = new Size(160, 17);
            titulo.Text = nombre;
            panel.Controls.Add(titulo);
            titulo.Location = new Point(41, 16);//70, 30); 91,39
            Label subtitulo = new Label();//cambiar el tamño del subtitulo,mas chico
            //subtitulo.Size = new Size(100,10);
            subtitulo.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            subtitulo.Text = Convert.ToString(id);
            subtitulo.Location = new Point(71, 56);
            panel.Controls.Add(subtitulo);
            panel.Click += panel_click;
            panel.MouseEnter += panel_Enter;
            panel.MouseLeave += panel_Leave;

            Button boton_Borrar = new Button();
            boton_Borrar.Text = "X";
            boton_Borrar.Size = new Size(16, 16);
            boton_Borrar.Location = new Point(1, 1);
            boton_Borrar.BackColor = Color.Black;
            boton_Borrar.ForeColor = Color.White;
            panel.Controls.Add(boton_Borrar);
            boton_Borrar.Click += btnBorrarChat_click;
            boton_Borrar.Tag = id;
        }

        void Cargar_Usuarios_posibles()
        {
            
            List<string> usuarios;
            if (SessionManager.tiene_permiso(5) || SessionManager.tiene_permiso(61))
            {
                usuarios = oBLLmensajes.Todos_los_usuarios_a_conectar();
            }
            else
            {
                usuarios = oBLLmensajes.Usuarios_con_quien_conectar(SessionManager.GetInstance.Usuario.id);
            }
           
            usuarios = usuarios.Distinct().ToList(); 
            foreach(string usuario in usuarios) 
            {
                if (SessionManager.GetInstance.Usuario.user != usuario)
                {
                    comboBox1.Items.Add(usuario);
                }
                
            } 
         //   comboBox1.Items.Add(usuarios);
        }
        private void btnBorrarChat_click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            int User = 0;

            DialogResult resultado = MessageBox.Show("Estas seguro que deseas eliminar la conversacion con este usuario", "confirmacion", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                if (sender is Button btn)
                {
                    
                    User = (int)btn.Tag;
                    if (Chat.usuarioAconectar.id == User)
                    {
                        ChatActual.Close();
                    }
                    oBLLmensajes.Eliminar_Chat(SessionManager.GetInstance.Usuario.id, User);
                    //agregar metodo de bll de eliminar los mensajes de ese usuario, con el sseion manager y actualizar los chats
                    cargarchats();
                }
            }
        

        }

        private void panel_Leave(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.LightSkyBlue;//Color.FromArgb(26, 25, 62);
        }

        private void panel_Enter(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.DodgerBlue;//Color.FromArgb(16, 15, 52);
        }

        void cargarchats()
        {
            panel1.Controls.Clear();
            List<BEUsuario> usuariosConChat = new List<BEUsuario>();
            usuariosConChat = oBLLmensajes.obtenerchats(SessionManager.GetInstance.Usuario.id);
           foreach(BEUsuario usuario in usuariosConChat)
            {
                crear_chat(usuario.user,usuario.id);
            }
        }
        string titulo = "";
        bool contieneNumeros = false;
        public bool ContieneNumeros(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
        private void panel_click(object sender, EventArgs e)
        {

            string titulo = "";
            int id = 0;
            foreach (var control in ((Panel)sender).Controls)
            {
                
                if (control is Label)
                {
                    contieneNumeros = ContieneNumeros(control.ToString());

                    if (contieneNumeros)
                    {
                        // Realizar acciones adicionales si el Label contiene números.
                        // Por ejemplo, puedes mostrar un mensaje, cambiar el color del Label, etc.
                        // ((Label)control).ForeColor = Color.Red;
                        id = Convert.ToInt32(((Label)control).Text);
                    }
                    else
                    {
                        titulo = ((Label)control).Text;
                    }

                    

                }
            }
            BEUsuario usuario = new BEUsuario();
            usuario.user = titulo;
            usuario.id = id;
            Chat.usuarioAconectar = usuario;
            //Chat.esteusuario = usuarioo;   si va
            abrirChat(new Chat(), titulo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ChatActual != null)
            {
                ChatActual.Close();
                cargarchats();
            }
            else
            {
                MessageBox.Show("No hay ningun chat abierto");
            }
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            int error = 0;
         /*   if (userControl11.Texts == string.Empty)
            {
                error++;
            }
            if (!validar.usuario(userControl11.Texts))
            {
                error++;
            }*/
            if (error == 0) 
            {
                BEUsuario usuarioNuevoChat = oBLLusuario.buscar_usuario(comboBox1.SelectedItem.ToString());//userControl11.Texts);
                if (usuarioNuevoChat != null)
                {
                    Chat.usuarioAconectar = usuarioNuevoChat;
                    abrirChat(new Chat(), usuarioNuevoChat.user);
                    MessageBox.Show("se encontro al usuario");
                    //userControl11.Texts = "";
                }
                else
                {
                    MessageBox.Show("Este usuario no existe");
                }

            }
            else
            {
                MessageBox.Show("hubo un error");
            }
         
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            int error = 0;
            int comodin = 0;
          /*  if (userControl11.Texts == string.Empty)
            {
                error++;
            }
            if (!validar.usuario(userControl11.Texts))
            {
                error++;
            }*/
            if (error == 0)
            {
                BEUsuario usuarioNuevoChat = oBLLusuario.buscar_usuario(comboBox1.SelectedItem.ToString());//userControl11.Texts);
                if (usuarioNuevoChat != null)
                {
                    List<BEUsuario> usuariosConChat = new List<BEUsuario>();
                    usuariosConChat = oBLLmensajes.obtenerchats(SessionManager.GetInstance.Usuario.id);
                    foreach (BEUsuario usuario in usuariosConChat)
                    {
                        if (usuarioNuevoChat.user == usuario.user)
                        {
                            comodin++;
                            Chat.usuarioAconectar = usuarioNuevoChat;
                            //Chat.esteusuario = usuarioo;   si va
                            abrirChat(new Chat(), usuarioNuevoChat.user);

                        }
                       // crear_chat(usuario.user, usuario.id);
                    }
                    if (comodin == 0)
                    {
                        MessageBox.Show("no se encontro ningun chat con este usuario");
                    }
                    else
                    {
                        MessageBox.Show("se encontro el chat");
                    }
                /*    Chat.usuarioAconectar = usuarioNuevoChat;
                    abrirChat(new Chat(), usuarioNuevoChat.user);
                    MessageBox.Show("se encontro al usuario");*/
                    //userControl11.Texts = "";
                }
                else
                {
                    MessageBox.Show("Este usuario no existe");
                }

            }
            else
            {
                MessageBox.Show("hubo un error");
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {

        }

        private void form_closed(object sender, FormClosedEventArgs e)
        {
            oBit.guardar_cierre_mensajes();
        }
    }
}
