using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PantrylyDesktopApp
{
    public class Checklist
    {
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet ChecklistDS = new DataSet();
        DataTable ChecklistDT = new DataTable();

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

        public Guid Checklist_Id { get; set; }
        public string ChecklistID { get; set; }
        public string Checklist_Name { get; set; }
        public List<ChecklistItems> ChecklistItems { get; set; }
        public string Checklist_CreatorEmail { get; set; }
        public string Checklist_DateCreated { get; set; }
        
        public Checklist(string name, string email) //constructor used to create checklist
        {
            Checklist_Id = Guid.NewGuid();
            ChecklistID = Checklist_Id.ToString();
            Checklist_Name = name;
            Checklist_CreatorEmail = email;
            Checklist_DateCreated = DateTime.Now.ToString("dd'/'MM'/'yyyy");
        }

        public Checklist()
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM Checklist WHERE checklist_CreatorID = '" + Checklist_CreatorEmail + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            ChecklistDS.Reset();
            DB.Fill(ChecklistDS);
            ChecklistDT = ChecklistDS.Tables[0];

            ChecklistID = ChecklistDT.Rows[0][0].ToString();
            Checklist_Name = ChecklistDT.Rows[0][1].ToString();
            Checklist_CreatorEmail = ChecklistDT.Rows[0][2].ToString();
            Checklist_DateCreated = ChecklistDT.Rows[0][3].ToString();
        }

        public Checklist(string id) //constructor to get specific checklist, idk if this will be used...
        {
            ChecklistID = id;
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "Select * from Checklist where checklist_ID = '" + ChecklistID + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            ChecklistDS.Reset();
            DB.Fill(ChecklistDS);
            ChecklistDT = ChecklistDS.Tables[0];

            Checklist_Name = ChecklistDT.Rows[0][1].ToString();
            Checklist_CreatorEmail = ChecklistDT.Rows[0][2].ToString();
            Checklist_DateCreated = ChecklistDT.Rows[0][3].ToString();
        }
        
        
        public void CreateNewChecklist()
        {
            string txtQuery = @"INSERT INTO Checklist (checklist_ID, checklist_Name, checklist_CreatorID, checklist_DateCreated) 
                            VALUES('" + Checklist_Id + "', '" + Checklist_Name + "','" + Checklist_CreatorEmail + "','" + Checklist_DateCreated + "')";

            executeQuery(txtQuery);
            MessageBox.Show("Added a new checklist.");
        }

        public void UpdateTitleChecklist(string newTitle, string id)
        {
            string txtQuery = "UPDATE Checklist set checklist_Name = '"+newTitle+ "' WHERE checklist_ID = '"+id+"' ";
            
            executeQuery(txtQuery);
        }

        public List<ChecklistItems> GetChecklistItems(string checklistId)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM ChecklistItems WHERE checklistItems_ChecklistID = '" + checklistId + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            ChecklistItemsDS.Reset();
            DB.Fill(ChecklistItemsDS);
            ChecklistItemsDT = ChecklistItemsDS.Tables[0];

            List<ChecklistItems> checklistItems = new List<ChecklistItems>();

            for (int i = 0; i < ChecklistItemsDT.Rows.Count; i++)
            {
                ChecklistItems checklistItem = new ChecklistItems(ChecklistItemsDT.Rows[i][0].ToString());
                checklistItems.Add(checklistItem);
            }

            return checklistItems;
        }

        //Delete
        public void DeleteChecklistItems()
        {
            string txtQuery = "DELETE FROM ChecklistItems WHERE checklistItems_ChecklistID = '" + ChecklistID + "'";
            executeQuery(txtQuery);
        }

        public void DeleteChecklist()
        {
            string txtQuery = "DELETE FROM Checklist WHERE checklist_ID = '" + ChecklistID + "'";
            executeQuery(txtQuery);
        }
    }
}
