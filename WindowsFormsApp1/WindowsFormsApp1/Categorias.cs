using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Modelo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Categorias : Form
    {
        private Practica1Modelo _context;
        //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
        //string connectionsql = ConfigurationManager.ConnectionStrings["connectionsql"].ConnectionString;
        public Categorias()
        {
            InitializeComponent();
        }
        private void Mostrar()
        {
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                SqlDataAdapter data = new SqlDataAdapter("Select * From Categorias", connection);
                DataTable dataTable = new DataTable();
                data.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }*/
            var ListaCategorias = _context.Categorias.Select(p => new
            {
                ID = p.CategoriaID,
                Nombre = p.NombreCategoria
            }).ToList();
            dataGridView1.DataSource = ListaCategorias;
        }
        private void Categorias_Load(object sender, EventArgs e)
        {
            _context = new Practica1Modelo();
            Mostrar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
               string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Recuerde completar todos los campos, es obligatorio.");
                return;
            }
            if (!int.TryParse(textBox1.Text, out int categoriaID))
            {
                MessageBox.Show("El id debe ingresado debe ser numerico.");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                connection.Open();
                SqlCommand checkCmd = new SqlCommand(
                    "Select Count(*) From Categorias Where CategoriaID=@CategoriaID", connection);
                checkCmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                int existe = (int)checkCmd.ExecuteScalar();
                if (existe > 0)
                {
                    MessageBox.Show("Ya existe una categoria con ese ID");
                    return;
                }
                string query = "INSERT INTO Categorias Values (@CategoriaID, @NombreCategoria)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                cmd.Parameters.AddWithValue("@NombreCategoria", textBox2.Text);

                cmd.ExecuteNonQuery();
            */

            WindowsFormsApp1.Modelo.Categorias categorias = new WindowsFormsApp1.Modelo.Categorias()
            {
                CategoriaID = int.Parse(textBox1.Text),
                NombreCategoria = textBox2.Text,
            };

            _context.Categorias.Add(categorias);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha insertado el categoria en la base de datos.");
            }
            Mostrar();
            MessageBox.Show("Operacion de insertar exitosa");
        }
                        
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text) ||
               string.IsNullOrWhiteSpace(textBox7.Text))
            {
                MessageBox.Show("Recuerde completar todos los campos, es obligatorio.");
                return;
            }
            if (!int.TryParse(textBox6.Text, out int categoriaID))
            {
                MessageBox.Show("El id debe ingresado debe ser numerico.");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                connection.Open();
                SqlCommand checkCmd = new SqlCommand(
                    "Select Count(*) From Categorias Where CategoriaID=@CategoriaID", connection);
                checkCmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                int existe = (int)checkCmd.ExecuteScalar();
                if (existe == 0)
                {
                    MessageBox.Show("Esta categoria no existe, verifique las ya existentes");
                    return;
                }
                string query = "Update Categorias SET NombreCategoria=@NombreCategoria Where CategoriaID=@CategoriaID";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                cmd.Parameters.AddWithValue("@NombreCategoria", textBox7.Text);

                cmd.ExecuteNonQuery();
            }*/
            //algo
            WindowsFormsApp1.Modelo.Categorias categorias = _context.Categorias.FirstOrDefault(q => q.CategoriaID.Equals(categoriaID));
            if (categorias == null)
            {
                MessageBox.Show("Producto no existe.");
                return;
            }

            categorias.NombreCategoria = textBox7.Text;

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha actualizado el categoria en la base de datos.");
            }
            Mostrar();
            MessageBox.Show("Operacion de actualizar exitosa");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox11.Text, out int categoriaID))
            {
                MessageBox.Show("Coloque un id valido.");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                connection.Open();
                SqlCommand checkCmd = new SqlCommand(
                    "Select Count(*) From Categorias Where CategoriaID=@CategoriaID", connection);
                checkCmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                int existe = (int)checkCmd.ExecuteScalar();
                if (existe == 0)
                {
                    MessageBox.Show("Esta categoria no existe, verifique las ya existentes");
                    return;
                }
                string query = "Delete From Categorias Where CategoriaID=@CategoriaID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);

                cmd.ExecuteNonQuery();
            }*/
            WindowsFormsApp1.Modelo.Categorias categorias = _context.Categorias.FirstOrDefault(q => q.CategoriaID.Equals(categoriaID));
            if (categorias == null)
            {
                MessageBox.Show("Categoria no existe.");
                return;
            }

            _context.Categorias.Remove(categorias);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha eliminado el categoria en la base de datos.");
            }
            Mostrar();
            MessageBox.Show("Operacion de borrar exitosa");
        }
    }
}
