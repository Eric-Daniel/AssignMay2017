using EricDaniel_Assignment.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EricDaniel_Assignment
{
    public partial class CarBreakdownAssistanceApp : Form
    {
        private List<Member> _memberslist = new List<Member>();
        string storedPath = @"A:\EricDaniel_Assignment/membersT7.txt"; 
        public CarBreakdownAssistanceApp()
        {
            InitializeComponent();
            dateTimePickerDateOfBirth.Format = DateTimePickerFormat.Custom;
            dateTimePickerDateOfBirth.CustomFormat = "d/MM/yyyy";
            //  dateTimePicker2.CustomFormat = "yyyy";
            dateTimePickerCarYear.Format = DateTimePickerFormat.Custom;
            dateTimePickerCarYear.CustomFormat = "yyyy";
            dateTimePickerMembershipRenewalDate.Format = DateTimePickerFormat.Custom;
            dateTimePickerMembershipRenewalDate.CustomFormat = "d/MM/yyyy";
            grpDisplayMemberDetails.Visible = false;
            btnAddMember.Enabled = true;
           // tbxDisplayName.ReadOnly = true;
            // grpUpdatePhoneNumber.Visible = false;
            // grpUpdateCarDetails.Visible = false;
            // grpIcNumberValidation.Visible = true;
            grpAddMember.Enabled = false;
            //grpDisplayMemberDetails.
            grpIcNumberValidation.Visible = false;
            grpUpdatePhoneNumber.Enabled = false;
            grpUpdateCarDetails.Enabled = false;
            grpMembershipRenewal.Enabled = false;
        }

        private void CarBreakdownAssistanceApp_Load(object sender, EventArgs e)
        {
            if (File.Exists(storedPath))
            {
                LoadMemberData();
            }
        }

        private void CarBreakdownAssistanceApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Are you sure want to exit?",
                        " ",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Information) == DialogResult.OK)
                {
                   
                    SaveMemberData();
                    Application.Exit();
                }
                else
                    e.Cancel = true;
            }
        }
        private void SaveMemberData()
        {
  
            try
            {
                if (!File.Exists(storedPath))
                {
                    using (Stream stream = File.Open(storedPath, FileMode.CreateNew))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        bformatter.Serialize(stream, _memberslist);
                        stream.Close();
                    }
                }
                else
                {
                    using (Stream stream = File.Open(storedPath, FileMode.Open))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        bformatter.Serialize(stream, _memberslist);
                        stream.Close();
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "IOEXCEPTION");
            }
           

        }

        private void LoadMemberData()
        {
           
            try
            {
                if (File.Exists(storedPath))
                {
                    using (Stream stream = File.Open(storedPath, FileMode.Open))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        _memberslist = (List<Member>)bformatter.Deserialize(stream);
                     
                        stream.Close();
                    }
                }
                else
                {
                    MessageBox.Show("File not found. \nFile Name: members.txt");
                    Environment.Exit(0);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message, "IOEXCEPTION");
            }
            
            
           
        }
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            string name = tbxName.Text;
            string ic = tbxIcNumber.Text;
            string dOB = dateTimePickerDateOfBirth.Text; 
            string phoneNum = tbxPhoneNumber.Text;
            string newDate = dateTimePickerMembershipRenewalDate.Text; 
            string registrationNumber = tbxCarRegistrationNumber.Text;
            string model = tbxCarModel.Text;
            int year = Convert.ToInt32(dateTimePickerCarYear.Text);

            

                if (!rbnOneYearMembershipRenewal.Checked && !rbnFiveYearMembershipRenewal.Checked)
                {
                    MessageBox.Show("Choose type!");
                    
                }

                else if (string.IsNullOrWhiteSpace(tbxName.Text))
                {
                    MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                    "\n\nMEMBER'S NAME NOT INSERTED!\"", " ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
               
                    tbxName.Focus();
                }
                else if (string.IsNullOrWhiteSpace(tbxIcNumber.Text) )
                {
                
                MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                "\n\nMEMBER'S IC NUMBER NOT INSERTED!\"", " ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxIcNumber.Focus();
                }
                else if (string.IsNullOrWhiteSpace(tbxPhoneNumber.Text))
                {
                MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                "\n\nMEMBER'S PHONE NUMBER NOT INSERTED!\"", " ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPhoneNumber.Focus();
                }
                else if (string.IsNullOrWhiteSpace(tbxCarRegistrationNumber.Text))
                {
                MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                "\n\nMEMBER'S CAR REGISTRATION NUMBER NOT INSERTED!\"", " ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxCarRegistrationNumber.Focus();
                }
                else if (string.IsNullOrWhiteSpace(tbxCarModel.Text))
                {
                MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                "\n\nMEMBER'S CAR MODEL NOT INSERTED!\"", " ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxCarModel.Focus();
                }

            else 
                {
                string pos = tbxIcNumber.Text;
                Member m1 = IcInput(_memberslist, pos);
                if (m1 == null)
                { 
             
                if (rbnOneYearMembershipRenewal.Checked)
                {
                    m1 = new OneYearMembershipRenewal(name, ic, dOB, phoneNum, newDate, registrationNumber, model,
                        year);

                   
                }

                else if (rbnFiveYearMembershipRenewal.Checked)
                {
                    m1 = new FiveYearsMembershipRenewal(name, ic, dOB, phoneNum, newDate, registrationNumber, model,
                        year);
                   
                }
                _memberslist.Add(m1);
                   
                    tbxName.Text = string.Empty;
                    tbxIcNumber.Text = string.Empty;
                    dateTimePickerDateOfBirth.ResetText();
                    tbxPhoneNumber.Text = string.Empty;
                    tbxCarRegistrationNumber.Text = string.Empty;
                    tbxCarModel.Text = string.Empty;
                    dateTimePickerCarYear.ResetText();

                    dateTimePickerMembershipRenewalDate.ResetText();
                    rbnOneYearMembershipRenewal.Checked = false;
                    rbnFiveYearMembershipRenewal.Checked = false;
                }
                else
                {
                    MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                    "\n\nIC NUMBER ALREADY EXISTED!\"", " ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
            
           
        }

        private void btnAddNewMember_Click(object sender, EventArgs e)
        {
            grpAddMember.Enabled = true;
            btnSearchExistingMember.Enabled = false;
            btnUpdateExistingPhoneNumber.Enabled = false;
            btnUpdateExistingCarDetails.Enabled = false;
            btnRenewCurrentMembershipDate.Enabled = false;
        }
        private void btnDoneAddMember_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Click YES if want to EXIT OUT OF ADD MEMBER SECTION", " ", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                grpAddMember.Enabled = false;
                btnSearchExistingMember.Enabled = true;
                btnUpdateExistingPhoneNumber.Enabled = true;
                btnUpdateExistingCarDetails.Enabled = true;
                btnRenewCurrentMembershipDate.Enabled = true;

                tbxName.Text = string.Empty;
                tbxIcNumber.Text = string.Empty;
                dateTimePickerDateOfBirth.ResetText();
                tbxPhoneNumber.Text = string.Empty;
                tbxCarRegistrationNumber.Text = string.Empty;
                tbxCarModel.Text = string.Empty;
                dateTimePickerCarYear.ResetText();

                dateTimePickerMembershipRenewalDate.ResetText();
                rbnOneYearMembershipRenewal.Checked = false;
                rbnFiveYearMembershipRenewal.Checked = false;
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                
                
            }

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string pos = tbxIcNumberSearchMember.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

            
            //Since m1 can have null values, m1 value must be checked first
            if (m1 != null)
            {
               // tbxIcNumberSearchMember.BackColor = Color.GreenYellow;
                grpDisplayMemberDetails.Visible = true;
                SearchMember(m1);

                grpIcNumberValidation.Enabled = false;

                tbxDisplayName.ReadOnly = true;
                tbxDisplayIcNumber.ReadOnly = true;
                tbxDisplayDateOfBirth.ReadOnly = true;
                tbxDisplayPhoneNumber.ReadOnly = true;
                tbxDisplayCarRegistrationNumber.ReadOnly = true;
                tbxDisplayCarModel.ReadOnly = true;
                tbxDisplayCarYear.ReadOnly = true;
                tbxDisplayMembershipRenewalDate.ReadOnly = true;
                

                tbxDisplayName.Font = new Font(tbxDisplayName.Font, FontStyle.Bold);
                tbxDisplayIcNumber.Font = new Font(tbxDisplayIcNumber.Font, FontStyle.Bold);
                tbxDisplayDateOfBirth.Font = new Font(tbxDisplayDateOfBirth.Font, FontStyle.Bold);
                tbxDisplayPhoneNumber.Font = new Font(tbxDisplayPhoneNumber.Font, FontStyle.Bold);
                tbxDisplayCarRegistrationNumber.Font = new Font(tbxDisplayCarRegistrationNumber.Font, FontStyle.Bold);
                tbxDisplayCarModel.Font = new Font(tbxDisplayCarModel.Font, FontStyle.Bold);
                tbxDisplayCarYear.Font = new Font(tbxDisplayCarYear.Font, FontStyle.Bold);
                tbxDisplayMembershipRenewalDate.Font = new Font(tbxDisplayMembershipRenewalDate.Font, FontStyle.Bold);

                btnDoneSearchAMember.Focus();
            }
            else
            {
                tbxIcNumberSearchMember.BackColor = Color.Red;
                MessageBox.Show("Invalid IC! "); // error warning message
                grpIcNumberValidation.Visible = false;
                btnAddNewMember.Enabled = true;
                btnUpdateExistingPhoneNumber.Enabled = true;
                btnUpdateExistingCarDetails.Enabled = true;
                btnRenewCurrentMembershipDate.Enabled = true;
            }
            //txtIcNumber1.RedColor = Color.Black;
            tbxIcNumberSearchMember.Text = string.Empty;
            tbxIcNumberSearchMember.BackColor = Color.White;
        }
        private void btnSearchExistingMember_Click(object sender, EventArgs e)
        {
            grpIcNumberValidation.Visible = true;
            btnAddNewMember.Enabled = false;
            btnUpdateExistingPhoneNumber.Enabled = false;
            btnUpdateExistingCarDetails.Enabled = false;
            btnRenewCurrentMembershipDate.Enabled = false;
        }
        private void btnDoneSearchAMember_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Click YES if want to EXIT OUT OF SEARCH MEMBER SECTION", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                tbxDisplayName.Text = string.Empty;
                tbxDisplayIcNumber.Text = string.Empty;
                tbxDisplayDateOfBirth.Text = string.Empty;
                tbxDisplayPhoneNumber.Text = string.Empty;
                tbxDisplayCarRegistrationNumber.Text = string.Empty;
                tbxDisplayCarModel.Text = string.Empty;
                tbxDisplayCarYear.Text = string.Empty;
                tbxDisplayMembershipRenewalDate.Text = string.Empty;

                grpIcNumberValidation.Enabled = true;
                grpDisplayMemberDetails.Visible = false;
                btnAddNewMember.Enabled = true;
                btnUpdateExistingPhoneNumber.Enabled = true;
                btnUpdateExistingCarDetails.Enabled = true;
                btnRenewCurrentMembershipDate.Enabled = true;
                grpIcNumberValidation.Visible = false;
            }
            
        }
        private void btnUpdatePhoneNumber_Click(object sender, EventArgs e)
        {
           

           

            string pos = tbxVerifyInputIcNumber.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

           
            if (string.IsNullOrWhiteSpace(tbxVerifyInputIcNumber.Text))
            {
                MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                "\n\nMEMBER'S IC NUMBER NOT INSERTED!\"", " ",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                tbxVerifyInputIcNumber.Focus();
            }
            else if (m1 != null)
            {
                if (string.IsNullOrWhiteSpace(tbxNewPhoneNumber.Text))
                {
                    MessageBox.Show("\"Oops, Agent E encountered an error over here!" +
                                    "\n\nMEMBER'S PHONE NUMBER NOT INSERTED!\"", " ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    tbxNewPhoneNumber.Focus();
                }
                else
                {
                    tbxVerifyInputIcNumber.ReadOnly = true;
                    m1.PhoneNum = tbxNewPhoneNumber.Text;
                    UpdatePhoneNumber(_memberslist, m1);
                }


                // StoreRegisteredMemberintoFile(m1);
            }
            else
            {
                tbxVerifyInputIcNumber.BackColor = Color.Red;
                MessageBox.Show("Invalid IC! ");
                grpUpdatePhoneNumber.Visible = false;
            }
           // tbxVerifyInputIcNumber.Text = string.Empty;
           // tbxVerifyInputIcNumber.BackColor = Color.White;

        }
        private void btnUpdateExistingPhoneNumber_Click(object sender, EventArgs e)
        {
            grpUpdatePhoneNumber.Enabled = true;
            //grpIcNumberValidation.Visible = true;
            btnAddNewMember.Enabled = false;
            btnSearchExistingMember.Enabled = false;
            //btnUpdateExistingPhoneNumber.Enabled = false;
            btnUpdateExistingCarDetails.Enabled = false;
            btnRenewCurrentMembershipDate.Enabled = false;
        }
        private void btnDoneUpdatePhoneNumber_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Click YES if want to EXIT OUT OF UPDATE MEMBER's PHONE NUMBER SECTION", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                tbxVerifyInputIcNumber.Text = string.Empty;
                tbxNewPhoneNumber.Text = string.Empty;

                grpUpdatePhoneNumber.Enabled = false;
                //grpDisplayMemberDetails.Enabled = false;
                btnAddNewMember.Enabled = true;
                btnSearchExistingMember.Enabled = true;
                //btnUpdateExistingPhoneNumber.Enabled = true;
                btnUpdateExistingCarDetails.Enabled = true;
                btnRenewCurrentMembershipDate.Enabled = true;
                // grpIcNumberValidation.Visible = false;
            }

        }

       

        private void btnUpdateCarDetails_Click(object sender, EventArgs e)
        {
            string pos = tbxVerifyInputIcNumber2.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

            if (m1 != null)
            {
                tbxVerifyInputIcNumber2.ReadOnly = true;
                m1.MCar.RegistrationNumber = tbxNewCarRegistrationNumber.Text;
                m1.MCar.Model = tbxNewCarModel.Text;
                m1.MCar.Year = Convert.ToInt32(tbxNewCarYear.Text);
                UpdateCarDetails(_memberslist, m1.MCar);
                //StoreRegisteredMemberintoFile(m1);
            }
            else
            {
                tbxVerifyInputIcNumber2.BackColor = Color.Red;
                MessageBox.Show("Invalid IC! ");
            }
            tbxVerifyInputIcNumber2.Text = string.Empty;
            tbxVerifyInputIcNumber2.BackColor = Color.White;
        }
        private void btnUpdateExistingCarDetails_Click(object sender, EventArgs e)
        {
            grpUpdateCarDetails.Enabled = true;
            //grpIcNumberValidation.Visible = true;
            btnAddNewMember.Enabled = false;
            btnSearchExistingMember.Enabled = false;
            btnUpdateExistingPhoneNumber.Enabled = false;
           // btnUpdateExistingCarDetails.Enabled = false;
            btnRenewCurrentMembershipDate.Enabled = false;
        }
        private void btnDoneUpdateCarDetails_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Click YES if want to EXIT OUT OF UPDATE MEMBER's CAR DETAILS SECTION", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                tbxVerifyInputIcNumber2.Text = string.Empty;
                tbxNewCarRegistrationNumber.Text = string.Empty;
                tbxNewCarModel.Text = string.Empty;
                tbxNewCarYear.Text = string.Empty;

                grpUpdateCarDetails.Enabled = false;
                //grpDisplayMemberDetails.Enabled = false;
                btnAddNewMember.Enabled = true;
                btnSearchExistingMember.Enabled = true;
                btnUpdateExistingPhoneNumber.Enabled = true;
                // btnUpdateExistingCarDetails.Enabled = true;
                btnRenewCurrentMembershipDate.Enabled = true;
                // grpIcNumberValidation.Visible = false;
            }
        
        }


        private void btnRenewMembershipDate_Click(object sender, EventArgs e)
        {
            string pos = tbxVerifyInputIcNumber3.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

            if (m1 != null)
            {
                MembershipRenewalDate(_memberslist, m1);
                //  StoreRegisteredMemberintoFile(m1);
            }
            else
            {
                tbxVerifyInputIcNumber3.BackColor = Color.Red;
                MessageBox.Show("Invalid IC! ");
            }
            tbxVerifyInputIcNumber3.Text = string.Empty;
            tbxVerifyInputIcNumber3.BackColor = Color.White;
        }
        private void btnRenewCurrentMembershipDate_Click(object sender, EventArgs e)
        {
            tbxNewMembershipRenewalDate.ReadOnly = true;

            grpMembershipRenewal.Enabled = true;
            //grpIcNumberValidation.Visible = true;
            btnAddNewMember.Enabled = false;
            btnSearchExistingMember.Enabled = false;
            btnUpdateExistingPhoneNumber.Enabled = false;
            btnUpdateExistingCarDetails.Enabled = false;
           // btnRenewCurrentMembershipDate.Enabled = false;
        }
        private void btnDoneMembershipRenewal_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Click YES if want to EXIT OUT OF RENEW MEMBER'S MEMBERSHIP SECTION", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                tbxVerifyInputIcNumber3.Text = string.Empty;
                tbxNewMembershipRenewalDate.Text = string.Empty;

                grpMembershipRenewal.Enabled = false;
                //grpDisplayMemberDetails.Enabled = false;
                btnAddNewMember.Enabled = true;
                btnSearchExistingMember.Enabled = true;
                btnUpdateExistingPhoneNumber.Enabled = true;
                btnUpdateExistingCarDetails.Enabled = true;
                //btnRenewCurrentMembershipDate.Enabled = true;
                // grpIcNumberValidation.Visible = false;
            }

        }

        //This method returns the object or null, after the ic number is passed
        public Member IcInput(List<Member> membersList, string pos)
        {


            foreach (var m in membersList)
            {
               
                if (m.IcNum.Equals(pos))
                    return m;
            }
            return null;
        }

        //This Method only displays the member details.
        public void SearchMember(Member m) 
        {
            tbxDisplayName.AppendText(m.Name);
            tbxDisplayIcNumber.AppendText(m.IcNum);
            tbxDisplayDateOfBirth.AppendText(m.DateOfBirth);
            tbxDisplayPhoneNumber.AppendText(m.PhoneNum);
            tbxDisplayCarRegistrationNumber.AppendText(m.MCar.RegistrationNumber);
            tbxDisplayCarModel.AppendText(m.MCar.Model);
            tbxDisplayMembershipRenewalDate.AppendText(m.MembershipRenewalDate);
            tbxDisplayCarYear.AppendText(m.MCar.Year.ToString());
        }

        public void UpdatePhoneNumber(List<Member> member, Member m)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to UPDATE MEMBER'S PHONE NUMBER", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show("Phone Number Sucessfully updated");
                tbxNewPhoneNumber.Text = string.Empty;

                tbxNewPhoneNumber.AppendText(m.PhoneNum);
                tbxNewPhoneNumber.ReadOnly = true;

                btnDoneUpdatePhoneNumber.Focus();
            }
            
            // tbxNewPhoneNumber.BackColor = Color.GreenYellow;

        }

        public void UpdateCarDetails(List<Member> member, Car mCar)
        {
            
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to UPDATE MEMBER'S CAR DETAILS", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show("Car Details Updated Successfully");

                tbxNewCarRegistrationNumber.Text = string.Empty;
                tbxNewCarModel.Text = string.Empty;
                tbxNewCarYear.Text = string.Empty;

                tbxNewCarRegistrationNumber.ReadOnly = true;
                tbxNewCarModel.ReadOnly = true;
                tbxNewCarYear.ReadOnly = true;

                tbxNewCarRegistrationNumber.AppendText(mCar.RegistrationNumber);
                tbxNewCarModel.AppendText(mCar.Model);
                // tbxNewCarYear.AppendText((mCar.Year));
                // tbxNewCarYear.Text = dateTimePickerCarYear.Value.ToString("yyyy");
                tbxNewCarYear.AppendText(mCar.Year.ToString());
            }
            
        }

        public void MembershipRenewalDate(List<Member> list, Member m)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to RENEW MEMBER'S MEMBERSHIP DATE", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dialogResult == DialogResult.Yes)
            {
                m.RenewMembership();
                MessageBox.Show("Membership Successfully Renewed");
                tbxNewMembershipRenewalDate.AppendText(m.MembershipRenewalDate);
            }
      
        }

        private void tbxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter /*|| e.KeyCode == Keys.Up || e.KeyCode == Keys.Down*/)
            {
                // tbxName.Focus();
                this.SelectNextControl((Control)sender, true, true, true, true);
               //tbxName.Focus();
            }
        }

       

        private void tbxIcNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter/* || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down*/)
            {
                
                    this.SelectNextControl((Control)sender, true, true, true, true);
                //tbxDisplayDateOfBirth.Focus();
                // dateTimePickerDateOfBirth.Focus();
                dateTimePickerDateOfBirth.Focus();
            }
        }

        private void dateTimePickerDateOfBirth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter/* || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down*/)
            {
                // tbxName.Focus();
                {
                    this.SelectNextControl((Control)sender, true, true, true, true);
                    tbxPhoneNumber.Focus();
                }
               
            }
        }
        private void tbxPhoneNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbxCarRegistrationNumber.Focus();
            }
            //{
            //    this.SelectNextControl((Control)sender, true, true, true, true);
            // //  Enter = tbxPhoneNumber.Select();
            //   // tbxCarRegistrationNumber.Focus();
            //}
        }
        private void tbxCarRegistrationNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbxCarModel.Focus();
            }
            //{
            //    this.SelectNextControl((Control)sender, true, true, true, true);
            //   // tbxCarRegistrationNumber.Focus();
            //}
        }
        private void tbxCarModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerCarYear.Focus();
            }
        }

        private void dateTimePickerCarYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerMembershipRenewalDate.Focus();
            }
        }

        private void tbxVerifyInputIcNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbxNewPhoneNumber.Focus();
            }
        }


        private void tbxVerifyInputIcNumber2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbxNewCarRegistrationNumber.Focus();
            }
        }

        private void tbxNewCarRegistrationNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbxNewCarModel.Focus();
            }
        }

        private void tbxNewCarModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tbxNewCarYear.Focus();
            }
        }
        /* private void txtInputValue_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
        if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
        {
            MessageBox.Show("Please enter only numbers.");
            textBox1.Text = string.Empty;
        }
    }*/
        private void tbxIcNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }

        private void tbxPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }

        private void tbxName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ////if (char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Letters
            ////if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
            //e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char) Keys.Back); // && e.KeyChar == ' ');
            //// tbxName.Text name = (tbxName)sender;
            ////if ((e.KeyChar == ' ') && (tbxName.Text.Contains(' ')))
            ////    e.Handled = true;
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }


        private void tbxVerifyInputIcNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }

        private void tbxNewPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }

        private void tbxVerifyInputIcNumber2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }

        private void tbxNewCarYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;            //Allow Backspace
        }
    }
}
