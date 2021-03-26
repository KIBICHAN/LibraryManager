using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace LibraryManagement
{
    public class ReaderDAO
    {
        string strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";

        public List<Reader> getAllReaders()
        {
            List<Reader> list = new List<Reader>(); ;
            SqlConnection con = null;
            SqlCommand cm = null;
            SqlDataReader rd = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select * From Reader";
                    cm = new SqlCommand(sql, con);
                    rd = cm.ExecuteReader();
                    while (rd.Read())
                    {
                        string readerID = rd.GetValue(0).ToString();
                        string readerName = rd.GetValue(1).ToString();
                        string readerAddress = rd.GetValue(2).ToString();
                        string readerPhone = rd.GetValue(3).ToString();
                        string email = rd.GetValue(4).ToString();
                       

                        list.Add(new Reader(readerID, readerName, readerAddress, readerPhone, email));
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

        public Reader findReaderByID(string readerID)
        {
            Reader reader = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                if(con!= null)
                {
                    con.Open();
                    string sql = "Select * From Reader Where ReaderID = @ID";
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", readerID);
                    SqlDataReader rd = cm.ExecuteReader();
                    if (rd.Read())
                    {
                        string name = rd.GetValue(1).ToString();
                        string address = rd.GetValue(2).ToString();
                        string phone = rd.GetValue(3).ToString();
                        string email = rd.GetValue(4).ToString();
                        reader = new Reader(readerID, name, address, phone, email);
                    }
                }
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            } finally
            {
                con.Close();
            }
            return reader;
        }

       
        public bool insertReader( Reader reader)
        {
            bool check = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Insert into Reader(ReaderID, ReaderName, ReaderAddress, ReaderPhone, Email) " +
                        "Values(@ID, @Name, @Address, @Phone, @Email)";
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", reader.ReaderID);
                    cm.Parameters.AddWithValue("@Name", reader.ReaderName);
                    cm.Parameters.AddWithValue("@Address", reader.ReaderAddress);
                    cm.Parameters.AddWithValue("@Phone", reader.ReaderPhone);
                    cm.Parameters.AddWithValue("@Email", reader.Email);
                    check = cm.ExecuteNonQuery() > 0? true:check;
                    
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
            return check;
        }

        public bool updateReader(Reader reader)
        {
            bool check = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Update Reader set ReaderName=@Name,ReaderAddress=@Address,ReaderPhone=@Phone,Email=@Email Where ReaderID=@ID";
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@Name", reader.ReaderName);
                    cm.Parameters.AddWithValue("@Address", reader.ReaderAddress);
                    cm.Parameters.AddWithValue("@Phone", reader.ReaderPhone);
                    cm.Parameters.AddWithValue("@Email", reader.Email);
                    cm.Parameters.AddWithValue("@ID", reader.ReaderID);
                    check = cm.ExecuteNonQuery() > 0;
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return check;
        }

        public List<Order> getAllOrderByReader(string readerID)
        {
            List<Order> list = new List<Order>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select * " +
                        "From Record " +
                        "Where ReaderID = @ID";
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", readerID);
                    SqlDataReader rd = cm.ExecuteReader();
                    while (rd.Read())
                    {
                        string recordID = rd.GetValue(0).ToString();
                        string employeeID = rd.GetValue(1).ToString();
                        int quantity = int.Parse(rd.GetValue(3).ToString());
                        string status = rd.GetValue(4).ToString();
                        DateTime borrowDate = DateTime.Parse(rd.GetValue(5).ToString());
                        DateTime returnDate = DateTime.Parse(rd.GetValue(6).ToString());
                        DateTime dateOfCreate = DateTime.Parse(rd.GetValue(7).ToString());
                        
                        list.Add(new Order(recordID, employeeID, readerID, borrowDate, returnDate, dateOfCreate, quantity, status));
                    }
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            } finally
            {
                con.Close();
            }
            return list;
        }
    }
}
