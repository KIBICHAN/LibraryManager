using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Author
    {
        private string authorID;
        private string authorName;
        private string descriptions;
        public string AuthorID { get => authorID; set => authorID = value; }
        public string AuthorName { get => authorName; set => authorName = value; }
        public String Descriptions { get => descriptions; set => descriptions = value; }
        
        public Author()
        {
        }

        public Author(string authorID, string authorName, string descriptions)
        {
            this.authorID = authorID;
            this.authorName = authorName;
            this.descriptions = descriptions;
        }
    }

}
