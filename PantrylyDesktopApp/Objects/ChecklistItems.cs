using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantrylyDesktopApp
{
    public class ChecklistItems
    {
        #region
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet ChecklistItemDS = new DataSet();
        DataTable ChecklistItemDT = new DataTable();

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
        #endregion

        public Guid ChecklistItem_ID { get; set; }
        public string Description { get; set; }
        public string ChecklistItem_ChecklistID { get; set; }
        public int ChecklistItem_isDone { get; set; }

        public ChecklistItems(string description, string checklist)
        {
            ChecklistItem_ID = Guid.NewGuid();
            Description = description;
            ChecklistItem_ChecklistID = checklist;
            ChecklistItem_isDone = 0;
        }

        public void AddItemToChecklist()
        {
            string txtQuery = @"INSERT INTO ChecklistItems (checklistItems_ID, checklistItems_Description, checklistItems_ChecklistID, checklistItems_isDone)
                            VALUES ('" + ChecklistItem_ID + "', '" + Description + "','" + ChecklistItem_ChecklistID + "','" + ChecklistItem_isDone + "')";

            executeQuery(txtQuery);
        }
    }
}
