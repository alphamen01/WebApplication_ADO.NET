using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication_ADO.NET.Models;
using Antlr.Runtime.Misc;
using System.Drawing;
using System.Web.Services.Description;
using System.Web.UI.WebControls.WebParts;
using Antlr.Runtime;

namespace WebApplication_ADO.NET.DAL
{
    public class UsuarioDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ADOCONEXION"].ToString();

        public string ConvertirSha256(string texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public (bool, string) RegistrarUser(Usuario usuario)
        {
            bool registrado;
            string mensaje;

            using (SqlConnection connection = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand(@"mantenimiento.ADO_ASP_MVC_REGISTRARUSUARIO", connection);
                cmd.Parameters.AddWithValue("@CORREO", usuario.correo);
                cmd.Parameters.AddWithValue("@CLAVE", usuario.clave);
                cmd.Parameters.Add("@REGISTRADO", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@MENSAJE", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["@REGISTRADO"].Value);
                mensaje = cmd.Parameters["@MENSAJE"].Value.ToString();
            }
            return (registrado, mensaje);
        }

        //private (bool, string) RegistrarUsuarioEnBaseDeDatos(Usuario oUsuario)
        //{
        //    bool registrado;
        //    string mensaje;

        //    using (SqlConnection cn = new SqlConnection(cadena))
        //    {

        //        SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
        //        cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
        //        cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
        //        cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cn.Open();

        //        cmd.ExecuteNonQuery();

        //        registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
        //        mensaje = cmd.Parameters["Mensaje"].Value.ToString();
        //    }
        //    return (registrado, mensaje);
        //}


        //[HttpPost]
        //public ActionResult Registrar(Usuario oUsuario)
        //{
        //    bool registrado;
        //    string mensaje;

        //    if (oUsuario.Clave == oUsuario.ConfirmarClave)
        //    {

        //        oUsuario.Clave = ConvertirSha256(oUsuario.Clave);
        //    }
        //    else
        //    {
        //        ViewData["Mensaje"] = "Las contraseñas no coinciden";
        //        return View();
        //    }

        //    using (SqlConnection cn = new SqlConnection(cadena))
        //    {

        //        SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
        //        cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
        //        cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
        //        cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cn.Open();

        //        cmd.ExecuteNonQuery();

        //        registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
        //        mensaje = cmd.Parameters["Mensaje"].Value.ToString();


        //    }

        //    ViewData["Mensaje"] = mensaje;

        //    if (registrado)
        //    {
        //        return RedirectToAction("Login", "Acceso");
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}

        public int ValidarUser(Usuario usuario)
        {
            int idUsuario = 0;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(@"mantenimiento.ADO_ASP_MVC_VALIDARUSUARIO", connection);
                cmd.Parameters.AddWithValue("@CORREO", usuario.correo);
                cmd.Parameters.AddWithValue("@CLAVE", usuario.clave);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                usuario.id_usuario = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                idUsuario = usuario.id_usuario;

                //object result = cmd.ExecuteScalar();

                //if (result != null)
                //{
                //    idUsuario = Convert.ToInt32(result);
                //}
            }

            return idUsuario;
        }
       



    }
}