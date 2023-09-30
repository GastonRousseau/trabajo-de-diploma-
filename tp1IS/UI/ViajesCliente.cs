﻿using System;
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
using Patrones.Singleton.Core;
namespace UI
{
    public partial class ViajesCliente : MetroFramework.Forms.MetroForm
    {
        public ViajesCliente()
        {
            InitializeComponent();
            oBLLviaje = new BLLviaje();
            oBLLmensaje = new BLLMensaje();
            panel3.Visible = false;
            //  dateTimePicker1.Value = null;
            this.Size = new Size(826, 311);
        }
        BLLviaje oBLLviaje;
        BLLMensaje oBLLmensaje;
        BLLCamion oBLLcamion = new BLLCamion();
        BEViaje viajeSelect = new BEViaje();
        private void ViajesCliente_Load(object sender, EventArgs e)
        {
            label1.Visible =false;
            barrarProgreso1.Visible = false;
            metroLabel2.Visible = false;
            Listar();
        }
        void Listar()
        {
            List<BEViaje> viajes = new List<BEViaje>();
            viajes = oBLLviaje.Traer_Viajes_Clientes(SessionManager.GetInstance.Usuario.id);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = viajes;
         /*   dataGridView1.DataMember ="producto";
            dataGridView1.Columns["nombre"].HeaderText ="producto";
           // dataGridView1.Columns["Nombre"].HeaderText = "Productos";*/
           /* dataGridView1.Columns["producto"].DisplayMember = "nombre";
            dataGridView1.Columns["producto"].DataPropertyName = "producto";
            dataGridView1.Columns["producto"].HeaderText = "Producto";*/


        }

        private void formating(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.Value != null)
            {
                BEProducto producto = (BEProducto)e.Value;
                e.Value = producto.nombre;
            }
            if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.Value != null)
            {
                BECamion camion = (BECamion)e.Value;
                e.Value = camion.patente;
            }

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
          /*  int error = 0;
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (viajeSelect == null)
            {
                error++;
            }
            if (error == 0)
            {
                BEMensaje mensaje = new BEMensaje();
             //   mensaje.mensaje = userControl11.Texts;//textBox1.Text;
                mensaje.destinatario = SessionManager.GetInstance.Usuario;
                mensaje.remitente = viajeSelect.camion.conductor;
            //    oBLLmensaje.GuardarMensaje(mensaje,viajeSelect.id);
                MessageBox.Show("se escribio el mensaje");

            }
            else
            {
                MessageBox.Show("hubo un error");
            }*/
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            BEViaje viajeSelect = ((BEViaje)dataGridView1.CurrentRow.DataBoundItem);
            if (viajeSelect != null)
            {
                if(viajeSelect.estado=="En proceso")
                {
                     label1.Visible = true;
                barrarProgreso1.Visible = true;
                //progressBar1.Minimum = 0;
                barrarProgreso1.MaximumValue =viajeSelect.cantidad_KM;
                    barrarProgreso1.ProgressValue = viajeSelect.Km_Recorridos;
                    metroLabel2.Visible = true;
                    metroLabel2.Text = (viajeSelect.Km_Recorridos + "/" + viajeSelect.cantidad_KM);
                }
               
            } 
           
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton3_Click_1(object sender, EventArgs e)
        {
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (viajeSelect != null)
            {
                if (viajeSelect.estado == "pendiente")
                {
                    oBLLviaje.ActualizarEstado(viajeSelect.id, "Cancelado");
                    BEMensaje mensaja = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "El viaje con id:" + viajeSelect.id, DateTime.Now, 2);
                    oBLLmensaje.GuardarMensaje(mensaja);
                    Chat.usuarioAconectar = viajeSelect.camion.conductor;
                    Chat form = new Chat();
                    form.button2.Visible = true;
                    form.userControl11.Texts = "Solicite la cancelacion del viaje devido a que";
                    form.Show();
                    Listar();
                    
                }
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if (viajeSelect != null)
            {
                
                Chat.usuarioAconectar = viajeSelect.camion.conductor;
                
                // form.label1.Text = viajeSelect.camion.conductor.user;
                Chat form = new Chat();
                form.button2.Visible = true;
                form.Show();

            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            if(viajeSelect != null)
            {
                this.Size = new Size(1081, 311);
                panel3.Visible = true;
            
            }
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            this.Size= new Size(826,311);
            panel3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value != null&& viajeSelect!=null&&viajeSelect.estado=="pendiente")
            {
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = oBLLcamion.Camiones_Disponibles(dateTimePicker1.Value, viajeSelect.cantidad_Pallets);
            }
            else 
            {
                MessageBox.Show("Ocurrio un error,puede ser que el estado del viaje no sea pendiente");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
            BECamion camionSelect = (BECamion)dataGridView2.CurrentRow.DataBoundItem;
            if(camionSelect != null&&viajeSelect!=null&&dateTimePicker1.Value!=null)
            {
                if (viajeSelect.camion != camionSelect)
                {
                    BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "Dejaras del ser el conductor del viaje con id: " + viajeSelect.id, DateTime.Now, 2);
                    oBLLmensaje.GuardarMensaje(mensaje);
                    viajeSelect.camion = camionSelect;
                    BEMensaje mensaje2 = new BEMensaje(SessionManager.GetInstance.Usuario, viajeSelect.camion.conductor, "Eres el nuevo conductor del viaje con id: " + viajeSelect.id, DateTime.Now, 2);
                    oBLLmensaje.GuardarMensaje(mensaje2);
                }
                else
                {
                    BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario,viajeSelect.camion.conductor,"Se postargo el viaje"+viajeSelect.id +"a la fecha " + dateTimePicker1.Value,DateTime.Now,2);
                    oBLLmensaje.GuardarMensaje(mensaje);
                }
               
                viajeSelect.fecha = dateTimePicker1.Value;
                oBLLviaje.Modifica_Viaje(viajeSelect);


                MessageBox.Show("Se postergo la fecha del viaje al dia" +dateTimePicker1.Value);
                panel3.Visible = false;
                this.Size = new Size(826, 311);
                Listar();
            }
            else
            {
                MessageBox.Show("hubo un error al intentar postergar el viaje");
            }
        }
    }
}
