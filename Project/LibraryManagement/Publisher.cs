using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class Publisher
    {
        private string publisherID;
        private string publisherName;
        private string publisherAddress;
        private string email;
        private string publisherPhone;

        public string PublisherID { get => publisherID; set => publisherID = value; }
        public string PublisherName { get => publisherName; set => publisherName = value; }
        public string PublisherAddress { get => publisherAddress; set => publisherAddress = value; }
        public string Email { get => email; set => email = value; }
        public string PublisherPhone { get => publisherPhone; set => publisherPhone = value; }

        public Publisher()
        {
        }

        public Publisher(string publisherID, string publisherName, string publisherAddress, string email, string publisherPhone)
        {
            this.publisherID = publisherID;
            this.publisherName = publisherName;
            this.publisherAddress = publisherAddress;
            this.email = email;
            this.publisherPhone = publisherPhone;
        }
    }
}
