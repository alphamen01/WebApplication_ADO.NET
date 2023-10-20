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


        /// INSERT CLIENTE
        
        public bool InsertCliente(Cliente cliente)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(@"mantenimiento.ADO_ASP_MVC_INSERTCLIENTE", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Crear una tabla de datos con los datos del cliente
                DataTable clienteTable = new DataTable();
                clienteTable.Columns.Add("id_cliente", typeof(int));
                clienteTable.Columns.Add("nombre", typeof(string));
                clienteTable.Columns.Add("direccion", typeof(string));
                clienteTable.Columns.Add("telefono", typeof(string));

                // Agregar los datos del cliente a la tabla
                DataRow nuevoCliente = clienteTable.NewRow();
                nuevoCliente["nombre"] = cliente.nombre;
                nuevoCliente["direccion"] = cliente.direccion;
                nuevoCliente["telefono"] = cliente.telefono;
                clienteTable.Rows.Add(nuevoCliente);


                // Agregar la tabla de datos como un parámetro al comando
                SqlParameter clienteParam = command.Parameters.AddWithValue("@CLIENTE_TYPE", clienteTable);
                clienteParam.SqlDbType = SqlDbType.Structured;

                connection.Open();               
                // Ejecutar el procedimiento almacenado
                id = command.ExecuteNonQuery();
                connection.Close();
                
            }
            if (id > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// GET CLIENTE

        public List<Cliente> GetCliente(int id)
        {
            List<Cliente> clienteList = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"mantenimiento.ADO_ASP_MVC_GETCLIENTE";
                command.Parameters.AddWithValue ("@ID_CLIENTE", id);

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);

                DataTable dtCliente = new DataTable();

                connection.Open();
                sqlDA.Fill(dtCliente);
                connection.Close();

                foreach (DataRow dr in dtCliente.Rows)
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


        /// UPDATE CLIENTE

        public bool UpdateCliente(Cliente cliente)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(@"mantenimiento.ADO_ASP_MVC_UPDATECLIENTE", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Crear una tabla de datos con los datos del cliente
                DataTable clienteTable = new DataTable();
                clienteTable.Columns.Add("id_cliente", typeof(int));
                clienteTable.Columns.Add("nombre", typeof(string));
                clienteTable.Columns.Add("direccion", typeof(string));
                clienteTable.Columns.Add("telefono", typeof(string));

                // Agregar los datos del cliente a la tabla
                DataRow nuevoCliente = clienteTable.NewRow();
                nuevoCliente["id_cliente"] = cliente.id_cliente;
                nuevoCliente["nombre"] = cliente.nombre;
                nuevoCliente["direccion"] = cliente.direccion;
                nuevoCliente["telefono"] = cliente.telefono;
                clienteTable.Rows.Add(nuevoCliente);


                // Agregar la tabla de datos como un parámetro al comando
                SqlParameter clienteParam = command.Parameters.AddWithValue("@CLIENTE_TYPE", clienteTable);
                clienteParam.SqlDbType = SqlDbType.Structured;

                connection.Open();
                // Ejecutar el procedimiento almacenado
                i = command.ExecuteNonQuery();
                connection.Close();

            }
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}