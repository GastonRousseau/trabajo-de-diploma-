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
using Negocio;
using servicios;
using servicios.ClasesMultiLenguaje;
using System.IO;
using Newtonsoft.Json;
using Patrones.Singleton.Core;
namespace UI
{
    public partial class GenerarJSON : Form,IdiomaObserver
    {
        public GenerarJSON()
        {
            InitializeComponent();
            oBEusuario = new BEUsuario();
            oBLLusuario = new BLLUsuario();
            oBit = new BLLBitacora();
            buscar(null, 1);
        }
        BEUsuario oBEusuario;
        BLLUsuario oBLLusuario;
        IList<BEUsuario> usuarios;
        BLLBitacora oBit;
        int pag;
        string nombre;
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();
        private void GenerarJSON_Load(object sender, EventArgs e)
        {
            Observer.agregarObservador(this);
            pag = 1;
            metroButton2.Enabled = false;
        }

        public void buscar(string nombre, int pag)
        {
            try
            {
                usuarios = oBLLusuario.GetAllConductores(nombre, pag);
                if (usuarios.Count == 0) { metroButton3.Enabled = false; }
                else { metroButton3.Enabled = true; }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = usuarios;
                dataGridView1.Columns["permisos"].Visible = false;
                dataGridView1.Columns["rol"].Visible = false;
                dataGridView1.Columns["DV"].Visible = false;
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
            pag -= 1;
            metroButton2.Enabled = true;
            if (pag <= 1) metroButton2.Enabled = false;
            if (pag > 0) buscar(nombre, pag);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            metroButton2.Enabled = true;
            pag += 1;
            buscar(nombre, pag);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos JSON|*.json";
            saveFileDialog.Title = "Guardar archivo JSON";
            saveFileDialog.FileName = "Conductores.json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = saveFileDialog.FileName;

                using (FileStream fs = new FileStream(rutaArchivo, FileMode.Append, FileAccess.Write))
                {
                    if (usuarios.Count > 0)
                    {
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(writer, usuarios);
                        }
                    }
                    else
                    {
                        MessageBox.Show("no hay ningun conductor");
                    }
                    
                }
            }
        }

        public void CambiarIdioma(Idioma Idioma)
        {
            //throw new NotImplementedException();
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
                        RecorrerPanel(this, 1);
                        // RecorrerPanel(panel2, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void RecorrerPanel(Control panel, int v)
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

                RecorrerPanel(this, 2);
                //  RecorrerPanel(panel2, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
