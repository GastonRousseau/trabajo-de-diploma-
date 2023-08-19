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
using servicios.ClasesMultiLenguaje;
using servicios;
using Negocio;
using Patrones.Singleton.Core;
namespace UI
{
    public partial class modificarTranslation : MetroFramework.Forms.MetroForm,IdiomaObserver
    {
        public modificarTranslation()
        {
            InitializeComponent();
            OBLLtraductor = new BLLTraductor();
        }
        BLLTraductor OBLLtraductor;
        BLLBitacora oBit = new BLLBitacora();
        string idioma = string.Empty;
        validaciones validar = new validaciones();
        private void modificarTranslation_Load(object sender, EventArgs e)
        {
            ListarIdiomas();
            servicios.Observer.agregarObservador(this);
         
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem != null)
                {
                    idioma = comboBox1.SelectedItem.ToString();
                }
                refrescar();
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

        private void DeshabilitarEdicionSiColumnaVacia(DataGridView dataGridView, string nombreColumna)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                DataGridViewCell cell = row.Cells[nombreColumna];
                if (cell != null && string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                {
                    cell.ReadOnly = true;
                }
            }
        }
        void refrescar()
        {
            try
            {
               // dataGridView1.DataSource = null;

                if (idioma == null || idioma == string.Empty)
                {
                    MessageBox.Show("you must select a language");
                }
                else
                {
                    servicios.ClasesMultiLenguaje.Idioma Oidioma = OBLLtraductor.TraerIdioma(idioma);


                    dataGridView1.DataSource = OBLLtraductor.traerTablaxIdioma(Oidioma.ID);
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.Columns["IDidioma"].Visible = false;
                    dataGridView1.Columns["ID"].Visible = false;
                    dataGridView1.ReadOnly = false;
                    dataGridView1.Columns["Traduccion"].ReadOnly = false;
                    dataGridView1.Columns["Palabra"].ReadOnly = true;

                    DeshabilitarEdicionSiColumnaVacia(dataGridView1, "traduccion");
                  

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

        void ListarIdiomas()
        {
            try
            {
                comboBox1.Items.Clear();
                var ListaIdiomas = OBLLtraductor.ObtenerIdiomas();

                foreach (Idioma idioma in ListaIdiomas)
                {


                    if (idioma.Default == true)
                    {
                        comboBox2.Items.Add(idioma.Nombre);
                    }
                    else
                    {
                        comboBox1.Items.Add(idioma.Nombre);
                        var traducciones = OBLLtraductor.obtenertraducciones(idioma);
                        List<string> Lista = new List<string>();
                        Lista = OBLLtraductor.obtenerIdiomaOriginal();
                        if (traducciones.Values.Count == Lista.Count)
                        {
                            comboBox2.Items.Add(idioma.Nombre);
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
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }
        void Traducir()
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
                        if (label1.Tag != null && traducciones.ContainsKey(label1.Tag.ToString()))
                        {
                            label1.Text = traducciones[this.Tag.ToString()].texto;
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
        void VolverAidiomaOriginal()
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
                if (label1.Tag != null && palabras.Contains(label1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(label1.Tag.ToString()));
                    this.label1.Text = traduccion;
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
            ListarIdiomas();
            Traducir();
     
        }

      

        private void dataGridView_CellLeave__(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void datagrid_changed_(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["traduccion"].Index)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    int ididioma = Convert.ToInt32(row.Cells["IDidioma"].Value);
                    int idpalabra = Convert.ToInt32(row.Cells["ID"].Value);
                    string traduccion = Convert.ToString(row.Cells["Traduccion"].Value);

                    if (!validar.traduccion(traduccion))
                    {
                        MessageBox.Show("the translation was not written correctly");
                        dataGridView1.CancelEdit();
                        // refrescar();
                    }
                    else if (string.IsNullOrEmpty(traduccion))
                    {
                        MessageBox.Show("No se puede dejar la traducción en blanco");
                        dataGridView1.CancelEdit();

                    }
                    else
                    {
                        OBLLtraductor.ActualizarTraduccion(ididioma, idpalabra, traduccion);
                        refrescar();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string idiomaSelec = comboBox2.SelectedItem.ToString();
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

        private void form_closed(object sender, FormClosedEventArgs e)
        {
            servicios.Observer.eliminarObservador(this);
        }
    }
}
