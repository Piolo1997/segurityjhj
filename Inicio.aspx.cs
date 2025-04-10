using MySql.Data.MySqlClient;
using System.Configuration;
using System;

namespace SeguritycJHJ
{
    public partial class Inicio : System.Web.UI.Page
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)  // Verifica si el usuario está logueado
                {
                    string usuarioCorreo = Session["usuario"].ToString();
                    CargarSaludo(usuarioCorreo); // Llamar al método para cargar el saludo
                }
                else
                {
                    Response.Redirect("Login.aspx"); // Redirige al login si no hay sesión
                }
            }
        }

        private void CargarSaludo(string usuarioCorreo)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string sql = "SELECT Primer_Nombre, Primer_Apellido FROM usuario WHERE Correo_Electronico = @Correo_Electronico";
                    MySqlCommand comando = new MySqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("@Correo_Electronico", usuarioCorreo);

                    MySqlDataReader lector = comando.ExecuteReader();
                    if (lector.HasRows)
                    {
                        lector.Read();
                        string primerNombre = lector["Primer_Nombre"].ToString();
                        string primerApellido = lector["Primer_Apellido"].ToString();

                        // Actualizar el saludo en el Label (o cualquier otro control que estés usando)
                        lblSaludo.Text = $"{primerNombre} {primerApellido}";
                    }
                    else
                    {
                        lblSaludo.Text = "Usuario desconocido";
                    }
                }
                catch (Exception)
                {
                    lblSaludo.Text = "Error al cargar los datos del usuario.";
                }
            }
        }
    }
}