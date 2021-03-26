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
    public partial class DetailRecordForm : Form
    {
        public DetailRecordForm(string recordID)
        {
            OrderDAO dao = new OrderDAO();
            InitializeComponent();
            List<RecordDetail> listRecordDetail = dao.getListDetailByRecordID(recordID);
            DataTable table = new DataTable();
            table.Columns.Add("RecordDetailID", typeof(string));
            table.Columns.Add("RecordID", typeof(string));
            table.Columns.Add("BookID", typeof(string));
            table.Columns.Add("Quantity", typeof(int));
            foreach (RecordDetail recordDetail in listRecordDetail)
            {
                
                table.Rows.Add(recordDetail.RecordDetailID, recordDetail.RecordID, recordDetail.BookID, recordDetail.Quantity);
            }

            dgvListRecordDetail.DataSource = table;
            dgvListRecordDetail.AllowUserToAddRows = false;
            dgvListRecordDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
