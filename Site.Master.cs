using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeguritycJHJ
{
    public partial class Site : System.Web.UI.MasterPage
    {
        string cadenaConexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null) // Verifica si el usuario está logueado
            {
                string usuarioCorreo = Session["usuario"].ToString();
                MostrarSaludo(usuarioCorreo);  // Llamamos al método para cargar el saludo
            }
            else
            {
                Response.Redirect("Login.aspx"); // Redirige al login si no está logueado
            }
        }

        private void MostrarSaludo(string usuarioCorreo)
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

                        // Establecer el saludo en el Label del MasterPage
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
    

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx", true);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}