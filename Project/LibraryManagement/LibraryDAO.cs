using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace LibraryManagement
{
    public class LibraryDAO
    {
        string strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";

        public string getCateNameByID(string id)
        {
            string cateName = null;
            SqlConnection con = null;
            SqlCommand cm = null;
            SqlDataReader rd = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select CategoryName From Category Where CategoryID = @ID";
                    cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", id);
                    rd = cm.ExecuteReader();
                    if (rd.Read())
                    {
                        cateName = rd.GetValue(0).ToString();                   
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();

            }
            return cateName;
        }

        public string getAuthorNameByID(string id)
        {
            string authorName = null;
            SqlConnection con = null;
            SqlCommand cm = null;
            SqlDataReader rd = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select AuthorName From Author Where AuthorID = @ID";
                    cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", id);
                    rd = cm.ExecuteReader();
                    if (rd.Read())
                    {
                        authorName = rd.GetValue(0).ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();

            }
            return authorName;
        }

        public string getPublisherNameByID(string id)
        {
            string publisherName = null;
            SqlConnection con = null;
            SqlCommand cm = null;
            SqlDataReader rd = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select PublisherName From Publisher Where PublisherID = @ID";
                    cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", id);
                    rd = cm.ExecuteReader();
                    if (rd.Read())
                    {
                        publisherName = rd.GetValue(0).ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();

            }
            return publisherName;
        }
        public List<Book> getAllBooks()
        {
            List<Book> list = new List<Book>(); ;
            SqlConnection con = null;
            SqlCommand cm = null;
            SqlDataReader rd = null;
            try
            {
                con = new SqlConnection(strConnection);
                if(con != null)
                {
                    con.Open();
                    string sql = "Select * From Book";
                    cm = new SqlCommand(sql, con);
                    rd = cm.ExecuteReader();
                    while (rd.Read())
                    {
                        string bookID = rd.GetValue(0).ToString();
                        string bookName = rd.GetValue(1).ToString();
                        string authorID = rd.GetValue(2).ToString();
                        string categoryID = rd.GetValue(3).ToString();
                        string publisherID = rd.GetValue(4).ToString();
                        int quantity = int.Parse(rd.GetValue(5).ToString());
                        DateTime date = DateTime.Parse(rd.GetValue(6).ToString());
                    
                        list.Add(new Book(bookID, bookName, authorID, categoryID, publisherID, quantity, date));
                    }
                }
            }catch(SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                con.Close();
            }
            return list;
        }
        public List<Book> getAllBooksBycateID(string id)
        {
            List<Book> list = new List<Book>(); ;
            SqlConnection con = null;
            SqlCommand cm = null;
            SqlDataReader rd = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select * From Book Where CategoryID = @ID";
                    cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", id);
                    rd = cm.ExecuteReader();
                    while (rd.Read())
                    {
                        string bookID = rd.GetValue(0).ToString();
                        string bookName = rd.GetValue(1).ToString();
                        string authorID = rd.GetValue(2).ToString();
                        string categoryID = rd.GetValue(3).ToString();
                        string publisherID = rd.GetValue(4).ToString();
                        int quantity = int.Parse(rd.GetValue(5).ToString());
                        DateTime date = DateTime.Parse(rd.GetValue(6).ToString());
                        
                        list.Add(new Book(bookID, bookName, authorID, categoryID, publisherID, quantity, date));
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public List<Book> getBooksByName(string search)
        {
            List<Book> list = new List<Book>(); ;
            SqlConnection con = null;
            SqlCommand cm = null;
            SqlDataReader rd = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select * From Book Where BookName Like @Name";
                    cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@Name", "%" + search + "%");
                    rd = cm.ExecuteReader();
                    while (rd.Read())
                    {
                        string bookID = rd.GetValue(0).ToString();
                        string bookName = rd.GetValue(1).ToString();
                        string authorID = rd.GetValue(2).ToString();
                        string categoryID = rd.GetValue(3).ToString();
                        string publisherID = rd.GetValue(4).ToString();
                        int quantity = int.Parse(rd.GetValue(5).ToString());
                        DateTime date = DateTime.Parse(rd.GetValue(6).ToString());

                        list.Add(new Book(bookID, bookName, authorID, categoryID, publisherID, quantity, date));
                    }
                }
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return list;
        }
    }
}
