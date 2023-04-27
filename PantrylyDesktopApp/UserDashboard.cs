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
    public partial class UserDashboard : Form
    {
        string currentUser_id = "";
        public UserDashboard(string user_id)
        {
            currentUser_id = user_id;
            InitializeComponent();
        }

        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet UserPantriesDS = new DataSet();
        DataTable UserPantriesDT = new DataTable();

        DataSet UserChecklistDS = new DataSet();
        DataTable UserChecklistDT = new DataTable();


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

        private void GetUserPantries(string user_id)
        {
            //each pantry has an FK of the user id. it will retrieve all pantries that have the user id as foreign key.
        }

        private void GetUserChecklist(string user_id)
        {

        }

        public void OpenPantry(string pantry_id)
        {
            
        }

    }
}
