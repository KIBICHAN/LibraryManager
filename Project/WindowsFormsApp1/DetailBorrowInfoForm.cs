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
namespace WindowsFormsApp1
{
    public partial class DetailBorrowInfoForm : Form
    {
            List<Order> list = new List<Order>();
        public DetailBorrowInfoForm(string readerID)
        {
            
            InitializeComponent();
            ReaderDAO dao = new ReaderDAO();
            
            list = dao.getAllOrderByReader(readerID);
            DataTable table = new DataTable();
            table.Columns.Add("RecordID", typeof(string));
            table.Columns.Add("EmployeeID", typeof(string));
            table.Columns.Add("BorrowQuantity", typeof(string));
            table.Columns.Add("BorrowDate", typeof(DateTime));
            table.Columns.Add("ReturnDate", typeof(DateTime));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Time remaining", typeof(int));
            foreach (Order order in list)
            {
                TimeSpan time = order.ReturnDate - order.BorrowDate;
                int timeRemaining = time.Days;
                table.Rows.Add(order.RecordID, order.EmployeeID, order.TotalQuantity, order.BorrowDate, order.ReturnDate, order.Status, timeRemaining);
            }
            
            dgvListOrder.DataSource = table;
            dgvListOrder.AllowUserToAddRows = false;
            dgvListOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvListOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListOrder.Rows[e.RowIndex];
            txtRecordID.Text = row.Cells[0].Value.ToString();
            txtBorrowQuantity.Text = row.Cells[2].Value.ToString();
            dtpBorrowDate.Value = DateTime.Parse(row.Cells[3].Value.ToString());
            dtpReturnDate.Value = DateTime.Parse(row.Cells[4].Value.ToString());
            string status = row.Cells[5].Value.ToString();
            cbStatus.Text = status;
            if(cbStatus.Text.Equals("Đã trả"))
            {
                cbStatus.Enabled = false;
            } else
            {
                cbStatus.Enabled = true;
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            OrderDAO dao = new OrderDAO();
            string recordID = txtRecordID.Text;
            int borrowQuantity = int.Parse(txtBorrowQuantity.Text);
            DateTime borrowDate = dtpBorrowDate.Value;
            DateTime returnDate = dtpReturnDate.Value;
            string status = cbStatus.Text;
            
            if(recordID.Length == 0)
            {
                MessageBox.Show("Pls choose record first!!");
                return;
            }
            foreach(Order order in list)
            {
                if (recordID.Equals(order.RecordID))
                {
                    
                    order.TotalQuantity = borrowQuantity;
                    order.BorrowDate = borrowDate;
                    order.ReturnDate = returnDate;
                    order.Status = status;
                    if (dao.updateRecord(order))
                    {
                        BookDAO bookDAO = new BookDAO();
                        if (status.Equals("Đã trả"))
                        {
                            List<RecordDetail> listRecordDetail = dao.getListDetailByRecordID(recordID);
                            List<Book> listBook = bookDAO.getAllBook();
                            foreach (RecordDetail recordDetail in listRecordDetail)
                            {
                                foreach (Book book in listBook)
                                {
                                    if (recordDetail.BookID.Equals(book.BookID))
                                    {
                                        book.Quantity = recordDetail.Quantity + book.Quantity;
                                    }
                                }
                            }
                            dao.setQuantityBook(listBook);
                            
                        }
                        MessageBox.Show("Update Sucess!!");
                        DataTable table = new DataTable();
                        table.Columns.Add("RecordID", typeof(string));
                        table.Columns.Add("EmployeeID", typeof(string));
                        table.Columns.Add("BorrowQuantity", typeof(string));
                        table.Columns.Add("BorrowDate", typeof(DateTime));
                        table.Columns.Add("ReturnDate", typeof(DateTime));
                        table.Columns.Add("Status", typeof(string));
                        table.Columns.Add("Time remaining", typeof(int));
                        foreach (Order o in list)
                        {
                            TimeSpan time = o.ReturnDate - o.BorrowDate;
                            int timeRemaining = time.Days;
                            table.Rows.Add(o.RecordID, o.EmployeeID, o.TotalQuantity, o.BorrowDate, o.ReturnDate, o.Status, timeRemaining);
                        }

                        dgvListOrder.DataSource = table;
                        dgvListOrder.AllowUserToAddRows = false;
                        dgvListOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        return;
                    } else
                    {
                        MessageBox.Show("Update Fail...");
                        return;
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string recordID = txtSearchRecord.Text;
            OrderDAO dao = new OrderDAO();
            
            if(recordID.Length == 0)
            {
                
                List<Order> list = dao.getAllOrder();
                DataTable table = new DataTable();
                table.Columns.Add("RecordID", typeof(string));
                table.Columns.Add("EmployeeID", typeof(string));
                table.Columns.Add("BorrowQuantity", typeof(string));
                table.Columns.Add("BorrowDate", typeof(DateTime));
                table.Columns.Add("ReturnDate", typeof(DateTime));
                table.Columns.Add("Status", typeof(string));
                table.Columns.Add("Time remaining", typeof(int));
                foreach (Order o in list)
                {
                    TimeSpan time = o.ReturnDate - o.BorrowDate;
                    int timeRemaining = time.Days;
                    table.Rows.Add(o.RecordID, o.EmployeeID, o.TotalQuantity, o.BorrowDate, o.ReturnDate, o.Status, timeRemaining);
                }

                dgvListOrder.DataSource = table;
                dgvListOrder.AllowUserToAddRows = false;
                dgvListOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            } else
            {
                DataTable table = new DataTable();
                Order order = dao.getOrderByRecordID(recordID);
                if (order != null)
                {
                    table.Columns.Add("RecordID", typeof(string));
                    table.Columns.Add("EmployeeID", typeof(string));
                    table.Columns.Add("BorrowQuantity", typeof(string));
                    table.Columns.Add("BorrowDate", typeof(DateTime));
                    table.Columns.Add("ReturnDate", typeof(DateTime));
                    table.Columns.Add("Status", typeof(string));
                    table.Columns.Add("Time remaining", typeof(int));
                    TimeSpan time = order.ReturnDate - order.BorrowDate;
                    int timeRemaining = time.Days;
                    table.Rows.Add(order.RecordID, order.EmployeeID, order.TotalQuantity, order.BorrowDate, order.ReturnDate, order.Status, timeRemaining);
                    dgvListOrder.DataSource = table;
                    dgvListOrder.AllowUserToAddRows = false;
                    dgvListOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                } else
                {
                    MessageBox.Show("Not Found!!");
                }
            }
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            DetailRecordForm frm = new DetailRecordForm(txtRecordID.Text);
            frm.Show();
        }
    }
}
