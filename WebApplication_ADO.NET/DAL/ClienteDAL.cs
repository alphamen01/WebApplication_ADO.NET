using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication_ADO.NET.Models;

namespace WebApplication_ADO.NET.DAL
{
    public class ClienteDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ADOCONEXION"].ToString();

        ///GET ALL CLIENTES
        
        public List<Cliente> GetAllClientes()
        {
            List<Cliente> clienteList = new List<Cliente>();

                using (SqlConnection connection = new SqlConnection(conString))
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = @"mantenimiento.ADO_ASP_MVC_GETALLCLIENTES";

                    SqlDataAdapter sqlDA = new SqlDataAdapter(command);

                    DataTable dtClientes = new DataTable();

                    connection.Open();
                    sqlDA.Fill(dtClientes);
                    connection.Close();

                    foreach (DataRow dr in dtClientes.Rows)
                    {
                        clienteList.Add(new Cliente
                        {
                            id_cliente = Convert.ToInt32(dr["id_cliente"]),
                            nombre = dr["nombre"].ToString(),
                            direccion = dr["direccion"].ToString(),
                            telefono = dr["telefono"].ToString()
                        });
                    }
                }

                return clienteList;
        }
    }
}