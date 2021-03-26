using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Cart
    {
        private string bookID, bookName;
        private int quantity;

        public string BookID { get => bookID; set => bookID = value; }
        public string BookName { get => bookName; set => bookName = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        public Cart(string bookID, string bookName, int quantity)
        {
            this.BookID = bookID;
            this.BookName = bookName;
            this.Quantity = quantity;
        }

    

    }
}
