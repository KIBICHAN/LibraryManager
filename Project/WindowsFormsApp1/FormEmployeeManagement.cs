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
    public partial class FormEmployeeManagement : Form
    {
        private EmployeeDAO dao = new EmployeeDAO();
        private BookDAO bookdao = new BookDAO();
        private AuthorDAO authordao = new AuthorDAO();
        private CategoryDAO categorydao = new CategoryDAO();
        private PublisherDAO publisherdao = new PublisherDAO();
        private List<string> listAuthor = new List<string>();
        private List<string> listCate = new List<string>();
        private List<string> listPub = new List<string>();

        public FormEmployeeManagement()
        {
            InitializeComponent();
            LoadEmp();
        }

        public FormEmployeeManagement(Employee emp)
        {
            InitializeComponent();
            label6.Text = "Wellcome " + emp.EmployeeName;
            label8.Text = "Wellcome " + emp.EmployeeName;
            label15.Text = "Wellcome " + emp.EmployeeName;
            label22.Text = "Wellcome " + emp.EmployeeName;
            label20.Text = "Wellcome " + emp.EmployeeName;
            LoadEmp();
            LoadBook();
            LoadAuthor();
            LoadAuthorName();
            LoadCategory();
            LoadCategoryName();
            LoadPublisher();
            LoadPublisherName();
        }

        private bool checkDupplicateEmployee(string id)
        {
            bool check = true;
            dao.getAllEmployees().ForEach(delegate (Employee e)
            {
                if (e.EmployeeID == id)
                {
                    check = false;
                }
            });
            return check;
        }

        private bool checkDupplicateBook(string id)
        {
            bool check = true;
            bookdao.getAllBook().ForEach(delegate (Book b)
            {
                if (b.BookID == id)
                {
                    check = false;
                }
            });
            return check;
        }

        private bool checkDupplicateCategory(string id)
        {
            bool check = true;
            categorydao.getAllCategory().ForEach(delegate (Category c)
            {
                if (c.CategoryID == id)
                {
                    check = false;
                }
            });
            return check;
        }

        private bool checkDupplicateAuthor(string id)
        {
            bool check = true;
            authordao.getAllAuthor().ForEach(delegate (Author a)
            {
                if (a.AuthorID == id)
                {
                    check = false;
                }
            });
            return check;
        }

        private bool checkDupplicatePublisher(string id)
        {
            bool check = true;
            publisherdao.getAllPublisher().ForEach(delegate (Publisher p)
            {
                if (p.PublisherID == id)
                {
                    check = false;
                }
            });
            return check;
        }

        private void LoadBook()
        {

            //laydu lieu
            List<Book> list;
            list = bookdao.getAllBook();
            //Clear
            txtID.DataBindings.Clear();
            txtName.DataBindings.Clear();
            txtDate.DataBindings.Clear();
            txtQuan.DataBindings.Clear();
            dgvListBook.DataSource = list;
            //rang buoc du lieu
        }

        private void LoadCategory()
        {

            //laydu lieu
            List<Category> list;
            list = categorydao.getAllCategory();
            //Clear
            txtCategoryID.DataBindings.Clear();
            txtCategoryName.DataBindings.Clear();
            dgvListCategory.DataSource = list;
            //rang buoc du lieu
        }

        private void LoadPublisher()
        {

            //laydu lieu
            List<Publisher> list;
            list = publisherdao.getAllPublisher();
            //Clear
            txtPublisherID.DataBindings.Clear();
            txtPublisherName.DataBindings.Clear();
            txtPublisherAddress.DataBindings.Clear();
            txtPublisherEmail.DataBindings.Clear();
            txtPublisherPhone.DataBindings.Clear();
            dgvListPublisher.DataSource = list;
            //rang buoc du lieu
        }

        private void LoadAuthor()
        {

            //laydu lieu
            List<Author> list;
            list = authordao.getAllAuthor();
            //Clear
            txtAuthorID.DataBindings.Clear();
            txtAuthorName.DataBindings.Clear();
            txtDes.DataBindings.Clear();
            dgvListAuthor.DataSource = list;
            //rang buoc du lieu
        }

        private void LoadCategoryName()
        {

            //laydu lieu
            List<String> list;
            list = categorydao.getCategoryName();
            listCate = categorydao.getCategoryID();
            //Clear
            cbCategory.DataSource = list;
            //rang buoc du lieu
        }

        private void LoadPublisherName()
        {

            //laydu lieu
            List<String> list;
            list = publisherdao.getPublisherName();
            listPub = publisherdao.getPublisherID();
            //Clear
            cbPub.DataSource = list;
            //rang buoc du lieu
        }

        private void LoadAuthorName()
        {

            //laydu lieu
            List<String> list;
            list = authordao.getAuthorName();
            listAuthor = authordao.getAuthorID();
            //Clear
            cbAuthor.DataSource = list;
            //rang buoc du lieu
        }

        private void LoadEmp()
        {

            //laydu lieu
            List<Employee> list;
            list = dao.getAllEmployees();
            //Clear
            txtEmpID.DataBindings.Clear();
            txtPass.DataBindings.Clear();
            txtEmpName.DataBindings.Clear();
            txtEmpDOB.DataBindings.Clear();
            txtEmpPhone.DataBindings.Clear();
            cbRole.DataBindings.Clear();
            dgvListEmp.DataSource = list;
            //rang buoc du lieu
        }

        private void clearTextBox()
        {
            txtEmpID.Text = "";
            txtEmpName.Text = "";
            txtEmpDOB.Text = "";
            txtEmpPhone.Text = "";
            txtSearchEmp.Text = "";
        }
        private void btnSearchEmp_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearchEmp.Text;
            dgvListEmp.CurrentCell = null;
            dgvListEmp.AllowUserToAddRows = false;
            dgvListEmp.ClearSelection();
            dgvListEmp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgvListEmp.Rows)
                {
                    if (row.Cells[0].Value.ToString().Contains(searchValue))
                    {
                        row.Selected = true;
                        txtEmpID.Text = row.Cells[0].Value.ToString();
                        txtEmpName.Text = row.Cells[1].Value.ToString();
                        txtEmpDOB.Text = row.Cells[2].Value.ToString();
                        txtEmpPhone.Text = row.Cells[3].Value.ToString();
                        if (row.Cells[4].Value.ToString().Equals("Admin"))
                        {
                            cbRole.Checked = true;
                        }
                        else
                        {
                            cbRole.Checked = false;
                        }
                        break;
                    }
                }
            }   
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void dgvListEmp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListEmp.Rows[e.RowIndex];
            txtEmpID.Text = row.Cells[0].Value.ToString();
            txtPass.Text = row.Cells[1].Value.ToString();
            txtEmpName.Text = row.Cells[2].Value.ToString();
            txtEmpDOB.Text = row.Cells[3].Value.ToString();
            txtEmpPhone.Text = row.Cells[4].Value.ToString();
            if (row.Cells[5].Value.ToString().Equals("Admin"))
            {
                cbRole.Checked = true;
            }
            else
            {
                cbRole.Checked = false;
            }
        }
        private void dgvListEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAddNewEmp_Click(object sender, EventArgs e)
        {
            
        }

        private void DeleteEmp_Click(object sender, EventArgs e)
        {
            String empID = txtEmpID.Text;
            if (empID.Length == 0)
            {
                MessageBox.Show("Pls input empID first!!");
            }
            else
            {
                if (!checkDupplicateEmployee(empID))
                {
                    if(MessageBox.Show("Are you sure? Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (dao.delete(empID))
                        {
                            MessageBox.Show("Delete Success!!");
                            clearTextBox();
                            LoadEmp();
                        } else
                        {
                            MessageBox.Show("Can not Delete!!");
                        }
                    }
                } else
                {
                    MessageBox.Show("This employee is not exist!!");
                }
            }
        }

        private void txtEmpDOB_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchEmp_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdateEmp_Click(object sender, EventArgs e)
        {
            string id = txtEmpID.Text;
            string pass = txtPass.Text;
            string name = txtEmpName.Text;
            string date = txtEmpDOB.Text;
            string phone = txtEmpPhone.Text;
            Boolean admin;

            if(cbRole.Checked)
            {
                admin = true;
            } else
            {
                admin = false;
            }


            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    dao.updateEmp(id, pass, name, date, phone, admin);
                    MessageBox.Show("Update Success!!");
                    LoadEmp();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cbRole_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmpName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvListEmp_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtEmpPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dgvListBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListBook.Rows[e.RowIndex];
            txtID.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtDate.Text = row.Cells[2].Value.ToString();
            String name = authordao.searchAuthorName(row.Cells[3].Value.ToString());
            cbAuthor.SelectedIndex = cbAuthor.Items.IndexOf(name);
            String category = categorydao.searchCategoryName(row.Cells[4].Value.ToString());
            cbCategory.SelectedIndex = cbCategory.Items.IndexOf(category);
            String pub = publisherdao.searchPublisherName(row.Cells[5].Value.ToString());
            cbPub.SelectedIndex = cbPub.Items.IndexOf(pub);
            txtQuan.Text = row.Cells[6].Value.ToString();
        }

        private void dgvListBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvListAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListAuthor.Rows[e.RowIndex];
            txtAuthorID.Text = row.Cells[0].Value.ToString();
            txtAuthorName.Text = row.Cells[1].Value.ToString();
            txtDes.Text = row.Cells[2].Value.ToString();
        }

        private void dgvListCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListCategory.Rows[e.RowIndex];
            txtCategoryID.Text = row.Cells[0].Value.ToString();
            txtCategoryName.Text = row.Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String bookID = txtID.Text;
            if (bookID.Length == 0)
            {
                MessageBox.Show("Pls input bookID first!!");
            }
            else
            {
                if (!checkDupplicateBook(bookID))
                {
                    if (MessageBox.Show("Are you sure? Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (bookdao.delete(bookID))
                        {
                            MessageBox.Show("Delete Success!!");
                            txtID.Text = "";
                            txtName.Text = "";
                            txtDate.Text = "";
                            txtQuan.Text = "";
                            LoadBook();
                        }
                        else
                        {
                            MessageBox.Show("Can not Delete!!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This Book is not exist!!");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            String authorID = txtAuthorID.Text;
            if (authorID.Length == 0)
            {
                MessageBox.Show("Pls input authorID first!!");
            }
            else
            {
                if (!checkDupplicateAuthor(authorID))
                {
                    if (MessageBox.Show("Are you sure? Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (authordao.delete(authorID))
                        {
                            MessageBox.Show("Delete Success!!");
                            txtAuthorID.Text = "";
                            txtAuthorName.Text = "";
                            txtDes.Text = "";
                            LoadAuthor();
                        }
                        else
                        {
                            MessageBox.Show("Can not Delete!!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This Author is not exist!!");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            String cetegoryID = txtCategoryID.Text;
            if (cetegoryID.Length == 0)
            {
                MessageBox.Show("Pls input cetegoryID first!!");
            }
            else
            {
                if (!checkDupplicateCategory(cetegoryID))
                {
                    if (MessageBox.Show("Are you sure? Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (categorydao.delete(cetegoryID))
                        {
                            MessageBox.Show("Delete Success!!");
                            txtCategoryID.Text = "";
                            txtCategoryName.Text = "";
                            LoadCategory();
                        }
                        else
                        {
                            MessageBox.Show("Can not Delete!!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This Category is not exist!!");
                }
            }
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            String publisherID = txtPublisherID.Text;
            if (publisherID.Length == 0)
            {
                MessageBox.Show("Pls input publisherID first!!");
            }
            else
            {
                if (!checkDupplicatePublisher(publisherID))
                {
                    if (MessageBox.Show("Are you sure? Delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (publisherdao.delete(publisherID))
                        {
                            MessageBox.Show("Delete Success!!");
                            txtPublisherID.Text = "";
                            txtPublisherName.Text = "";
                            txtPublisherPhone.Text = "";
                            txtPublisherEmail.Text = "";
                            txtPublisherAddress.Text = "";
                            LoadPublisher();
                        }
                        else
                        {
                            MessageBox.Show("Can not Delete!!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("This Publisher is not exist!!");
                }
            }
        }

        private void dgvListPublisher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            DataGridViewRow row = dgvListPublisher.Rows[e.RowIndex];
            txtPublisherID.Text = row.Cells[0].Value.ToString();
            txtPublisherName.Text = row.Cells[1].Value.ToString();
            txtPublisherAddress.Text = row.Cells[2].Value.ToString();
            txtPublisherEmail.Text = row.Cells[3].Value.ToString();
            txtPublisherPhone.Text = row.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearchBookID.Text;
            dgvListBook.CurrentCell = null;
            dgvListBook.AllowUserToAddRows = false;
            dgvListBook.ClearSelection();
            dgvListBook.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgvListBook.Rows)
                {
                    if (row.Cells[0].Value.ToString().Contains(searchValue))
                    {
                        row.Selected = true;
                        txtID.Text = row.Cells[0].Value.ToString();
                        txtName.Text = row.Cells[1].Value.ToString();
                        txtDate.Text = row.Cells[2].Value.ToString();
                        String name = authordao.searchAuthorName(row.Cells[3].Value.ToString());
                        cbAuthor.SelectedIndex = cbAuthor.Items.IndexOf(name);
                        String category = categorydao.searchCategoryName(row.Cells[4].Value.ToString());
                        cbCategory.SelectedIndex = cbCategory.Items.IndexOf(category);
                        String pub = publisherdao.searchPublisherName(row.Cells[5].Value.ToString());
                        cbPub.SelectedIndex = cbPub.Items.IndexOf(pub);
                        txtQuan.Text = row.Cells[6].Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearchAuthor.Text;
            dgvListAuthor.CurrentCell = null;
            dgvListAuthor.AllowUserToAddRows = false;
            dgvListAuthor.ClearSelection();
            dgvListAuthor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgvListAuthor.Rows)
                {
                    if (row.Cells[0].Value.ToString().Contains(searchValue))
                    {
                        row.Selected = true;
                        txtAuthorID.Text = row.Cells[0].Value.ToString();
                        txtAuthorName.Text = row.Cells[1].Value.ToString();
                        txtDes.Text = row.Cells[2].Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearchCate.Text;
            dgvListCategory.CurrentCell = null;
            dgvListCategory.AllowUserToAddRows = false;
            dgvListCategory.ClearSelection();
            dgvListCategory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgvListCategory.Rows)
                {
                    if (row.Cells[0].Value.ToString().Contains(searchValue))
                    {
                        row.Selected = true;
                        txtCategoryID.Text = row.Cells[0].Value.ToString();
                        txtCategoryName.Text = row.Cells[1].Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearchPub.Text;
            dgvListPublisher.CurrentCell = null;
            dgvListPublisher.AllowUserToAddRows = false;
            dgvListPublisher.ClearSelection();
            dgvListPublisher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dgvListPublisher.Rows)
                {
                    if (row.Cells[0].Value.ToString().Contains(searchValue))
                    {
                        row.Selected = true;
                        txtPublisherID.Text = row.Cells[0].Value.ToString();
                        txtPass.Text = row.Cells[1].Value.ToString();
                        txtPublisherName.Text = row.Cells[2].Value.ToString();
                        txtPublisherAddress.Text = row.Cells[3].Value.ToString();
                        txtPublisherEmail.Text = row.Cells[4].Value.ToString();
                        txtPublisherPhone.Text = row.Cells[5].Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnUpdateAuthor_Click(object sender, EventArgs e)
        {
            string id = txtAuthorID.Text;
            string name = txtAuthorName.Text;
            string des = txtDes.Text;
            try
            {
                if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(des))
                {
                    MessageBox.Show("Don't leave the blank box!");
                } else
                {
                    authordao.updateAuthor(id, name, des);
                    MessageBox.Show("Update Success!!");
                    LoadAuthor();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string publishID = listPub[cbPub.SelectedIndex];
            string cateID = listCate[cbCategory.SelectedIndex];
            string authorID = listAuthor[cbAuthor.SelectedIndex];
            string quantity = txtQuan.Text;
            string date = txtDate.Text;

            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) )
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    bookdao.updateBook(id, name, authorID, cateID, publishID, quantity, date);
                    MessageBox.Show("Update Success!!");
                    LoadBook();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnUpdateCate_Click(object sender, EventArgs e)
        {
            string id = txtCategoryID.Text;
            string name = txtCategoryName.Text;
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    categorydao.updateCategory(id, name);
                    MessageBox.Show("Update Success!!");
                    LoadCategory();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnUpdatePublisher_Click(object sender, EventArgs e)
        {
            string id = txtPublisherID.Text;
            string name = txtPublisherName.Text;
            string address = txtPublisherAddress.Text;
            string email = txtPublisherEmail.Text;
            string phone = txtPublisherPhone.Text;
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    publisherdao.updatePublisher(id, name, address, email, phone);
                    MessageBox.Show("Update Success!!");
                    LoadPublisher();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            string id = txtEmpID.Text;
            string pass = txtPass.Text;
            string name = txtEmpName.Text;
            string date = txtEmpDOB.Text;
            string phone = txtEmpPhone.Text;
            Boolean admin;

            if (cbRole.Checked)
            {
                admin = true;
            }
            else
            {
                admin = false;
            }


            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    dao.updateEmp(id, pass, name, date, phone, admin);

                    LoadEmp();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string name = txtName.Text;
            string publishID = listPub[cbPub.SelectedIndex];
            string cateID = listCate[cbCategory.SelectedIndex];
            string authorID = listAuthor[cbAuthor.SelectedIndex];
            string quantity = txtQuan.Text;
            string date = txtDate.Text;

            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    bookdao.addBook(id, name, authorID, cateID, publishID, quantity, date);
                    LoadBook();
                    MessageBox.Show("Create successfully!");
                }
            }
            catch (Exception exc)
            {
                if(exc.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Book is existed!");
                }
            }
        }

        private void btnClearBook_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtID.ReadOnly = false;
            txtName.Text = "";
            txtDate.Text = "";
            txtQuan.Text = "";
        }

        private void btnAddAuthor_Click(object sender, EventArgs e)
        {
            string id = txtAuthorID.Text;
            string name = txtAuthorName.Text;
            string des = txtDes.Text;
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(des))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    authordao.addAuthor(id, name, des);
                    LoadAuthor();
                    MessageBox.Show("Create successfully!");
                }
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Author is existed!");
                }
            }
        }

        private void btnClearAuthor_Click(object sender, EventArgs e)
        {
            txtAuthorID.Text = "";
            txtAuthorID.ReadOnly = false;
            txtAuthorName.Text = "";
            txtDes.Text = "";
        }

        private void btnAddCate_Click(object sender, EventArgs e)
        {
            string id = txtCategoryID.Text;
            string name = txtCategoryName.Text;
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    categorydao.addCategory(id, name);
                    LoadCategory();
                    MessageBox.Show("Create successfully!");
                }
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Category is existed!");
                }
            }
        }

        private void btnClearCate_Click(object sender, EventArgs e)
        {
            txtCategoryID.Text = "";
            txtCategoryID.ReadOnly = false;
            txtCategoryName.Text = "";
        }

        private void btnAddPub_Click(object sender, EventArgs e)
        {
            string id = txtPublisherID.Text;
            string name = txtPublisherName.Text;
            string address = txtPublisherAddress.Text;
            string email = txtPublisherEmail.Text;
            string phone = txtPublisherPhone.Text;
            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    publisherdao.addPublisher(id, name, address, email, phone);
                    LoadPublisher();
                    MessageBox.Show("Create successfully!");
                }
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Publisher is existed!");
                }
            }
        }

        private void btnClearPub_Click(object sender, EventArgs e)
        {
            txtPublisherID.Text = "";
            txtPublisherID.ReadOnly = false;
            txtPublisherName.Text = "";
            txtPublisherPhone.Text = "";
            txtPublisherEmail.Text = "";
            txtPublisherAddress.Text = "";
        }

        private void btnAddEmp_Click_1(object sender, EventArgs e)
        {
            string id = txtEmpID.Text;
            string pass = txtPass.Text;
            string name = txtEmpName.Text;
            string date = txtEmpDOB.Text;
            string phone = txtEmpPhone.Text;
            Boolean admin;

            if (cbRole.Checked)
            {
                admin = true;
            }
            else
            {
                admin = false;
            }

            try
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(phone))
                {
                    MessageBox.Show("Don't leave the blank box!");
                }
                else
                {
                    dao.addEmp(id, pass, name, date, phone, admin);
                    LoadEmp();
                    MessageBox.Show("Create successfully!");
                }
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains("duplicate"))
                {
                    MessageBox.Show("EmployeeID is existed!");
                }
            }
        }

        private void btnClearEmp_Click(object sender, EventArgs e)
        {
            txtEmpID.Text = "";
            txtEmpID.ReadOnly = false;
            txtPass.Text = "";
            txtEmpName.Text = "";
            txtEmpDOB.Text = "";
            txtEmpPhone.Text = "";
        }

        private void FormEmployeeManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginForm lgf = new LoginForm();
            lgf.Show();
        }
    }
}
