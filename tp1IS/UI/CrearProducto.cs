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
using servicios;
using BE;
using BLL;
using Patrones.Singleton.Core;
using Negocio;
using servicios.ClasesMultiLenguaje;
namespace UI
{
    public partial class CrearProducto : Form,IdiomaObserver
    {
        public CrearProducto()
        {
            InitializeComponent();
            oBLLproducto = new BLLProducto();
            label1.Visible = false;
            label2.Visible = false;
        }
        BLLProducto oBLLproducto;
        validaciones validar = new validaciones();
        BLLBitacora oBit = new BLLBitacora();
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();
        BEProducto ProductoSelect = new BEProducto();
        private void CrearProducto_Load(object sender, EventArgs e)
        {
            Listar();
            Observer.agregarObservador(this);
            traducir();
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
        void Listar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource=oBLLproducto.ListarProductos(SessionManager.GetInstance.Usuario.id);
            dataGridView1.Columns["cliente"].Visible = false;
            dataGridView1.Columns["id"].ReadOnly = true;
            dataGridView1.Columns["nombre"].ReadOnly = true;
            dataGridView1.Columns["Estado"].ReadOnly = true;
            DeshabilitarEdicionSiColumnaVacia(dataGridView1, "nombre");
            DeshabilitarEdicionSiColumnaVacia(dataGridView1, "cantPallets");
            dataGridView1.Columns["Cliente"].Visible = false;
            dataGridView1.Columns["id"].Visible = false;

        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var error = 0;
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                errorProvider1.SetError(textBox2, "");

                if (textBox1.Text == string.Empty || !validar.usuario(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "You should enter a name without special characters");
                    error++;

                }
                if (textBox2.Text == string.Empty || !validar.id(textBox2.Text))
                {
                    errorProvider1.SetError(textBox2, "You should enter a number without special characters");
                    error++;
                }
                if (error == 0)
                {
                    BEProducto producto = new BEProducto();
                    producto.nombre = textBox1.Text;
                    producto.CantPallets = Convert.ToInt32(textBox2.Text);
                    producto.cliente = SessionManager.GetInstance.Usuario;
                    oBLLproducto.CrearProdcto(producto);
                    Listar();
                }
                else
                {
                    MessageBox.Show("There was a problem creating the object");
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

        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                   // BEProducto ProductoSelect = (BEProducto)dataGridView1.CurrentRow.DataBoundItem;
                    if (ProductoSelect.nombre != null)
                    {
                        oBLLproducto.EliminarProducto(ProductoSelect.id);
                        Listar();
                    }
                    else
                    {
                        MessageBox.Show("Select the product you want to remove");
                    }
                }
                else
                {
                    MessageBox.Show("does not have any products");
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

        private void cell_changed(object sender, DataGridViewCellEventArgs e)
        {
           
                try
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["nombre"].Index|| e.ColumnIndex == dataGridView1.Columns["cantPallets"].Index)
                    {
                        BEProducto producto = new BEProducto();
                        DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                        producto.id = Convert.ToInt32(row.Cells["id"].Value);
                        producto.nombre = Convert.ToString(row.Cells["nombre"].Value);
                        producto.CantPallets = Convert.ToInt32(row.Cells["cantPallets"].Value);
                    producto.cliente = SessionManager.GetInstance.Usuario;
                        if (!validar.usuario(producto.nombre))
                        {
                            MessageBox.Show("the name was not written correctly");
                            dataGridView1.CancelEdit();
                            // refrescar();
                        }
                        else if (string.IsNullOrEmpty(producto.nombre))
                        {
                            MessageBox.Show("No se puede dejar el nombre en blanco");
                            dataGridView1.CancelEdit();

                        }else if (!validar.id(Convert.ToString(producto.CantPallets)))
                         {

                         }else if (string.IsNullOrEmpty(Convert.ToString(producto.CantPallets)))
                          {
                        MessageBox.Show("No se puede dejar la cantidad de pallets en blanco");
                          }
                          else
                           {
                             oBLLproducto.CrearProdcto(producto);
                             Listar();
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

        private void metroLabel3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CambiarIdioma(Idioma Idioma)
        {
            // throw new NotImplementedException();
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
                        RecorrerPanel(panel1, 1);
                        RecorrerPanel(panel2, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        void VolverAidiomaOriginal()
        {
            try
            {
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                palabras = Traductor.obtenerIdiomaOriginal();

                RecorrerPanel(panel1, 2);
                RecorrerPanel(panel2, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //  ProductoSelect = (BEProducto)dataGridView1.CurrentRow.DataBoundItem;
            ProductoSelect = (BEProducto)dataGridView1.CurrentRow.DataBoundItem;
            if (label1.Visible == false && label2.Visible == false)
            {
                label1.Visible = true;
                label2.Visible = true;
            }
            label2.Text = ProductoSelect.id+" "+ProductoSelect.nombre;
        }

        private void form_closing(object sender, FormClosingEventArgs e)
        {
            servicios.Observer.eliminarObservador(this);
        }
    }
}
