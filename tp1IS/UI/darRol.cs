using BE;
using BLL;
using MetroFramework;
using Negocio;
using Patrones.Singleton.Core;
using servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using servicios.ClasesMultiLenguaje;

namespace UI
{
    public partial class darRol : MetroFramework.Forms.MetroForm,IdiomaObserver
    {
        public darRol()
        {
            InitializeComponent();
            llenarComboBox();
            iniciarTreeView();
            resetControls();
        } 
        BLLBitacora oBit = new BLLBitacora();
        validaciones validar = new validaciones();
        public void resetControls()
        {
            comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1;
            treeView2.Nodes[0].TreeView.SelectedNode = null;
            treeView2.SelectedNode = null;
        }
        public void iniciarTreeView()
        {
            try
            {
                treeView2.Nodes.Clear();
                IList<Componente> lista = oComp.GetFamilias();
                IList<Componente> cada_Flia;
                TreeNode padre = new TreeNode("Main");
                TreeNode familia;
                foreach (Componente comp in lista)
                {
                    cada_Flia = oComp.GetAll(comp.Id);
                    familia = new TreeNode(comp.Nombre);

                    cargarTreeView(cada_Flia, familia);
                    padre.Nodes.Add(familia);
                }
                treeView2.Nodes.Add(padre);
            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex) {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }


        }
        public void cargar_roles(int id)
        {
            try
            {
                treeView1.Nodes.Clear();
                IList<Componente> lista2 = oComp.GetPermisosdeUser(id);
                IList<Componente> cada_Flia2;
                TreeNode padre2 = new TreeNode("Main");
                TreeNode familia2;
                foreach (Componente comp in lista2)
                {
                    cada_Flia2 = oComp.GetAll(comp.Id);
                    familia2 = new TreeNode(comp.Nombre);

                    cargarTreeView(cada_Flia2, familia2);
                    padre2.Nodes.Add(familia2);
                }
                treeView1.Nodes.Add(padre2);
            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex) {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
        }

        public void cargarTreeView(IList<Componente> list, TreeNode parentNode)
        {
            try
            {
                foreach (var item in list)
                {
                    TreeNode newNode = new TreeNode(item.Nombre);
                    parentNode.Nodes.Add(newNode);
                    if (item.Hijos != null && item.Hijos.Count != 0) cargarTreeView(item.Hijos, newNode);
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
        public void llenarComboBox()
        {
            try
            {
                IList<Componente> lista = oComp.GetFamilias();
                List<string> familias = new List<string>();
                foreach (Componente componente in lista)
                {
                    familias.Add(componente.Nombre);
                }
                comboBox2.DataSource = familias;
                IList<Componente> lista2 = oComp.GetPermisos();
                List<string> permisos = new List<string>();
                foreach (Componente componente in lista2)
                {
                    permisos.Add(componente.Nombre);
                }
                comboBox1.DataSource = permisos;
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
        private void darRol_Load(object sender, EventArgs e)
        {
            try
            {
                servicios.Observer.agregarObservador(this);
                Traducir();
                ListarIdiomas();

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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Observer.eliminarObservador(this);
        }
        private void darRol_FormClosing(object sender, FormClosingEventArgs e)
        {
            servicios.Observer.eliminarObservador(this);
        }
        BLLComposite oComp = new BLLComposite();
        BLLUsuario oLog = new BLLUsuario();
        public Componente findInList(IList<Componente> list, string nombre)
        {
            try
            {
                Componente encontrado = null;
                foreach (Componente item in list)
                {
                    if (item != null)
                    {
                        if (item.Nombre == nombre) return item;
                        if (item.Hijos != null) encontrado = findInList(item.Hijos, nombre);
                        if (encontrado != null)
                        {
                            return encontrado;
                        }
                    }

                }
                return null;
            }
            catch (NullReferenceException ex)
            {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex) {
                var accion = ex.Message;
                oBit.guardar_accion(accion, 1);
                MessageBox.Show(ex.Message);
            }
            return null;
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                errorProvider1.SetError(treeView2, "");
                errorProvider1.SetError(textBox1, "");
                if (treeView2.SelectedNode == null && comboBox1.SelectedIndex == -1 && comboBox2.SelectedIndex == -1) 
                {
                    errorProvider1.SetError(treeView2, "Select a role");
                    return;
                }
                if (textBox1.Text == string.Empty || !validar.id(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "You should enter an id with 1 to 9 numbers");
                    return;
                }
                if (u.Usuario.id == Convert.ToInt32(textBox1.Text))
                {
                    errorProvider1.SetError(treeView2, "You can not add roles to yourself");
                    return;
                }
                IList<Componente> listFam = oComp.GetFamilias();
                IList<Componente> listPer = oComp.GetPermisos();
                Componente itemPatente = null;
                Componente itemFamilia = null;
                if (!oLog.usuario_existente(Convert.ToInt32(textBox1.Text))) errorProvider1.SetError(treeView2, "User does not exist");
                if (treeView2.SelectedNode != null)
                {
                    itemPatente = findInList(listPer, treeView2.SelectedNode.Text);
                    itemFamilia = findInList(listFam, treeView2.SelectedNode.Text);
                }
                else if (comboBox1.SelectedIndex != -1)
                {
                    itemPatente = findInList(listPer, comboBox1.SelectedItem.ToString());
                }
                else if (comboBox2.SelectedIndex != -1)
                {
                    itemFamilia = findInList(listFam, comboBox2.SelectedItem.ToString());
                }
                    if (oLog.usuario_existente(Convert.ToInt32(textBox1.Text)) && (itemFamilia != null || itemPatente != null)) { 
                        if(itemPatente != null)
                        {
                            var accion = "dio al usuario " + textBox1.Text + " el permiso " + itemPatente.Id.ToString();
                            oBit.guardar_accion(accion, 2);
                            if (!oLog.tiene_rol(Convert.ToInt32(textBox1.Text), itemPatente.Id))
                            {
                                if (oLog.agregar_rol(Convert.ToInt32(textBox1.Text), itemPatente.Id)) MetroMessageBox.Show(this, "rol asignado");
                            }
                        else MetroMessageBox.Show(this, "Selected user already has that rol");

                    }



                    else if(itemFamilia != null)
                    {
                            var accion = "dio al usuario " + textBox1.Text + " el rol " + itemFamilia.Id.ToString();
                            oBit.guardar_accion(accion, 2);
                            if (!oLog.tiene_rol(Convert.ToInt32(textBox1.Text), itemFamilia.Id))
                            {
                                if (oLog.agregar_rol(Convert.ToInt32(textBox1.Text), itemFamilia.Id)) MetroMessageBox.Show(this, "rol asignado");
                            }
                        else MetroMessageBox.Show(this, "Selected user already has that rol");
                    
                    }
                }
                else
                    {
                        errorProvider1.SetError(treeView2, "Select a father role different from main");
                    }

                resetControls();
                cargar_roles(Convert.ToInt32(textBox1.Text));
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
        SessionManager u = SessionManager.GetInstance;
        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                
                errorProvider1.Clear();
                errorProvider1.SetError(treeView1, "");
                errorProvider1.SetError(textBox1, "");
                if (treeView1.SelectedNode == null)
                {
                    errorProvider1.SetError(treeView1, "Select a role to delete");
                    return;
                }
                if (textBox1.Text == string.Empty || !validar.id(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "You should enter an id with 1 to 9 numbers");
                    return;
                }
                if (u.Usuario.id == Convert.ToInt32(textBox1.Text))
                {
                    errorProvider1.SetError(treeView1, "You can not delete roles from yourself");
                    return;
                }
                if (!oLog.usuario_existente(Convert.ToInt32(textBox1.Text)))
                {
                    errorProvider1.SetError(treeView1, "User does not exist");
                    return;
                }
                IList<Componente> listFam = oComp.GetFamilias();
                IList<Componente> listPer = oComp.GetPermisos();
                Componente itemPatente = null;
                Componente itemFamilia = null;
                if (treeView1.SelectedNode != null)
                {
                    itemPatente = findInList(listPer, treeView1.SelectedNode.Text);
                    itemFamilia = findInList(listFam, treeView1.SelectedNode.Text);
                }
                if (oLog.usuario_existente(Convert.ToInt32(textBox1.Text)) && (itemFamilia != null || itemPatente != null))
                    {
                        if (itemPatente != null)
                        {
                            var accion = "desasigno al usuario " + textBox1.Text + " el permiso " + itemPatente.Id.ToString();
                            oBit.guardar_accion(accion, 2);
                            if (oLog.borrar_rol(Convert.ToInt32(textBox1.Text), itemPatente.Id)) MetroMessageBox.Show(this, "Role unasigned");
                            else MetroMessageBox.Show(this, "Role not found asigned to user");
                        }
                        else if (itemFamilia != null)
                        {
                            var accion = "desasigno al usuario " + textBox1.Text + " el rol " + itemFamilia.Id.ToString();
                            oBit.guardar_accion(accion, 2);
                            if (oLog.borrar_rol(Convert.ToInt32(textBox1.Text), itemFamilia.Id)) MetroMessageBox.Show(this, "Role unasigned");
                            else MetroMessageBox.Show(this, "Role not found asigned to user");

                        }

                        else MetroMessageBox.Show(this, "Error, try again");


                    }
                else{
                    MetroMessageBox.Show(this, "Error, try again");
                }
                resetControls();
                cargar_roles(Convert.ToInt32(textBox1.Text));
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

        private void metroButton3_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                errorProvider1.SetError(textBox1, "");
                if (textBox1.Text == string.Empty || !validar.id(textBox1.Text))
                {
                    errorProvider1.SetError(textBox1, "You should enter an id with 1 to 9 numbers");
                    return;
                }
                if (oLog.usuario_existente(Convert.ToInt32(textBox1.Text))){
                    cargar_roles(Convert.ToInt32(textBox1.Text));
                }
                else
                {
                    MetroMessageBox.Show(this, "There is no user related with this id");
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

        public void CambiarIdioma(Idioma Idioma)
        {
            try
            {
                Traducir();
                ListarIdiomas();
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
        public void ListarIdiomas()
        {
            try
            {
                comboBox3.Items.Clear();
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                var ListaIdiomas = Traductor.ObtenerIdiomas();

                foreach (Idioma idioma in ListaIdiomas)
                {
                    var traducciones = Traductor.obtenertraducciones(idioma);
                    List<string> Lista = new List<string>();
                    Lista = Traductor.obtenerIdiomaOriginal();
                    if (traducciones.Values.Count == Lista.Count)
                    {
                        comboBox3.Items.Add(idioma.Nombre);
                    }
                    else
                    {
                        if (idioma.Default == true)
                        {
                            comboBox3.Items.Add(idioma.Nombre);
                        }
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
        public void Traducir()
        {
            try
            {
                Idioma Idioma = null;

                if (SessionManager.TraerUsuario())
                    Idioma = SessionManager.GetInstance.idioma;
                if (Idioma.Nombre == "Ingles")
                {
                    VolverAlIdiomaOriginal();
                }
                else
                {
                    BLL.BLLTraductor Traductor = new BLL.BLLTraductor();


                    var traducciones = Traductor.obtenertraducciones(Idioma);
                    List<string> Lista = new List<string>();
                    Lista = Traductor.obtenerIdiomaOriginal();
                    if (traducciones.Values.Count != Lista.Count)
                    {
                        
                    }
                    else
                    {
                        if (this.Tag != null && traducciones.ContainsKey(this.Tag.ToString()))
                        {
                            this.Text = traducciones[this.Tag.ToString()].texto;
                        }
                        if (metroButton1.Tag != null && traducciones.ContainsKey(metroButton1.Tag.ToString()))
                        {
                            this.metroButton1.Text = traducciones[metroButton1.Tag.ToString()].texto;
                        }
                        if (metroButton2.Tag != null && traducciones.ContainsKey(metroButton2.Tag.ToString()))
                        {
                            this.metroButton2.Text = traducciones[metroButton2.Tag.ToString()].texto;
                        }
                        if (metroButton3.Tag != null && traducciones.ContainsKey(metroButton3.Tag.ToString()))
                        {
                            this.metroButton3.Text = traducciones[metroButton3.Tag.ToString()].texto;
                        }
                       
                        if (metroLabel1.Tag != null && traducciones.ContainsKey(metroLabel1.Tag.ToString()))
                        {
                            this.metroLabel1.Text = traducciones[metroLabel1.Tag.ToString()].texto;
                        }
                        if (metroLabel2.Tag != null && traducciones.ContainsKey(metroLabel2.Tag.ToString()))
                        {
                            this.metroLabel2.Text = traducciones[metroLabel2.Tag.ToString()].texto;
                        }
                        if (metroLabel3.Tag != null && traducciones.ContainsKey(metroLabel3.Tag.ToString()))
                        {
                            this.metroLabel3.Text = traducciones[metroLabel3.Tag.ToString()].texto;
                        }
                        if (metroLabel4.Tag != null && traducciones.ContainsKey(metroLabel4.Tag.ToString()))
                        {
                            this.metroLabel4.Text = traducciones[metroLabel4.Tag.ToString()].texto;
                        }
                        if (metroLabel5.Tag != null && traducciones.ContainsKey(metroLabel5.Tag.ToString()))
                        {
                            this.metroLabel5.Text = traducciones[metroLabel5.Tag.ToString()].texto;
                        }
                        if (metroLabel6.Tag != null && traducciones.ContainsKey(metroLabel6.Tag.ToString()))
                        {
                            this.metroLabel6.Text = traducciones[metroLabel6.Tag.ToString()].texto;
                        }
                        if (metroLabel7.Tag != null && traducciones.ContainsKey(metroLabel7.Tag.ToString()))
                        {
                            this.metroLabel7.Text = traducciones[metroLabel7.Tag.ToString()].texto;
                        }

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
        public void VolverAlIdiomaOriginal()
        {
            try
            {
                BLL.BLLTraductor Traductor = new BLL.BLLTraductor();
                List<string> palabras = Traductor.obtenerIdiomaOriginal();


                if (this.Tag != null && palabras.Contains(this.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(this.Tag.ToString()));
                    this.Text = traduccion;
                }
                if (metroButton1.Tag != null && palabras.Contains(metroButton1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton1.Tag.ToString()));
                    this.metroButton1.Text = traduccion;
                }
                if (metroButton2.Tag != null && palabras.Contains(metroButton2.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton2.Tag.ToString()));
                    this.metroButton2.Text = traduccion;
                }
                if (metroButton3.Tag != null && palabras.Contains(metroButton3.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroButton3.Tag.ToString()));
                    this.metroButton3.Text = traduccion;
                }
                
                if (metroLabel1.Tag != null && palabras.Contains(metroLabel1.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel1.Tag.ToString()));
                    this.metroLabel1.Text = traduccion;
                }
                if (metroLabel2.Tag != null && palabras.Contains(metroLabel2.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel2.Tag.ToString()));
                    this.metroLabel2.Text = traduccion;
                }
                if (metroLabel3.Tag != null && palabras.Contains(metroLabel3.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel3.Tag.ToString()));
                    this.metroLabel3.Text = traduccion;
                }
                if (metroLabel4.Tag != null && palabras.Contains(metroLabel4.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel4.Tag.ToString()));
                    this.metroLabel4.Text = traduccion;
                }
                if (metroLabel5.Tag != null && palabras.Contains(metroLabel5.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel5.Tag.ToString()));
                    this.metroLabel5.Text = traduccion;
                }
                if (metroLabel6.Tag != null && palabras.Contains(metroLabel6.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel6.Tag.ToString()));
                    this.metroLabel6.Text = traduccion;
                }
                if (metroLabel7.Tag != null && palabras.Contains(metroLabel7.Tag.ToString()))
                {
                    string traduccion = palabras.Find(p => p.Equals(metroLabel7.Tag.ToString()));
                    this.metroLabel7.Text = traduccion;
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
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string idiomaSelec = comboBox3.SelectedItem.ToString();
                BLL.BLLTraductor traductor = new BLL.BLLTraductor();
                Idioma Oidioma = new Idioma();
                Oidioma = traductor.TraerIdioma(idiomaSelec);
                servicios.Observer.cambiarIdioma(Oidioma);

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
    }
}
