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
using BLL;
using Negocio;
using servicios;
using Patrones.Singleton.Core;
namespace UI
{
    public partial class ServicioTecnicoHome : MetroFramework.Forms.MetroForm
    {
        public ServicioTecnicoHome()
        {
            InitializeComponent();
        }
        Form formularioAbierto = new Form();
        BLLBitacora oBit = new BLLBitacora();
        private void ServicioTecnicoHome_Load(object sender, EventArgs e)
        {

        }
        private void AbrirFormulario(Form formulario)
        {
            try
            {
                if (formularioAbierto != null)
                {

                    formularioAbierto.Close();
                }

                formularioAbierto = formulario;
                formulario.Location = new Point(513, 25);
                formularioAbierto.Show();
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
        private void metroButton2_Click(object sender, EventArgs e)
        {
            Bitacora form = new Bitacora();
            AbrirFormulario(form);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            Changes form = new Changes();
            AbrirFormulario(form);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            InterfazMensajes mensaje = new InterfazMensajes();
            AbrirFormulario(mensaje);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            ViajesPendientes_Empresa form = new ViajesPendientes_Empresa();
            AbrirFormulario(form);
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {


                oBit.guardar_logOut();
                SessionManager.Logout();
                this.Close();

            // Agregar observer    servicios.Observer.eliminarObservador(this);

                var formularios = Application.OpenForms;

                var copiaFormularios = new List<Form>(formularios.OfType<Form>());

                foreach (Form formulario in copiaFormularios)
                {
                    if (formulario.Text != "Welcome!")
                    {
                        formulario.Close();
                    }
                }

                SignIn form = new SignIn();
                form.Show();
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
          //  servicios.Observer.eliminarObservador(this);
        }
    }
}
