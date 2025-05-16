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
    internal class buyerDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select data from database
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_buyer";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion

        #region Insert data into database
        public bool Insert(buyerBLL b)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                String sql = "INSERT INTO tbl_buyer (buyer_name, address, contact_no, brokerage_rate, gst_no) VALUES (@buyer_name, @address, @contact_no, @brokerage_rate, @gst_no)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("buyer_name", b.buyer_name);
                cmd.Parameters.AddWithValue("address", b.address);
                cmd.Parameters.AddWithValue("contact_no", b.contact_no);
                cmd.Parameters.AddWithValue("brokerage_rate", b.brokerage_rate);
                cmd.Parameters.AddWithValue("gst_no", b.gst_no);

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

        #region Update Data in Database
        public bool Update(buyerBLL b)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_buyer SET buyer_name=@buyer_name, address=@address, contact_no=@contact_no, brokerage_rate=@brokerage_rate, gst_no=@gst_no WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@buyer_name", b.buyer_name);
                cmd.Parameters.AddWithValue("@address", b.address);
                cmd.Parameters.AddWithValue("@contact_no", b.contact_no);
                cmd.Parameters.AddWithValue("@brokerage_rate", b.brokerage_rate);
                cmd.Parameters.AddWithValue("@gst_no", b.gst_no);
                cmd.Parameters.AddWithValue("@id", b.id);

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
        public bool Delete(buyerBLL b)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_buyer WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", b.id);
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
                string sql = "SELECT * FROM tbl_buyer WHERE id LIKE '%" + keywords + "%' OR buyer_name LIKE '%" + keywords + "%' OR address LIKE '%" + keywords + "%' OR contact_no LIKE '%" + keywords + "%'";
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
    }
}
