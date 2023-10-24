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
    }
}