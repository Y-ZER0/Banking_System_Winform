using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bank_System_WinForm_Application.Properties;
using BankBusinessLogicLayer;
using System.Net.Sockets;


namespace Bank_System_WinForm_Application
{
    public partial class frmLogin : Form
    {
        byte countFailure = 3;
        enum enpB { ShowEye , HideEye }
        enpB Eye = enpB.HideEye;
        static public string TextBoxValue { get; set; }

        private enum enTabs
        {
            ManageClientsTab= 1, ManageUsersTab = 2,
            ClientsTransactionsTab = 4, CurrencyExchangeTab = 8
        };

        public frmLogin()
        {
            InitializeComponent();
        }


        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToLongDateString() + "\n" +
                DateTime.Now.ToLocalTime();
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text)
                || (txtUserName.Text == "Enter your username" && txtPassword.Text == "Enter your password"))
                MessageBox.Show("Username and password are required !", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                if (clsUser.IsUserExist(txtUserName.Text, txtPassword.Text))
                {
                    TextBoxValue = txtUserName.Text;

                    if (MessageBox.Show("Login Successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        // after each successfull login >> add a new log date
                        clsUserLoginRegistration.AddNewLoginRegistration(txtUserName.Text);

                        // giving access for a certain screens to each user

                        clsUser User = clsUser.FindUser(txtUserName.Text);

                        frmMainPage frmMainPage = new frmMainPage();

                        if (User.Permissions != -1) // if false >> user has access to all screens
                        {
                            foreach (enTabs screen in Enum.GetValues(typeof(enTabs)))
                            {
                                if ((User.Permissions & (int)screen) != 0) // Bitwise AND to check permission

                                    // User has access to this tab, keep it visible
                                    SetTabVisible(screen, true, frmMainPage);

                                else

                                    // User does not have access to this tab, hide it
                                    SetTabVisible(screen, false, frmMainPage);
                            }
                        }

                        // call method to enter another Form (Enter Bank Main Menu) 
                        this.Hide();

                        frmMainPage.ShowDialog();
                    }
                }

                else
                {
                    if (countFailure == 1)
                    {
                        MessageBox.Show("You Are Locked after 3 failed trials \n" +
                            "Please Contact the System Adminstrators to Retrieve your account", "Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // end applications
                        Application.Exit();
                    }

                    MessageBox.Show("Invalid User Name or Password!!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show($"You have {--countFailure} attempts before locking your account ", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtUserName.ForeColor = Color.Gray;
                    txtUserName.Text = "Enter your username";
                    txtPassword.UseSystemPasswordChar = false;
                    txtPassword.ForeColor = Color.Gray;
                    txtPassword.Text = "Enter your password";

                }
            }
        }

        private void SetTabVisible(enTabs screen, bool isVisible , frmMainPage frm)
        {
            switch (screen)
            {
                case enTabs.ManageClientsTab:
                    frm.btnManageClients.Enabled = isVisible;
                    break;

                case enTabs.ClientsTransactionsTab:
                    frm.btnClientsTrans.Enabled = isVisible;
                    break;

                case enTabs.ManageUsersTab:
                    frm.btnManageUsers.Enabled = isVisible;
                    break;

                case enTabs.CurrencyExchangeTab:
                    frm.btnCurrencyEx.Enabled = isVisible;
                    break;
            }
        }

        private void llLinkedIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llLinkedIn.LinkVisited = true;
            System.Diagnostics.Process.Start("www.linkedin.com/in/yousef-abu-nimreh-04781232b");
        }


        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "Enter your username")
            {
                txtUserName.Clear();
                txtUserName.ForeColor = Color.Black;
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                txtUserName.ForeColor = Color.Gray;
                txtUserName.Text = "Enter your username";
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Enter your password")
            {
                txtPassword.Clear();
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }

        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.ForeColor = Color.Gray;
                txtPassword.Text = "Enter your password";
            }
        }


        private void pBShwPass_Click(object sender, EventArgs e)
        {
            if (Eye == enpB.ShowEye)
            {
                Eye = enpB.HideEye;
                pBShwPass.Image = Resources.eye_slash_visible_hide_hidden_show_icon_145987;

                if (txtPassword.Text != "Enter your password")
                    txtPassword.UseSystemPasswordChar = true;
            }
            else
            {
                Eye = enpB.ShowEye;
                pBShwPass.Image = Resources.eye_visible_hide_hidden_show_icon_145988;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }


        private void pBExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
