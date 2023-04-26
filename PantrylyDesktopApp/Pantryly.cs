using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PantrylyDesktopApp
{
    public partial class Pantryly : Form
    {
        public Pantryly()
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

        //when credentials match:
        string LoggingUserId = "";
        private void LogInSuccess() //opens user's dashboard;
        {
            UserDashboard userDashboard = new UserDashboard(LoggingUserId);
            userDashboard.ShowDialog();
        }
        

        //Check user log in credentials
        private void CheckUserAndPassword()
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = $"Select * from Users where user_Email = {txt_Email.Text}"; //gets data row with the email input
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            if (PantrylyUsersDT.Rows.Count == 1)//row count will be 1 if there is one (there should only be one because emails should be unique)
            {
                LoggingUserId = PantrylyUsersDT.Rows[0][1].ToString(); //assigns the userid that will be send to the next window form.
                //we should create a user class for this to be safer but idk...

                string userPassword = PantrylyUsersDT.Rows[0][6].ToString(); //variable to compare the input password and the one in the database

                if (userPassword == txt_Password.Text)
                {
                    LogInSuccess(); //goes to dashboard because password is right. 
                } 
                else
                {
                    //instead of message box, possible ba na the textbox will change color na lang?
                    MessageBox.Show("Invalid password.");
                }
            }
            else
            {
                MessageBox.Show("No user found."); //if email is not in the database it'll just show a popup
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (txt_Email.Text.Trim() != "" && txt_Password.Text.Trim() != "")
            {
                CheckUserAndPassword();
            } else
            {
                MessageBox.Show("Please input your email and/or password.");
            }

            txt_Email.Clear();
            txt_Password.Clear();
        }

        private void btn_Signup_Click(object sender, EventArgs e)
        {
            UserSignUp userSignUp = new UserSignUp();
            userSignUp.ShowDialog();

            txt_Email.Clear();
            txt_Password.Clear();
        }
    }
}
