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
    public partial class ChangePasswordForm : Form
    {
        Employee employee = new Employee();
        public ChangePasswordForm(Employee emp)
        {
            InitializeComponent();
            employee = emp;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtNewPass.Text.Length == 0 || txtConfirmPass.Text.Length == 0)
            {
                MessageBox.Show("Must be not blank");
            }
            EmployeeDAO dao = new EmployeeDAO();
            string newPass = txtNewPass.Text;
            string confirm = txtConfirmPass.Text;
            if (confirm.Equals(newPass))
            {
                dao.updatePassword(employee.EmployeeID, newPass);
                MessageBox.Show("change Password Success!!");
            } else
            {
                MessageBox.Show("The Confirm password is not the same New Password");
            }
        }
    }
}
