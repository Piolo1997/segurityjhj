using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web;

namespace SeguritycJHJ.Minuta
{
    public class SoporteHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string cadenaConexion = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;

            // Obtiene el parámetro 'Num_Minuta'
            string numMinuta = context.Request.QueryString["Num_Minuta"];
            string descargar = context.Request.QueryString["descargar"];

            if (string.IsNullOrEmpty(numMinuta))
            {
                context.Response.StatusCode = 400; // Bad Request
                context.Response.Write("Número de minuta no proporcionado.");
                return;
            }

            using (MySqlConnection conexion = new MySqlConnection(cadenaConexion))
            {
                try
                {
                    conexion.Open();
                    string sql = "SELECT Soporte FROM minuta WHERE Num_Minuta = @Num_Minuta";
                    MySqlCommand comando = new MySqlCommand(sql, conexion);
                    comando.Parameters.AddWithValue("@Num_Minuta", numMinuta);

                    object soporte = comando.ExecuteScalar();
                    if (soporte != null && soporte != DBNull.Value)
                    {
                        byte[] archivoBytes = (byte[])soporte;

                        if (!string.IsNullOrEmpty(descargar) && descargar.ToLower() == "true")
                        {
                            // Configurar respuesta para descarga
                            context.Response.ContentType = "application/octet-stream";
                            context.Response.AddHeader("Content-Disposition", $"attachment; filename=Soporte_{numMinuta}.jpg"); // Cambia la extensión según el formato
                            context.Response.BinaryWrite(archivoBytes);
                        }
                        else
                        {
                            // Configurar respuesta para mostrar la imagen
                            context.Response.ContentType = "image/jpeg"; // Cambiar si usas otro formato como PNG
                            context.Response.BinaryWrite(archivoBytes);
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 404; // Not Found
                        context.Response.Write("Archivo de soporte no encontrado.");
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500; // Internal Server Error
                    context.Response.Write("Error al recuperar el archivo: " + ex.Message);
                }
            }
        }

        public bool IsReusable
        {
            get { return false; } // Cambia a 'true' si este handler se puede reutilizar para varias solicitudes.
        }
    }
}
