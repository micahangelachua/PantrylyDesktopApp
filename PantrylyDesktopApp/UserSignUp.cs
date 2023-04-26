using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
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
        }

        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet PantrylyUsersDS = new DataSet();
        DataTable PantrylyUsersDT = new DataTable();

        private void setConnection()
        {
            sql_con = new SQLiteConnection("Data Source = PantrylyDB.db");
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
            return txt_Password.Text == txt_RetypedPassword.Text ? true: false;
        }

        private string UserIdGenerator()
        {

        }

        private void AddNewUser()//Add new user to database
        {
            string txtQuery = @"INSERT INTO Users (user_ID, user_FirstName, user_LastName, user_Birthday, user_Email, user_Password) VLAUES('"+User+"')";
        }

        private void btn_SignUp_Click(object sender, EventArgs e) 
        {
            
        }
    }
}
