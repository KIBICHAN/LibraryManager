using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class RecordDetail
    {
        private string recordDetailID, recordID, bookID;
        private int quantity;

        public RecordDetail(string recordDetailID, string recordID, string bookID, int quantity)
        {
            this.RecordDetailID = recordDetailID;
            this.RecordID = recordID;
            this.BookID = bookID;
            this.Quantity = quantity;
        }

        public string RecordDetailID { get => recordDetailID; set => recordDetailID = value; }
        public string RecordID { get => recordID; set => recordID = value; }
        public string BookID { get => bookID; set => bookID = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}
