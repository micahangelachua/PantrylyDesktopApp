using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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

        DataSet UserChecklistDS = new DataSet();
        DataTable UserChecklistDT = new DataTable();

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
        public string UserID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Birthday { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; } //no apostrophes please idk how to.

        public List<Pantry> pantries { get; set; }
        public List<Checklist> checklists { get; set; }

        //user constructor
        public User(string fname, string lname, string bday, string email, string password)
        {
            Id = Guid.NewGuid();
            UserID = Id.ToString();
            Email = email;
            Password = password;
            FirstName = fname;
            LastName = lname;
            Birthday = bday;
        }

        public User(string id)
        {
            UserID = id;
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "Select * from Users where user_ID = '" + id + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            FirstName = PantrylyUsersDT.Rows[0][1].ToString();
            LastName = PantrylyUsersDT.Rows[0][2].ToString();
            Birthday = PantrylyUsersDT.Rows[0][3].ToString();
            Email = PantrylyUsersDT.Rows[0][4].ToString();
            Password = PantrylyUsersDT.Rows[0][5].ToString();
        }

        public void AddNewUser()//Add new user to database
        {
            string txtQuery = @"INSERT INTO Users (user_ID, user_FirstName, user_LastName, user_Birthday, user_Email, user_Password) 
                            VALUES('" + Id + "', '" + FirstName + "','" + LastName + "', '" + Birthday + "', '" + Email + "', '" + Password + "')";

            executeQuery(txtQuery);
            MessageBox.Show("Your account has been created.");
        }

        public List<Checklist> GetUserChecklists(string email)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM Checklist WHERE checklist_CreatorID = '" + email + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            UserChecklistDS.Reset();
            DB.Fill(UserChecklistDS);
            UserChecklistDT = UserChecklistDS.Tables[0];

            List<Checklist> checklists = new List<Checklist>();

            for (int i = 0; i < UserChecklistDT.Rows.Count; i++)
            {
                Checklist checklist = new Checklist(UserChecklistDT.Rows[i][0].ToString());
                checklists.Add(checklist);
            }

            return checklists;
        }

        public List<Pantry> GetUserPantries(string email)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM Pantry WHERE pantry_CreatorID = '" + email + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantryDS.Reset();
            DB.Fill(PantryDS);
            PantryDT = PantryDS.Tables[0];

            List<Pantry> pantries = new List<Pantry>();

            for (int i = 0; i < PantryDT.Rows.Count; i++)
            {
                Pantry pantry = new Pantry(PantryDT.Rows[i][0].ToString());
                pantries.Add(pantry);
            }

            return pantries;
        }

        public void UserInformationUpdate(string fname, string lname, string bday)
        {
            string txtQuery = @"UPDATE Users SET user_FirstName = '" + fname + "', user_LastName = '" + lname + "', " +
                "user_Birthday = '" + bday + "' WHERE user_ID = '" + UserID + "'";
             executeQuery(txtQuery);
            
        }
    }
}
