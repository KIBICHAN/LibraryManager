using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace LibraryManagement
{
    public class OrderDAO
    {
        string strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";
        public string  getLastOrderIDByReader(string readerID)
        {
            string lastOrder = null;
            SqlConnection con = null;
            
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select RecordID From Record Where DateOfCreate = (Select Max(DateOfCreate) From Record Where ReaderID = @ID)";
                    SqlCommand cm = new SqlCommand(sql,con);
                    cm.Parameters.AddWithValue("@ID", readerID);
                    SqlDataReader rd = cm.ExecuteReader();
                    if (rd.Read())
                    {
                        lastOrder = rd.GetValue(0).ToString();
                    }
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            } finally
            {
                con.Close();
            }
            return lastOrder;
        }

        public void setQuantityBook(List<Book> list)
        {
            SqlConnection con = null;

        try{
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    foreach (Book dto in list)
                    {
                        string sql = "Update Book set Quantity = @Quantity Where BookID = @ID";
                        SqlCommand cm = new SqlCommand(sql, con);
                        cm.Parameters.AddWithValue("@ID", dto.BookID);
                        cm.Parameters.AddWithValue("@Quantity", dto.Quantity);
                        cm.ExecuteNonQuery();
                    }
                }
            }finally{
                con.Close();
            }
        }

        public bool createOrder(Order dto)
        {
            bool check = false;
            SqlConnection con = null;
        try{
                string sql = "Insert into Record(RecordID, EmployeeID, ReaderID, TotalQuantity, Status, BorrowDate, ReturnDate, DateOfCreate) " +
                    "values(@RecordID,@EmployeeID,@ReaderID,@Total,@Status,@BorrowDate,@ReturnDate,@DateOfCreate)";

                con = new SqlConnection(strConnection);
                if(con!= null)
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@RecordID", dto.RecordID);
                    cm.Parameters.AddWithValue("@EmployeeID", dto.EmployeeID);
                    cm.Parameters.AddWithValue("@ReaderID", dto.ReaderID);
                    cm.Parameters.AddWithValue("@Total", dto.TotalQuantity);
                    cm.Parameters.AddWithValue("@Status", dto.Status);
                    cm.Parameters.AddWithValue("@BorrowDate", dto.BorrowDate.ToString());
                    cm.Parameters.AddWithValue("@ReturnDate", dto.ReturnDate.ToString());
                    
                    cm.Parameters.AddWithValue("@DateOfCreate", dto.DateOfCreate.ToString());
                    check = cm.ExecuteNonQuery() > 0;
                }

                
            }finally{
                con.Close();
            }
        return check;
        }
        
        public bool createOrderDetail(string recordDetailID, string recordID, string bookID, int quantity)
        {
            SqlConnection con = null;
            bool check = false;
        try {
                string sql = "Insert into RecordDetail(RecordDetailID, RecordID, BookID, Quantity) values(@RecordDetailID,@RecordID,@BookID,@Quantity)";
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@RecordDetailID", recordDetailID);
                    cm.Parameters.AddWithValue("@RecordID", recordID);
                    cm.Parameters.AddWithValue("@BookID", bookID);
                    cm.Parameters.AddWithValue("@Quantity", quantity);
                    check = cm.ExecuteNonQuery() > 0;
                }
            } finally {
                con.Close();
            }
        return check;
        }

        public bool updateRecord(Order order)
        {
            bool check = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                if(con != null)
                {
                    con.Open();
                    string sql = "Update Record Set TotalQuantity=@Quantity, Status=@Status, BorrowDate=@BorrowDate, ReturnDate=@ReturnDate Where RecordID=@ID";
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@Quantity", order.TotalQuantity);
                    cm.Parameters.AddWithValue("@Status", order.Status);
                    cm.Parameters.AddWithValue("@BorrowDate", order.BorrowDate);
                    cm.Parameters.AddWithValue("@ReturnDate", order.ReturnDate);
                    cm.Parameters.AddWithValue("@ID", order.RecordID);
                    check = cm.ExecuteNonQuery() > 0;
                }
            }catch (Exception e)
            {
                throw new Exception(e.Message);
            } finally
            {
                con.Close();
            }
            return check;
        }

        public List<RecordDetail> getListDetailByRecordID(string recordID)
        {
            List<RecordDetail> list = null;
            SqlConnection con = null;
            try
            {
                string sql = "Select * From RecordDetail Where RecordID = @ID";
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", recordID);
                    SqlDataReader rd = cm.ExecuteReader();
                    while (rd.Read())
                    {
                        string recordDetail = rd.GetValue(0).ToString();
                        string bookID = rd.GetValue(2).ToString();
                        int quantity = int.Parse(rd.GetValue(3).ToString());
                        if(list == null)
                        {
                            list = new List<RecordDetail>();
                        }
                        list.Add(new RecordDetail(recordDetail, recordID, bookID, quantity));
                    }
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return list;
        }

        public Order getOrderByRecordID(string recordID)
        {
            Order order = null;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                if (con != null)
                {
                    con.Open();
                    string sql = "Select * From Record Where RecordID = @ID";
                    SqlCommand cm = new SqlCommand(sql, con);
                    cm.Parameters.AddWithValue("@ID", recordID);
                    SqlDataReader rd = cm.ExecuteReader();
                    if (rd.Read())
                    {
                        string employeeID = rd.GetValue(1).ToString();
                        string readerID = rd.GetValue(2).ToString();
                        int total = int.Parse(rd.GetValue(3).ToString());
                        string status = rd.GetValue(4).ToString();
                        DateTime borrowDate = DateTime.Parse(rd.GetValue(5).ToString());
                        DateTime returnDate = DateTime.Parse(rd.GetValue(6).ToString());
                        DateTime dateOfTime = DateTime.Parse(rd.GetValue(7).ToString());
                        order = new Order(recordID, employeeID, readerID, borrowDate, returnDate, dateOfTime, total, status);
                    }
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            } finally
            {
                con.Close();
            }
            return order;
        }

        public List<Order> getAllOrder()
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
                        "From Record ";
                    SqlCommand cm = new SqlCommand(sql, con);
                    SqlDataReader rd = cm.ExecuteReader();
                    while (rd.Read())
                    {
                        string recordID = rd.GetValue(0).ToString();
                        string employeeID = rd.GetValue(1).ToString();
                        string readerID = rd.GetValue(2).ToString();
                        int quantity = int.Parse(rd.GetValue(3).ToString());
                        string status = rd.GetValue(4).ToString();
                        DateTime borrowDate = DateTime.Parse(rd.GetValue(5).ToString());
                        DateTime returnDate = DateTime.Parse(rd.GetValue(6).ToString());
                        DateTime dateOfCreate = DateTime.Parse(rd.GetValue(7).ToString());

                        list.Add(new Order(recordID, employeeID, readerID, borrowDate, returnDate, dateOfCreate, quantity, status));
                    }
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
    }

    
}
