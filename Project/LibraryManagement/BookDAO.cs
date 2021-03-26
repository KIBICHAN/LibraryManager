using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class BookDAO
    {
        string strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";
 
        public List<Book> getAllBook()
        {
            string sql = "Select BookID, BookName, AuthorID, CategoryID, PublisherID, Quantity, PublishDate From Book";
            List<Book> list = new List<Book>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                Book b = null;
                while (rd.Read())
                {
                    string bookID = rd.GetValue(0).ToString();
                    string bookName = rd.GetValue(1).ToString();
                    DateTime publishDate = DateTime.Parse(rd.GetValue(6).ToString());
                    string authorID = rd.GetValue(2).ToString();
                    string categoryID = rd.GetValue(3).ToString();
                    string publisherID = rd.GetValue(4).ToString();
                    int quantity = Int32.Parse(rd.GetValue(5).ToString());
                    b = new Book(bookID, bookName, authorID, categoryID, publisherID, quantity, publishDate);
                    list.Add(b);
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

        public bool delete(String bookID)
        {
            bool check = false;
            string sql = "Delete From Book Where BookID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand cm = new SqlCommand(sql, con);

                cm.Parameters.AddWithValue("@ID", bookID);
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

        public void updateBook(string bookID, string bookName, string authorID, string categoryID, string publisherID, string quantity, string publishDate)
        {
            string sql = "UPDATE Book SET BookName = @name, AuthorID = @authorID, CategoryID = @cateID, " +
                "PublisherID = @pubID, Quantity = @quan, PublishDate = @date Where BookID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", bookName);
                command.Parameters.AddWithValue("@authorID", authorID);
                command.Parameters.AddWithValue("@cateID", categoryID);
                command.Parameters.AddWithValue("@pubID", publisherID);
                command.Parameters.AddWithValue("@quan", quantity);
                command.Parameters.AddWithValue("@date", publishDate);
                command.Parameters.AddWithValue("@ID", bookID);
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

        public void addBook(string bookID, string bookName, string authorID, string categoryID, string publisherID, string quantity, string publishDate)
        {
            string sql = "INSERT INTO Book (BookID, BookName, AuthorID, CategoryID, PublisherID, Quantity, PublishDate) " +
                "VALUES (@id, @name, @authorID, @cateID, @pubID, @quantity, @publishDate)";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", bookName);
                command.Parameters.AddWithValue("@authorID", authorID);
                command.Parameters.AddWithValue("@cateID", categoryID);
                command.Parameters.AddWithValue("@pubID", publisherID);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@publishDate", publishDate);
                command.Parameters.AddWithValue("@id", bookID);
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
