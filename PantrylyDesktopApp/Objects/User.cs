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
    public class User
    {
        //DATABASE
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet PantryDS = new DataSet();
        DataTable PantryDT = new DataTable();

        DataSet PantrylyUsersDS = new DataSet();
        DataTable PantrylyUsersDT = new DataTable();

        private void setConnection()
        {
            sql_con = new SQLiteConnection("Data Source = ../../Resources/PantrylyDB.db");
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

        //initializing variables
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Birthday { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } //no apostrophes please idk how to.

        public List<Pantry> pantries { get; private set; }
        public List<Checklist> checklists { get; private set; }

        //user constructor
        public User(string fname, string lname, string bday, string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            FirstName = fname;
            LastName = lname;
            Birthday = bday;
        }
        
        /**
        public User GetCurrentUser(string id) //just in case i will need this set of codes..
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "Select * from Users where user_ID = '" + id + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            string fname, lname, bday, email, password;
            fname = PantrylyUsersDT.Rows[0][1].ToString();
            lname = PantrylyUsersDT.Rows[0][2].ToString();
            bday = PantrylyUsersDT.Rows[0][3].ToString();
            email = PantrylyUsersDT.Rows[0][4].ToString();
            password = PantrylyUsersDT.Rows[0][5].ToString();


            return new User(fname, lname, bday, email, password);
        }**/

        public string GetCurrentUserEmail()
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "Select * from Users where user_ID = '" + Id + "'"; //gets data row with the email input
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            return PantrylyUsersDT.Rows[0][4].ToString();
        }


        public void AddNewUser()//Add new user to database
        {
            string txtQuery = @"INSERT INTO Users (user_ID, user_FirstName, user_LastName, user_Birthday, user_Email, user_Password) 
                            VALUES('" + Id + "', '" + FirstName + "','" + LastName + "', '" + Birthday + "', '" + Email + "', '" + Password + "')";

            executeQuery(txtQuery);
            MessageBox.Show("Your account has been created.");
        }

        public List<Pantry> GetPantries(string userEmail)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM Pantry WHERE pantry_CreatorID = '"+userEmail+"'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantryDS.Reset();
            DB.Fill(PantryDS);
            PantryDT = PantryDS.Tables[0];

            for (int i = 0; i < PantryDT.Rows.Count; i++)
            {
                Pantry pantry = new Pantry(userEmail, PantryDT.Rows[i][1].ToString());
                pantries.Add(pantry);
            }
            
            return pantries;
        }

    }
}
