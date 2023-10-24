using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication_ADO.NET.Models;

namespace WebApplication_ADO.NET.DAL
{
    public class AreaDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["ADOCONEXION"].ToString();

        ///GET ALL AREAS

        public List<Area> GetAllAreas()
        {
            List<Area> areaList = new List<Area>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"mantenimiento.ADO_ASP_MVC_GETALLAREAS";

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);

                DataTable dtAreas = new DataTable();

                connection.Open();
                sqlDA.Fill(dtAreas);
                connection.Close();

                foreach (DataRow dr in dtAreas.Rows)
                {
                    areaList.Add(new Area
                    {
                        //id_area = Convert.ToInt32(dr["id_area"]),
                        //descripcion = dr["descripcion"].ToString(),
                        //id_cliente = Convert.ToInt32(dr["id_cliente"]),
                        //enu_estado_registro = Convert.ToChar(dr["enu_estado_registro"]),
                        //usuario_creacion = dr["usuario_creacion"].ToString(),
                        //fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"]),
                        //usuario_modificacion = dr["usuario_modificacion"].ToString(),
                        //fecha_modificacion = Convert.ToDateTime(dr["fecha_modificacion"]),
                        //nombre_cliente = dr["nombre_cliente"].ToString()

                        id_area = dr["id_area"] != DBNull.Value ? Convert.ToInt32(dr["id_area"]) : 0,
                        descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : string.Empty,
                        id_cliente = dr["id_cliente"] != DBNull.Value ? Convert.ToInt32(dr["id_cliente"]) : 0,
                        enu_estado_registro = dr["enu_estado_registro"] != DBNull.Value ? Convert.ToChar(dr["enu_estado_registro"]) : ' ',
                        usuario_creacion = dr["usuario_creacion"] != DBNull.Value ? dr["usuario_creacion"].ToString() : string.Empty,
                        fecha_creacion = dr["fecha_creacion"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_creacion"]) : DateTime.MinValue,
                        usuario_modificacion = dr["usuario_modificacion"] != DBNull.Value ? dr["usuario_modificacion"].ToString() : string.Empty,
                        fecha_modificacion = dr["fecha_modificacion"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_modificacion"]) : DateTime.MinValue,
                        nombre_cliente = dr["nombre_cliente"] != DBNull.Value ? dr["nombre_cliente"].ToString() : string.Empty

                    });
                }
            }

            return areaList;
        }

        /// INSERT AREA

        public bool InsertArea(Area area)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(@"mantenimiento.ADO_ASP_MVC_INSERTAREA", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Crear una tabla de datos con los datos de la area
                DataTable areaTable = new DataTable();
                areaTable.Columns.Add("id_area", typeof(int));
                areaTable.Columns.Add("descripcion", typeof(string));
                areaTable.Columns.Add("id_cliente", typeof(int));
                areaTable.Columns.Add("enu_estado_registro", typeof(char));
                areaTable.Columns.Add("usuario_creacion", typeof(string));
                areaTable.Columns.Add("fecha_creacion", typeof(DateTime));
                areaTable.Columns.Add("usuario_modificacion", typeof(string));
                areaTable.Columns.Add("fecha_modificacion", typeof(DateTime));

                // Agregar los datos de la area a la tabla
                DataRow nuevaArea = areaTable.NewRow();
                nuevaArea["descripcion"] = area.descripcion;
                nuevaArea["id_cliente"] = area.id_cliente;
                nuevaArea["enu_estado_registro"] = area.enu_estado_registro;
                nuevaArea["usuario_creacion"] = "LUIS CREA";
                nuevaArea["fecha_creacion"] = DateTime.Now;
                areaTable.Rows.Add(nuevaArea);


                // Agregar la tabla de datos como un parámetro al comando
                SqlParameter areaParam = command.Parameters.AddWithValue("@AREA_TYPE", areaTable);
                areaParam.SqlDbType = SqlDbType.Structured;

                connection.Open();
                // Ejecutar el procedimiento almacenado
                id = command.ExecuteNonQuery();
                connection.Close();

            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// GET AREA

        public List<Area> GetArea(int id)
        {
            List<Area> areaList = new List<Area>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = @"mantenimiento.ADO_ASP_MVC_GETAREA";
                command.Parameters.AddWithValue("@ID_AREA", id);

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);

                DataTable dtArea = new DataTable();

                connection.Open();
                sqlDA.Fill(dtArea);
                connection.Close();

                foreach (DataRow dr in dtArea.Rows)
                {
                    areaList.Add(new Area
                    {
                        id_area = Convert.ToInt32(dr["id_area"]),
                        descripcion = dr["descripcion"].ToString(),
                        id_cliente = Convert.ToInt32(dr["id_cliente"]),
                        enu_estado_registro = Convert.ToChar(dr["enu_estado_registro"]),
                        usuario_creacion = dr["usuario_creacion"].ToString(),
                        fecha_creacion = Convert.ToDateTime(dr["fecha_creacion"]),
                        usuario_modificacion = dr["usuario_modificacion"] != DBNull.Value ? dr["usuario_modificacion"].ToString() : string.Empty,
                        fecha_modificacion = dr["fecha_modificacion"] != DBNull.Value ? Convert.ToDateTime(dr["fecha_modificacion"]) : DateTime.MinValue,
                        nombre_cliente = dr["nombre_cliente"].ToString()
                    });
                }
            }

            return areaList;
        }

        /// UPDATE AREA

        public bool UpdateArea(Area area)
        {
            int i = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(@"mantenimiento.ADO_ASP_MVC_UPDATEAREA", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Crear una tabla de datos con los datos del area
                DataTable areaTable = new DataTable();
                areaTable.Columns.Add("id_area", typeof(int));
                areaTable.Columns.Add("descripcion", typeof(string));
                areaTable.Columns.Add("id_cliente", typeof(int));
                areaTable.Columns.Add("enu_estado_registro", typeof(char));
                areaTable.Columns.Add("usuario_creacion", typeof(string));
                areaTable.Columns.Add("fecha_creacion", typeof(DateTime));
                areaTable.Columns.Add("usuario_modificacion", typeof(string));
                areaTable.Columns.Add("fecha_modificacion", typeof(DateTime));

                // Agregar los datos del area a la tabla
                DataRow nuevaArea = areaTable.NewRow();
                nuevaArea["id_area"] = area.id_area;
                nuevaArea["descripcion"] = area.descripcion;
                nuevaArea["id_cliente"] = area.id_cliente;
                nuevaArea["enu_estado_registro"] = area.enu_estado_registro;
                nuevaArea["usuario_creacion"] = area.usuario_creacion;
                nuevaArea["fecha_creacion"] = area.fecha_creacion;
                nuevaArea["usuario_modificacion"] = "LUIS MODIFICA";
                nuevaArea["fecha_modificacion"] = DateTime.Now;
                areaTable.Rows.Add(nuevaArea);


                // Agregar la tabla de datos como un parámetro al comando
                SqlParameter areaParam = command.Parameters.AddWithValue("@AREA_TYPE", areaTable);
                areaParam.SqlDbType = SqlDbType.Structured;

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

        /// DELETE AREA

        public string DeleteArea(int id)
        {
            string result = "";

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(@"mantenimiento.ADO_ASP_MVC_DELETEAREA", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID_AREA", id);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }

            return result;
        }
    }
}