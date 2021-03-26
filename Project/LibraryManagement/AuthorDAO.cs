using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class AuthorDAO
    {
        String strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";
        

        public List<Author> getAllAuthor()
        {
            string sql = "Select AuthorID, AuthorName, Descriptions From Author";
            List<Author> list = new List<Author>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                Author a = null;
                while (rd.Read())
                {
                    string authorID = rd.GetValue(0).ToString();
                    string authorName = rd.GetValue(1).ToString();
                    string descriptions = rd.GetValue(2).ToString();
                    a = new Author(authorID, authorName, descriptions);
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

        public List<string> getAuthorName()
        {
            string sql = "Select AuthorName From Author";
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
                    string authorName = rd.GetValue(0).ToString();
                    a = authorName;
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

        public List<string> getAuthorID()
        {
            string sql = "Select AuthorID From Author";
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
                    string authorID = rd.GetValue(0).ToString();
                    a = authorID;
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

        public string searchAuthorName(string authorID)
        {
            string sql = "Select AuthorName From Author Where AuthorID = @ID";
            string authorName = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@ID", authorID);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                if (rd.Read())
                {
                    authorName = rd.GetValue(0).ToString();
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
            return authorName;
        }

        public bool delete(String authorID)
        {
            bool check = false;
            string sql = "Delete From Author Where AuthorID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand cm = new SqlCommand(sql, con);

                cm.Parameters.AddWithValue("@ID", authorID);
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

        public void updateAuthor(string authorID, string authorName, string descriptions)
        {
            string sql = "UPDATE Author SET AuthorName = @name, Descriptions = @des Where AuthorID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", authorName);
                command.Parameters.AddWithValue("@des", descriptions);
                command.Parameters.AddWithValue("@ID", authorID);
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

        public void addAuthor(string authorID, string authorName, string descriptions)
        {
            string sql = "INSERT INTO Author (AuthorID, AuthorName, Descriptions) VALUES (@id, @name, @des)";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", authorName);
                command.Parameters.AddWithValue("@des", descriptions);
                command.Parameters.AddWithValue("@id", authorID);
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
