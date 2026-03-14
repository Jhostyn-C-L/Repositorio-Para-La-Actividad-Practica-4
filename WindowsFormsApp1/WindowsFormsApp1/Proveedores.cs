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
    public partial class Proveedores : Form
    {
        private Practica1Modelo _context;
        //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
        //string connectionsql = ConfigurationManager.ConnectionStrings["connectionsql"].ConnectionString;
        public Proveedores()
        {
            InitializeComponent();
        }
        private void Mostrar()
        {
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                SqlDataAdapter data = new SqlDataAdapter("Select * From Proveedores", connection);
                DataTable dataTable = new DataTable();
                data.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }*/
            var ListaProveedores = _context.Proveedores.Select(p => new
            {
                ID = p.ProveedorID,
                Nombre = p.NombreProveedor,
                Telefono = p.Telefono,
                CorreoElectronico = p.CorreoElectronico
            }).ToList();
            dataGridView1.DataSource = ListaProveedores;
        }
        private bool Correovalido(string correo)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(correo);
                return mail.Address == correo;
            }
            catch
            {
                return false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Recuerde completar todos los campos, es obligatorio.");
                return;
            }
            if (!int.TryParse(textBox1.Text, out int proveedorID))
            {
                MessageBox.Show("El id debe ingresado debe ser numerico.");
                return;
            }
            if (!Correovalido(textBox4.Text))
            {
                MessageBox.Show("El correo electronico es invalido.");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                connection.Open();
                SqlCommand checkCmd = new SqlCommand(
                    "Select Count(*) From Proveedores Where ProveedorID=@ProveedorID", connection);
                checkCmd.Parameters.AddWithValue("@ProveedorID", proveedorID);
                int existe = (int)checkCmd.ExecuteScalar();
                if (existe > 0)
                {
                    MessageBox.Show("Ya existe un proveedor con ese ID");
                    return;
                }
                string query = "INSERT INTO Proveedores Values (@ProveedorID, @NombreProveedor, @Telefono, @CorreoElectronico)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@ProveedorID", proveedorID);
                cmd.Parameters.AddWithValue("@NombreProveedor", textBox2.Text);
                cmd.Parameters.AddWithValue("@Telefono", textBox3.Text);
                cmd.Parameters.AddWithValue("@CorreoElectronico", textBox4.Text);

                cmd.ExecuteNonQuery();
            }*/
            WindowsFormsApp1.Modelo.Proveedores proveedores = new WindowsFormsApp1.Modelo.Proveedores()
            {
                ProveedorID = int.Parse(textBox1.Text),
                NombreProveedor = textBox2.Text,
                Telefono = textBox3.Text,
                CorreoElectronico = textBox4.Text,
            };

            _context.Proveedores.Add(proveedores);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha insertado el proveedor en la base de datos.");
            }
            Mostrar();
            MessageBox.Show("Operacion de insertar exitosa");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text) ||
               string.IsNullOrWhiteSpace(textBox7.Text) || string.IsNullOrWhiteSpace(textBox8.Text) || string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Recuerde completar todos los campos, es obligatorio.");
                return;
            }
            if (!int.TryParse(textBox6.Text, out int proveedorID))
            {
                MessageBox.Show("El id debe ingresado debe ser numerico.");
                return;
            }
            if (!Correovalido(textBox9.Text))
            {
                MessageBox.Show("El correo electronico a actualizar es invalido.");
                return;
            }
            //Este comentario esta a modo de evidencia de que se utilizo de base el anterior proyecto, osea, no afecta al programa
            /*
            using (SqlConnection connection = new SqlConnection(connectionsql))
            {
                connection.Open();
                SqlCommand checkCmd = new SqlCommand(
                    "Select Count(*) From Proveedores Where ProveedorID=@ProveedorID", connection);
                checkCmd.Parameters.AddWithValue("@ProveedorID", proveedorID);
                int existe = (int)checkCmd.ExecuteScalar();
                if (existe == 0)
                {
                    MessageBox.Show("No existe un proveedor con ese ID");
                    return;
                }

                string query = @"Update Proveedores SET NombreProveedor=@NombreProveedor, Telefono=@Telefono, CorreoElectronico=@CorreoElectronico Where ProveedorID=@ProveedorID";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@ProveedorID", textBox6.Text);
                cmd.Parameters.AddWithValue("@NombreProveedor", textBox7.Text);
                cmd.Parameters.AddWithValue("@Telefono", textBox8.Text);
                cmd.Parameters.AddWithValue("@CorreoElectronico", textBox9.Text);

                int filas = cmd.ExecuteNonQuery();
                if (filas > 0)
                    MessageBox.Show("Operacion de actualizar exitosa");
                else
                    MessageBox.Show("La operacion de actualizar no a sido exitosa");
            }*/
            WindowsFormsApp1.Modelo.Proveedores proveedores = _context.Proveedores.FirstOrDefault(q => q.ProveedorID.Equals(proveedorID));
            if (proveedores == null)
            {
                MessageBox.Show("Proveedor no existe.");
                return;
            }
            proveedores.NombreProveedor = textBox7.Text;
            proveedores.Telefono = textBox8.Text;
            proveedores.CorreoElectronico = textBox9.Text;

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha actualizado el proveedor en la base de datos.");
            }
            Mostrar();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox11.Text, out int proveedorID))
            {
                MessageBox.Show("El id debe ingresado debe ser numerico.");
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
                connection.Open();
                SqlCommand checkCmd = new SqlCommand(
                    "Select Count(*) From Proveedores Where ProveedorID=@ProveedorID", connection);
                checkCmd.Parameters.AddWithValue("@ProveedorID", proveedorID);
                int existe = (int)checkCmd.ExecuteScalar();
                if (existe == 0)
                {
                    MessageBox.Show("No existe un proveedor con ese ID");
                    return;
                }
                string query = "Delete From Proveedores Where ProveedorID=@ProveedorID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ProveedorID", proveedorID);

                int filas = cmd.ExecuteNonQuery();

                if (filas > 0)
                    MessageBox.Show("Operacion de borrar exitosa");
                else
                    MessageBox.Show("La operacion de borrar no a sido exitosa");

            }*/
            WindowsFormsApp1.Modelo.Proveedores proveedores = _context.Proveedores.FirstOrDefault(q => q.ProveedorID.Equals(proveedorID));
            if (proveedores == null)
            {
                MessageBox.Show("Proveedor no existe.");
                return;
            }

            _context.Proveedores.Remove(proveedores);
            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha eliminado el proveedor en la base de datos.");
            }
            Mostrar();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            _context = new Practica1Modelo();
            Mostrar();
        }
    }
}
