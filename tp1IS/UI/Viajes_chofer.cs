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
using servicios;
using Patrones.Singleton.Core;
using Negocio;
namespace UI
{
    public partial class Viajes_chofer : Form
    {
        public Viajes_chofer()
        {
            InitializeComponent();
            oBLLviaje = new BLLviaje();
            buscar(null,1);
            panel2.Visible = false;
            cargarCombo();
        }
        BLLviaje oBLLviaje;
        int pag = 0;
        string NombreCliente;
        IList<BEViaje> viajes = new List<BEViaje>();
        BLLBitacora oBit = new BLLBitacora();
        validaciones validar = new validaciones();
        BLLUsuario oBLLusuario = new BLLUsuario();
        BEViaje ViajeSleccionado = new BEViaje();
        BLLMensaje oBLLmensaje = new BLLMensaje();
        private void Viajes_chofer_Load(object sender, EventArgs e)
        {
            pag = 1;
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                BEViaje viaje = (BEViaje)row.DataBoundItem;
                if (viaje.estado == "En proceso")
                {
                    row.Cells["Km_Recorridos"].ReadOnly = false;
                }
                else
                {
                    row.Cells["Km_Recorridos"].ReadOnly = true;
                }
            }
        }
        void buscar(string nombre,int pag) 
        {

            try
            {
               
                viajes = oBLLviaje.Traer_Viajes_Chofer(SessionManager.GetInstance.Usuario.id,pag,nombre);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                //   dataGridView1.ReadOnly = true;
                dataGridView1.Columns["cantidad_Pallets"].ReadOnly = true;
                dataGridView1.Columns["id"].ReadOnly = true;
                dataGridView1.Columns["producto"].ReadOnly = true;
                dataGridView1.Columns["partida"].ReadOnly = true;
                dataGridView1.Columns["destino"].ReadOnly = true;
                dataGridView1.Columns["cantidad_KM"].ReadOnly = true;
                dataGridView1.Columns["fecha"].ReadOnly = true;
                dataGridView1.Columns["estado"].ReadOnly = true;
             
            

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

        

        void cargarCombo()
        {
            metroComboBox1.DataSource = oBLLusuario.S_Traer_Administradores();
         //   metroComboBox1.DisplayMember = "username";
            metroComboBox1.ValueMember = "id";
            metroComboBox1.SelectedItem = null;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            pag -= 1;
            metroButton1.Enabled = true;
            if (pag <= 1) metroButton1.Enabled = false;
            if (pag > 0) buscar(NombreCliente, pag);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
           metroButton1.Enabled = true;
            pag += 1;
            buscar(NombreCliente, pag);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            NombreCliente = textBox1.Text;
            buscar(NombreCliente, 1);
        }

        private void formato(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.Value != null)
                {
                    BEProducto producto = (BEProducto)e.Value;
                    e.Value = producto.nombre + " " + producto.cliente.user;
                }
                if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.Value != null)
                {
                    BECamion camion = (BECamion)e.Value;
                    e.Value = camion.patente;
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

        private void metroButton4_Click(object sender, EventArgs e)
        {

            try
            {
                BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
                int error = 0;
                if (viajeSelect == null)
                {
                    error++;
                }
                if (viajeSelect.estado == "En proceso")
                {
                    error++;
                }
                if (viajeSelect.fecha < DateTime.Now)
                {
                    error++;
                }
                if (error == 0)
                {
                    oBLLviaje.ActualizarEstado(viajeSelect.id, "En proceso", null);
                }
                else
                {
                    MessageBox.Show("Ocurrio un error");
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

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {
                BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
                int error = 0;
                if (viajeSelect == null)
                {
                    error++;
                }
                if (viajeSelect.estado == "Pendiente")
                {
                    error++;
                }
                if (error == 0)
                {
                    oBLLviaje.ActualizarEstado(viajeSelect.id, "Finalizado", DateTime.Now);
                }
                else
                {
                    MessageBox.Show("Ocurrio un error");
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
        private BEViaje viajeAntesDeEditar = null;
        private void datagrid1_changued(object sender, DataGridViewCellEventArgs e)
        {
            int error = 0;
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Km_Recorridos"].Index)
                {
                    // Obtén la fila actual
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Obtén el objeto BEViaje asociado a esta fila
                    if (row.DataBoundItem is BEViaje viaje)
                    {
                        // Obtén el valor de la celda editada
                        object cellValue = row.Cells[e.ColumnIndex].Value;

                        if (cellValue != null && int.TryParse(cellValue.ToString(), out int nuevoKM))
                        {
                            // Actualiza el valor de "Km_recorridos" en el objeto BEViaje
                            if (nuevoKM > viaje.cantidad_KM)
                            {
                                MessageBox.Show("La cantidad de Km SUPERO EL limite");
                                dataGridView1.CancelEdit();
                                error++;
                            }
                            if (!validar.id(Convert.ToString(nuevoKM)))
                            {
                                error++;
                                dataGridView1.CancelEdit();
                            }
                            if (error == 0)
                            {
                                viaje.Km_Recorridos = nuevoKM;
                                oBLLviaje.actualizar_KM_recorridos(viaje.id, viaje.Km_Recorridos);
                                MessageBox.Show("se actualizo");
                            }
                            

                            // Realiza las validaciones adicionales y otras acciones si es necesario
                            // ...

                            // Actualiza la fila en el DataGridView para reflejar el cambio
                            dataGridView1.InvalidateRow(e.RowIndex);
                        }
                        else
                        {
                            MessageBox.Show("El valor de KM no es válido.");
                            dataGridView1.CancelEdit();
                        }
                    }
                    else
                    {
                        MessageBox.Show("La fila no está asociada a un objeto BEViaje.");
                        dataGridView1.CancelEdit();
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

        private void metroButton6_Click(object sender, EventArgs e)
        {
            try
            {
                BEViaje viajeSelect = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;
                if (viajeSelect != null)
                {
                    Chat.usuarioAconectar = viajeSelect.producto.cliente;

                    // form.label1.Text = viajeSelect.camion.conductor.user;
                    Chat form = new Chat();
                    form.button2.Visible = true;
                    form.Show();
                }
                else
                {
                    MessageBox.Show("No se selecciono ningun viaje seleccionado");
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

        private void metroButton7_Click(object sender, EventArgs e)
        {
            if (ViajeSleccionado != null)
            {
                //   oBLLusuario.
                panel2.Visible = Visible;
            }
            else
            {
                MessageBox.Show("no se selecciono ningun viaje");
            }

            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ViajeSleccionado = (BEViaje)dataGridView1.CurrentRow.DataBoundItem;

            if (ViajeSleccionado.fecha < DateTime.Now)
            {

            }
        }

        private void metroButton8_Click(object sender, EventArgs e)
        {
            if (ViajeSleccionado != null)
            {
                if (metroComboBox1.SelectedItem != null)
                {
                    BEUsuario adminSelect = (BEUsuario)metroComboBox1.SelectedItem;
                    BEMensaje mensaje = new BEMensaje(SessionManager.GetInstance.Usuario,adminSelect,"Solicito un remplazo de camion/conductor, para el viaje :"+ViajeSleccionado.id,DateTime.Now,2);
                    oBLLmensaje.GuardarMensaje(mensaje);
                    Chat.usuarioAconectar = adminSelect;

                    // form.label1.Text = viajeSelect.camion.conductor.user;
                    Chat form = new Chat();
                    form.button2.Visible = true;
                    form.Show();
                }
            }
        }
    }
}
