using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagement;
using System.Text.RegularExpressions;
namespace WindowsFormsApp1
{
    public partial class LibraryForm : Form
    {
        Employee emp = new Employee(); 
        LibraryDAO dao = new LibraryDAO();
        List<Cart> listcart = new List<Cart>();
        public LibraryForm(Employee employee)
        {
            InitializeComponent();
            LoadBook();
            lbWellcome.Text = "Wellcome "+ employee.EmployeeName;
            emp = employee;
            txtEmployeeID.Text = employee.EmployeeID;
            txtEmployeeName.Text = employee.EmployeeName;
            txtDateOfBirth.Text = employee.DateOfBirth.ToString("yyyy/MM/dd");
            txtEmployeePhone.Text = employee.Phone;
        }

        private void LoadBook()
        {
            List<Book> listBook = dao.getAllBooks();
            DataTable table = new DataTable();
            if (listBook.Any())
            {
                table.Columns.Add("BookID", typeof(string));
                table.Columns.Add("BookName", typeof(string));
                table.Columns.Add("Author", typeof(string));
                table.Columns.Add("Category", typeof(string));
                table.Columns.Add("Publisher", typeof(string));
                table.Columns.Add("PublishDate", typeof(DateTime));
                listBook.ForEach(delegate (Book book)
                {
                    string author = dao.getAuthorNameByID(book.AuthorID);
                    string cate = dao.getCateNameByID(book.CategoryID);
                    string publisher = dao.getPublisherNameByID(book.PublisherID);
                    table.Rows.Add(book.BookID, book.BookName, author, cate, publisher, book.PublisherDate);
                });
                dgvListBook.DataSource = table;
                dgvListBook.AllowUserToAddRows = false;
                dgvListBook.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
            }
        }
        
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void dgvListBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1) return;
                DataGridViewRow row = dgvListBook.Rows[e.RowIndex];
                txtBookID.Text = row.Cells[0].Value.ToString();
                txtBookName.Text = row.Cells[1].Value.ToString();
                txtAuthor.Text = row.Cells[2].Value.ToString();
                txtCategory.Text = row.Cells[3].Value.ToString();
                txtPublisher.Text = row.Cells[4].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(row.Cells[5].Value.ToString());
            }
            catch (Exception ex) { }
        }
        

        private void btnSearchBook_Click(object sender, EventArgs e)
        {
            string search = txtSearchBook.Text;
            if (search.Length == 0)
            {
                LoadBook();
            }
            else
            {
                List<Book> list = dao.getBooksByName(search);

                if (list.Any())
                {
                    DataTable table = new DataTable();
                    table.Columns.Add("BookID", typeof(string));
                    table.Columns.Add("BookName", typeof(string));
                    table.Columns.Add("Author", typeof(string));
                    table.Columns.Add("Category", typeof(string));
                    table.Columns.Add("Publisher", typeof(string));
                    table.Columns.Add("PublishDate", typeof(DateTime));
                    int count = 0;
                    list.ForEach(delegate (Book book)
                    {
                        count++;
                        string author = dao.getAuthorNameByID(book.AuthorID);
                        string cate = dao.getCateNameByID(book.CategoryID);
                        string publisher = dao.getPublisherNameByID(book.PublisherID);
                        table.Rows.Add(book.BookID, book.BookName, author, cate, publisher, book.PublisherDate);
                    });
                    dgvListBook.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvListBook.AllowUserToAddRows = false;
                    dgvListBook.DataSource = table;
                } else
                {
                    MessageBox.Show("Not Found!!");
                }
            }
        }

        private bool checkDuplicateCartBook(string id)
        {
            bool check = true;
            foreach(Cart c in listcart)
            {
                if (c.BookID.Equals(id))
                {
                    check = false;
                    break;
                }

            }
            return check;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtBookID.Text.Length == 0)
            {
                MessageBox.Show("Pls choose the book first");
            }
            else
            {
                if (!listcart.Any())
                {
                    listcart.Add(new Cart(txtBookID.Text, txtBookName.Text, 1));
                }
                else
                {
                    foreach (Cart c in listcart)
                    {
                        if (c.BookID.Equals(txtBookID.Text))
                        {
                            c.Quantity++;
                        }

                    }
                    if (checkDuplicateCartBook(txtBookID.Text))
                    {
                        listcart.Add(new Cart(txtBookID.Text, txtBookName.Text, 1));
                    }

                }
                
                DataTable table = new DataTable();
                table.Columns.Add("BookID", typeof(string));
                table.Columns.Add("BookName", typeof(string));
                table.Columns.Add("Quantity", typeof(int));
                dgvListBorrow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                listcart.ForEach(delegate (Cart cart)
                {

                    table.Rows.Add(cart.BookID, cart.BookName, cart.Quantity);
                });
                MessageBox.Show("Add Success!");
                dgvListBorrow.DataSource = table;
                dgvListBorrow.AllowUserToAddRows = false;
                dgvListBorrow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        

        private void dgvListBorrow_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListBorrow.Rows[e.RowIndex];
            txtCartBookID.Text = row.Cells[0].Value.ToString();
            txtCartBookName.Text = row.Cells[1].Value.ToString();
            txtCartQuantityBook.Text = row.Cells[2].Value.ToString();
        }

        private void btnUpdateCart_Click(object sender, EventArgs e)
        {
            try
            {
                int test = int.Parse(txtCartQuantityBook.Text);
            }catch(Exception ex)
            {
                MessageBox.Show("Pls input a number");
                return;
            }
            if (txtBookID.Text.Length == 0)
            {
                MessageBox.Show("Pls choose the book first");
            }
            else
            {
                try
                {

                    int quantity = int.Parse(txtCartQuantityBook.Text);
                    if(txtCartQuantityBook.Text.Length == 0)
                    {
                        MessageBox.Show("Quantity can not be blank");
                        return;
                    }
                    foreach (Cart c in listcart)
                    {
                        if (c.BookID.Equals(txtCartBookID.Text))
                        {
                            c.Quantity = quantity;

                            break;
                        }
                    }
                    DataTable table = new DataTable();
                    table.Columns.Add("BookID", typeof(string));
                    table.Columns.Add("BookName", typeof(string));
                    table.Columns.Add("Quantity", typeof(int));

                    listcart.ForEach(delegate (Cart cart)
                    {

                        table.Rows.Add(cart.BookID, cart.BookName, cart.Quantity);
                    });
                    MessageBox.Show("Update Success!!");
                    dgvListBorrow.DataSource = table;
                    dgvListBorrow.AllowUserToAddRows = false;
                    dgvListBorrow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Pls input a number");
                }
            }
        }

        private void btnDeleteCart_Click(object sender, EventArgs e)
        {
            string bookID = txtBookID.Text;
            if(bookID.Length == 0)
            {
                MessageBox.Show("Pls choose the book first!!");
            }
            else
            {
                if (MessageBox.Show("Do you want to remove?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (Cart cart in listcart)
                    {
                        if (cart.BookID.Equals(txtBookID.Text))
                        {
                            listcart.Remove(cart);
                            break;
                        }
                    }
                    DataTable table = new DataTable();
                    table.Columns.Add("BookID", typeof(string));
                    table.Columns.Add("BookName", typeof(string));
                    table.Columns.Add("Quantity", typeof(int));

                    listcart.ForEach(delegate (Cart cart)
                    {

                        table.Rows.Add(cart.BookID, cart.BookName, cart.Quantity);
                    });
                    MessageBox.Show("Delete Success!!");
                    dgvListBorrow.DataSource = table;
                    dgvListBorrow.AllowUserToAddRows = false;
                    dgvListBorrow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
        }

        private bool checkDuplicateReaderID(string readerID)
        {
            bool check = true;
            ReaderDAO readerDAO = new ReaderDAO();
            List<Reader> listReader = readerDAO.getAllReaders();
            foreach(Reader r in listReader)
            {
                if (r.ReaderID.Equals(readerID))
                {
                    check = false;
                }
            }
            return check;
        }

        
        private void btnConfirmNewReader_Click(object sender, EventArgs e)
        {
            try
            {
                int test = int.Parse(txtNewReaderPhone.Text);
            } catch (Exception ex)
            {
                MessageBox.Show("Phone must be a number");
                return;
            }
            OrderDAO orderDAO = new OrderDAO();
            ReaderDAO readerDAO = new ReaderDAO();
            List<Reader> listReader = readerDAO.getAllReaders();
            if(txtNewReaderName.Text.Length == 0 || txtNewReaderEmail.Text.Length == 0 || txtNewReaderAddress.Text.Length == 0 || txtNewReaderPhone.Text.Length == 0)
            {
                MessageBox.Show("All info must not blank!!");
                return;
            }
            int cc = 1;
            foreach(Reader r in listReader)
            {
                string[] tmp = r.ReaderID.Split('-');
                if(int.Parse(tmp[1]) == cc)
                {
                    cc++;
                } else
                {
                    string ID = "SE-" + cc;
                    if (checkDuplicateReaderID(ID))
                    {
                        break;
                    } else
                    {
                        cc++;
                    }
                } 
                
            }
            string readerID = "SE-" + cc;
                string readerName = txtNewReaderName.Text;
                string readerAddress = txtNewReaderAddress.Text;
                string readerPhone = txtNewReaderPhone.Text;
                string readerEmail = txtNewReaderEmail.Text;
                DateTime borrowDate = dtpBorrowDate.Value;
                DateTime returnDate = dtpReturnDate.Value;
                if(returnDate.CompareTo(borrowDate) < 0)
                {
                    MessageBox.Show("Return Date must not be earlier than Borrow Date!!");
                    return;
                }
                DateTime dateOfCreate = DateTime.Now;
                Reader rd = new Reader(readerID, readerName, readerAddress, readerPhone, readerEmail);
                
                
                string recordID = "OD-" + readerID + "-1";
                List<Book> listBook = dao.getAllBooks();
                List<Book> listcheckQuantityBook = new List<Book>();
                foreach(Cart c in listcart)
                {
                    foreach(Book b in listBook)
                    {
                        if (b.BookID.Equals(c.BookID))
                        {
                            b.Quantity = b.Quantity - c.Quantity;
                            listcheckQuantityBook.Add(b);
                        }
                    }
                }
                foreach (Book b in listcheckQuantityBook)
                {
                    if (b.Quantity < 0)
                    {
                        MessageBox.Show("Quantity of the book " + b.BookName + " is not enough");                 
                        return;
                    }
                }
                int totalQuantity = 0;
                foreach (Cart c in listcart)
                {
                    totalQuantity += c.Quantity;
                }
                Order order = new Order(recordID, emp.EmployeeID, readerID, borrowDate, returnDate, dateOfCreate, totalQuantity, "Đang mượn");
                readerDAO.insertReader(rd);
                bool checkCreateOrder = orderDAO.createOrder(order);
                if (checkCreateOrder)
                {
                    int count = 0;
                    foreach (Cart c in listcart)
                    {
                        count++;
                        string recordDetailID = recordID + "-" + count;
                        orderDAO.createOrderDetail(recordDetailID, recordID, c.BookID, c.Quantity);
                    }
                    orderDAO.setQuantityBook(listcheckQuantityBook);
                MessageBox.Show("New Reader has ID: " + readerID + " is added!!");
                    MessageBox.Show("Order Success!!");
                    listcart.Clear();
                    txtCartBookID.Text = "";
                    txtCartBookName.Text = "";
                    txtCartQuantityBook.Text = "";
                    
                    txtNewReaderName.Text = "";
                    txtNewReaderAddress.Text = "";
                    txtNewReaderPhone.Text = "";
                    txtNewReaderEmail.Text = "";
                dgvListBorrow.DataSource = null;
                }
            
        }

        private void btnConfirmOldReader_Click(object sender, EventArgs e)
        {
            if(txtOldReaderID.Text.Length == 0)
            {
                MessageBox.Show("Pls input ReaderID first!!");
            }
            OrderDAO orderDAO = new OrderDAO();

            string readerID = txtOldReaderID.Text;
            if (checkDuplicateReaderID(readerID))
            {
                MessageBox.Show("This Reader is not exist!!");
                return;
            }
            DateTime borrowDate = dtpBorrowDate1.Value;
            DateTime returnDate = dtpReturnDate1.Value;
            if (returnDate.CompareTo(borrowDate) < 0)
            {
                MessageBox.Show("Return Date must not be earlier than Borrow Date!!");
                return;
            }
            DateTime dateOfCreate = DateTime.Now;
            string lastOrder = orderDAO.getLastOrderIDByReader(readerID);
            string recordID = "";
            ReaderDAO readerDAO = new ReaderDAO();
            
            if (lastOrder != null)
            {
                string[] tmp = lastOrder.Split('-');
                recordID = "OD" + "-" + readerID + "-" + (int.Parse(tmp[3]) + 1);
            }
            else
            {
                recordID = "OD-" + readerID + "-1";
            }
            List<Book> listBook = dao.getAllBooks();
            List<Book> listcheckQuantityBook = new List<Book>();
            foreach (Cart c in listcart)
            {
                foreach (Book b in listBook)
                {
                    if (b.BookID.Equals(c.BookID))
                    {
                        b.Quantity = b.Quantity - c.Quantity;
                        listcheckQuantityBook.Add(b);
                    }
                }
            }
            foreach (Book b in listcheckQuantityBook)
            {
                if (b.Quantity < 0)
                {
                    MessageBox.Show("Quantity of the book " + b.BookName + " is not enough");
                    return;
                }
            }
            int totalQuantity = 0;
            foreach (Cart c in listcart)
            {
                totalQuantity += c.Quantity;
            }
            Order order = new Order(recordID, emp.EmployeeID, readerID, borrowDate, returnDate, dateOfCreate, totalQuantity, "Đang mượn");
            
            bool checkCreateOrder = orderDAO.createOrder(order);
            if (checkCreateOrder)
            {
                int count = 0;
                foreach (Cart c in listcart)
                {
                    count++;
                    string recordDetailID = recordID + "-" + count;
                    orderDAO.createOrderDetail(recordDetailID, recordID, c.BookID, c.Quantity);
                }
                orderDAO.setQuantityBook(listcheckQuantityBook);
                MessageBox.Show("Order Success!!");
                listcart.Clear();
                
                txtCartBookID.Text = ""; 
                txtCartBookName.Text = "";
                txtCartQuantityBook.Text = "";
                
                txtNewReaderName.Text = "";
                txtNewReaderAddress.Text = "";
                txtNewReaderPhone.Text = "";
                txtNewReaderEmail.Text = "";
                txtOldReaderID.Text = "";
                dgvListBorrow.DataSource = null;
            }
        }

        private void btnSearchReader_Click(object sender, EventArgs e)
        {
            List<Reader> list = null;
            string search = txtSearchReader.Text;
            ReaderDAO readerDAO = new ReaderDAO();
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("Email", typeof(string));
            if (search.Length == 0)
            {
                
                list = readerDAO.getAllReaders();
                foreach(Reader r in list)
                {
                    table.Rows.Add(r.ReaderID, r.ReaderName, r.ReaderAddress, r.ReaderPhone, r.Email);
                }
                
            } else
            {
                Reader rd = readerDAO.findReaderByID(search);
                if(rd == null)
                {
                    MessageBox.Show("Not Found!!");
                    return;
                } else
                {
                    table.Rows.Add(rd.ReaderID, rd.ReaderName, rd.ReaderAddress, rd.ReaderPhone, rd.Email);
                }
            }
            dgvListReader.DataSource = table;
            dgvListReader.AllowUserToAddRows = false;
            dgvListReader.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvListReader_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListReader.Rows[e.RowIndex];
            txtReaderID1.Text = row.Cells[0].Value.ToString();
            txtReaderName1.Text = row.Cells[1].Value.ToString();
            txtAddress1.Text = row.Cells[2].Value.ToString();
            txtPhone1.Text = row.Cells[3].Value.ToString();
            txtEmail1.Text = row.Cells[4].Value.ToString();
        }

        private void btnUpdateReader_Click(object sender, EventArgs e)
        {
            if(txtReaderID1.Text.Length == 0)
            {
                MessageBox.Show("Pls Chosse the Reader first!!");
                return;
            }
            if(txtReaderName1.Text.Length == 0 || txtAddress1.Text.Length == 0 || txtPhone1.Text.Length == 0 || txtEmail1.Text.Length ==0) {
                MessageBox.Show("All value must not blank");
                return;
            }
            try
            {
                int test = int.Parse(txtPhone1.Text);
            }catch (Exception ex)
            {
                MessageBox.Show("The phone must be a number!!");
                return;
            }
            Reader reader = new Reader(txtReaderID1.Text, txtReaderName1.Text, txtAddress1.Text, txtPhone1.Text, txtEmail1.Text);
            ReaderDAO readerDAO = new ReaderDAO();
            if (readerDAO.updateReader(reader)) {
                DataTable table = new DataTable();
                table.Columns.Add("ID", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Address", typeof(string));
                table.Columns.Add("Phone", typeof(string));
                table.Columns.Add("Email", typeof(string));
                List<Reader> list = readerDAO.getAllReaders();
                foreach (Reader r in list)
                {
                    table.Rows.Add(r.ReaderID, r.ReaderName, r.ReaderAddress, r.ReaderPhone, r.Email);
                }
                MessageBox.Show("Update Success!!");
                dgvListReader.DataSource = table;
                dgvListReader.AllowUserToAddRows = false;
                dgvListReader.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            } else
            {
                MessageBox.Show("Update Fail...");
            }
        }

        private void btnHistoryBorrow_Click(object sender, EventArgs e)
        {
            if (txtReaderID1.Text.Length == 0)
            {
                MessageBox.Show("Pls Chosse the Reader first!!");
                return;
            }
            ReaderDAO readerDAO = new ReaderDAO();
           
            DetailBorrowInfoForm frm = new DetailBorrowInfoForm(txtReaderID1.Text);
            this.Visible = false;
            frm.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditProfileForm frm = new EditProfileForm(emp);
            
            frm.ShowDialog();
            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm frm = new ChangePasswordForm(emp);
            frm.ShowDialog();
            this.Close();
        }
    }
}
