using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PantrylyDesktopApp
{
    public class Checklist
    {
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet ChecklistDS = new DataSet();
        DataTable ChecklistDT = new DataTable();

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

        public Guid Checklist_Id { get; set; }
        public string Checklist_Name { get; set; }
        public List<PantryItems> ChecklistItems { get; set; }
        public string Checklist_CreatorEmail { get; set; }
        public Checklist(string name, string email) 
        {
            Checklist_Id = Guid.NewGuid();
            Checklist_Name = name;
            Checklist_CreatorEmail = email;
        }

        public void CreateNewChecklist()
        {
            string txtQuery = @"INSERT INTO Checklist (checklist_ID, checklist_Name, checklist_CreatorID) 
                            VALUES('" + Checklist_Id + "', '" + Checklist_Name + "','" + Checklist_CreatorEmail + "')";

            executeQuery(txtQuery);
            MessageBox.Show("Added a new checklist.");
        }
    }
}
