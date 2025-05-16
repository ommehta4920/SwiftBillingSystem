using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwiftBillingSystem.BLL;

namespace SwiftBillingSystem.DAL
{
    internal class userDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Data from Database

        public DataTable Select()
        {
            // Static method to connect to the database
            SqlConnection conn = new SqlConnection(myconnstrng);
            // To hold the data from database
            DataTable dt = new DataTable();
            try
            {
                // SQL query to get data from database
                String sql = "SELECT * FROM tbl_users";
                // For executing command
                SqlCommand cmd = new SqlCommand(sql, conn);
                // To get data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Database Connection open
                conn.Open();
                // Fill data in our datatable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Throw message if any error occurs
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Closing Connection
                conn.Close();
            }
            // Return the value in datatable
            return dt;
        }
        #endregion

        #region Insert Data into Database
        public bool Insert(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                String sql = "INSERT INTO tbl_users (first_name, last_name, email, username, password, contact, address, user_type, added_by) VALUES (@first_name, @last_name, @email, @username, @password, @contact, @address, @user_type, @added_by)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("first_name", u.first_name);
                cmd.Parameters.AddWithValue("last_name", u.last_name);
                cmd.Parameters.AddWithValue("email", u.email);
                cmd.Parameters.AddWithValue("username", u.username);
                cmd.Parameters.AddWithValue("password", u.password);
                cmd.Parameters.AddWithValue("contact", u.contact);
                cmd.Parameters.AddWithValue("address", u.address);
                cmd.Parameters.AddWithValue("user_type", u.user_type);
                cmd.Parameters.AddWithValue("added_by", u.added_by);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query executed successfully then the value to rows will be greater then 0 else it will be less than 0.
                if (rows > 0)
                {
                    // Query Successfull
                    isSuccess = true;
                }
                else
                {
                    // Query Failed
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        
        #region Update Data in Database
        public bool Update(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_users SET first_name=@first_name, last_name=@last_name, email=@email, username=@username, password=@password, contact=@contact, address=@address, user_type=@user_type, added_by=@added_by WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@first_name", u.first_name);
                cmd.Parameters.AddWithValue("@last_name", u.last_name);
                cmd.Parameters.AddWithValue("@email", u.email);
                cmd.Parameters.AddWithValue("@username", u.username);
                cmd.Parameters.AddWithValue("@password", u.password);
                cmd.Parameters.AddWithValue("@contact", u.contact);
                cmd.Parameters.AddWithValue("@address", u.address);
                cmd.Parameters.AddWithValue("@user_type", u.user_type);
                cmd.Parameters.AddWithValue("@added_by", u.added_by);
                cmd.Parameters.AddWithValue("@id", u.id);

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    // Query Successfull
                    isSuccess = true;
                }
                else
                {
                    // Query failed
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        
        #region Delete Data from Database
        public bool Delete(userBLL u)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_users WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", u.id);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    // Query Successfull
                    isSuccess = true;
                }
                else
                {
                    // Query failed
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        #endregion
        
        #region Search User on Database usingKeywords
        public DataTable Search(string keywords)
        {
            // Static method to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            // To hold data from database
            DataTable dt = new DataTable();
            try
            {
                // SQL query to get data from database
                string sql = "SELECT * FROM tbl_users WHERE id LIKE '%" + keywords + "%' OR first_name LIKE '%" + keywords + "%' OR last_name LIKE '%" + keywords + "%' OR username LIKE '%" + keywords + "%' OR address LIKE '%" + keywords + "%'";
                // For executing query
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Getting data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Database connection open
                conn.Open();
                // Fill data into datatable
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Throw message if any error occures
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Closing connection
                conn.Close();
            }
            // Return the value in datatable
            return dt;
        }
        #endregion

        #region Getting User ID for UserName
        public userBLL GetIDFromUsername (String username)
        {
            userBLL u = new userBLL();
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT id FROM tbl_users WHERE username = '" + username + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                conn.Open();
                adapter.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    u.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return u;
        }
        #endregion
    }
}
