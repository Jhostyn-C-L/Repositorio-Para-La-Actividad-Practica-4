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
    public partial class Clientes : Form
    {
        private Practica1Modelo _context;
        //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
        //string connectionsql = ConfigurationManager.ConnectionStrings["connectionsql"].ConnectionString;
        public Clientes()
        {
            InitializeComponent();
        }
        private void Mostrar()
        {
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                SqlDataAdapter data = new SqlDataAdapter("Select * From Clientes", connection);
                DataTable dataTable = new DataTable();
                data.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }*/
            var ListaClientes = _context.Clientes.Select(p => new
            {
                ID = p.ClienteID,
                Nombre = p.NombreCompleto,
                CorreoElectronico = p.CorreoElectronico,
                Telefono = p.Telefono,
                Direccion = p.Direccion
            }).ToList();
            dataGridView1.DataSource = ListaClientes;
        }
        private void Clientes_Load(object sender, EventArgs e)
        {
            _context = new Practica1Modelo();
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
            if (!textBox3.Text.Contains("@") || !textBox3.Text.Contains("."))
            {
                MessageBox.Show("Porfavor, ingrese un correo electronico que sea valido.");
                return;
            }

            if (!long.TryParse(textBox4.Text, out _))
            {
                MessageBox.Show("Recuerde que el telefono solo debe contener valores numericos.");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                string queryinsertclientes = "INSERT INTO Clientes Values (@ClienteID, @NombreCompleto, @CorreoElectronico, @Telefono, @Direccion)";
                SqlCommand cmd = new SqlCommand(queryinsertclientes, connection);

                if (!int.TryParse(textBox1.Text, out int clienteID))
                {
                    MessageBox.Show("El ID debe ser de tipo numerico");
                    return;
                }
                cmd.Parameters.AddWithValue("@ClienteID", clienteID);
                cmd.Parameters.AddWithValue("@NombreCompleto", textBox2.Text);
                cmd.Parameters.AddWithValue("@CorreoElectronico", textBox3.Text);
                cmd.Parameters.AddWithValue("@Telefono", textBox4.Text);
                cmd.Parameters.AddWithValue("@Direccion", textBox5.Text);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }*/
            WindowsFormsApp1.Modelo.Clientes clientes = new WindowsFormsApp1.Modelo.Clientes()
            {
                ClienteID = int.Parse(textBox1.Text),
                NombreCompleto = textBox2.Text,
                CorreoElectronico = textBox3.Text,
                Telefono = textBox4.Text,
                Direccion = textBox5.Text,
            };

            _context.Clientes.Add(clientes);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha insertado el cliente en la base de datos.");
            }
            MessageBox.Show("Operacion de insertar exitosa");
            Mostrar();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Recuerde ingresar un ID de un producto ya existente a actualizar");
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox8.Text) ||
                string.IsNullOrWhiteSpace(textBox9.Text) || string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("Recuerde completar todos los campos, es obligatorio.");
                return;
            }
            if (!textBox8.Text.Contains("@") || !textBox8.Text.Contains("."))
            {
                MessageBox.Show("Porfavor, ingrese un correo electronico que sea valido.");
                return;
            }

            if (!long.TryParse(textBox9.Text, out _))
            {
                MessageBox.Show("Recuerde que el telefono solo debe contener valores numericos.");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                string query = "Update Clientes SET NombreCompleto=@NombreCompleto, CorreoElectronico=@CorreoElectronico, Telefono=@Telefono, Direccion=@Direccion Where ClienteID=@ClienteID";
                SqlCommand cmd = new SqlCommand(@query, connection);

                cmd.Parameters.AddWithValue("@ClienteID", textBox6.Text);
                cmd.Parameters.AddWithValue("@NombreCompleto", textBox7.Text);
                cmd.Parameters.AddWithValue("@CorreoElectronico", textBox8.Text);
                cmd.Parameters.AddWithValue("@Telefono", textBox9.Text);
                cmd.Parameters.AddWithValue("@Direccion", textBox10.Text);
                connection.Open();
                int filas = cmd.ExecuteNonQuery();

                if (filas == 0)
                {
                    MessageBox.Show("No se pudo encontrar el cliente");
                }
                else
                {
                    MessageBox.Show("Operacion de actualizar exitosa");
                }
            }*/
            int clienteID = Convert.ToInt32(textBox6.Text);
            WindowsFormsApp1.Modelo.Clientes clientes = _context.Clientes.FirstOrDefault(q => q.ClienteID.Equals(clienteID));
            if (clientes == null)
            {
                MessageBox.Show("Cliente no existe.");
                return;
            }

            clientes.NombreCompleto = textBox7.Text;
            clientes.CorreoElectronico = textBox8.Text;
            clientes.Telefono = textBox9.Text;
            clientes.Direccion = textBox10.Text;

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha actualizado el cliente en la base de datos.");
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
                "Esta seguro de eliminar este componente de la tabla?",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                string query = "Delete From Clientes Where ClienteID=@ClienteID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ClienteID", textBox11.Text);

                connection.Open();
                int filas = cmd.ExecuteNonQuery();

                if (filas == 0)
                {
                    MessageBox.Show("No se pudo encontrar el cliente");
                }
                else
                {
                    MessageBox.Show("Operacion de borrar exitosa");
                }
            }*/
            int clienteID = Convert.ToInt32(textBox11.Text);
            WindowsFormsApp1.Modelo.Clientes clientes = _context.Clientes.FirstOrDefault(q => q.ClienteID.Equals(clienteID));
            if (clientes == null)
            {
                MessageBox.Show("Cliente no existe.");
                return;
            }

            _context.Clientes.Remove(clientes);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha eliminado el cliente en la base de datos.");
            }
            Mostrar();
        }
    }
}
