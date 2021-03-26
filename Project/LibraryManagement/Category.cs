using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Category
    {
        private string categoryID;
        private string categoryName;
        public string CategoryID { get => categoryID; set => categoryID = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }

        public Category()
        {
        }

        public Category(string categoryID, string categoryName)
        {
            this.categoryID = categoryID;
            this.categoryName = categoryName;
        }
    }

}
