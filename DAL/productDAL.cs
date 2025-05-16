using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwiftBillingSystem.BLL;
using System.Windows.Forms;

namespace SwiftBillingSystem.DAL
{
    internal class productDAL
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select data from database
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                String sql = "SELECT * FROM tbl_product";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
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
        public bool Insert(productBLL p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                String sql = "INSERT INTO tbl_product (product_name, brand, packing) VALUES (@product_name, @brand, @packing)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("product_name", p.product_name);
                cmd.Parameters.AddWithValue("brand", p.brand);
                cmd.Parameters.AddWithValue("packing", p.packing);

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
        public bool Update(productBLL p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE tbl_product SET product_name=@product_name, brand=@brand, packing=@packing WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@product_name", p.product_name);
                cmd.Parameters.AddWithValue("@brand", p.brand);
                cmd.Parameters.AddWithValue("@packing", p.packing);
                cmd.Parameters.AddWithValue("@id", p.id);

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
        public bool Delete(productBLL p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "DELETE FROM tbl_product WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", p.id);
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
                string sql = "SELECT * FROM tbl_product WHERE id LIKE '%" + keywords + "%' OR product_name LIKE '%" + keywords + "%' OR brand LIKE '%" + keywords + "%' OR packing LIKE '%" + keywords + "%'";
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
