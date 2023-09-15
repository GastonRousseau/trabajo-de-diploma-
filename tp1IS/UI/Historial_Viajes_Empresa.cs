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
namespace UI
{
    public partial class Historial_Viajes_Empresa : MetroFramework.Forms.MetroForm
    {
        public Historial_Viajes_Empresa()
        {
            InitializeComponent();
           // buscar(null, 1, null);    arreglar para que sea de una fecha a otra
        }
        IList<BEViaje> viajes = new List<BEViaje>();
        BLLviaje oBLLviajes = new BLLviaje();
        int pag;
        string nombreCliente;
        DateTime fecha;
        private void Historial_Viajes_Empresa_Load(object sender, EventArgs e)
        {

        }

        void buscar(string nombreCliente,int pag,DateTime fecha)
        {
            try
            {

                viajes = oBLLviajes.getAll_Historial_viajes_(pag,nombreCliente, fecha);
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
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

            pag -= 1;
            metroButton1.Enabled = true;
            if (pag <= 1) metroButton1.Enabled = false;
            if (pag > 0) buscar(nombreCliente,pag,fecha);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            metroButton1.Enabled = true;
            pag += 1;
            buscar(nombreCliente, pag, fecha);
        }
    }
}
