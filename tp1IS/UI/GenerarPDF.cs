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
using MetroFramework;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
//using iText.Layout;
//using iText.Layout.Element;
//using iText.Kernel.Pdf;
using servicios;
using Patrones.Singleton.Core;
using servicios.ClasesMultiLenguaje;
namespace UI
{
    public partial class GenerarPDF : Form,IdiomaObserver
    {
        public GenerarPDF()
        {
            InitializeComponent();
            oBllviaje = new BLLviaje();

        }
        BLLviaje oBllviaje;
        IList<BEViaje> viajes = new List<BEViaje>();
        BLLviaje oBLLviajes = new BLLviaje();
        string nombreCliente;
        Nullable<DateTime> from;
        Nullable<DateTime> to;
        string RutaGuardado;
        string NombrePDF;
        Dictionary<string, Traduccion> traducciones = new Dictionary<string, Traduccion>();
        List<string> palabras = new List<string>();

        private void GenerarPDF_Load(object sender, EventArgs e)
        {
            buscar(null,null,null);
            Observer.agregarObservador(this);
            traducir();
        }

        void buscar(string nombreCliente, Nullable<DateTime> from, Nullable<DateTime> to)
        {
            try
            {

                viajes = oBLLviajes.getAll_Historial_viajes_SF(nombreCliente, from, to);
          
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = viajes;
                

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

        private void metroButton3_Click(object sender, EventArgs e)
        {
            from = null;
            to = null;
            nombreCliente = null;
            if (textBox3.Text != string.Empty)
            {
                nombreCliente = textBox3.Text;
            }
            if (metroDateTime1.Value != null)
            {
                from = metroDateTime1.Value;
            }
            if (metroDateTime2.Value != null)
            {
                to = metroDateTime2.Value;
            }
            buscar(nombreCliente,from, to);
         
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            from = null;
            to = null;
            nombreCliente = null;
            buscar(nombreCliente, from, to);
           
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
       
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
       
        }

        private bool ValidarRutaYNombrePDF(string rutaGuardado, string nombrePDF)
        {
            if (NombrePDF == null || RutaGuardado == null)
            {
                return false;
            }
  
            if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(rutaGuardado)))
            {
                MessageBox.Show("La ruta de guardado no es válida o no existe.");
                return false;
            }
            return true;
        }
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (viajes != null && viajes.Count>0)
                {
                    OpenFileDialog file = new OpenFileDialog();
                   
                   using(SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            int contador = 0;
                            string Documento = saveFileDialog.FileName+ ".pdf";
                            Document doc = new Document();
                            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Documento, FileMode.Create));
                            doc.Open();
                            doc.Add(new Paragraph("Lista de viajes"));
                            iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8);


                            foreach (var viaje in viajes)
                            {
                                contador++;
                                AgregarTitulo(doc, "viaje " + contador);
                                doc.Add(new Paragraph($"ID del Viaje: {viaje.id}", fuente));
                                doc.Add(new Paragraph($"Partida: {viaje.partida}", fuente));
                                doc.Add(new Paragraph($"Destino: {viaje.destino}", fuente));
                                doc.Add(new Paragraph($"Cantidad de KM: {viaje.cantidad_KM}", fuente));
                                doc.Add(new Paragraph($"Fecha: {viaje.fecha.ToShortDateString()}", fuente));
                                doc.Add(new Paragraph($"Estado: {viaje.estado}", fuente));
                                doc.Add(new Paragraph($"Cantidad de Pallets: {viaje.cantidad_Pallets}", fuente));
                                doc.Add(new Paragraph($"KM Recorridos: {viaje.Km_Recorridos}", fuente));

                                AgregarSubtitulo(doc, "Datos Producto: ");
                                doc.Add(new Paragraph($"ID del Producto: {viaje.producto.id}", fuente));
                                doc.Add(new Paragraph($"Nombre del Producto: {viaje.producto.nombre}", fuente));
                                doc.Add(new Paragraph($"Cantidad de Pallets del Producto: {viaje.producto.CantPallets}", fuente));

                                AgregarSubtitulo(doc, "Datos Camión: ");
                                doc.Add(new Paragraph($"ID del Camión: {viaje.camion.id}", fuente));
                                doc.Add(new Paragraph($"Patente del Camión: {viaje.camion.patente}", fuente));
                                doc.Add(new Paragraph($"Tipo del Camión: {viaje.camion.tipo}", fuente));
                                doc.Add(new Paragraph($"Capacidad de Pallets del Camión: {viaje.camion.capacidad_Pallets}", fuente));

                                AgregarSubtitulo(doc, "Datos Cliente: ");
                                doc.Add(new Paragraph($"ID del Cliente: {viaje.producto.cliente.id}", fuente));
                                doc.Add(new Paragraph($"Nombre del Cliente: {viaje.producto.cliente.user}", fuente));
                                doc.Add(new Paragraph($"Dirección del Cliente: {viaje.producto.cliente.Direccion}", fuente));

                                AgregarSubtitulo(doc, "Datos Conductor: ");
                                doc.Add(new Paragraph($"ID del Conductor: {viaje.camion.conductor.id}", fuente));
                                doc.Add(new Paragraph($"Nombre del Conductor: {viaje.camion.conductor.user}", fuente));
                                doc.Add(new Paragraph($"Dirección del Conductor: {viaje.camion.conductor.Direccion}", fuente));
                            }
                            doc.Close();

                            MessageBox.Show("Se guardo el PDF correctamente, en la ubicacion: " + Documento);
                        }
                    }
                    
                    if (ValidarRutaYNombrePDF(RutaGuardado,NombrePDF)==true)
                    {
                        
                      
                    }
                }
                else
                {
                    MessageBox.Show("hubo un error al guardar el PDF");
                }

            }
            catch(Exception ex)
            {
                throw ex;
       
            }
        }
        private void AgregarTitulo(Document doc, string texto)
        {

            try
            {
                iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18);
                Paragraph paragraph = new Paragraph(texto, fuente);
                paragraph.Alignment = Element.ALIGN_CENTER;
                doc.Add(paragraph);
                doc.Add(new Paragraph(""));
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

        private void AgregarSubtitulo(Document doc, string texto)
        {
            try
            {
                iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14);
                Paragraph paragraph = new Paragraph(texto, fuente);
                doc.Add(paragraph);
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

        private void CellFormatingDatagrid(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;


                    if (viaje != null && viaje.producto != null)
                    {

                        e.Value = viaje.producto.nombre;
                    }
                }
                if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.RowIndex >= 0)
                {
                    BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;


                    if (viaje != null && viaje.camion != null)
                    {

                        e.Value = viaje.camion.patente + " " + viaje.camion.conductor.user;
                    }
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
                       // RecorrerPanel(panel2, 1);
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
              //  RecorrerPanel(panel2, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
