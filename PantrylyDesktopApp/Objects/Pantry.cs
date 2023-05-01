﻿using System;
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
        public string Pantry_Name { get; set; }
        public List<PantryItems> PantryItems { get; set; }
        public string Pantry_CreatorEmail { get; set; }
        
        public Pantry(string ownerEmail, string name)
        {
            Pantry_Id = Guid.NewGuid();
            Pantry_Name = name;
            Pantry_CreatorEmail = ownerEmail;
        }

        public void CreateNewPantry()
        {
            string txtQuery = @"INSERT INTO Pantry (pantry_ID, pantry_Name, pantry_CreatorID) 
                            VALUES('" + Pantry_Id + "', '" + Pantry_Name + "','" + Pantry_CreatorEmail + "')";

            executeQuery(txtQuery);
            MessageBox.Show("Added a new pantry.");
        }

        /**
        public List<Pantry> GetPantries(string userEmail) //just in case i will need this set of codes..
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM Pantry WHERE pantry_CreatorID = '" + userEmail + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantryDS.Reset();
            DB.Fill(PantryDS);
            PantryDT = PantryDS.Tables[0];

            List<Pantry> pantries = new List<Pantry>();

            for (int i = 0; i < PantryDT.Rows.Count; i++)
            {
                Pantry pantry = new Pantry(userEmail, PantryDT.Rows[i][1].ToString());
                pantries.Add(pantry);
            }

            return pantries;
        }**/

    }
}
