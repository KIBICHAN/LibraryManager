using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Book
    {
        private string bookID;
        private string bookName;
        private string authorID;
        private string categoryID;
        private string publisherID;
        private int quantity;
        private DateTime publisherDate;

        public string BookID { get => bookID; set => bookID = value; }
        public string BookName { get => bookName; set => bookName = value; }
        public string AuthorID { get => authorID; set => authorID = value; }
        public string CategoryID { get => categoryID; set => categoryID = value; }
        public string PublisherID { get => publisherID; set => publisherID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public DateTime PublisherDate { get => publisherDate; set => publisherDate = value; }

        public Book(string bookID, string bookName, string authorID, string categoryID, string publisherID, int quantity, DateTime publisherDate)
        {
            this.BookID = bookID;
            this.BookName = bookName;
            this.AuthorID = authorID;
            this.CategoryID = categoryID;
            this.PublisherID = publisherID;
            this.Quantity = quantity;
            this.PublisherDate = publisherDate;
        }

        public Book()
        {
        }
    }
}
