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
    public partial class LoginForm : Form
    {
        private EmployeeDAO dao = new EmployeeDAO();
        public LoginForm()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtUsername.Text;
            String password = txtPassword.Text;
            Employee emp = dao.checkLogin(username, password);
            if(emp != null)
            {
                if (emp.Role.Equals("Admin"))
                { 
                    FormEmployeeManagement frm = new FormEmployeeManagement(emp);
                    
                    this.Visible = false;
                    frm.ShowDialog();
                    this.Visible = true;
                   
                    
                    
                }
                else 
                {
                    LibraryForm frm = new LibraryForm(emp);
                    this.Visible = false;
                    frm.ShowDialog();
                    this.Visible = true;
                }
            } else
            {
                MessageBox.Show("Your Username or Password is not correct!!");
            }

        }
    }
}
