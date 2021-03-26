using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class PublisherDAO
    {
        string strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";
        

        public List<Publisher> getAllPublisher()
        {
            string sql = "Select PublisherID, PublisherName, PublisherAddress, Email, PublisherPhone From Publisher";
            List<Publisher> list = new List<Publisher>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                Publisher p = null;
                while (rd.Read())
                {
                    string publisherID = rd.GetValue(0).ToString();
                    string publisherName = rd.GetValue(1).ToString();
                    string publisherAddress = rd.GetValue(2).ToString();
                    string email = rd.GetValue(3).ToString();
                    string publisherPhone = rd.GetValue(4).ToString();
                    p = new Publisher(publisherID, publisherName, publisherAddress, email, publisherPhone);
                    list.Add(p);
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

        public string searchPublisherName(string id)
        {
            string sql = "Select PublisherName From Publisher Where PublisherID = @ID";
            string authorName = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@ID", id);
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

        public List<string> getPublisherName()
        {
            string sql = "Select PublisherName From Publisher";
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
                    string publisherName = rd.GetValue(0).ToString();
                    a = publisherName;
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

        public List<string> getPublisherID()
        {
            string sql = "Select PublisherID From Publisher";
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
                    string publisherID = rd.GetValue(0).ToString();
                    a = publisherID;
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

        public bool delete(string publisherID)
        {
            bool check = false;
            string sql = "Delete From Publisher Where PublisherID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand cm = new SqlCommand(sql, con);

                cm.Parameters.AddWithValue("@ID", publisherID);
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

        public void updatePublisher(string publisherID, string publisherName, string publisherAddress, string publisherEmail, string publisherPhone)
        {
            string sql = "UPDATE Publisher SET PublisherName = @name, PublisherAddress = @address, Email = @email, PublisherPhone = @phone Where PublisherID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", publisherName);
                command.Parameters.AddWithValue("@address", publisherAddress);
                command.Parameters.AddWithValue("@email", publisherEmail);
                command.Parameters.AddWithValue("@phone", publisherPhone);
                command.Parameters.AddWithValue("@ID", publisherID);
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

        public void addPublisher(string publisherID, string publisherName, string publisherAddress, string publisherEmail, string publisherPhone)
        {
            string sql = "INSERT INTO Publisher (PublisherID, PublisherName, PublisherAddress, Email, PublisherPhone) " +
                "VALUES (@ID, @name, @address, @email, @phone) ";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", publisherName);
                command.Parameters.AddWithValue("@address", publisherAddress);
                command.Parameters.AddWithValue("@email", publisherEmail);
                command.Parameters.AddWithValue("@phone", publisherPhone);
                command.Parameters.AddWithValue("@ID", publisherID);
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
