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
    public partial class Historial_de_viajes_Clinte : MetroFramework.Forms.MetroForm
    {
        public Historial_de_viajes_Clinte()
        {
            InitializeComponent();
            oBLLviajes = new BLLviaje();
        }
        BLLviaje oBLLviajes;
        private void Historial_de_viajes_Clinte_Load(object sender, EventArgs e)
        {

        }

        void Listar()
        {
            dataGridView1.DataSource = null;
           // dataGridView1.DataSource=oBLLviajes.   hacer metodo en el MPP que me permita traer los viajes del usuario de la session manager
        }
    }
}
