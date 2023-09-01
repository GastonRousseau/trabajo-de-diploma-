using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.controles
{
    public partial class BarrarProgreso : UserControl
    {
        public BarrarProgreso()
        {
            InitializeComponent();
            BackColor = Color.LightSkyBlue;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 3;
        }
        public int ProgressValue
        {
            get { return progressBar1.Value; }
            set
            {
                if (value >= progressBar1.Minimum && value <= progressBar1.Maximum)
                {
                    progressBar1.Value = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("ProgressValue debe estar entre Minimum y Maximum.");
                }
            }
        }
        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
