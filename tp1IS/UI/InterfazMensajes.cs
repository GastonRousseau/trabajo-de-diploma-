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
namespace UI
{
    public partial class InterfazMensajes : Form
    {
        public InterfazMensajes()
        {
            InitializeComponent();
            oBLLmensajes = new BLLMensaje();
        }
        private Form ChatActual;
        BLLMensaje oBLLmensajes;
        BEUsuario usuarioChat = new BEUsuario();
        private void InterfazMensajes_Load(object sender, EventArgs e)
        {
            cargarchats();
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

            Label titulo = new Label();
            titulo.Font = new Font("Century Gothic", 12, FontStyle.Regular);
            titulo.Size = new Size(160, 17);
            titulo.Text = nombre;
            panel.Controls.Add(titulo);
            titulo.Location = new Point(91, 18);
            Label subtitulo = new Label();
            subtitulo.Font = new Font("Century Gothic", 10, FontStyle.Regular);
            subtitulo.Text = Convert.ToString(id);
            subtitulo.Location = new Point(41, 18);
           // panel.Controls.Add(subtitulo);
            panel.Click += panel_click;
            panel.MouseEnter += panel_Enter;
            panel.MouseLeave += panel_Leave;
        }

        private void panel_Leave(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.FromArgb(26, 25, 62);
        }

        private void panel_Enter(object sender, EventArgs e)
        {
            ((Panel)sender).BackColor = Color.FromArgb(16, 15, 52);
        }

        void cargarchats()
        {
            List<BEUsuario> usuariosConChat = new List<BEUsuario>();
            usuariosConChat = oBLLmensajes.obtenerchats(SessionManager.GetInstance.Usuario.id);
           foreach(BEUsuario usuario in usuariosConChat)
            {
                crear_chat(usuario.user,usuario.id);
            }
        }
        private void panel_click(object sender, EventArgs e)
        {
            string titulo = "";
            foreach (var control in ((Panel)sender).Controls)
            {
                if (control is Label)
                {
                    

                    titulo = ((Label)control).Text;

                }
            }
            BEUsuario usuario = new BEUsuario();
            usuario.user = titulo;
            Chat.usuarioAconectar = usuario;
            //Chat.esteusuario = usuarioo;   si va
            abrirChat(new Chat(), titulo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ChatActual != null)
            {
                ChatActual.Close();
            }
            else
            {
                MessageBox.Show("No hay ningun chat abierto");
            }
           
        }
    }
}
