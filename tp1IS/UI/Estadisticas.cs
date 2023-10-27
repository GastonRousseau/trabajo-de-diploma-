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
using Negocio;
using System.Windows.Forms.DataVisualization.Charting;
namespace UI
{
    public partial class Estadisticas : Form
    {
        public Estadisticas()
        {
            InitializeComponent();
            oBLLusuario = new BLLUsuario();
            oBLLviaje = new BLLviaje();
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
        }
        Nullable<DateTime> from;
        Nullable<DateTime> to;
        Nullable<DateTime> from2;
        Nullable<DateTime> to2;
        BLLUsuario oBLLusuario;
        BEUsuario ConductoSeleccionado = new BEUsuario();
        List<BEViaje> viajes = new List<BEViaje>();
        BLLviaje oBLLviaje;
        private void Estadisticas_Load(object sender, EventArgs e)
        {
            // metroDateTime1.Value = null;
            //  metroDateTime2.Value = null;
            cargarDatos();
            CargarChart();
            metroComboBox1.SelectedIndex = -1;
        }

        void cargarDatos()
        {
        
            metroComboBox1.DataSource = null;
            metroComboBox1.DataSource = oBLLusuario.Username_Conductor();
            metroComboBox1.SelectedItem = null;
     
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            if (metroDateTime1.Value < metroDateTime2.Value)
            {
                if (metroDateTime1.Value != null)
                {
                    from = metroDateTime1.Value;
                }
                if (metroDateTime2.Value != null)
                {
                    to = metroDateTime2.Value;
                }
      
            }
           
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        void CalcularEstadisticas()
        {
            try
            {
                int KM_totalesRecorridos = 0;
                int cantidad_viajes = viajes.Count;
                int HorasManejadas = 0;
                int promedioKM_Tiempo = 0;


                foreach (BEViaje viaje in viajes)
                {
                    KM_totalesRecorridos += viaje.cantidad_KM;
                    HorasManejadas += viaje.fechaFinalizacion.Hour - viaje.fecha.Hour;
                }
                promedioKM_Tiempo = HorasManejadas / KM_totalesRecorridos;


                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label7.Text = KM_totalesRecorridos.ToString();
                label8.Text = HorasManejadas.ToString() + " Horas";
                label9.Text = cantidad_viajes.ToString();
                label10.Text = promedioKM_Tiempo.ToString() + " por hora";
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

        void CargarChart()
        {
            try
            {
                Dictionary<string, int> conductores = oBLLviaje.Conductores_viajes_realizados(from2, to2);
                if (conductores.Count > 0)
                {
                    chart1.Series.Clear();
                    Series serie = new Series("Drivers");
                    serie.Color = Color.AliceBlue;
                    serie.ChartType = SeriesChartType.StackedBar;
                    foreach (var par in conductores)
                    {
                        serie.Points.AddXY(par.Key, par.Value);
                    }
                    chart1.Series.Add(serie);
                }
                else
                {
                    MessageBox.Show("no se realizaron viajes en es fecha");
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
            from = null;
            to = null;
            CalcularEstadisticas();
           
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (metroComboBox1.SelectedItem != null)
                {
                    ConductoSeleccionado.user = metroComboBox1.SelectedItem.ToString();
                    ConductoSeleccionado = oBLLusuario.buscar_usuario(ConductoSeleccionado.user);
                    viajes = oBLLviaje.TraerViajesDelConductor(ConductoSeleccionado.user, from, to);
                    if (viajes.Count > 0)
                    {
                        CalcularEstadisticas();
                    }
                    else
                    {
                        MessageBox.Show("Este conductor no a realizado ningun viaje durante ese periodo de timepo");
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (metroDateTime4.Value != null)
            {
                from2 = metroDateTime4.Value;
            }
            if (metroDateTime3.Value != null)
            {
                to2 = metroDateTime3.Value;
            }
            CargarChart();

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            from2 = null;
            to2 = null;
            CargarChart();
        }
    }
}
