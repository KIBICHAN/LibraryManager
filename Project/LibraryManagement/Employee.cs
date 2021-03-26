using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Employee
    {
        private string employeeID;
        private string password;
        private string employeeName;
        private DateTime dateOfBirth;
        private string phone;
        private string role;

        public string EmployeeID { get => employeeID; set => employeeID = value; }
        public string Password { get => password; set => password = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Role { get => role; set => role = value; }

        public Employee()
        {
        }

        public Employee(string employeeID, string password, string employeeName, DateTime dateOfBirth, string phone, string role)
        {
            this.employeeID = employeeID;
            this.password = password;
            this.employeeName = employeeName;
            this.dateOfBirth = dateOfBirth;
            this.phone = phone;
            this.role = role;
        }
    }
}
