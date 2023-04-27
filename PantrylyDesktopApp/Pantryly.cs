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
            // TODO: Make the rounded edges smoother (anti-aliasing?)
            // Rounded corners for the form 
            GraphicsPath path = new GraphicsPath();
            int arcSize = 20;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            path.AddArc(rect.X, rect.Y, arcSize, arcSize, 180, 90);
            path.AddArc(rect.X + rect.Width - arcSize, rect.Y, arcSize, arcSize, 270, 90);
            path.AddArc(rect.X + rect.Width - arcSize, rect.Y + rect.Height - arcSize, arcSize, arcSize, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - arcSize, arcSize, arcSize, 90, 90);
            Region = new Region(path);
            this.FormBorderStyle = FormBorderStyle.None;

            // make btnLoginClose circle.
            GraphicsPath closePath = new GraphicsPath();
            closePath.AddEllipse(0, 0, btn_LoginClose.Width, btn_LoginClose.Height);
            btn_LoginClose.Region = new Region(closePath);

            // make btnLoginMinimize circle.
            GraphicsPath minimizePath = new GraphicsPath();
            minimizePath.AddEllipse(0, 0, btn_LoginMinimize.Width, btn_LoginMinimize.Height);
            btn_LoginMinimize.Region = new Region(minimizePath);

            /* 
             * These are too stretched out
             */
            /* Rounded corners for txtEmail
            GraphicsPath emailPath = new GraphicsPath();
            emailPath.AddEllipse(0, 0, txt_Email.Width, txt_Email.Height);
            txt_Email.Region = new Region(emailPath);
            // Rounded corners for txtPassword
            GraphicsPath passwordPath = new GraphicsPath();
            passwordPath.AddEllipse(0, 0, txt_Password.Width, txt_Password.Height);
            txt_Password.Region = new Region(passwordPath);*/

            // Rounded corners for btn_Login
            GraphicsPath loginButtonPath = new GraphicsPath();
            loginButtonPath.AddEllipse(0, 0, btn_Login.Width, btn_Login.Height);
            btn_Login.Region = new Region(loginButtonPath);

            // Rounded corners for btn_Signup
            GraphicsPath signUpButtonPath = new GraphicsPath();
            signUpButtonPath.AddEllipse(0, 0, btn_Signup.Width, btn_Signup.Height);
            btn_Signup.Region = new Region(signUpButtonPath);
        }

        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet PantrylyUsersDS = new DataSet();
        DataTable PantrylyUsersDT = new DataTable();

        private void setConnection()
        {
            sql_con = new SQLiteConnection("Data Source = PantrylyDB.db; Version = 3; New = False; Compress = True;");
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
                LoggingUserId = PantrylyUsersDT.Rows[0][1].ToString(); //assigns the userid that will be send to the next window form.
                //we should create a user class for this to be safer but idk...

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

        // Variables to use for dragging around window
        private bool isDragging = false;
        private Point lastCursorPos;

        /* 
         * When mouse is over pnlWinTitleAndControls and a button is clicked, 
         * set isDragging to true and store location of cursor in lastCursorPos.
         */
        private void pnl_WinTitleAndControls_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursorPos = Cursor.Position;
        }

        /*
         * If isDragging is true and the mouse is moved while a button is held,
         * calculate the difference between current cursor position and lastCursorPos 
         * and use the result to calculate and update the position of the Windows Form
         * on the screen and update and store new cursor location to be used again
         * for calculation should the user drag the window(in this case, the panel) again.
         */
        private void pnl_WinTitleAndControls_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point deltaCursorPos = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(this.Location.X + deltaCursorPos.X, this.Location.Y + deltaCursorPos.Y);
                lastCursorPos = Cursor.Position;
            }
        }

        /*
         * When the user is no longer clicking or holding a mouse button, set
         * isDragging to false to indicate that the user no longer wants to move
         * around the Window Form.
         */
        private void pnl_WinTitleAndControls_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void btn_LoginClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_LoginMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
