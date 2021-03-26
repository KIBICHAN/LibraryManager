using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Order
    {
        string recordID, employeeID, readerID;
        DateTime borrowDate, returnDate ,dateOfCreate;
        int totalQuantity;
        string status;

        public string RecordID { get => recordID; set => recordID = value; }
        public string EmployeeID { get => employeeID; set => employeeID = value; }
        public string ReaderID { get => readerID; set => readerID = value; }
        public DateTime BorrowDate { get => borrowDate; set => borrowDate = value; }
        public DateTime ReturnDate { get => returnDate; set => returnDate = value; }
        public string Status { get => status; set => status = value; }
        public int TotalQuantity { get => totalQuantity; set => totalQuantity = value; }

        public DateTime DateOfCreate { get => dateOfCreate; }

        public Order(string recordID, string employeeID, string readerID, DateTime borrowDate, DateTime returnDate, DateTime dateOfCreate, int totalQuantity, string status)
        {
            this.recordID = recordID;
            this.employeeID = employeeID;
            this.readerID = readerID;
            this.borrowDate = borrowDate;
            this.returnDate = returnDate;
            this.dateOfCreate = dateOfCreate;
            this.totalQuantity = totalQuantity;
            this.status = status;
        }

        
    }
}
