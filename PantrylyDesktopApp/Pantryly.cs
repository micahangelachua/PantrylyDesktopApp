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


        private void executeQuery(string txtQuery)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        //when credentials match:
        string LoggingUserId = "";
        private void LogInSuccess() //opens user's dashboard;
        {

            UserDashboard userDashboard = new UserDashboard(LoggingUserId);
        }

        private void Pantryly_Load(object sender, EventArgs e)
        {
            
        }
        

        //Check user log in credentials
        private bool CheckUser()
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "Select * from Users where user_Email = '" + txt_Email.Text + "' and user_Password = '"+txt_Password.Text+"'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            bool result;
            if (PantrylyUsersDT.Rows.Count == 1)
            {
                LoggingUserId = PantrylyUsersDT.Rows[0][1].ToString();
                result = true;
            } else
            {
                result = false;
            }

            return result;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {

        }

        private void btn_Signup_Click(object sender, EventArgs e)
        {
            UserSignUp userSignUp = new UserSignUp();
            userSignUp.ShowDialog();
        }
    }
}
