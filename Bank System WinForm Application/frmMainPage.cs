using BankBusinessLogicLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Bank_System_WinForm_Application
{
    public partial class frmMainPage : Form
    {
        private bool SideBarExpand;
        private DataTable ClientsDataTable, ClientsBalancesDataTable, CurrenciesDataTable, UsersDataTable , LoginRegistersDatatable;
        private int NumOfClients , NumOfBalances , NumOfCurrencies , NumOfUsers;
        
        private enum enPermissions { ManageClients = 1, ManageUsers = 2, 
            ClientsTransactions = 4, CurrencyExchange = 8 };

        public frmMainPage()
        {
            InitializeComponent();
        }

        private void frmMainPage_Load(object sender, EventArgs e)
        {
            ManageClientsTab.Visible = false;
            ClientsTransactionTab.Visible = false;
            ManageUsersTab.Visible = false;
            CurrencyExchangeTab.Visible = false;

            lblDateTime.Text = DateTime.Now.ToLongDateString() + "\n" +
                DateTime.Now.ToLocalTime();

            lblWelcomingMsg.Text = $"Welcome \n{frmLogin.TextBoxValue}";

            lblTotalBalancesMaintxt.Text = $"Total Balances is: (${clsClient.GetTotalBalances()})";

            // Load Data to all ListViews
            LoadClientsToListView();
            LoadTotalBalancesToListView();
            LoadAllCurrenciesToListView();
            LoadAllUsersToListView();
            LoadLogRegsDate();

            UpdateNumOfClientstxt();
            LoadItemsToCB(); // loading items (main items) from list view to combo box
        }

        private void LoadItemsToCB()
        {
            foreach (ListViewItem item in LVClientsListScreen.Items)
            {
                cbAccNum.Items.Add(item.Text);
                cbAccNumDep.Items.Add(item.Text);
                cbAccNumWith.Items.Add(item.Text);
                cbAccNumFrom.Items.Add(item.Text);
                cbAccNumTo.Items.Add(item.Text);
            }

            foreach (ListViewItem item in LVShowAllCurrenciesScreen.Items)
            {
                cbCurrCodes.Items.Add(item.Text);
                cbSrcCurr.Items.Add(item.Text);
                cbDestCurr.Items.Add(item.Text);
            }

            cbSrcCurr.SelectedItem = cbDestCurr.SelectedItem = "USD";

            foreach (ListViewItem item in LVUsersScreen.Items)
                cbUserNames.Items.Add(item.Text);
        }

        // ListView Screens
        private void UpdateNumOfClientstxt()
        {
            lblNumOfClients.Text = NumOfClients.ToString() + " Client (s) Found";
            lblNumOfBalances.Text = NumOfBalances.ToString() + " Client (s) Found";
            lblNumOfCurrencies.Text = NumOfCurrencies.ToString() + " Currency (s) Found";
            lblUsersFound.Text = NumOfUsers.ToString() + " User (s) Found";
        }
        private void LoadTotalBalancesToListView()
        {
            NumOfBalances = 0;
            LVTotalBalancesScreen.Items.Clear();

            ClientsBalancesDataTable = clsClient.GetAllClientsBalances();

            foreach (DataRow row in ClientsBalancesDataTable.Rows)
            {
                ListViewItem ClientItem = new ListViewItem(row["AccNumber"].ToString());
                NumOfBalances++;

                ClientItem.SubItems.Add(row["FirstName"].ToString());
                ClientItem.SubItems.Add(row["Balance"].ToString());

                LVTotalBalancesScreen.Items.Add(ClientItem);
            }
        }
        private void LoadClientsToListView()
        {
            NumOfClients = 0;
            LVClientsListScreen.Items.Clear();

            ClientsDataTable = clsClient.GetAllClients();

            foreach (DataRow row in ClientsDataTable.Rows)
            {
                ListViewItem ClientItem = new ListViewItem(row["AccNumber"].ToString());
                NumOfClients++;

                ClientItem.SubItems.Add(row["PinCode"].ToString());
                ClientItem.SubItems.Add(row["FirstName"].ToString());
                ClientItem.SubItems.Add(row["LastName"].ToString());
                ClientItem.SubItems.Add(row["Email"].ToString());
                ClientItem.SubItems.Add(row["Phone"].ToString());
                ClientItem.SubItems.Add(row["Balance"].ToString());

                LVClientsListScreen.Items.Add(ClientItem);
            }
        }
        private void txtSearchBal_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchBal.Text))
            {
                lblNumOfBalances.Text = $"{NumOfBalances} Client (s) Found";
                Re_loadClientsBalancesToListView();
            }

            else if (int.TryParse(txtSearchBal.Text, out int AccNum))
            {
                if (clsClient.IsClientExist(AccNum))
                {
                    lblNumOfBalances.Text = "1 Client (s) Found";
                    LVTotalBalancesScreen.Items.Clear();

                    // will return always one record / row to the datatable
                    DataRow ClientRecord = clsClient.GetClientByAccNum(int.Parse(txtSearchBal.Text));

                    ListViewItem ClientItem = new ListViewItem(ClientRecord["AccNumber"].ToString());

                    ClientItem.SubItems.Add(ClientRecord["FirstName"].ToString());
                    ClientItem.SubItems.Add(ClientRecord["Balance"].ToString());

                    LVTotalBalancesScreen.Items.Add(ClientItem);
                }
            }

            else
            {
                lblNumOfBalances.Text = "0 Client (s) Found";
                LVTotalBalancesScreen.Items.Clear();
            }

        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                lblNumOfClients.Text = $"{NumOfClients} Client (s) Found";
                Re_loadClientsToListView();
            }

            else if (int.TryParse(txtSearch.Text, out int AccNum))
            {
                if (clsClient.IsClientExist(AccNum))
                {
                    lblNumOfClients.Text = "1 Client (s) Found";
                    LVClientsListScreen.Items.Clear();

                    // will return always one record / row to the datatable
                    DataRow ClientRecord = clsClient.GetClientByAccNum(int.Parse(txtSearch.Text));

                    ListViewItem ClientItem = new ListViewItem(ClientRecord["AccNumber"].ToString());

                    ClientItem.SubItems.Add(ClientRecord["PinCode"].ToString());
                    ClientItem.SubItems.Add(ClientRecord["FirstName"].ToString());
                    ClientItem.SubItems.Add(ClientRecord["LastName"].ToString());
                    ClientItem.SubItems.Add(ClientRecord["Email"].ToString());
                    ClientItem.SubItems.Add(ClientRecord["Phone"].ToString());
                    ClientItem.SubItems.Add(ClientRecord["Balance"].ToString());

                    LVClientsListScreen.Items.Add(ClientItem);
                }
            }

            else
            {
                lblNumOfClients.Text = "0 Client (s) Found";
                LVClientsListScreen.Items.Clear();
            }
        }

        private void ResetProgressProfile()
        {
            while (progressBar.Value > 0)
            {
                Thread.Sleep(100);
                progressBar.Value -= 10;
                lblProfilePercentage.Text = (float)progressBar.Value + "%";
                progressBar.Refresh();
                lblProfilePercentage.Refresh();
            }
        }
        private void ResetTxtBoxes()
        {
            txtAccNum.Clear();
            txtFirstName.Clear();
            txtPinCode.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
        }
        private void AddNewClientToDataSource()
        {
            // NewClient is in Add New Mode
            clsClient NewClient = new clsClient();

            NewClient.AccNumber = int.Parse(txtAccNum.Text);
            NewClient.FirstName = txtFirstName.Text;
            NewClient.LastName = txtLastName.Text;
            NewClient.PinCode = txtPinCode.Text;
            NewClient.Phone = txtPhone.Text;
            NewClient.Email = txtEmail.Text;
            NewClient.Balance = NUPBalance.Value;

            NewClient.Save();
        }
        private void AddNewClientToListView()
        {
            // add new item (client) to the viewlist ++ add new client to the database
            ListViewItem ClientItem = new ListViewItem(txtAccNum.Text);

            ClientItem.SubItems.Add(txtPinCode.Text);
            ClientItem.SubItems.Add(txtFirstName.Text);
            ClientItem.SubItems.Add(txtLastName.Text);
            ClientItem.SubItems.Add(txtEmail.Text);
            ClientItem.SubItems.Add(txtPhone.Text);
            ClientItem.SubItems.Add(NUPBalance.Value.ToString());

            LVClientsListScreen.Items.Add(ClientItem);

            AddNewClientToDataSource();
        }
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to Add a new client ( {txtFirstName.Text} ) ?"
                , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                AddNewClientToListView();

                MessageBox.Show($"Client {txtAccNum.Text} Added Successfully"
                , "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                NumOfClients++;

                ResetTxtBoxes();
                ResetProgressProfile();
                UpdateNumOfClientstxt();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            // delete all clients from the list view && from the database
            LVClientsListScreen.Items.Clear();
            ClientsDataTable.Clear();
            clsClient.DeleteAllClients();
            lblNumOfClients.Text = 0 + " Client (s) Found";
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();
                eP.SetError(txtFirstName, "First Name is Required");
            }
            else
            {
                e.Cancel = false;
                eP.SetError(txtFirstName, string.Empty);
                UpgradeProgressBar.Start();
            }
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                e.Cancel = true;
                txtEmail.Focus();
                eP.SetError(txtEmail, "Email is Required");
            }

            else if (!txtEmail.Text.Contains("@gmail.com") &&
                !txtEmail.Text.Contains("@hotmail.com") &&
                !txtEmail.Text.Contains("@hotmail.com") &&
                !txtEmail.Text.Contains("@yahoo.com") &&
                !txtEmail.Text.Contains("@outlook.com"))
            {
                e.Cancel = true;
                txtEmail.Focus();
                eP.SetError(txtEmail, "Emails Must Contains @ .com gmail, yahoo, hotmail, outlook etc!");
            }

            else
            {
                e.Cancel = false;
                eP.SetError(txtEmail, string.Empty);
                UpgradeProgressBar.Start();
            }

        }
        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                txtLastName.Focus();
                eP.SetError(txtLastName, "Last Name is Required");
            }
            else
            {
                e.Cancel = false;
                eP.SetError(txtLastName, string.Empty);
                UpgradeProgressBar.Start();
            }

        }
        private void txtPinCode_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPinCode.Text))
            {
                e.Cancel = true;
                txtPinCode.Focus();
                eP.SetError(txtPinCode, "Pin Code is Required");
            }
            else
            {
                e.Cancel = false;
                eP.SetError(txtPinCode, string.Empty);
                UpgradeProgressBar.Start();
            }
        }
        private void txtAccNum_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAccNum.Text))
            {
                e.Cancel = true;
                txtAccNum.Focus();
                eP.SetError(txtAccNum, "Account Number is required");
            }

            else if (!int.TryParse(txtAccNum.Text, out int AccNum))
            {
                e.Cancel = true;
                txtAccNum.Focus();
                eP.SetError(txtAccNum, "Invalid Input , please enter a valid number");
            }

            else if (clsClient.IsClientExist(AccNum))
            {
                e.Cancel = true;
                txtAccNum.Focus();
                eP.SetError(txtAccNum, $"Account Number ({AccNum}) is already in used , Choose Another one.");
            }

            else
            {
                e.Cancel = false;
                eP.SetError(txtAccNum, string.Empty);
                UpgradeProgressBar.Start();
            }
        }
        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                e.Cancel = true;
                txtPhone.Focus();
                eP.SetError(txtPhone, "Phone is Required");
            }

            else if (!txtPhone.Text.Any(char.IsDigit))
            {
                e.Cancel = true;
                txtPhone.Focus();
                eP.SetError(txtPhone, "Invalid Phone , Only Numbers are Accepted");
            }

            else
            {
                e.Cancel = false;
                eP.SetError(txtPhone, string.Empty);
                UpgradeProgressBar.Start();
            }
        }
        private void txtAccNum_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAccNum.Text) &&
                !string.IsNullOrWhiteSpace(txtFirstName.Text) &&
                !string.IsNullOrWhiteSpace(txtLastName.Text) &&
                !string.IsNullOrWhiteSpace(txtPinCode.Text) &&
                !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !string.IsNullOrWhiteSpace(txtPhone.Text))
                btnAddClient.Enabled = true;
        }


        // Sort Clients on ListView
        private void rbDesc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDesc.Checked)
            {
                ClientsDataTable.DefaultView.Sort = "AccNumber DESC";
                Re_loadClientsToListView();
            }
        }
        private void rbAsc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAsc.Checked)
            {
                ClientsDataTable.DefaultView.Sort = "AccNumber ASC";
                Re_loadClientsToListView();
            }
        }
        private void Re_loadClientsToListView()
        {
            LVClientsListScreen.Items.Clear();

            // Sorted DataTable taken as parameter >> Iterate through the DefaultView, which is the sorted view of the DataTable
            foreach (DataRowView row in ClientsDataTable.DefaultView)
            {
                ListViewItem ClientItem = new ListViewItem(row["AccNumber"].ToString());

                ClientItem.SubItems.Add(row["PinCode"].ToString());
                ClientItem.SubItems.Add(row["FirstName"].ToString());
                ClientItem.SubItems.Add(row["LastName"].ToString());
                ClientItem.SubItems.Add(row["Email"].ToString());
                ClientItem.SubItems.Add(row["Phone"].ToString());
                ClientItem.SubItems.Add(row["Balance"].ToString());

                LVClientsListScreen.Items.Add(ClientItem);

                // The DefaultView contains the sorted data,
                // while the original Rows collection in the DataTable remains unsorted unless you explicitly sort it in the DataTable itself.
            }

        }

        // Sort Clients Balances on ListView
        private void rbDescTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDescTotal.Checked)
            {
                ClientsBalancesDataTable.DefaultView.Sort = "AccNumber DESC";
                Re_loadClientsBalancesToListView();
            }
        }
        private void rbAscTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAscTotal.Checked)
            {
                ClientsBalancesDataTable.DefaultView.Sort = "AccNumber ASC";
                Re_loadClientsBalancesToListView();
            }

        }
        private void Re_loadClientsBalancesToListView()
        {
            LVTotalBalancesScreen.Items.Clear();

            // Sorted DataTable taken as parameter >> Iterate through the DefaultView, which is the sorted view of the DataTable
            foreach (DataRowView row in ClientsBalancesDataTable.DefaultView)
            {
                ListViewItem ClientItem = new ListViewItem(row["AccNumber"].ToString());

                ClientItem.SubItems.Add(row["FirstName"].ToString());
                ClientItem.SubItems.Add(row["Balance"].ToString());

                LVTotalBalancesScreen.Items.Add(ClientItem);

                // The DefaultView contains the sorted data,
                // while the original Rows collection in the DataTable remains unsorted unless you explicitly sort it in the DataTable itself.
            }

        }

        // Context Menu Strips
        private void TSMIDelete_Click(object sender, EventArgs e)
        {
            if (LVClientsListScreen.SelectedItems.Count > 0)
            {
                ListViewItem item = LVClientsListScreen.SelectedItems[0];

                if (MessageBox.Show($"Are you sure to Delete Acc. ({item.Text})",
                    "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    MessageBox.Show($"Client ({item.Text}) Deleted Successfully",
                       "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LVClientsListScreen.Items.Remove(item);
                    clsClient.DeleteClient(int.Parse(item.Text));
                    NumOfClients--;
                    UpdateNumOfClientstxt();

                    // Add Multi-Select Option >> deleting multiple clients at once
                }

            }
        }
        private void TSMIDeposit_Click(object sender, EventArgs e)
        {
            ManageClientsTab.Visible = false;
            ClientsTransactionTab.Visible = true;

            ClientsTransactionTab.SelectedTab = ClientsTransactionTab.TabPages["DepositPage"];
        }
        private void TSMIWithdraw_Click(object sender, EventArgs e)
        {
            ManageClientsTab.Visible = false;
            ClientsTransactionTab.Visible = true;

            ClientsTransactionTab.SelectedTab = ClientsTransactionTab.TabPages["WithdrawPage"];
        }
        private void TSMITransfer_Click(object sender, EventArgs e)
        {
            ManageClientsTab.Visible = false;
            ClientsTransactionTab.Visible = true;

            ClientsTransactionTab.SelectedTab = ClientsTransactionTab.TabPages["TransferBalancesPage"];
        }


        private void SlideBarAnimation_Tick(object sender, EventArgs e)
        {
            if (SideBarExpand)
            {
                SlidingSidebarFlowPanel.Width += 10;

                if (SlidingSidebarFlowPanel.Width == SlidingSidebarFlowPanel.MaximumSize.Width)
                {
                    SideBarExpand = false;
                    SlideBarAnimation.Stop();
                }
            }

            else
            {
                SlidingSidebarFlowPanel.Width -= 10;

                if (SlidingSidebarFlowPanel.Width == SlidingSidebarFlowPanel.MinimumSize.Width)
                {
                    SideBarExpand = true;
                    SlideBarAnimation.Stop();
                }
            }
        }

        private void pBMenu_Click(object sender, EventArgs e)
        {
            SlideBarAnimation.Start();
        }

        private void pBSignOut_Click(object sender, EventArgs e)
        {
            this.Hide();

            btnManageClients.Enabled = true;
            btnClientsTrans.Enabled = true;
            btnManageUsers.Enabled = true;
            btnCurrencyEx.Enabled = true;

            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
        }

        private void llLinkedIn_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llLinkedIn.LinkVisited = true;
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/yousef-abu-nimreh-04781232b/");
        }


        private void cbAccNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccNum.SelectedIndex != -1)
            {
                // unlock button
                btnUpdateClient.Enabled = true;

                // assign field values of client to all txt boxes
                clsClient Client = clsClient.FindClientByAccNumber(int.Parse(cbAccNum.SelectedItem.ToString()));

                txtFirstUp.Text = Client.FirstName;
                txtLastUp.Text = Client.LastName;
                txtPinUp.Text = Client.PinCode;
                txtEmailUp.Text = Client.Email;
                txtPhoneUp.Text = Client.Phone;
                NUPBalanceUp.Value = Client.Balance;
            }
        }

        // Update Client
        private void btnUpdateClient_Click(object sender, EventArgs e)
        {
            if (int.TryParse(cbAccNum.SelectedItem.ToString(), out int AccNum))
            {
                clsClient Client = clsClient.FindClientByAccNumber(AccNum);

                if (MessageBox.Show($"Are you sure to Update Acc. ({AccNum})",
                    "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    MessageBox.Show($"Client ({AccNum}) Updated Successfully",
                       "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Client.FirstName = txtFirstUp.Text;
                    Client.LastName = txtLastUp.Text;
                    Client.PinCode = txtPinUp.Text;
                    Client.Email = txtEmailUp.Text;
                    Client.Phone = txtPhoneUp.Text;
                    Client.Balance = NUPBalanceUp.Value;

                    Client.Save();
                    LoadClientsToListView();

                    cbAccNum.SelectedIndex = -1;

                    txtFirstUp.Text = txtLastUp.Text = txtEmailUp.Text = txtPhoneUp.Text = txtPinUp.Text
                        = string.Empty;

                    NUPBalanceUp.Value = 0;
                    btnUpdateClient.Enabled = false;

                    // Add Multi-Select Option >> deleting multiple clients at once
                }

            }

            else
                MessageBox.Show("Bug Occured while Updating Client\nPlease Enter valid Entries",
                    "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

        }


        // Menu Tabs
        private void ShowTabControl(TabControl CurrentTab)
        {
            ManageClientsTab.Visible = false;
            ClientsTransactionTab.Visible = false;
            ManageUsersTab.Visible = false;
            CurrencyExchangeTab.Visible = false;

            CurrentTab.Visible = true;
        }
        private void btnManageClients_Click(object sender, EventArgs e)
        {
            ShowTabControl(ManageClientsTab);
        }
        private void btnClientsTrans_Click(object sender, EventArgs e)
        {
            ShowTabControl(ClientsTransactionTab);
        }
        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            ShowTabControl(ManageUsersTab);
        }
        private void btnCurrencyEx_Click(object sender, EventArgs e)
        {
            ShowTabControl(CurrencyExchangeTab);
        }



        private void UpgradeProgressBar_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value == 85)
                progressBar.Value += 15;
            else
                progressBar.Value += 17;

            lblProfilePercentage.Text = (float)progressBar.Value + "%";

            progressBar.Refresh();
            lblProfilePercentage.Refresh();

            UpgradeProgressBar.Stop();
        }


        // All Transfer Buttons
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to perform this transaction ?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (int.TryParse(cbAccNumDep.SelectedItem.ToString(), out int AccNum))
                {
                    clsClient Client = clsClient.FindClientByAccNumber(AccNum);

                    if (decimal.TryParse(NUDdeposit.Value.ToString(), out decimal Amount))
                    {
                        Client.Balance += Amount;
                        Client.Save();

                        MessageBox.Show($"Amount has been Deposited (${lblClientBalanceDP.Text})", "Alarm",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblCurrBalDep.Visible = lblClientBalanceDP.Visible = false;

                        LoadClientsToListView();
                        LoadTotalBalancesToListView();

                        cbAccNumDep.SelectedIndex = -1;
                        NUDdeposit.Value = 0;
                    }

                }
            }
        }
        private void btnWithDraw_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to perform this transaction ?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (int.TryParse(cbAccNumWith.SelectedItem.ToString(), out int AccNum))
                {
                    clsClient Client = clsClient.FindClientByAccNumber(AccNum);

                    if (decimal.TryParse(NUDwithdraw.Value.ToString(), out decimal Amount))
                    {
                        if (Client.Balance < Amount)
                            MessageBox.Show($"Error: Insufficient balance. You cannot withdraw more than your current balance.",
                                 "Withdrawal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        else
                        {
                            Client.Balance -= Amount;
                            Client.Save();

                            MessageBox.Show($"Amount has been Withdrawed (${lblClientBalanceWD.Text})", "Alarm",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            lblCurrBalWith.Visible = lblClientBalanceWD.Visible = false;

                            LoadClientsToListView();
                            LoadTotalBalancesToListView();

                            cbAccNumWith.SelectedIndex = -1;
                            NUDwithdraw.Value = 0;
                        }
                    }

                }
            }

        }
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to perform this transaction ?", "Confirm",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (int.TryParse(cbAccNumFrom.SelectedItem.ToString(), out int AccNumSrc)
                    && int.TryParse(cbAccNumTo.SelectedItem.ToString(), out int AccNumDest))
                {
                    clsClient ClientSrc = clsClient.FindClientByAccNumber(AccNumSrc);
                    clsClient ClientDest = clsClient.FindClientByAccNumber(AccNumDest);

                    if (decimal.TryParse(NUDTransfer.Text, out decimal Amount))
                    {
                        if (ClientSrc.Balance < Amount)
                            MessageBox.Show($"Error: Insufficient balance. You cannot withdraw more than your current balance.",
                                 "Withdrawal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        else
                        {
                            ClientSrc.Balance -= Amount;
                            ClientDest.Balance += Amount;

                            ClientSrc.Save(); ClientDest.Save();

                            MessageBox.Show($"Transfer Done Successfully", "Alarm",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadClientsToListView();
                            LoadTotalBalancesToListView();

                            lblCurrBalFrom.Visible = lblClientBalanceFrom.Visible =
                            lblCurrBalTo.Visible = lblClientBalanceTo.Visible = false;

                            cbAccNumFrom.SelectedIndex = cbAccNumTo.SelectedIndex = -1;
                            NUDTransfer.Value = 0;
                        }
                    }

                }
            }

        }


        // Transfer selectindex combo boxes
        private void cbAccNumDe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccNumDep.SelectedIndex != -1 && LVTotalBalancesScreen.Items.Count > 0)
            {
                btnDeposit.Enabled = true;

                ListViewItem item = LVTotalBalancesScreen.FindItemWithText(cbAccNumDep.Text);

                lblClientBalanceDP.Text = item.SubItems[2].Text;

                lblCurrBalDep.Visible = lblClientBalanceDP.Visible = true;
            }
        }
        private void cbAccNumWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccNumWith.SelectedIndex != -1 && LVTotalBalancesScreen.Items.Count > 0)
            {
                btnWithDraw.Enabled = true;

                ListViewItem item = LVTotalBalancesScreen.FindItemWithText(cbAccNumWith.Text);

                lblClientBalanceWD.Text = item.SubItems[2].Text;

                lblCurrBalWith.Visible = lblClientBalanceWD.Visible = true;
            }
        }
        private void cbAccNumFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccNumFrom.SelectedIndex != -1 && LVTotalBalancesScreen.Items.Count > 0)
            {
                ListViewItem item = LVTotalBalancesScreen.FindItemWithText(cbAccNumFrom.Text);

                lblClientBalanceFrom.Text = item.SubItems[2].Text;

                lblCurrBalFrom.Visible = lblClientBalanceFrom.Visible = true;
            }

            if (cbAccNumFrom.SelectedIndex != -1 && cbAccNumTo.SelectedIndex != -1)
                btnTransfer.Enabled = true;

        }
        private void cbAccNumTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAccNumTo.SelectedIndex != -1 && LVTotalBalancesScreen.Items.Count > 0)
            {
                ListViewItem item = LVTotalBalancesScreen.FindItemWithText(cbAccNumTo.Text);

                lblClientBalanceTo.Text = item.SubItems[2].Text;

                lblCurrBalTo.Visible = lblClientBalanceTo.Visible = true;
            }

            if (cbAccNumFrom.SelectedIndex != -1 && cbAccNumTo.SelectedIndex != -1)
                btnTransfer.Enabled = true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Currencies Exchange Screen

        // Show Currencies Tab
        private void LoadAllCurrenciesToListView()
        {
            NumOfCurrencies = 0;
            LVShowAllCurrenciesScreen.Items.Clear();

            CurrenciesDataTable = clsCurrency.GetAllCurrencies();

            foreach (DataRow row in CurrenciesDataTable.Rows)
            {
                ListViewItem CurrencyItem = new ListViewItem(row["CurrencyCode"].ToString());
                NumOfCurrencies++;

                CurrencyItem.SubItems.Add(row["Country"].ToString());
                CurrencyItem.SubItems.Add(row["CurrencyName"].ToString());
                CurrencyItem.SubItems.Add(row["Rate"].ToString());

                LVShowAllCurrenciesScreen.Items.Add(CurrencyItem);
            }
        }

        private void rbAscCurr_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAscCurr.Checked)
            {
                CurrenciesDataTable.DefaultView.Sort = "CurrencyCode ASC";
                Re_LoadCurrenciesToListView();
            }
        }

        private void rbDescCurr_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDescCurr.Checked)
            {
                CurrenciesDataTable.DefaultView.Sort = "CurrencyCode DESC";
                Re_LoadCurrenciesToListView();
            }
        }

        private void txtSearchCode_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchCode.Text))
            {
                lblNumOfCurrencies.Text = $"{NumOfCurrencies} Currency (s) Found";
                Re_LoadCurrenciesToListView();
            }

            else if (clsCurrency.IsCurrencyExist(txtSearchCode.Text.ToString()))
            {
                lblNumOfCurrencies.Text = "1 Currency (s) Found";
                LVShowAllCurrenciesScreen.Items.Clear();

                // will return always one record / row to the datatable
                DataRow CurrencyRecord = clsCurrency.GetCurrencyByCode(txtSearchCode.Text.ToString());

                ListViewItem CurrencyItem = new ListViewItem(CurrencyRecord["CurrencyCode"].ToString());

                CurrencyItem.SubItems.Add(CurrencyRecord["Country"].ToString());
                CurrencyItem.SubItems.Add(CurrencyRecord["CurrencyName"].ToString());
                CurrencyItem.SubItems.Add(CurrencyRecord["Rate"].ToString());

                LVShowAllCurrenciesScreen.Items.Add(CurrencyItem);
            }

            else
            {
                lblNumOfCurrencies.Text = "0 Currency (s) Found";
                LVShowAllCurrenciesScreen.Items.Clear();
            }

        }

        private void Re_LoadCurrenciesToListView()
        {
            LVShowAllCurrenciesScreen.Items.Clear();

            // Sorted DataTable taken as parameter >> Iterate through the DefaultView, which is the sorted view of the DataTable
            foreach (DataRowView row in CurrenciesDataTable.DefaultView)
            {
                ListViewItem CurrencyItem = new ListViewItem(row["CurrencyCode"].ToString());

                CurrencyItem.SubItems.Add(row["Country"].ToString());
                CurrencyItem.SubItems.Add(row["CurrencyName"].ToString());
                CurrencyItem.SubItems.Add(row["Rate"].ToString());

                LVShowAllCurrenciesScreen.Items.Add(CurrencyItem);

                // The DefaultView contains the sorted data,
                // while the original Rows collection in the DataTable remains unsorted unless you explicitly sort it in the DataTable itself.
            }

        }

        // Update Rate Tab
        private void cbCurrCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsCurrency Currency = clsCurrency.FindCurrency(cbCurrCodes.SelectedItem.ToString());

            lblCountryName.Text = $"Country : {Currency.Country}";
            lblCountryCode.Text = $"Code : {Currency.CurrencyCode}";
            lblCurrName.Text = $"Name : {Currency.CurrencyName}";
            lblCurrRate.Text = $"Rate({Currency.Rate}$)";

            NUDrate.Value = Currency.Rate;

            btnUpdateRate.Enabled = true;
        }

        private void btnUpdateRate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to perform this transaction ?", "Confirm",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                clsCurrency.UpdateRate(cbCurrCodes.SelectedItem.ToString(), NUDrate.Value);

                MessageBox.Show($"Currency {cbCurrCodes.Text} Updated Successfully",
                 "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblCountryName.Text = "Country : United States";
                lblCountryCode.Text = "Code : USD";
                lblCurrName.Text = "Name : US Dollar";
                lblCurrRate.Text = "Rate(1$)";
                cbCurrCodes.Text = "USD";

                NUDrate.Value = 1;
                btnUpdateRate.Enabled = false;
            }
        }

        // Currency Calculaotr Tab
        private void cbSrcCurr_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsCurrency Currency = clsCurrency.FindCurrency(cbSrcCurr.SelectedItem.ToString());

            lblCountryNameSrc.Text = $"Country : {Currency.Country}";
            lblCountryCodeSrc.Text = $"Code : {Currency.CurrencyCode}";
            lblCurrNameSrc.Text = $"Name : {Currency.CurrencyName}";
            lblRateSrc.Text = $"Rate({Currency.Rate}$)";
        }

        private void btnConvertCurr_Click(object sender, EventArgs e)
        {
            decimal SrcRate = 0, DestRate = 0, ConvertAmount = 0;

            clsCurrency CurrencySrc = clsCurrency.FindCurrency(cbSrcCurr.SelectedItem.ToString());
            clsCurrency CurrencyDest = clsCurrency.FindCurrency(cbDestCurr.SelectedItem.ToString());

            SrcRate = Math.Truncate(CurrencySrc.Rate);
            DestRate = Math.Truncate(CurrencyDest.Rate);
            ConvertAmount = Math.Truncate(NUDConvertCurrAmount.Value);

            lblCurrResult.Text = $@"{SrcRate * ConvertAmount} {CurrencySrc.CurrencyCode} = {DestRate * ConvertAmount} {CurrencyDest.CurrencyCode}";

            lblCurrResult.Visible = true;
            NUDConvertCurrAmount.Value = 1;
            ResetCurrencies();
        }

        private void cbDestCurr_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsCurrency Currency = clsCurrency.FindCurrency(cbDestCurr.SelectedItem.ToString());

            lblCountryNameDest.Text = $"Country : {Currency.Country}";
            lblCountryCodeDest.Text = $"Code : {Currency.CurrencyCode}";
            lblCurrNameDest.Text = $"Name : {Currency.CurrencyName}";
            lblRateDest.Text = $"Rate({Currency.Rate}$)";
        }

        private void ResetCurrencies()
        {
            lblCountryNameDest.Text = lblCountryNameSrc.Text = "Country : United States";
            lblCountryCodeDest.Text = lblCountryCodeSrc.Text = "Code : USD";
            lblCurrNameDest.Text = lblCurrNameSrc.Text = "Name : US Dollar";
            lblRateDest.Text = lblRateSrc.Text = "Rate(1$)";

            cbSrcCurr.Text = cbDestCurr.Text = "USD";
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Manage Users

        // Show Users Tab
        private void LoadAllUsersToListView()
        {
            NumOfUsers = 0;
            LVUsersScreen.Items.Clear();

            UsersDataTable = clsUser.GetAllUsers();

            foreach (DataRow row in UsersDataTable.Rows)
            {
                ListViewItem UserItem = new ListViewItem(row["UserName"].ToString());
                NumOfUsers++;

                UserItem.SubItems.Add(row["FirstName"].ToString());
                UserItem.SubItems.Add(row["LastName"].ToString());
                UserItem.SubItems.Add(row["Email"].ToString());
                UserItem.SubItems.Add(row["Phone"].ToString());
                UserItem.SubItems.Add(row["Password"].ToString());
                UserItem.SubItems.Add(row["Permissions"].ToString());

                LVUsersScreen.Items.Add(UserItem);
            }
        }

        private void txtSearchUserName_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchUserName.Text))
            {
                lblUsersFound.Text = $"{NumOfUsers} User (s) Found";
                Re_loadUsersToListView();
            }

            else if (clsUser.IsUserExist(txtSearchUserName.Text))
            {
                lblUsersFound.Text = "1 User (s) Found";
                LVUsersScreen.Items.Clear();

                // will return always one record / row to the datatable
                DataRow UserRecord = clsUser.GetUserByUserName(txtSearchUserName.Text);

                ListViewItem UserItem = new ListViewItem(UserRecord["UserName"].ToString());

                UserItem.SubItems.Add(UserRecord["FirstName"].ToString());
                UserItem.SubItems.Add(UserRecord["LastName"].ToString());
                UserItem.SubItems.Add(UserRecord["Email"].ToString());
                UserItem.SubItems.Add(UserRecord["Phone"].ToString());
                UserItem.SubItems.Add(UserRecord["Password"].ToString());
                UserItem.SubItems.Add(UserRecord["Permissions"].ToString());

                LVUsersScreen.Items.Add(UserItem);
            }

            else
            {
                lblUsersFound.Text = "0 User (s) Found";
                LVUsersScreen.Items.Clear();
            }

        }

        private void rbAscUsers_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAscUsers.Checked)
            {
                UsersDataTable.DefaultView.Sort = "UserName ASC";
                Re_loadUsersToListView();
            }
        }

        private void rbDescUsers_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDescUsers.Checked)
            {
                UsersDataTable.DefaultView.Sort = "UserName DESC";
                Re_loadUsersToListView();
            }
        }

        private void Re_loadUsersToListView()
        {
            LVUsersScreen.Items.Clear();

            // Sorted DataTable taken as parameter >> Iterate through the DefaultView, which is the sorted view of the DataTable
            foreach (DataRowView row in UsersDataTable.DefaultView)
            {
                ListViewItem UserItem = new ListViewItem(row["UserName"].ToString());

                UserItem.SubItems.Add(row["FirstName"].ToString());
                UserItem.SubItems.Add(row["LastName"].ToString());
                UserItem.SubItems.Add(row["Email"].ToString());
                UserItem.SubItems.Add(row["Phone"].ToString());
                UserItem.SubItems.Add(row["Password"].ToString());
                UserItem.SubItems.Add(row["Permissions"].ToString());

                LVUsersScreen.Items.Add(UserItem);

                // The DefaultView contains the sorted data,
                // while the original Rows collection in the DataTable remains unsorted unless you explicitly sort it in the DataTable itself.
            }
        }

        // Add New User Tab
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                txtUserName.Focus();
                eP.SetError(txtUserName, "UserName is required");
            }

            else if (clsUser.IsUserExist(txtUserName.Text))
            {
                e.Cancel = true;
                txtAccNum.Focus();
                eP.SetError(txtUserName, $"UserName ({txtUserName}) is already in used , Choose Another one.");
            }

            else
            {
                e.Cancel = false;
                eP.SetError(txtUserName, string.Empty);
            }

        }
        private void txtUserEM_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserEM.Text))
            {
                e.Cancel = true;
                txtUserEM.Focus();
                eP.SetError(txtUserEM, "Email is Required");
            }

            else if (!txtUserEM.Text.Contains("@gmail.com") &&
                !txtUserEM.Text.Contains("@hotmail.com") &&
                !txtUserEM.Text.Contains("@hotmail.com") &&
                !txtUserEM.Text.Contains("@yahoo.com") &&
                !txtUserEM.Text.Contains("@outlook.com"))
            {
                e.Cancel = true;
                txtUserEM.Focus();
                eP.SetError(txtUserEM, "Emails Must Contains @ .com gmail, yahoo, hotmail, outlook etc!");
            }

            else
            {
                e.Cancel = false;
                eP.SetError(txtUserEM, string.Empty);
            }

        }
        private void txtUserFN_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserFN.Text))
            {
                e.Cancel = true;
                txtUserFN.Focus();
                eP.SetError(txtUserFN, "First Name is Required");
            }
            else
            {
                e.Cancel = false;
                eP.SetError(txtUserFN, "");
            }
        }
        private void txtUserLN_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserLN.Text))
            {
                e.Cancel = true;
                txtUserLN.Focus();
                eP.SetError(txtUserLN, "Last Name is Required");
            }
            else
            {
                e.Cancel = false;
                eP.SetError(txtUserLN, string.Empty);
            }

        }
        private void txtUserPH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserPH.Text))
            {
                e.Cancel = true;
                txtUserPH.Focus();
                eP.SetError(txtUserPH, "Phone is Required");
            }

            else if (!txtUserPH.Text.Any(char.IsDigit))
            {
                e.Cancel = true;
                txtUserPH.Focus();
                eP.SetError(txtUserPH, "Invalid Phone , Only Numbers are Accepted");
            }

            else
            {
                e.Cancel = false;
                eP.SetError(txtUserPH, string.Empty);
            }

        }
        private void txtUserPass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserPass.Text))
            {
                e.Cancel = true;
                txtUserPass.Focus();
                eP.SetError(txtUserPass, "Password is Required");
            }
            else
            {
                e.Cancel = false;
                eP.SetError(txtUserPass, string.Empty);
            }

        }
        private void txtUserEM_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) &&
                !string.IsNullOrWhiteSpace(txtUserFN.Text) &&
                !string.IsNullOrWhiteSpace(txtUserLN.Text) &&
                !string.IsNullOrWhiteSpace(txtUserPass.Text) &&
                !string.IsNullOrWhiteSpace(txtUserEM.Text) &&
                !string.IsNullOrWhiteSpace(txtUserPH.Text))
            {
                gBPermissions.Enabled = true;
                btnAddNewUser.Enabled = true;
            }
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            chxManCli.Checked = chxManUsers.Checked = chxCliTrans.Checked
                = chxCurrEx.Checked = false;

        }
        private void rbYes_CheckedChanged(object sender, EventArgs e)
        {
            chxManCli.Checked = chxManUsers.Checked = chxCliTrans.Checked
                = chxCurrEx.Checked = true;
        }

        // All Users buttons
        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to Add User ( {txtUserName.Text} )?" , 
                "Confirm" , MessageBoxButtons.OKCancel , MessageBoxIcon.Information) == DialogResult.OK)

            {
                NumOfUsers++;

                MessageBox.Show($"User {txtUserName.Text} Added Successfully",
                  "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AddNewUserToListView();
                UpdateNumOfClientstxt();
                ResetUsertxtboxes();
                ResetAddNewUserControls();
            }
        }
        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to Update The User ( {cbUserNames.Text} )?",
              "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)

            {
                MessageBox.Show($"User {cbUserNames.Text} Updated Successfully",
                  "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                clsUser User = clsUser.FindUser(cbUserNames.Text);

                User.Password = txtUserPassUP.Text;
                User.Email = txtUserEMUP.Text;
                User.FirstName = txtUserFNUP.Text;
                User.LastName = txtUserLNUP.Text;
                User.Phone = txtUserPHUP.Text;

                int Permissions = 0;

                if (rbYesUp.Checked)
                    Permissions = -1; // access to all screens

                else
                {
                    if (chxManCliUp.Checked)
                        Permissions += (int)enPermissions.ManageClients;

                    if (chxManUsersUp.Checked)
                        Permissions += (int)enPermissions.ManageUsers;

                    if (chxCliTransUp.Checked)
                        Permissions += (int)enPermissions.ClientsTransactions;

                    if (chxCurrExUp.Checked)
                        Permissions += (int)enPermissions.CurrencyExchange;
                }


                User.Permissions = Permissions;

                User.Save();

                // reload all clients
                LoadAllUsersToListView();

                // reset controls
                ResetUsertxtboxesUP();
                ResetUpdateUserControls();
            }
        }

        // Process of adding a new User / Update
        private void ResetUsertxtboxes()
        {
            txtUserName.Clear();
            txtUserPass.Clear();
            txtUserPH.Clear();
            txtUserEM.Clear();
            txtUserFN.Clear();
            txtUserLN.Clear();
        }
        private void ResetUsertxtboxesUP()
        {
            cbUserNames.SelectedIndex = -1;
            txtUserPassUP.Clear();
            txtUserPHUP.Clear();
            txtUserEMUP.Clear();
            txtUserFNUP.Clear();
            txtUserLNUP.Clear();
        }

        private void ResetAddNewUserControls()
        {
            rbYes.Checked = rbNo.Checked = false;

            chxManCli.Checked = chxManUsers.Checked = chxCliTrans.Checked
                = chxCurrEx.Checked = false;

            btnAddNewUser.Enabled = false;
            gBPermissions.Enabled = false;
        }
        private void ResetUpdateUserControls()
        {
            rbYesUp.Checked = rbNoUp.Checked = false;

            chxManCliUp.Checked = chxManUsersUp.Checked = chxCliTransUp.Checked
                = chxCurrExUp.Checked = false;

            btnUpdateUser.Enabled = false;
            gBPermissionsUP.Enabled = false;
        }

        private void AddNewUserToDataSource(int Permissions)
        {
            // add new User Record
            clsUser NewUser = new clsUser();

            NewUser.UserName = txtUserName.Text;
            NewUser.Password = txtUserPass.Text;
            NewUser.Email = txtUserEM.Text;
            NewUser.FirstName = txtUserFN.Text;
            NewUser.LastName = txtUserLN.Text;
            NewUser.Phone = txtUserPH.Text;
            NewUser.Permissions = Permissions;

            NewUser.Save();
        }
        private void AddNewUserToListView()
        {
            ListViewItem UserItem = new ListViewItem(txtUserName.Text);

            UserItem.SubItems.Add(txtUserFN.Text);
            UserItem.SubItems.Add(txtUserLN.Text);
            UserItem.SubItems.Add(txtUserEM.Text);
            UserItem.SubItems.Add(txtUserPH.Text);
            UserItem.SubItems.Add(txtUserPass.Text);

            // Permisison Accessiblity for a certain screens to each user (Using Binary System)

            int Permissions = 0;

            if (rbYes.Checked)
                Permissions = -1; // access to all screens

            else
            {
                if (chxManCli.Checked)
                    Permissions += (int)enPermissions.ManageClients;

                if (chxManUsers.Checked)
                    Permissions += (int)enPermissions.ManageUsers;

                if (chxCliTrans.Checked)
                    Permissions += (int)enPermissions.ClientsTransactions;

                if (chxCurrEx.Checked)
                    Permissions += (int)enPermissions.CurrencyExchange;
            }

            UserItem.SubItems.Add(Permissions.ToString());

            LVUsersScreen.Items.Add(UserItem);

            AddNewUserToDataSource(Permissions);
        }

        // Update A New User
        private void cbUserNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbUserNames.SelectedIndex != -1)
            {
                clsUser User = clsUser.FindUser(cbUserNames.SelectedItem.ToString());

                txtUserEMUP.Text = User.Email;
                txtUserPassUP.Text = User.Password;
                txtUserFNUP.Text = User.FirstName;
                txtUserLNUP.Text = User.LastName;
                txtUserPHUP.Text = User.Phone;

                gBPermissionsUP.Enabled = true;
                rbNoUp.Checked = true;
                btnUpdateUser.Enabled = true;
            }
        }
        private void rbYesUp_CheckedChanged(object sender, EventArgs e)
        {
            chxManCliUp.Checked = chxManUsersUp.Checked = chxCliTransUp.Checked
                = chxCurrExUp.Checked = true;
        }
        private void rbNoUp_CheckedChanged(object sender, EventArgs e)
        {
            chxManCliUp.Checked = chxManUsersUp.Checked = chxCliTransUp.Checked
                = chxCurrExUp.Checked = false;
        }

        // Login Register Screen Log
        private void LoadLogRegsDate()
        {
            LVUsersRegisters.Items.Clear();

            LoginRegistersDatatable = clsUserLoginRegistration.GetAllLogs();

            foreach (DataRow row in LoginRegistersDatatable.Rows)
            {
                ListViewItem LoginRegister = new ListViewItem(row["UserName"].ToString());

                LoginRegister.SubItems.Add(row["FirstName"].ToString());
                LoginRegister.SubItems.Add(row["LastName"].ToString());
                LoginRegister.SubItems.Add(row["Email"].ToString());
                LoginRegister.SubItems.Add(row["Phone"].ToString());
                LoginRegister.SubItems.Add(row["Password"].ToString());
                LoginRegister.SubItems.Add(row["LogDate"].ToString());

                LVUsersRegisters.Items.Add(LoginRegister);
            }
        }
    }
}
