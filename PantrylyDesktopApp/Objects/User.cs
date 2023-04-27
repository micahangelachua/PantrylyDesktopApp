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

        //initializing variables
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Birthday { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        
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


        public void AddNewUser()//Add new user to database
        {
            string txtQuery = @"INSERT INTO Users (user_ID, user_FirstName, user_LastName, user_Birthday, user_Email, user_Password) 
                            VALUES('" + Id + "', '" + FirstName + "','" + LastName + "', '" + Birthday + "', '" + Email + "', '" + Password + "')";

            executeQuery(txtQuery);
            MessageBox.Show("Your account has been created.");
        }
    }
}
