using MyStoreDesktop.DAO;
using MyStoreDesktop.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreDesktop.Views
{
    public partial class FrmCategorias : Form
    {
        private List<Categories> categorias = new List<Categories>();
        private Categories categoria;

        public FrmCategorias()
        {
            InitializeComponent();
        }

       
        private void GetCategorias()
        {
            try
            {
                CategoriesDAO oDAO = new CategoriesDAO();
                categorias = oDAO.RetrieveAll();
                var tmpCategorias = (from c in categorias
                                     select new
                                     {
                                         Categoria = c.CategoryName,
                                         Descripcion = c.Description
                                     }).ToList();
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = tmpCategorias;
            }
            catch(Exception e)
            {
                MessageBox.Show( e.Message,"MyStoreDesktop");
            }
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            GetCategorias();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                bool band;
                CategoriesDAO oDAO = new CategoriesDAO();
                if (btnAgregar.Text.Equals("Agregar"))
                {
                    categoria = new Categories();
                    PasarObjeto();
                     band = oDAO.Create(categoria);
                }
                else
                {
                    PasarObjeto();
                    band = oDAO.Update(categoria);
                }
               
                if (band)
                {
                    GetCategorias();
                    LimpiarControles();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al agregar la categoria: " + ex.Message,"My Store Desktop",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarControles()
        {
            txtCategorias.Text = "";
            txtDescripcion.Text = "";
        }

        private void PasarObjeto()
        {         
            categoria.CategoryName = txtCategorias.Text;
            categoria.Description = txtDescripcion.Text;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            bool result=false;
            try
            {
                DialogResult res = MessageBox.Show("¿Desea eliminar la categoria?",
                    "My Store Desktop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(res == DialogResult.Yes)
                {
                    int fila = dgvDatos.CurrentRow.Index;
                    categoria = categorias.ElementAt(fila);
                    CategoriesDAO cDAO = new CategoriesDAO();
                    result = cDAO.Delete(categoria.CategoryID);
                    if(result)
                    {
                        GetCategorias();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar la categoria",
                            "My Store Desktop",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al eliminar la categoria: " + ex.Message, "My Store Desktop",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int fila = dgvDatos.CurrentRow.Index;
            categoria = categorias.ElementAt(fila);
            PasarAControles();
            btnAgregar.Text = "Actualizar";
        }

        private void PasarAControles()

        {
            txtCategorias.Text = categoria.CategoryName;
            txtDescripcion.Text = categoria.Description;
            btnAgregar.Text = "Actualizar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            btnAgregar.Text = "Agregar";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
