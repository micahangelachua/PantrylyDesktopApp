using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Forms;

namespace PantrylyDesktopApp
{
    public class Pantry
    {
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet PantryDS = new DataSet();
        DataTable PantryDT = new DataTable();

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
        public Guid Pantry_Id { get; set; }
        public string PantryName { get; set; }
        public List<PantryItems> PantryItems { get; set; }
        public string PantryOwnerEmail { get; set; }
        
        public Pantry(string ownerEmail, string name)
        {
            Pantry_Id = Guid.NewGuid();
            PantryName = name;
            PantryOwnerEmail = ownerEmail;
        }

        public void CreateNewPantry()
        {
            string txtQuery = @"INSERT INTO Pantry (pantry_ID, pantry_Name, pantry_User, pantry_CreatorID) 
                            VALUES('" + Pantry_Id + "', '" + PantryName + "','" + PantryOwnerEmail + "')";

            executeQuery(txtQuery);
            MessageBox.Show("Added a new pantry.");
        }
    }
}
