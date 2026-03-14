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
using WindowsFormsApp1.Modelo;

namespace WindowsFormsApp1
{
    public partial class Productos : Form
    {
        private Practica1Modelo _context;
        //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
        //string connectionsql = ConfigurationManager.ConnectionStrings["connectionsql"].ConnectionString;
        public Productos()
        {
            InitializeComponent();
        }
        private void Cargarcategoria()
        {
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                SqlDataAdapter datas = new SqlDataAdapter("SELECT CategoriaID, NombreCategoria From Categorias", connection);
                DataTable data = new DataTable();
                datas.Fill(data);

                comboBox1.DataSource = data.Copy();
                comboBox1.DisplayMember = "NombreCategoria";
                comboBox1.ValueMember = "CategoriaID";

                comboBox2.DataSource = data;
                comboBox2.DisplayMember = "NombreCategoria";
                comboBox2.ValueMember = "CategoriaID";
            }*/
            var categorias = _context.Categorias.Select(c => new {
                CategoriaID = c.CategoriaID,
                NombreCategoria = c.NombreCategoria
            }).ToList();

            comboBox1.DataSource = categorias;
            comboBox1.DisplayMember = "NombreCategoria";
            comboBox1.ValueMember = "CategoriaID";

            comboBox2.DataSource = categorias;
            comboBox2.DisplayMember = "NombreCategoria";
            comboBox2.ValueMember = "CategoriaID";
        }
        private void Mostrar()
        {
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                SqlDataAdapter data = new SqlDataAdapter("Select * From Productos", connection);
                DataTable dataTable = new DataTable();
                data.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            */
            var ListaProductos = _context.Productos.Select(p => new
        {
            ID = p.ProductoID,
            Nombre = p.NombreProducto,
            Descripcion = p.Descripcion,
            Precio = p.Precio,
            Stock = p.Stock,
            Categorias = p.Categorias.NombreCategoria
        }).ToList();
            dataGridView1.DataSource = ListaProductos;
        }


        private void Productos_Load(object sender, EventArgs e)
        {
            _context = new Practica1Modelo();

            Cargarcategoria();
            Mostrar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Recuerde completar todos los campos, es obligatorio.");
                return;
            }

            decimal precio;
            int stock;

            if (!decimal.TryParse(textBox4.Text, out precio) || precio < 0)
            {
                MessageBox.Show("Debe ingresar un precio valido superior o igual a 0");
                return;
            }

            if (!int.TryParse(textBox5.Text, out stock) || stock < 0)
            {
                MessageBox.Show("Debe ingresar un stock valido superior o igual a 0");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            //Esto seria el codigo cuando no se estaba utilizando antes del entity framework, osea se usaba Ado.net, directo a la base
            /*using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                string query = "INSERT INTO Productos Values (@ProductoID, @NombreProducto, @Descripcion, @Precio, @Stock, @Categoria)";
                SqlCommand cmd = new SqlCommand(query, connection);

                if (!int.TryParse(textBox1.Text, out int productoID))
                {
                    MessageBox.Show("El ID debe ser de tipo numerico");
                    return;
                }
                cmd.Parameters.AddWithValue("@ProductoID", productoID);
                cmd.Parameters.AddWithValue("@NombreProducto", textBox2.Text);
                cmd.Parameters.AddWithValue("@Descripcion", textBox3.Text);
                cmd.Parameters.AddWithValue("@Precio", decimal.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@Stock", int.Parse(textBox5.Text));
                cmd.Parameters.AddWithValue("@Categoria", comboBox1.SelectedValue);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
            */

            WindowsFormsApp1.Modelo.Productos productos = new WindowsFormsApp1.Modelo.Productos()
            {
                ProductoID = int.Parse(textBox1.Text),
                NombreProducto = textBox2.Text,
                Descripcion = textBox3.Text,
                Precio = decimal.Parse(textBox4.Text),
                Stock = int.Parse(textBox5.Text),
                CategoriaID = Convert.ToInt32(comboBox1.SelectedValue),
            };

            _context.Productos.Add(productos);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0) {
                MessageBox.Show("Se ha insertado el producto en la base de datos.");
            }
            MessageBox.Show("Operacion de insertar exitosa");
            Mostrar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("Recuerde ingresar un ID de un producto ya existente a actualizar");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox9.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(textBox7.Text) || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Recuerde completar todos los campos, es obligatorio.");
                return;
            }

            decimal precio;
            int stock;

            if (!decimal.TryParse(textBox7.Text, out precio) || precio < 0)
            {
                MessageBox.Show("Porfavor, coloque un precio valido");
                return;
            }
            if (!int.TryParse(textBox6.Text, out stock) || stock < 0)
            {
                MessageBox.Show("Porfavor, coloque un stock valido");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                string query = "Update Productos SET NombreProducto=@NombreProducto, Descripcion=@Descripcion, Precio=@Precio, Stock=@Stock, CategoriaID=@categoria Where ProductoID=@ProductoID";
                SqlCommand cmd = new SqlCommand(@query, connection);


                if (!int.TryParse(textBox10.Text, out int productoID))
                {
                    MessageBox.Show("El ID debe ser de tipo numerico");
                    return;
                }
                cmd.Parameters.AddWithValue("@ProductoID", productoID);
                cmd.Parameters.AddWithValue("@NombreProducto", textBox9.Text);
                cmd.Parameters.AddWithValue("@Descripcion", textBox8.Text);
                cmd.Parameters.AddWithValue("@Precio", decimal.Parse(textBox7.Text));
                cmd.Parameters.AddWithValue("@Stock", int.Parse(textBox6.Text));
                cmd.Parameters.AddWithValue("@categoria", comboBox2.SelectedValue);

                connection.Open();
                int filas = cmd.ExecuteNonQuery();

                if (filas == 0)
                {
                    MessageBox.Show("No se pudo encontrar el producto");
                }
                else
                {
                    MessageBox.Show("Operacion de actualizar exitosa");
                }
            }
            */
            int productoID = Convert.ToInt32(textBox10.Text);
            WindowsFormsApp1.Modelo.Productos productos = _context.Productos.FirstOrDefault(q => q.ProductoID.Equals(productoID));
            if (productos == null)
            {
                MessageBox.Show("Producto no existe.");
                return;
            }

            productos.NombreProducto = textBox9.Text;
            productos.Descripcion = textBox8.Text;
            productos.Precio = decimal.Parse(textBox7.Text);
            productos.Stock = int.Parse(textBox6.Text);
            productos.CategoriaID = Convert.ToInt32(comboBox2.SelectedValue);
        
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0) {
                MessageBox.Show("Se ha actualizado el producto en la base de datos.");
            }
            Mostrar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Porfavor, ingrese un ID ya existente para eliminar");
                return;
            }
            DialogResult result = MessageBox.Show(
                "Esta seguro de eliminar este producto de la tabla?",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                string query = "Delete From Productos Where ProductoID=@ProductoID";
                SqlCommand cmd = new SqlCommand(query, connection);
                if (!int.TryParse(textBox11.Text, out int productoID))
                {
                    MessageBox.Show("El ID debe ser de tipo numerico");
                    return;
                }
                cmd.Parameters.AddWithValue("@ProductoID", productoID);

                connection.Open();
                int filas = cmd.ExecuteNonQuery();

                if (filas == 0)
                {
                    MessageBox.Show("No se pudo encontrar el producto");
                }
                else
                {
                    MessageBox.Show("Operacion de borrar exitosa");
                }
            }*/
            int productoID = Convert.ToInt32(textBox11.Text);
            WindowsFormsApp1.Modelo.Productos productos = _context.Productos.FirstOrDefault(q => q.ProductoID.Equals(productoID));
            if (productos == null)
            {
                MessageBox.Show("Producto no existe.");
                return;
            }

            _context.Productos.Remove(productos);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0) {
                MessageBox.Show("Se ha eliminado el producto en la base de datos.");
            }
            Mostrar();
        }
    }
}
