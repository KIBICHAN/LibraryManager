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
    public partial class EditProfileForm : Form
    {
        Employee employee = new Employee();
        public EditProfileForm(Employee emp)
        {
            InitializeComponent();
            employee = emp;
            txtEmployeeName.Text = employee.EmployeeName;
            txtDateOfBirth.Text = employee.DateOfBirth.ToString("yyyy/MM/dd");
            txtPhone.Text = employee.Phone;
        }

        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            if (txtEmployeeName.Text.Length == 0 || txtDateOfBirth.Text.Length == 0 || txtPhone.Text.Length == 0)
            {
                MessageBox.Show("All input must not blank!!");
                return;
            }
            DateTime dt;
            bool check = DateTime.TryParseExact(txtDateOfBirth.Text, "yyyy/MM/dd", null,
                     System.Globalization.DateTimeStyles.AllowLeadingWhite,
                     out dt);
            if (!check)
            {
                MessageBox.Show("Pls input follow format year/Month/day");
                return;
            }
            try
            {
                int test = int.Parse(txtPhone.Text);
            }catch(Exception ex)
            {
                MessageBox.Show("The phone must be a number");
                return;
            }
            string name = txtEmployeeName.Text;
            string phone = txtPhone.Text;

            string DOB = txtDateOfBirth.Text;
           EmployeeDAO dao = new EmployeeDAO();
            dao.updateEmployee(employee.EmployeeID, name, DOB, phone);
            MessageBox.Show("Edit Success!!");
            employee.EmployeeName = name;
            employee.Phone = phone;
            employee.DateOfBirth = DateTime.Parse(DOB);
            LibraryForm frm = new LibraryForm(employee);
            this.Visible = false;
            frm.ShowDialog();
        }
    }
}
