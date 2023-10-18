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
namespace UI
{
    public partial class GenerarPDF : Form
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
       
        private void GenerarPDF_Load(object sender, EventArgs e)
        {
            buscar(null,null,null);
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
          /*  RutaGuardado = null;
            NombrePDF = null;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;*/
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
           /* RutaGuardado = textBox1.Text;
            NombrePDF = textBox2.Text+".PDF";*/
        }

        private bool ValidarRutaYNombrePDF(string rutaGuardado, string nombrePDF)
        {
            if (NombrePDF == null || RutaGuardado == null)
            {
                return false;
            }
            // Verificar si la ruta de guardado es una ruta válida
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
                //throw ex;
                //textBox1.Text=ex.ToString();
            }
        }
        private void AgregarTitulo(Document doc, string texto)
        {
            // Establecer el tamaño y el estilo del título
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 18);

            // Crear el párrafo para el título
            Paragraph paragraph = new Paragraph(texto, fuente);
            paragraph.Alignment = Element.ALIGN_CENTER;

            // Agregar el título al documento
            doc.Add(paragraph);

            // Agregar espacio después del título
            doc.Add(new Paragraph("")); // Salto de línea
        }

        private void AgregarSubtitulo(Document doc, string texto)
        {
            // Establecer el tamaño y el estilo del subtítulo
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14);

            // Crear el párrafo para el subtítulo
            Paragraph paragraph = new Paragraph(texto, fuente);

            // Agregar el subtítulo al documento
            doc.Add(paragraph);
        }

        private void CellFormatingDatagrid(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["producto"].Index && e.RowIndex >= 0)
            {
                BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;

                // Asegúrate de que la celda tenga un valor de Viaje no nulo
                if (viaje != null && viaje.producto != null)
                {
                    // Configura el valor de la celda para mostrar el nombre del producto
                    e.Value = viaje.producto.nombre;
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["camion"].Index && e.RowIndex >= 0)
            {
                BEViaje viaje = dataGridView1.Rows[e.RowIndex].DataBoundItem as BEViaje;

                // Asegúrate de que la celda tenga un valor de Viaje no nulo
                if (viaje != null && viaje.camion != null)
                {
                    // Configura el valor de la celda para mostrar el nombre del producto
                    e.Value = viaje.camion.patente + " " + viaje.camion.conductor.user;
                }
            }
        }
    }
}
