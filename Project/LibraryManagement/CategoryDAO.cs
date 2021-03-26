using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class CategoryDAO
    {
        string strConnection;
        public CategoryDAO()
        {
            strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";
        }

        public List<Category> getAllCategory()
        {
            string sql = "Select CategoryID, CategoryName From Category";
            List<Category> list = new List<Category>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                Category a = null;
                while (rd.Read())
                {
                    String categoryID = rd.GetValue(0).ToString();
                    String categoryName = rd.GetValue(1).ToString();
                    a = new Category(categoryID, categoryName);
                    list.Add(a);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public List<string> getCategoryName()
        {
            string sql = "Select CategoryName From Category";
            List<string> list = new List<string>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                string a = null;
                while (rd.Read())
                {
                    string categoryName = rd.GetValue(0).ToString();
                    a = categoryName;
                    list.Add(a);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public List<string> getCategoryID()
        {
            string sql = "Select CategoryID From Category";
            List<string> list = new List<string>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                string a = null;
                while (rd.Read())
                {
                    string categoryID = rd.GetValue(0).ToString();
                    a = categoryID;
                    list.Add(a);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public string searchCategoryName(string categoryID)
        {
            string sql = "Select CategoryName From Category Where CategoryID = @ID";
            string categoryName = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@ID", categoryID);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                if (rd.Read())
                {
                    categoryName = rd.GetValue(0).ToString();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return categoryName;
        }

        public bool delete(String categoryID)
        {
            bool check = false;
            string sql = "Delete From Category Where CategoryID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand cm = new SqlCommand(sql, con);

                cm.Parameters.AddWithValue("@ID", categoryID);
                con.Open();
                check = cm.ExecuteNonQuery() > 0 ? true : check;
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return check;
        }

        public void updateCategory(string categoryID, string categoryName)
        {
            string sql = "UPDATE Category SET CategoryName = @name Where CategoryID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", categoryName);
                command.Parameters.AddWithValue("@ID", categoryID);
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void addCategory(string categoryID, string categoryName)
        {
            string sql = "INSERT INTO Category (CategoryID, CategoryName) VALUES (@ID, @name)";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", categoryName);
                command.Parameters.AddWithValue("@ID", categoryID);
                con.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
