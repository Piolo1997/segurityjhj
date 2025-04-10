using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeguritycJHJ.minuta
{
    public partial class Minuta : System.Web.UI.Page
    {
        String cadenaConexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarGrindVIew();
        }

        private void CargarGrindVIew()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    String sql = "SELECT m.Num_Minuta, m.Usuario_Id_Usuario, u.Primer_Apellido, u.Primer_Nombre, m.Fecha_Registro, m.Hora, m.Tipo_Minuta, m.Observaciones, m.Soporte " +
                                 "FROM minuta m " +
                                 "JOIN usuario u ON m.Usuario_Id_Usuario = u.Id_Usuario";
                    conexion.Open();
                    MySqlCommand comando = new MySqlCommand(sql, conexion);
                    MySqlDataAdapter da = new MySqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvDatosMinuta.DataSource = dt;
                    gvDatosMinuta.DataBind();
                }
                catch (Exception)
                {
                    // Manejar errores si es necesario
                }
            }
        }

        protected void gvDatosMinuta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String Num_Minuta = e.CommandArgument.ToString();
            if (e.CommandName == "Actualizar")
            {
                Response.Redirect($"Crear.aspx?Num_Minuta={Num_Minuta}", false);
            }
            else if (e.CommandName == "Eliminar")
            {
                eliminarMinuta(Num_Minuta);
            }
        }
        private void eliminarMinuta(String Num_Minuta)
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    MySqlCommand comando = new MySqlCommand("DELETE FROM minuta WHERE Num_Minuta = @Num_Minuta", conexion);
                    comando.Parameters.AddWithValue("@Num_Minuta", Num_Minuta);
                    conexion.Open();
                    int filasBorradas = comando.ExecuteNonQuery();
                    if (filasBorradas > 0)
                    {
                        String script = "alert('Minuta eliminada exitosamente.'); window.location.href='Minuta.aspx';";
                        ClientScript.RegisterStartupScript(this.GetType(), "redirectOk", script, true);
                    }
                    else
                    {
                        String script = "alert('No se encontró la minuta a eliminar.'); window.location.href='Minuta.aspx';";
                        ClientScript.RegisterStartupScript(this.GetType(), "redirectNf", script, true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al registrar la minuta: " + HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
                }
            }
        }
    }
}