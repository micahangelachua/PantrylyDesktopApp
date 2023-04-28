using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PantrylyDesktopApp
{
    public partial class Pantryly : Form
    {
        public Pantryly()
        {
            InitializeComponent();
            FormUtils.MakeWindowFormRounded(this);
            FormUtils.AddDraggableWindowTitle(pnl_WinTitleAndControls);
            FormUtils.MakeButtonRounded(btn_LoginClose);
            FormUtils.AddCloseButton(btn_LoginClose);
            FormUtils.MakeButtonRounded(btn_LoginMinimize);
            FormUtils.AddMinimizeButton(btn_LoginMinimize);
            FormUtils.MakeButtonRounded(btn_Signup);
            FormUtils.MakeButtonRounded(btn_Login);
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
            string CommandText = "Select * from Users where user_Email = '"+txt_Email.Text+"'"; //gets data row with the email input
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            if (PantrylyUsersDT.Rows.Count == 1)//row count will be 1 if there is one (there should only be one because emails should be unique)
            {
                LoggingUserId = PantrylyUsersDT.Rows[0][0].ToString(); //assigns the userid that will be send to the next window form.
                
                string userPassword = PantrylyUsersDT.Rows[0][5].ToString(); //variable to compare the input password and the one in the database

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
