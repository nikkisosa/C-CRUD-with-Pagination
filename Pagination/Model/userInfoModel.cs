using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Pagination.Model
{
    class userInfoModel
    {
        private static string str = string.Empty;

        public static string getUserInfo(int page)
        {
            int pages = (Entity.variables.sizePerPage * (page - 1));
            string query = "SELECT * FROM info ORDER BY id DESC OFFSET "+ pages +" ROW FETCH NEXT " + Entity.variables.sizePerPage +" ROWS ONLY;";
            SqlConnection conn = config.sqlconnection;
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            try
            {
                config.records = new List<Entity.variables>();
                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Entity.variables ent = new Entity.variables();
                    ent.id = Convert.ToInt32(reader["id"].ToString());
                    ent.name = reader["name"].ToString();
                    ent.gender = reader["sex"].ToString();
                    config.records.Add(ent);
                }
                str = "success";
            }
            catch (SqlException err)
            {
                str = err.Message;
            }
            finally
            {
                conn.Close();
            }

            return str;
        }

        public static string getTotalCount()
        {
            string query = "SELECT COUNT(id) FROM info";
            SqlConnection conn = config.sqlconnection;
            SqlCommand cmd = new SqlCommand();
            try
            {
                config.records = new List<Entity.variables>();
                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;
                Entity.variables ent = new Entity.variables();
                ent.totalCount = (Int32)cmd.ExecuteScalar();
                config.records.Add(ent);
                str = "success";
            }
            catch (SqlException err)
            {
                str = err.Message;
            }
            finally
            {
                conn.Close();
            }
            return str;
        }

        public static string addUserInfo(Entity.variables ent)
        {
            string query = "INSERT INTO info(name,sex) VALUES (@name,@sex);";
            SqlConnection conn = config.sqlconnection;
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@name", ent.name);
                cmd.Parameters.AddWithValue("@sex", ent.gender);
                cmd.ExecuteNonQuery();
                str = "success";
            }
            catch(SqlException err)
            {
                str = err.Message;
            }
            finally
            {
                conn.Close();
            }

            return str;
        }

        public static string EditUserInfo(Entity.variables ent)
        {
            string query = "UPDATE info SET name = @name, sex = @sex WHERE id = @id;";
            SqlConnection conn = config.sqlconnection;
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@name", ent.name);
                cmd.Parameters.AddWithValue("@sex", ent.gender);
                cmd.Parameters.AddWithValue("@id", ent.id);
                cmd.ExecuteNonQuery();
                str = "success";
            }
            catch (SqlException err)
            {
                str = err.Message;
            }
            finally
            {
                conn.Close();
            }

            return str;
        }

        public static string DeleteUserInfo(int id)
        {
            string query = "DELETE FROM info WHERE id = @id;";
            SqlConnection conn = config.sqlconnection;
            SqlCommand cmd = new SqlCommand();
            try
            {
                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                str = "success";
            }
            catch (SqlException err)
            {
                str = err.Message;
            }
            finally
            {
                conn.Close();
            }

            return str;
        }
    }
}
