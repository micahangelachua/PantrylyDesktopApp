using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace PantrylyDesktopApp
{
    public class ChecklistItems
    {
        #region
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet ChecklistItemsDS = new DataSet();
        DataTable ChecklistItemsDT = new DataTable();

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
        public string ChecklistItemID { get; set; }
        public string Description { get; set; }
        public string ChecklistItem_ChecklistID { get; set; }
        public int ChecklistItem_isDone { get; set; }

        public ChecklistItems(string description, string checklistID)
        {
            ChecklistItem_ID = Guid.NewGuid();
            ChecklistItemID = ChecklistItem_ID.ToString();
            Description = description;
            ChecklistItem_ChecklistID = checklistID;
            ChecklistItem_isDone = 0;
        }

        public ChecklistItems(string id) //constructor to get and set object properties
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM ChecklistItems WHERE checklistItems_ID = '" + id + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            ChecklistItemsDS.Reset();
            DB.Fill(ChecklistItemsDS);
            ChecklistItemsDT = ChecklistItemsDS.Tables[0];

            ChecklistItemID = ChecklistItemsDT.Rows[0][0].ToString();
            Description = ChecklistItemsDT.Rows[0][1].ToString();
            ChecklistItem_ChecklistID = ChecklistItemsDT.Rows[0][2].ToString();
            ChecklistItem_isDone = int.Parse(ChecklistItemsDT.Rows[0][3].ToString());
        }

        // Create:
        public void AddItemToChecklist()
        {
            string txtQuery = @"INSERT INTO ChecklistItems (checklistItems_ID, checklistItems_Description, checklistItems_ChecklistID, checklistItems_isDone)
                            VALUES ('" + ChecklistItem_ID + "', '" + Description + "','" + ChecklistItem_ChecklistID + "','" + ChecklistItem_isDone + "')";

            executeQuery(txtQuery);
        }

        // Update:
        public void UpdateName(string newDescription)
        {
            string txtQuery = @"UPDATE ChecklistItems set checklistItems_Description = '" + newDescription + "' WHERE checklistItems_ID = '" + ChecklistItemID + "'";

            executeQuery(txtQuery);
        }

        public void UpdateChecklistIsDone(int isDone)
        {
            string txtQuery = @"UPDATE ChecklistItems set checklistItems_isDone = '" + isDone + "' WHERE checklistItems_ID = '" + ChecklistItemID + "'";
            
            executeQuery(txtQuery);
        }

        public void Delete()
        {
            string txtQuery = "DELETE FROM ChecklistItems WHERE checklistItems_ID = '" + ChecklistItemID + "'";
            executeQuery(txtQuery);
        }
    }
}
