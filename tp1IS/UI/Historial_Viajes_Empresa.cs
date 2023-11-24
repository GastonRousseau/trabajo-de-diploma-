using System;
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
using Patrones.Singleton.Core;
using servicios.ClasesMultiLenguaje;
using servicios;
namespace UI
{
    public partial class Historial_Viajes_Empresa : Form,IdiomaObserver
    {
        public Historial_Viajes_Empresa()
        {
            InitializeComponent();
            buscar(null, 1, null,null);//    arreglar para que sea de una fecha a otra
        }
        IList<BEViaje> viajes = new List<BEViaje>();
        BLLviaje oBLLviajes = new BLLviaje();
        int pag;
        string nombreCliente;
        Nullable<DateTime> from;
        Nullable<DateTime> to;
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();
        private void Historial_Viajes_Empresa_Load(object sender, EventArgs e)
        {
            Observer.agregarObservador(this);
            traducir();
        }

        void buscar(string nombreCliente,int pag,Nullable<DateTime> from,Nullable<DateTime>to)
        {
            try
            {

                viajes = oBLLviajes.getAll_Historial_viajes_(pag,nombreCliente,from,to);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                

            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                //  oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                var accion = ex.Message;
                //  oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

            pag -= 1;
            metroButton1.Enabled = true;
            if (pag <= 1) metroButton1.Enabled = false;
            if (pag > 0) buscar(nombreCliente,pag,from,to);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroButton1.Enabled = true;
            pag += 1;
            buscar(nombreCliente, pag, from,to);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                from = null;
                to = null;
                nombreCliente = null;
                if (textBox1.Text != string.Empty)
                {
                    nombreCliente = textBox1.Text;
                }
                if (metroDateTime1.Value != null)
                {
                    from = metroDateTime1.Value;
                }
                if (metroDateTime2.Value != null)
                {
                    to = metroDateTime2.Value;
                }
                buscar(nombreCliente, 1, from, to);
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

        private void metroButton4_Click(object sender, EventArgs e)
        {
            from = null;
            to = null;
            nombreCliente = null;
            buscar(nombreCliente, 1, from, to);
            pag = 1;
        }
        
        private void cellFormattingDataGrid(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;

                    if (viaje != null && viaje.producto != null)
                    {

                        e.Value = viaje.producto.nombre;
                    }
                }
                if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;


                    if (viaje != null && viaje.camion != null)
                    {
                        e.Value = viaje.camion.patente + " " + viaje.camion.conductor.user;
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

        public void CambiarIdioma(Idioma Idioma)
        {
            //   throw new NotImplementedException();
            traducir();
        }

        void traducir()
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

                    }
                    else
                    {
                        RecorrerPanel(this, 1);
                       // RecorrerPanel(panel2, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void RecorrerPanel(Form panel, int v)
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

        void VolverAidiomaOriginal()
        {
            try
            {
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                palabras = Traductor.obtenerIdiomaOriginal();

                RecorrerPanel(this, 2);
              //  RecorrerPanel(panel2, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
