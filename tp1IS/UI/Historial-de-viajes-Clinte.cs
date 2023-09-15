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
namespace UI
{
    public partial class Historial_de_viajes_Clinte : MetroFramework.Forms.MetroForm
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
        
        private void Historial_de_viajes_Clinte_Load(object sender, EventArgs e)
        {

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
                
                viajes = oBLLviajes.Traer_Viajes_Chofer(SessionManager.GetInstance.Usuario.id, pag, producto);
                if (viajes.Count == 0) { metroButton2.Enabled = false; }
                else { metroButton2.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                //  dataGridView1.ReadOnly = true;
                // dataGridView1.Columns["viajes_palets"].ReadOnly = true;
                //   dataGridView1.Columns["distancia"].ReadOnly = true;
                //  dataGridView1.Columns["estado"].ReadOnly = true;
                // dataGridView1.Columns["fecha"].ReadOnly = true;

                //               dataGridView1.Columns["Km_Recorridos"].ReadOnly = false;

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
            // dataGridView1.DataSource=oBLLviajes.   hacer metodo en el MPP que me permita traer los viajes del usuario de la session manager
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
}
}
