using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Reader
    {
        string readerID, readerName, readerAddress, readerPhone, email;

        public string ReaderID { get => readerID; set => readerID = value; }
        public string ReaderName { get => readerName; set => readerName = value; }
        public string ReaderAddress { get => readerAddress; set => readerAddress = value; }
        public string ReaderPhone { get => readerPhone; set => readerPhone = value; }
        public string Email { get => email; set => email = value; }

        public Reader(string readerID, string readerName, string readerAddress, string readerPhone, string email)
        {
            this.readerID = readerID;
            this.readerName = readerName;
            this.readerAddress = readerAddress;
            this.readerPhone = readerPhone;
            this.email = email;
        }
    }
}
