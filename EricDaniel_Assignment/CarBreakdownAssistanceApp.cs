using EricDaniel_Assignment.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EricDaniel_Assignment
{
    public partial class CarBreakdownAssistanceApp : Form
    {
        private List<Member> _memberslist = new List<Member>();
        string storedPath = @"A:\EricDaniel_Assignment/members2.txt"; 
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
          //  grpDisplayMemberDetails.Visible = false;
           // grpUpdateCarDetails.Visible = false;
            // grpIcNumberValidation.Visible = true;
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
                    // using (StreamWriter clr = new StreamWriter("members.txt"))
                    //  clr.WriteLine("");
                    //Member m1 = new NullMember();
                    // StoreRegisteredMemberintoFile(m1);
                 //   Member m1 = new NullMember();
                    SaveMemberData();
                    Application.Exit();
                }
                else
                    e.Cancel = true;
            }
        }
        private void SaveMemberData()
        {
        //    _memberslist.Add(m1);
        //    Console.WriteLine("name = {0}", m1.Name);
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

        private void LoadMemberData()
        {
            //  int count = 0;
            // List<Member> members;
            using (Stream stream = File.Open(storedPath, FileMode.Open))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                _memberslist = (List<Member>)bformatter.Deserialize(stream);
                //foreach (var m in _memberslist)
                //{
                //    Console.WriteLine("{0}. name in ReadRegisteredMemberFromFile = {1}", count++, m.Name);
                //}
                stream.Close();
            }
            //return;
        }
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            string name = tbxName.Text;
            string ic = tbxIcNumber.Text;
            string dOB = dateTimePickerDateOfBirth.Text; //dateTimePicker1.Value.ToString(); //txtDateOfBirth.Text;
            string phoneNum = tbxPhoneNumber.Text;
            string newDate = dateTimePickerMembershipRenewalDate.Text; //txtMembershipRenewalDate.Text;//txtMembershipRenewalDate.Text = dateTimePicker2.Value.ToString("yyyy-MM-dd");//dateTimePicker2.ToInt32;//txtMembershipRenewalDate.Text;
            string registrationNumber = tbxCarRegistrationNumber.Text;
            string model = tbxCarModel.Text;
            int year = Convert.ToInt32(dateTimePickerCarYear.Text /* txtCarYear.Text*/);

            //Can include validation here
            Member m1 = null;
            if (rbnOneYearMembershipRenewal.Checked)
            {
                m1 = new OneYearMembershipRenewal(name, ic, dOB, phoneNum, newDate, registrationNumber, model, year);

                // StoreRegisteredMemberintoFile(m1);
            }

            else if (rbnFiveYearMembershipRenewal.Checked)
            {
                m1 = new FiveYearsMembershipRenewal(name, ic, dOB, phoneNum, newDate, registrationNumber, model, year);
                //StoreRegisteredMemberintoFile(m1);
            }
            _memberslist.Add(m1);
            // Member m1 = new OneYearMembershipRenewal(name, ic, dOB, phoneNum, newDate, registrationNumber, model, year);
            // Member m2 = new FiveYearsMembershipRenewal(name, ic, dOB, phoneNum, newDate, registrationNumber, model, year);
            //  Member m1 = new Member(name, ic, dOB, phoneNum, newDate, registrationNumber, model, year);
            // StoreRegisteredMemberintoFile(m1);
            // StoreRegisteredMemberintoFile(m2);
            //  StoreRegisteredMemberintoFile(m1);
            //StoreRegisteredMemberintoFile(m1);
            tbxName.Text = string.Empty;
        }

        private void btnClearEveryInputData_Click(object sender, EventArgs e)
        {

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string pos = tbxIcNumberSearchMember.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

            // btnSearch.Enabled = false;
            //grpDisplayMemberDetails.Enabled = false;
            //anAccount = new Account(tbxName.Text, tbxNumber.Text);
            //btnAccount.Enabled = false;
            //tbxName.Enabled = false;
            //tbxNumber.Enabled = false;

            //lblBalance.Visible = true;
            //tbxBalance.Visible = true;
            //grbTransactionType.Visible = true;
            //rbnDeposit.Visible = true;
            //rbnWithdraw.Visible = true;
            //lblAmount.Visible = true;
            //tbxAmount.Visible = true;
            //btnUpdateBalance.Visible = true;
            // btnSearch.Enabled = false;
            // grpIcNumberValidation.Enabled = false;
            //tbxBalance.Text = "" + anAccount.Balance;
            //  grpDisplayMemberDetails.Visible = false;

            //if (grpDisplayMemberDetails.Visible == true)
            //{
            //    btnSearch.Enabled = true;
            //}
            // btnSearch.Enabled = false;
            // txtIcNumber1.Text.Enabled = false;

            //  string pos,icNum;
            //  Member m = IcInput(list);
            // pos = txtIcNumber1.ToString();
            // icNum = Convert.ToString(pos);
            //  SearchMember(list);
            //  IcInput() = new Member(txtIcNumber1.Text);
            // txtName.Text = ArrayList[1].ToString();
            //  Member m1 = new Member(aName, theIc, theDOB, aPhoneNum, aNewDate, theCarRegNum);
            //  SearchMember(m1) = btnSearch.ToString();
            // Member m = new Member(IcInput);

            // tbxDisplayName.Text = m1.Name;
            //Since m1 can have null values, m1 value must be checked first
            if (m1 != null)
            {
                tbxIcNumberSearchMember.BackColor = Color.GreenYellow;
                grpDisplayMemberDetails.Visible = true;
                SearchMember(m1);
            }
            else
            {
                tbxIcNumberSearchMember.BackColor = Color.Red;
                MessageBox.Show("Invalid IC! ");
            }
            //txtIcNumber1.RedColor = Color.Black;
            tbxIcNumberSearchMember.Text = string.Empty;
            tbxIcNumberSearchMember.BackColor = Color.White;
        }

        private void btnDoneSearchAMember_Click(object sender, EventArgs e)
        {

        }
        private void btnUpdatePhoneNumber_Click(object sender, EventArgs e)
        {
            string pos = tbxVerifyInputIcNumber.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

            if (m1 != null)
            {
                m1.PhoneNum = tbxNewPhoneNumber.Text;
                UpdatePhoneNumber(_memberslist, m1);
                // StoreRegisteredMemberintoFile(m1);
            }
            else
                MessageBox.Show("Invalid IC! ");
        }

        private void btnDoneUpdatePhoneNumber_Click(object sender, EventArgs e)
        {

        }

        private void btnVerifyInputIcNumber_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateCarDetails_Click(object sender, EventArgs e)
        {
            string pos = tbxVerifyInputIcNumber2.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

            if (m1 != null)
            {
                m1.MCar.RegistrationNumber = tbxNewCarRegistrationNumber.Text;
                m1.MCar.Model = tbxNewCarModel.Text;
                m1.MCar.Year = Convert.ToInt32(tbxNewCarYear.Text);
                UpdateCarDetails(_memberslist, m1.MCar);
                //StoreRegisteredMemberintoFile(m1);
            }
            else
                MessageBox.Show("Invalid IC! ");
        }

        private void btnDoneUpdateCarDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnVerifyInputIcNumber2_Click(object sender, EventArgs e)
        {

        }

        private void btnRenewMembershipDate_Click(object sender, EventArgs e)
        {
            string pos = tbxVerifyIcInputNumber3.Text;
            Member m1 = IcInput(_memberslist, pos); //storing object returned by the function

            if (m1 != null)
            {
                MembershipRenewalDate(_memberslist, m1);
                //  StoreRegisteredMemberintoFile(m1);
            }
            else
                MessageBox.Show("Invalid IC! ");
        }

        private void btnDoneMembershipRenewal_Click(object sender, EventArgs e)
        {

        }

        //This method returns the object or null, after the ic number is passed
        public Member IcInput(List<Member> membersArraylist, string pos)
        {


            foreach (Member m in membersArraylist)
            {
                // ReadRegisteredMemberFromFile();
                Console.WriteLine(" ic in IcInput = {0}", m.IcNum);
                // Member m = (Member)membersArraylist[i];
                if (m.IcNum.Equals(pos))
                    return m;
            }
            return null;
        }

        //This Method only displays the member details.
        public void SearchMember(Member m) //, Car mCar)//, Member car)
        {
            MessageBox.Show(m.Name + "\n" + m.DateOfBirth + "\n" + m.PhoneNum + "\n" + m.MembershipRenewalDate
                            /*m.MembershipRenewalDate*/ + "\n" + m.MCar.RegistrationNumber + "\n" + m.MCar.Model +
                            "\n" +
                            m.MCar.Year);
            //tbxDisplayName.AppendText(m.Name);
            //tbxDisplayIcNumber.AppendText(m.IcNum);
            tbxDisplayName.Text = m.Name;
            tbxDisplayIcNumber.Text = m.IcNum;

            //foreach (Member member in membersArraylist)
            //{
            //    if(member.IcNum.Equals(m.IcNum))
            //    {
            //        MessageBox.Show(m.Name + "\n" + m.DateOfBirth + "\n" + m.PhoneNum + "\n" + m.MembershipRenewalDate
            //               /*m.MembershipRenewalDate*/ + "\n" + m.MCar.RegistrationNumber + "\n" + m.MCar.Model +
            //               "\n" +
            //               m.MCar.Year);
            //    }
            //}


        }

        public void UpdatePhoneNumber(List<Member> member, Member m)
        {

            MessageBox.Show(m.PhoneNum);
            // ReadRegisteredMemberFromFile();
            //string input, NewPhoneNum;

            //Console.Write("Enter member's Identification Card Number(IC): ");
            //input = Console.ReadLine();

            //for (int i = 0; i < member.Count; i++)
            //{
            //    Member m = (Member)member[i];
            //    if (m.IcNum == input)
            //    {
            //        Console.Write("Enter new phone number: ");
            //        NewPhoneNum = Console.ReadLine();
            //        m.PhoneNum = NewPhoneNum;
            //        Console.WriteLine("Updated sucessfully. Your new Phone Number is " + m.PhoneNum);
            //    }
            //}
        }

        public void UpdateCarDetails(List<Member> member, Car mCar)
        {
            //ReadRegisteredMemberFromFile();
            MessageBox.Show("\n" + mCar.RegistrationNumber + "\n" + mCar.Model + "\n" + mCar.Year);
        }

        public void MembershipRenewalDate(List<Member> list, Member m)
        {
            // ReadRegisteredMemberFromFile();
            m.RenewMembership();
            MessageBox.Show(m.MembershipRenewalDate);
            //  string renewMembership1;
            //    List<Member> renewMembership = null;
            //Member.RenewMembership();
            // for (int i = 0; i < list.Count; i++)
            //  {
            //     renewMembership1 = list.RenewMembership();
            //    Member m = (Member)list[i];
            //     m.RenewMembership();
            //   DateTimePicker1.Text = RenewMembership;
            //TestSystem**/
            //  }
            /* the first issue is with the  public static void MembershipRenewalDate(ArrayList list),
             *  I need to remove the for loop that so it will not  renew all members in list AND i not sure how to specify which member to renew..*/
        }

        private void btnVerifyInputIcNumber3_Click(object sender, EventArgs e)
        {

        }

       
    }
}
