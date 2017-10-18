using MyStoreDesktop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStoreDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tsmiCategorias_Click(object sender, EventArgs e)
        {
            FrmCategorias dlgCategorias = new FrmCategorias();
            dlgCategorias.ShowDialog();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProvedores dlgProveedores = new FrmProvedores();
            dlgProveedores.ShowDialog();
        }

        private void tsmiSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmProductos frmProductos = new FrmProductos();
            frmProductos.ShowDialog();
        }
    }
}
