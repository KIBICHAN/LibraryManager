using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class EmployeeDAO
    {
        string strConnection = "server=.;database=LibraryManager;uid=sa;pwd=01678789381el";
        

        public List<Employee> getAllEmployees()
        {
            string sql = "Select EmployeeID, Password, EmployeeName, DateOfBirth, PhoneNumber, EmployeeRole From Employee";
            List<Employee> list = new List<Employee>();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader rd = command.ExecuteReader();
                Employee p = null;
                while (rd.Read())
                {
                    string empID = rd.GetValue(0).ToString();
                    string password = rd.GetValue(1).ToString();
                    string empName = rd.GetValue(2).ToString();
                    DateTime dateOfBirth = DateTime.Parse(rd.GetValue(3).ToString());
                    string phone = rd.GetValue(4).ToString();
                    bool role = Boolean.Parse(rd.GetValue(5).ToString());
                    string check = "Employee";
                    if (role)
                    {
                        check = "Admin";
                    }
                    p = new Employee(empID, password, empName, dateOfBirth, phone, check);
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

        public bool delete(string empID)
        {
            bool check = false;
            string sql = "Delete From Employee Where EmployeeID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand cm = new SqlCommand(sql, con);
            
                cm.Parameters.AddWithValue("@ID", empID);
                con.Open();
                check = cm.ExecuteNonQuery() > 0 ? true : check;
            }catch(SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
            }
            return check;
        }

        public Employee checkLogin(String id, String pass)
        {
            Employee emp = null;
            string sql = "Select EmployeeID, Password, EmployeeName, DateOfBirth, PhoneNumber, EmployeeRole from Employee Where EmployeeID = @ID and Password = @Pass";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand cm = new SqlCommand(sql, con);
                cm.Parameters.AddWithValue("@ID", id);
                cm.Parameters.AddWithValue("@Pass", pass);
                con.Open();
                SqlDataReader rd = cm.ExecuteReader();
                if (rd.Read())
                {
                    string empID = rd.GetValue(0).ToString();
                    string password = rd.GetValue(1).ToString();
                    string empName = rd.GetValue(2).ToString();
                    DateTime dateOfBirth = DateTime.Parse(rd.GetValue(3).ToString());         
                    string phone = rd.GetValue(4).ToString();
                    bool role = Boolean.Parse(rd.GetValue(5).ToString());
                    string check = "Employee";
                    if (role)
                    {
                        check = "Admin";
                    }
                    emp = new Employee(empID, password, empName, dateOfBirth, phone, check);

                }
                
            }
            catch (SqlException e)
            {
                throw new Exception(e.Message);
            } finally
            {
                con.Close();
            }
            return emp;
        }

        public void updateEmp(string id, string pass, string name, string date, string phone, Boolean role)
        {
            string sql = "UPDATE Employee SET EmployeeName = @name, Password = @pass, DateOfBirth = @date, PhoneNumber = @phone, EmployeeRole = @role Where EmployeeID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@ID", id);
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

        public void updateEmployee(string id, string name, string date, string phone)
        {
            string sql = "UPDATE Employee SET EmployeeName = @name, DateOfBirth = @date, PhoneNumber = @phone Where EmployeeID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", name);
              
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@phone", phone);
              
                command.Parameters.AddWithValue("@ID", id);
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

        public void addEmp(string id, string pass, string name, string date, string phone, Boolean role)
        {
            string sql = "INSERT INTO Employee (EmployeeID, Password, EmployeeName, DateOfBirth, PhoneNumber, EmployeeRole) " +
                "VALUES (@ID, @pass, @name, @date, @phone, @role)";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@pass", pass);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@role", role);
                command.Parameters.AddWithValue("@ID", id);
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
        public void updatePassword(string id, string pass)
        {
            string sql = "UPDATE Employee SET Password = @Pass Where EmployeeID = @ID";
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("@Pass", pass);

                command.Parameters.AddWithValue("@ID", id);
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
