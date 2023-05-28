using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PantrylyDesktopApp
{
    public class PantryItems
    {
        #region
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet PantryItemsDS = new DataSet();
        DataTable PantryItemsDT = new DataTable();

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

        public Guid PantryItem_ID { get; set; }
        public string PantryItemID {  get; set; }
        public string PantryItemName { get; set; }
        public int PantryItem_Quantity { get; set; }
        public string PantryID {  get; set; }
        
        public PantryItems(string name, string id, int quantity)
        {
            PantryItem_ID = Guid.NewGuid();
            PantryItemID = PantryItem_ID.ToString();
            PantryItemName = name;
            PantryID = id;
            PantryItem_Quantity = quantity;
        }

        public PantryItems(string id)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM PantryItems WHERE pantryItems_ID = '" + id + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantryItemsDS.Reset();
            DB.Fill(PantryItemsDS);
            PantryItemsDT = PantryItemsDS.Tables[0];

            PantryItemID = PantryItemsDT.Rows[0][0].ToString();
            PantryItemName = PantryItemsDT.Rows[0][1].ToString();
            PantryItem_Quantity = int.Parse(PantryItemsDT.Rows[0][2].ToString());
            PantryID = PantryItemsDT.Rows[0][3].ToString();
        }
         
        public void AddItemToPantry()
        {
            string txtQuery = @"INSERT INTO PantryItems (pantryItems_ID, pantryItems_ItemName, pantryItems_Qty, pantryItems_PantryID)
                    VALUES ('" + PantryItem_ID + "', '" + PantryItemName + "','" + PantryItem_Quantity + "','" + PantryID + "')";
            executeQuery(txtQuery);
        }
        
        public void UpdatePantryItemName(string name)
        {
            string txtQuery = @"UPDATE PantryItems set pantryItems_ItemName = '" + name + "' WHERE  pantryItems_ID = '" + PantryItemID + "'";
        
            executeQuery(txtQuery);
        }

        public void DecreaseQty()
        {
            if (PantryItem_Quantity > 0) 
            {
                string txtQuery = @"UPDATE PantryItems set pantryItems_Qty = '" + (PantryItem_Quantity - 1) + "' WHERE  pantryItems_ID = '" + PantryItemID + "'";

                executeQuery(txtQuery);
            }
        }

        public void IncreaseQty()
        {
            string txtQuery = @"UPDATE PantryItems set pantryItems_Qty = '" + (PantryItem_Quantity + 1) + "' WHERE  pantryItems_ID = '" + PantryItemID + "'";

            executeQuery(txtQuery);
        }

        public void DeletePantryItem()
        {
            string txtQuery = @"DELETE FROM PantryItems WHERE pantryItem_ID = '"+PantryItemID+"'";
            executeQuery(txtQuery);
        }
    }
}
