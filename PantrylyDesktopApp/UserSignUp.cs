using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PantrylyDesktopApp
{
    public partial class UserSignUp : Form
    {
        public UserSignUp()
        {
            InitializeComponent();
            FormUtils.MakeWindowFormRounded(this);
            FormUtils.AddDraggableWindowTitle(pnl_WinTitleAndControls);
            FormUtils.AddCloseButton(pb_SignupClose);
            FormUtils.AddMinimizeButton(pb_SignupMinimize);
            FormUtils.MakeButtonRounded(btn_SignUp);
        }

        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet PantrylyUsersDS = new DataSet();
        DataTable PantrylyUsersDT = new DataTable();

        private void setConnection()
        {
            sql_con = new SQLiteConnection("Data Source = ../../Resources/PantrylyDB.db; Version = 3; New = False; Compress = True;");
        }

        private void executeQuery(string txtQuery)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        //method that Checks if some textboxes are empty
        private bool CheckTextbox()
        {
            if (txt_Email.Text.Trim() == "" || txt_FirstName.Text.Trim() == "" ||
                txt_LastName.Text.Trim() == "" || txt_Password.Text.Trim() == "" ||
                txt_RetypedPassword.Text.Trim() == "")
            {
                return false; // at least one of the text box is null
            }
            else
            {
                return true; // all textbox are not empty
            }
        }

        //method that checks if email is already in database
        private bool CheckUserEmail()
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "Select * from Users where user_Email = '" + txt_Email.Text + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            bool result;
            if (PantrylyUsersDT.Rows.Count == 1)
            {
                
                result = false; //meaning the email cannot be used because there is a user with that email
            }
            else
            {
                result = true; //no user in the database with the email in txt_Email.
            }

            return result;
        }

        private bool CheckAge()
        {
            int age = DateTime.Now.Year - dtp_Birthday.Value.Year;

            if (DateTime.Now.Month > dtp_Birthday.Value.Month)
            {
                age--;
            }
            else if ((DateTime.Now.Month <= dtp_Birthday.Value.Month) && (DateTime.Now.Day > dtp_Birthday.Value.Day))
            {
                age--;
            }

            //true they can signup (13 is the youngest to have a n email.)
            return age > 12 ? true : false;
        }

        private bool CheckPassword()
        {
            return txt_Password.Text == txt_RetypedPassword.Text ? true : false;
        }


        private void btn_SignUp_Click(object sender, EventArgs e)
        {
            if (CheckUserEmail() && CheckAge() && CheckPassword() && CheckTextbox())
            {
                User newUser = new User(txt_FirstName.Text, txt_LastName.Text, dtp_Birthday.Value.ToString(), txt_Email.Text, txt_Password.Text);
                newUser.AddNewUser();
                ClearText();

            } else
            {
                if (!CheckTextbox())
                {
                    MessageBox.Show("Complete all required fields.");
                }
                else if (!CheckUserEmail())
                {
                    MessageBox.Show("Email is already in use.");
                }
                else if (!CheckAge())
                {
                    MessageBox.Show("You are too young.");
                }
                else if (!CheckPassword())
                {
                    MessageBox.Show("Password doesn't match");
                }

                ClearText();
            }
        }

        private void ClearText()
        {
            txt_Email.Clear();
            txt_Password.Clear();
            txt_RetypedPassword.Clear();
            txt_FirstName.Clear();
            txt_LastName.Clear();
            dtp_Birthday.ResetText();
        }
    }
}
