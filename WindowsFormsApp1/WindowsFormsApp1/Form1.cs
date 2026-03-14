using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string connectionsql = ConfigurationManager.ConnectionStrings["connectionsql"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clientes frm = new Clientes();
            frm.ShowDialog();
        }
        private void categoriasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Categorias frm = new Categorias();
            frm.ShowDialog();
        }

        private void proveedoresToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Proveedores frm = new Proveedores();
            frm.ShowDialog();
        }

        private void productosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Productos frm = new Productos();
            frm.ShowDialog();
        }
    }
}
