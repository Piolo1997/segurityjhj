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
    public partial class Login : System.Web.UI.Page
    {
        String cadenaConexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            String Username = txtUsuario.Text;
            String Password = txtPassword.Text;
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    String sql = "SELECT password FROM usuario where Correo_Electronico=@Correo_Electronico and Password=@Password";
                    MySqlCommand comando = new MySqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("Correo_Electronico", Username);
                    comando.Parameters.AddWithValue("Password", Password);
                    MySqlDataReader lector = comando.ExecuteReader();
                    if (lector.HasRows)
                    {
                        Session["usuario"] = Username;
                        Response.Redirect("Inicio.aspx", false);
                    }
                    else
                    {
                        lblEstado.Text = "Usuario y/o contraseña invalidos";
                    }
                }
                catch (Exception ex)
                {

                    lblEstado.Text = "Error de Conexión " + ex.Message;
                }
            }
        }
    }
}