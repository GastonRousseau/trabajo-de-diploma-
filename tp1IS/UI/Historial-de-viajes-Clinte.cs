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
using Negocio;
using servicios;
using Patrones.Singleton.Core;
using servicios.ClasesMultiLenguaje;
namespace UI
{
    public partial class Historial_de_viajes_Clinte : Form,IdiomaObserver
    {
        public Historial_de_viajes_Clinte()
        {
            InitializeComponent();
            oBLLviajes = new BLLviaje();
            Listar(null, 1);
            cargarCombo();
        }
        BLLviaje oBLLviajes;
        int pag = 0;
        string NombreProducto;
        IList<BEViaje> viajes = new List<BEViaje>();
        BLLBitacora oBit = new BLLBitacora();
        validaciones validar = new validaciones();
        BLLProducto oBLLproducto = new BLLProducto();
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();

        private void Historial_de_viajes_Clinte_Load(object sender, EventArgs e)
        {
            Observer.agregarObservador(this);
            traducir();
        }
        void cargarCombo()
        {
            //metroComboBox1.Items.AddR(oBLLproducto.Producto_asignado_viaje(SessionManager.GetInstance.Usuario.id));
            List<string> productos = oBLLproducto.Producto_asignado_viaje(SessionManager.GetInstance.Usuario.id);
            foreach(string nombre in productos)
            {
                metroComboBox1.Items.Add(nombre);
            }
        }
        void Listar(string producto,int pag)
        {
            try
            {
                
                viajes = oBLLviajes.Historial_viajes_clientes(SessionManager.GetInstance.Usuario.id, pag, producto);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                dataGridView1.Columns["id"].Visible = false;
                dataGridView1.Columns["Km_Recorridos"].Visible = false;
       

            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
           
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
            if (pag > 0) Listar(NombreProducto, pag);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroButton1.Enabled = true;
            pag += 1;
            Listar(NombreProducto, pag);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            metroComboBox1.SelectedIndex =-1;
            
                NombreProducto = null;
            Listar(null, 1);
            
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedItem != null)
            {
               
                NombreProducto = metroComboBox1.SelectedItem.ToString();
            }
            else
            {
               
                NombreProducto = null; 
            }
        }

        private void CellFormattingDatagrid(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.Value != null)
                {
                    BEProducto producto = (BEProducto)e.Value;
                    e.Value = producto.nombre;
                }
                if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.Value != null)
                {
                    BECamion camion = (BECamion)e.Value;
                    e.Value = camion.patente + " " + camion.conductor.user;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (NombreProducto != string.Empty)
            {
                Listar(NombreProducto, 1);
            }
            else
            {
                Listar(null, 1);
            }
            
        }

        public void CambiarIdioma(Idioma Idioma)
        {
            //  throw new NotImplementedException();
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
                    //    RecorrerPanel(panel2, 1);
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
               // RecorrerPanel(panel2, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void form_closing(object sender, FormClosingEventArgs e)
        {
            servicios.Observer.eliminarObservador(this);
        }
    }
}
