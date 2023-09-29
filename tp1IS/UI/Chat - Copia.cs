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
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
            oBLLmensajes = new BLLMensaje();
            button2.Visible = false;
           
        }
        private static Chat charla;

        BLLMensaje oBLLmensajes;
        public static BEUsuario usuarioAconectar = new BEUsuario();    //static
        BEMensaje mensaje = new BEMensaje();

        public static Chat GetInstance
        {
            get
            {
                if (charla != null)charla = new Chat();

                return charla;
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int error = 0;
            if (userControl11.Texts == string.Empty)
            {
                error++;
            }
            if (error == 0)
            {
                BEMensaje mensaje = new BEMensaje();
                mensaje.mensaje = userControl11.Texts;
                mensaje.remitente = SessionManager.GetInstance.Usuario;
                mensaje.destinatario = usuarioAconectar;
                mensaje.fecha = DateTime.Now;
                oBLLmensajes.GuardarMensaje(mensaje);
                cargar_Mensajes();
                // oBLLmensajes.GuardarMensaje()
                userControl11.Texts = "";
            }
            else
            {
                MessageBox.Show("Error al escribir el mensaje intentar de nuevo");
            }
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            
            cargar_Mensajes();
            if (label1.Text == "servicioTecnico")
            {
                button2.Visible=true;
            }
        }
        void cargar_Mensajes()
        {
            panel2.AutoScroll = true;
            panel2.AutoScrollPosition =new Point(10, 10);
            
            
            panel2.Controls.Clear();
            var list = new List<BEMensaje>();
            list = oBLLmensajes.ObtenerMensajes(SessionManager.GetInstance.Usuario.id, usuarioAconectar);
            foreach (BEMensaje mensaje in list)
            {
                if (mensaje.remitente.id == SessionManager.GetInstance.Usuario.id)
                {
                    msjLocal(mensaje.mensaje);

                }
                else
                {
                    msjExterno(mensaje.mensaje);
                }
            }
        }
        void msjLocal(string mensaje)
        {
            Panel msjmioo = new Panel();
            msjmioo.Height = 59;
            panel2.Controls.Add(msjmioo);
            msjmioo.Dock = DockStyle.Bottom;
          //  msjmioo.BorderStyle = BorderStyle.FixedSingle;

            TextBox text = new TextBox();
            text.BackColor = Color.AliceBlue;
            
            text.Multiline = true;
            text.Size = new Size(400, 45);
            text.Text = mensaje;
            text.Anchor = AnchorStyles.Right;
            msjmioo.Controls.Add(text);
            text.Location = new Point(472, 7);
            

        }
        void msjExterno(string mensaje)
        {
            Panel mstuyoo = new Panel();
            //   Panel msjuser = new Panel();
            mstuyoo.Height = 59;
            panel2.Controls.Add(mstuyoo);
            mstuyoo.Dock = DockStyle.Bottom;
          //  mstuyoo.BorderStyle = BorderStyle.FixedSingle;
            TextBox text = new TextBox();
            text.BackColor = Color.CadetBlue;
            text.Multiline = true;
            text.Text = mensaje;
            text.Size = new Size(400,45);
            text.Anchor = AnchorStyles.Left;
            mstuyoo.Controls.Add(text);
            text.Location = new Point(69,7);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
