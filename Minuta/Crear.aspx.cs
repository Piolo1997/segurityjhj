using AjaxControlToolkit;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeguritycJHJ.Minuta
{
    public partial class Crear : System.Web.UI.Page
    {
        String cadenaConexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
        String pa_Num_Minuta;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMinuta();
                if (Request.QueryString["Num_Minuta"] != null)
                {
                    pa_Num_Minuta = Request.QueryString["Num_Minuta"];
                    lblAccion.Text = "Actualizar Minuta";
                    btnActualizar.Visible = true;
                    txtNumMinuta.Enabled = false;
                    cargarDatos();
                }
                else
                {
                    lblAccion.Text = "Registrar Minuta";
                    btnCreate.Visible = true;
                    txtNumMinuta.Text = ObtenerSiguienteNumMinuta(); // Llenar automáticamente el número
                    txtNumMinuta.Enabled = false; // Deshabilitar edición manual
                }
            }
        }
        private void cargarDatos()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    String sql = "SELECT m.Num_Minuta,m.Usuario_Id_Usuario,m.Fecha_Registro,m.Hora,m.Tipo_Minuta,m.Observaciones,m.Soporte FROM minuta m WHERE m.Num_Minuta=@Num_Minuta";
                    MySqlCommand comando = new MySqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("Num_Minuta", pa_Num_Minuta);
                    conexion.Open();
                    MySqlDataReader lector = comando.ExecuteReader();
                    if (lector.HasRows)
                    {
                        lector.Read();
                        txtNumMinuta.Text = lector["Num_Minuta"].ToString();
                        txtIdUsuario.Text = lector["Usuario_Id_Usuario"].ToString();
                        txtFechaRegistro.Text = lector["Fecha_Registro"].ToString();
                        txtHoraRegistro.Text = lector["Hora"].ToString();
                        ddlTipoMinuta.SelectedValue = lector["Tipo_Minuta"].ToString();
                        txtObservaciones.Text = lector["Observaciones"].ToString();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No se encontro la minuta a actualizar.');", true);
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al cargar la minuta: " + HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
                }
            }
        }
        private string ObtenerSiguienteNumMinuta()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string sql = "SELECT COALESCE(MAX(Num_Minuta), 0) + 1 FROM minuta";
                    MySqlCommand comando = new MySqlCommand(sql, conexion);

                    object resultado = comando.ExecuteScalar();
                    return resultado.ToString();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al obtener el siguiente número de minuta: " + HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
                    return "1"; // Valor predeterminado en caso de error
                }
            }
        }
        private void LoadMinuta()
        {
            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    // Consulta para obtener la definición del campo ENUM
                    string query = "SELECT COLUMN_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'minuta' AND COLUMN_NAME = 'Tipo_Minuta'";
                    MySqlCommand comando = new MySqlCommand(query, conexion);
                    conexion.Open();
                    string columnType = comando.ExecuteScalar().ToString();
                    // Extraer los valores del ENUM (remover 'enum(' y ')')
                    string[] tiposMinuta = columnType.Replace("enum(", "").Replace(")", "").Replace("'", "").Split(',');
                    // Asignar los valores al DropDownList
                    ddlTipoMinuta.DataSource = tiposMinuta;
                    ddlTipoMinuta.DataBind();
                    // Insertar un ítem de opción predeterminado
                    ddlTipoMinuta.Items.Insert(0, new ListItem("--Seleccione Tipo de Minuta--", "0"));
                }
                catch (Exception)
                {

                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int Num_Minuta = int.Parse(txtNumMinuta.Text);
                int Usuario_Id_Usuario = int.Parse(txtIdUsuario.Text);
                DateTime Fecha_Registro;
                bool isValidDate = DateTime.TryParse(txtFechaRegistro.Text, out Fecha_Registro);
                TimeSpan Hora_Registro;
                bool isValidTime = TimeSpan.TryParse(txtHoraRegistro.Text, out Hora_Registro);
                string Tipo_Minuta = ddlTipoMinuta.SelectedValue;
                String Observaciones = txtObservaciones.Text;
                byte[] archivoBytes = fuArchivosSoporte.FileBytes;
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                {
                    try
                    {
                        MySqlCommand comando = new MySqlCommand("INSERT INTO minuta (Num_Minuta,Usuario_Id_Usuario,Fecha_Registro,Hora,Tipo_Minuta,Observaciones,Soporte) VALUES (@Num_Minuta,@Usuario_Id_Usuario,@Fecha_Registro,@Hora,@Tipo_Minuta,@Observaciones,@Soporte)", conexion);
                        comando.Parameters.AddWithValue("@Num_Minuta", Num_Minuta);
                        comando.Parameters.AddWithValue("@Usuario_Id_Usuario", Usuario_Id_Usuario);
                        comando.Parameters.AddWithValue("@Fecha_Registro", Fecha_Registro);
                        comando.Parameters.AddWithValue("@Hora", Hora_Registro);
                        comando.Parameters.AddWithValue("@Tipo_Minuta", Tipo_Minuta);
                        comando.Parameters.AddWithValue("@Observaciones", Observaciones);
                        comando.Parameters.AddWithValue("@Soporte", archivoBytes);
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        //Limpiar campos del formulario
                        txtNumMinuta.Text = "";
                        txtIdUsuario.Text = "";
                        txtFechaRegistro.Text = "";
                        txtHoraRegistro.Text = "";
                        ddlTipoMinuta.SelectedValue = "0";
                        txtObservaciones.Text = "";
                        fuArchivosSoporte.Attributes.Clear();

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Registro de minuta creado exitosamente.');", true);




                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al registrar la minuta: " + HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
                    }
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int Num_Minuta = int.Parse(txtNumMinuta.Text);
                int Usuario_Id_Usuario = int.Parse(txtIdUsuario.Text);
                DateTime Fecha_Registro;
                bool isValidDate = DateTime.TryParse(txtFechaRegistro.Text, out Fecha_Registro);
                TimeSpan Hora_Registro;
                bool isValidTime = TimeSpan.TryParse(txtHoraRegistro.Text, out Hora_Registro);
                string Tipo_Minuta = ddlTipoMinuta.SelectedValue;
                String Observaciones = txtObservaciones.Text;

                byte[] archivoBytes = null;
                if (fuArchivosSoporte.HasFile)
                {
                    archivoBytes = fuArchivosSoporte.FileBytes; // Obtener los bytes del archivo
                }
                else
                {
                    // Si no se subió un nuevo archivo, mantenemos el archivo actual
                    // (El archivo será mantenido si el valor en la base de datos no se cambia)
                    using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                    {
                        try
                        {
                            conexion.Open();
                            string sql = "SELECT Soporte FROM minuta WHERE Num_Minuta = @Num_Minuta";
                            MySqlCommand comando = new MySqlCommand(sql, conexion);
                            comando.Parameters.AddWithValue("@Num_Minuta", Num_Minuta);

                            object soporte = comando.ExecuteScalar();
                            if (soporte != DBNull.Value && soporte != null)
                            {
                                archivoBytes = (byte[])soporte; // Conservar el archivo existente
                            }
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al obtener el archivo actual: " + HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
                        }
                    }
                }
                using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
                    try
                    {
                        // Actualizar la minuta, incluyendo el archivo de soporte si se ha subido uno
                        MySqlCommand comando = new MySqlCommand("UPDATE minuta SET Usuario_Id_Usuario = @Usuario_Id_Usuario, Fecha_Registro = @Fecha_Registro, Hora = @Hora, Tipo_Minuta = @Tipo_Minuta, Observaciones = @Observaciones, Soporte = @Soporte WHERE Num_Minuta = @Num_Minuta", conexion);
                        comando.Parameters.AddWithValue("@Num_Minuta", Num_Minuta);
                        comando.Parameters.AddWithValue("@Usuario_Id_Usuario", Usuario_Id_Usuario);
                        comando.Parameters.AddWithValue("@Fecha_Registro", Fecha_Registro);
                        comando.Parameters.AddWithValue("@Hora", Hora_Registro);
                        comando.Parameters.AddWithValue("@Tipo_Minuta", Tipo_Minuta);
                        comando.Parameters.AddWithValue("@Observaciones", Observaciones);

                        // Si no hay archivo, se establece como DBNull
                        comando.Parameters.AddWithValue("@Soporte", archivoBytes ?? (object)DBNull.Value);

                        conexion.Open();
                        int filasAfectadas = comando.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            String script = "alert('Minuta actualizada exitosamente.'); window.location.href='Minuta.aspx';";
                            ClientScript.RegisterStartupScript(this.GetType(), "redirectOk", script, true);
                        }
                        else
                        {
                            String script = "alert('No se encontró la minuta a actualizar.'); window.location.href='Minuta.aspx';";
                            ClientScript.RegisterStartupScript(this.GetType(), "redirectNf", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error al actualizar la minuta: " + HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
                    }
            }
        }
    }
}
