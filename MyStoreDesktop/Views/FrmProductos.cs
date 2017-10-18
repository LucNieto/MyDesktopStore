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
    public partial class FrmProductos : Form
    {
        private List<ProductsCategorySupplier> productos = new List<ProductsCategorySupplier>();
       
        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
            {
                GetProductos();
            }

        private void GetProductos()
        {
            try
            {
                ProductsDAO oDAO = new ProductsDAO();
                productos = oDAO.RetrieveAll();
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = productos;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "MyStoreDesktop");
            }
        }

       
    }
}
