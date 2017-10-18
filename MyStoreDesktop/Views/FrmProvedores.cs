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
    public partial class FrmProvedores : Form
    {
        private List<Suppliers> proveedores = new List<Suppliers>();
        private Suppliers proveedor;

        public FrmProvedores()
        {
            InitializeComponent();
        }

        private void GetProveedores()
        {
            try
            {
                SuppliersDAO oDAO = new SuppliersDAO();
                proveedores = oDAO.RetrieveAll();
                var tmpProveedores = (from prov in proveedores
                                     select new
                                     {
                                         Compañia = prov.CompanyName,
                                         Contacto = prov.ContactName,
                                         TitContacto = prov.ContactTitle,
                                         Direccion = prov.Address,
                                         Ciudad = prov.City,
                                         //Region = prov.Region,
                                         CodigoPostal = prov.PostalCode,
                                         Pais = prov.Country,
                                         Telefono = prov.Phone
                                       
                                     }).ToList();
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = tmpProveedores;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "MyStoreDesktop");
            }
        }
        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmProvedores_Load_1(object sender, EventArgs e)
        {
            GetProveedores();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                bool band;
                
                SuppliersDAO oDAO = new SuppliersDAO();
                if (btnAgregar.Text.Equals("Agregar"))
                {
                    proveedor = new Suppliers();
                    PasarObjeto();
                    band = oDAO.Create(proveedor);
                }
                else
                {
                    PasarObjeto();
                    band = oDAO.Update(proveedor);
                }

                if (band)
                {
                    GetProveedores();
                    LimpiarControles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar al proveedor: " + ex.Message, "My Store Desktop",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarControles()
        {
            txtCompa.Text = "";
            txtProveedor.Text = "";
            txtTitCont.Text = "";
            txtDireccion.Text = "";
            txtCiudad.Text = "";
            txtCP.Text = "";
            txtPais.Text = "";
            txtTelefono.Text = "";
        }

        private void PasarObjeto()
        {
            proveedor.CompanyName = txtCompa.Text;
            proveedor.ContactName = txtProveedor.Text;
            proveedor.ContactTitle = txtTitCont.Text;
            proveedor.Address = txtDireccion.Text;
            proveedor.City = txtCiudad.Text;
            proveedor.PostalCode = txtCP.Text;
            proveedor.Country = txtPais.Text;
            proveedor.Phone = txtTelefono.Text;
        }

        private void PasarAControles()
        {
            txtCompa.Text = proveedor.CompanyName;
            txtProveedor.Text = proveedor.ContactName;
            txtTitCont.Text = proveedor.ContactTitle;
            txtDireccion.Text = proveedor.Address;
            txtCiudad.Text = proveedor.City;
            txtCP.Text = proveedor.PostalCode;
            txtPais.Text = proveedor.Country;
            txtTelefono.Text = proveedor.Phone;
            btnAgregar.Text = "Actualizar";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int fila = dgvDatos.CurrentRow.Index;
            proveedor = proveedores.ElementAt(fila);
            PasarAControles();
            btnAgregar.Text = "Actualizar";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            btnAgregar.Text = "Agregar";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                DialogResult res = MessageBox.Show("¿Desea eliminar el proveedor?",
                    "My Store Desktop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    int fila = dgvDatos.CurrentRow.Index;
                    proveedor = proveedores.ElementAt(fila);
                    SuppliersDAO cDAO = new SuppliersDAO();
                    result = cDAO.Delete(proveedor.SupplierID);
                    if (result)
                    {
                        GetProveedores();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el proveedor",
                            "My Store Desktop", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el proveedor: " + ex.Message, "My Store Desktop",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
