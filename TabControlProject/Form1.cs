using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabControlProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tCStudents.SelectedTab = tCStudents.TabPages[1];
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            tCStudents.SelectedTab = tCStudents.TabPages[2];
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                e.Cancel = true;
                txtName.Focus();
                errorProvider1.SetError(txtName, "Please Enter Student Name First");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtName, string.Empty);
            }
        }

        private void txtAge_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                e.Cancel = true;
                txtAge.Focus();
                errorProvider1.SetError(txtAge, "Please Enter Student Age");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAge, string.Empty);
            }

        }

        private void btnAddStudentInfo_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem(txtName.Text);

            if (rbMale.Checked)
                item.ImageIndex = 5;
            else
                item.ImageIndex = 7;    

            item.SubItems.Add(txtAge.Text); 
            item.SubItems.Add(cBMajors.Text);
            LVStudents.Items.Add(item);

            MessageBox.Show("New Student Added Successfully");
        }
    }
}
