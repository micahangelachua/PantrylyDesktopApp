using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
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

            // make btn_SignupClose circle.
            GraphicsPath closePath = new GraphicsPath();
            closePath.AddEllipse(0, 0, btn_SignupClose.Width, btn_SignupClose.Height);
            btn_SignupClose.Region = new Region(closePath);

            // make btn_SignupMinimize circle.
            GraphicsPath minimizePath = new GraphicsPath();
            minimizePath.AddEllipse(0, 0, btn_SignupMinimize.Width, btn_SignupMinimize.Height);
            btn_SignupMinimize.Region = new Region(minimizePath);

            // Rounded corners for btn_SignUp
            GraphicsPath signUpButtonPath = new GraphicsPath();
            signUpButtonPath.AddEllipse(0, 0, btn_SignUp.Width, btn_SignUp.Height);
            btn_SignUp.Region = new Region(signUpButtonPath);
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

/*        private string UserIdGenerator()
        {

        }

        private void AddNewUser()//Add new user to database
        {
            string txtQuery = @"INSERT INTO Users (user_ID, user_FirstName, user_LastName, user_Birthday, user_Email, user_Password) VLAUES('"+User+"')";
        }*/

        private void btn_SignUp_Click(object sender, EventArgs e) 
        {
            
        }

        // Variables to use for dragging around window
        private bool isDragging = false;
        private Point lastCursorPos;

        private void pnl_WinTitleAndControls_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursorPos = Cursor.Position;
        }

        private void pnl_WinTitleAndControls_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point deltaCursorPos = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(this.Location.X + deltaCursorPos.X, this.Location.Y + deltaCursorPos.Y);
                lastCursorPos = Cursor.Position;
            }
        }

        private void pnl_WinTitleAndControls_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void btn_SignupClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_SignupMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    }
}
