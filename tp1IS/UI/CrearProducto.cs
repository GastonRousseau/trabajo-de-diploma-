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
namespace UI
{
    public partial class CrearProducto : MetroFramework.Forms.MetroForm
    {
        public CrearProducto()
        {
            InitializeComponent();
            oBLLproducto = new BLLProducto();
        }
        BLLProducto oBLLproducto;
        validaciones validar = new validaciones();
        BLLBitacora oBit = new BLLBitacora();
        private void CrearProducto_Load(object sender, EventArgs e)
        {
            Listar();
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
            DeshabilitarEdicionSiColumnaVacia(dataGridView1, "nombre");
            DeshabilitarEdicionSiColumnaVacia(dataGridView1, "cantPallets");

        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            var error = 0;
            errorProvider1.Clear();
            errorProvider1.SetError(userControl11, "");
            errorProvider1.SetError(userControl12, "");
            
            if (userControl11.Text == string.Empty || !validar.usuario(userControl11.Text))
            {
                errorProvider1.SetError(userControl11, "You should enter a name without special characters");
                error++;

            }
            if (userControl12.Text == string.Empty || !validar.id(userControl12.Text))
            {
                errorProvider1.SetError(userControl12, "You should enter a number without special characters");
                error++;
            }
            if (error == 0)
            {
                BEProducto producto = new BEProducto();
                producto.nombre = userControl11.Text;
                producto.CantPallets = Convert.ToInt32(userControl12.Text);
                producto.cliente = SessionManager.GetInstance.Usuario;
                oBLLproducto.CrearProdcto(producto);
                Listar();
            }
            else
            {

            }
           
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            BEProducto ProductoSelect = (BEProducto)dataGridView1.CurrentRow.DataBoundItem;
            if (ProductoSelect != null)
            {
                oBLLproducto.EliminarProducto(ProductoSelect.id);
                Listar();
            }
            else
            {
                MessageBox.Show("Select the product you want to remove");
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
    }
}
