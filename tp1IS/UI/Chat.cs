﻿using System;
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
            label1.Text = usuarioAconectar.user;
            panel2.Controls.Clear();
         //   cargar_Mensajes();
           
        }
        private static Chat charla;
        int currentPage = 1;
        int totalPages = 0;
        int mesagesPorPagina = 6;
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
                mensaje.tipo = 1;
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
            //  panel2.AutoScroll = true;
            panel2.Controls.Clear();
            cargar_Mensajes();
            if (label1.Text == "servicioTecnico")
            {
                button2.Visible=true;
            }
            //button4.Visible = false;
        }
        void cargar_Mensajes()
        {
            //   panel2.AutoScroll = true;
            //  panel2.AutoScrollPosition =new Point(10, 10);
            int topPosition = 0;
            
            panel2.Controls.Clear();
            var list = new List<BEMensaje>();
            list = oBLLmensajes.ObtenerMensajes(SessionManager.GetInstance.Usuario.id, usuarioAconectar);//.OrderByDescending(m => m.fecha).ToList();
            int toalmensajes = list.Count();
            totalPages = (int)Math.Ceiling((double)toalmensajes / mesagesPorPagina);
            currentPage = totalPages;
            /*     foreach (BEMensaje mensaje in list)
                 {
                     if (mensaje.remitente.id == SessionManager.GetInstance.Usuario.id)
                     {
                         msjLocal(mensaje.mensaje);

                     }
                     else
                     {
                         msjExterno(mensaje.mensaje);
                     }
                 }*/

            traerMensajesPorPagina(currentPage);
        }

        void traerMensajesPorPagina(int pagina)
        {
            panel2.Controls.Clear();

            // Obtener los mensajes de la página actual
            var list = oBLLmensajes.ObtenerMensajes(SessionManager.GetInstance.Usuario.id, usuarioAconectar)
              //  .OrderByDescending(m => m.fecha)
                .Skip((pagina - 1) * mesagesPorPagina)
                .Take(mesagesPorPagina)
                .ToList();

            foreach (BEMensaje mensaje in list)
            {
                if (mensaje.remitente.id == SessionManager.GetInstance.Usuario.id)
                {
                    msjLocal(mensaje);
                }
                else
                {
                    msjExterno(mensaje);
                }
            }

        }
        void msjLocal(BEMensaje mensaje)
        {
            Panel msjmioo = new Panel();
            msjmioo.Height = 59;
            panel2.Controls.Add(msjmioo);
            
            msjmioo.Dock = DockStyle.Bottom;
          //  msjmioo.BorderStyle = BorderStyle.FixedSingle;

            TextBox text = new TextBox();
            if (mensaje.tipo == 2)
            {
                text.BackColor = Color.DarkRed;
                text.ForeColor = Color.White;

            }
            else
            {
                text.BackColor = Color.AliceBlue;
                text.ForeColor = Color.Black;
            }
            
            text.ReadOnly = true;
            text.Multiline = true;
            text.Size = new Size(400, 45);
            text.Text = mensaje.mensaje;
            text.Anchor = AnchorStyles.Right;
            text.Location = new Point(472, 7);
            msjmioo.Controls.Add(text);
            
            

        }
        void msjExterno(BEMensaje mensaje)
        {
            Panel mstuyoo = new Panel();
         
            //   Panel msjuser = new Panel();
            mstuyoo.Height = 59;
            panel2.Controls.Add(mstuyoo);
            mstuyoo.Dock = DockStyle.Bottom;
          //  mstuyoo.BorderStyle = BorderStyle.FixedSingle;
            TextBox text = new TextBox();
            if (mensaje.tipo == 2)
            {
                text.BackColor = Color.DarkRed;
                text.ForeColor = Color.White;
            }
            else
            {
                text.BackColor = Color.Aqua;
                text.ForeColor = Color.Black;
            }
            //text.BackColor = Color.CadetBlue;
            text.Multiline = true;
            text.Text = mensaje.mensaje;
            text.Size = new Size(400,45);
            text.Anchor = AnchorStyles.Left;
            text.Location = new Point(69, 7);
            text.ReadOnly = true;
            mstuyoo.Controls.Add(text);
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                traerMensajesPorPagina(currentPage);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                traerMensajesPorPagina(currentPage);
            }
        }
    }
}
