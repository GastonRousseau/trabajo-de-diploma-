using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class HomeAdmin : Form
    {
        public HomeAdmin()
        {
            InitializeComponent();
            customizar();
        }

        void customizar()
        {
            //sub menus
            panel2.Visible = false;
            panel3.Visible = false;
        }

        void customizar2()
        {
            if (panel2.Visible == true)
                panel2.Visible = false;
            if (panel3.Visible == true)
            {
                panel3.Visible = false;
            }
                
        }
        void showmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                customizar2();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }
        private void HomeAdmin_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showmenu(panel2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            showmenu(panel3);
            //depues de presionar cada sub boton,esconder todos los submenus
        }
    }
}
