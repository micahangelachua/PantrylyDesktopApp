using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PantrylyDesktopApp
{
    public partial class UserDashboard : Form
    {
        #region DATABASE
        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet UserPantriesDS = new DataSet();
        DataTable UserPantriesDT = new DataTable();

        DataSet UserChecklistDS = new DataSet();
        DataTable UserChecklistDT = new DataTable();

        DataSet PantrylyUsersDS = new DataSet();
        DataTable PantrylyUsersDT = new DataTable();
        #endregion

        #region VARIABLES
        private Panel pnl_newPantry;
        private TextBox txt_newPantryName;
        private Label lbl_newPantryName;
        private Panel pnl_newChecklist;
        private TextBox txt_newChecklistName;
        private Label lbl_newChecklistName;

        private Panel pnl_Pantry;
        private Label lbl_PantryName;
        private Panel pnl_Checklist;
        private Label lbl_ChecklistName;
        

        private string currentUser_Id, currentUser_Fname, currentUser_Email; //i don't know is this a proper way of doing this.
        #endregion

        
        private void GetCurrentUser(string id) //just in case i will need this set of codes..
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "Select * from Users where user_ID = '" + id + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            PantrylyUsersDS.Reset();
            DB.Fill(PantrylyUsersDS);
            PantrylyUsersDT = PantrylyUsersDS.Tables[0];

            currentUser_Id = PantrylyUsersDT.Rows[0][0].ToString();
            currentUser_Fname = PantrylyUsersDT.Rows[0][1].ToString();
            currentUser_Email = PantrylyUsersDT.Rows[0][4].ToString();
        }
        #region ONLOAD_METHODS
        public List<Pantry> GetPantries(string userEmail)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM Pantry WHERE pantry_CreatorID = '" + userEmail + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            UserPantriesDS.Reset();
            DB.Fill(UserPantriesDS);
            UserPantriesDT = UserPantriesDS.Tables[0];

            List<Pantry> pantries = new List<Pantry>();

            for (int i = 0; i < UserPantriesDT.Rows.Count; i++)
            {
                Pantry pantry = new Pantry(userEmail, UserPantriesDT.Rows[i][1].ToString());
                pantries.Add(pantry);
            }

            return pantries;
        }

        public List<Checklist> GetChecklists(string userEmail)
        {
            setConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM Checklist WHERE checklist_CreatorID = '" + userEmail + "'";
            DB = new SQLiteDataAdapter(CommandText, sql_con);

            UserChecklistDS.Reset();
            DB.Fill(UserChecklistDS);
            UserChecklistDT = UserChecklistDS.Tables[0];

            List<Checklist> checklists = new List<Checklist>();

            for (int i = 0; i < UserChecklistDT.Rows.Count; i++)
            {
                Checklist checklist = new Checklist(UserChecklistDT.Rows[i][1].ToString(), userEmail);
                checklists.Add(checklist);
            }

            return checklists;
        }
        #endregion
        
        //Constructor
        public UserDashboard(string id)
        {
            GetCurrentUser(id); //method that assigns private variables for current user
            
            
            InitializeComponent();
            FormUtils.MakeWindowFormRounded(this);
            FormUtils.AddDraggableWindowTitle(pnl_WinTitleAndControls);
            FormUtils.MakeButtonRounded(btn_DashboardClose);
            FormUtils.AddCloseButton(btn_DashboardClose);
            FormUtils.MakeButtonRounded(btn_DashboardMinimize);
            FormUtils.AddMinimizeButton(btn_DashboardMinimize);

            lbl_UserFname.Text = currentUser_Fname;
        }
        
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

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            LoadPantries();
            LoadChecklist();
        }

        private void pb_AddNewPantry_Click(object sender, EventArgs e)
        {
            pnl_newPantry = new Panel();
            pnl_newPantry.Size = new Size(250, 200);
            pnl_newPantry.BorderStyle = BorderStyle.None;
            pnl_newPantry.BackColor = ColorTranslator.FromHtml("#D9D9D9");

            txt_newPantryName = new TextBox();
            txt_newPantryName.Text = "Enter Pantry name...";
            txt_newPantryName.Size = new Size(250, 37);
            txt_newPantryName.Location = new Point(0, 0);

            lbl_newPantryName = new Label();
            lbl_newPantryName.Text = "New Pantry";
            lbl_newPantryName.Size = new Size(250, 37);
            lbl_newPantryName.BackColor = ColorTranslator.FromHtml("#D4664E");
            lbl_newPantryName.Font = new Font("Comic Sans MS", 16, FontStyle.Regular);
            lbl_newPantryName.TextAlign = ContentAlignment.MiddleCenter;

            pnl_newPantry.Controls.Add(txt_newPantryName);     

            // Add the new panel to the FlowLayoutPanel
            DialogResult result = MessageBox.Show("Are you sure you want to create a new Pantry?", "New Pantry", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                flp_PantriesContainer.Controls.Add(pnl_newPantry);
                txt_newPantryName.Focus();
                /*
                 * this is used to associate the KeyDown event handler with the KeyDown event of txt_newPantryName.KeyDown,
                 * if this isn't used, nothing happens after the user presses enter
                 */
                txt_newPantryName.KeyDown += new KeyEventHandler(txt_newPantryName_KeyDown);
                
                
                //LoadPantries(); instead of adding control; it needs to reload with the new data from database
            }
        }

        //Show All Pantries of the current user
        private void LoadPantries()
        {
            //flp_PantriesContainer.Controls.Clear();

            List<Pantry> pantries = GetPantries(currentUser_Email);

            foreach (Pantry pantry in pantries)
            {
                pnl_Pantry = new Panel();
                pnl_Pantry.Size = new Size(250, 200);
                pnl_Pantry.BorderStyle = BorderStyle.None;
                pnl_Pantry.BackColor = ColorTranslator.FromHtml("#D9D9D9");

                lbl_PantryName = new Label();
                lbl_PantryName.Text = pantry.Pantry_Name;
                lbl_PantryName.Size = new Size(250, 37);
                lbl_PantryName.BackColor = ColorTranslator.FromHtml("#D4664E");
                lbl_PantryName.Font = new Font("Comic Sans MS", 16, FontStyle.Regular);
                lbl_PantryName.TextAlign = ContentAlignment.MiddleCenter;

                pnl_Pantry.Controls.Add(lbl_PantryName);

                flp_PantriesContainer.Controls.Add(pnl_Pantry);
            }
        }
        

        private void txt_newPantryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = txt_newPantryName.Text;
                txt_newPantryName.Dispose();
                if (!string.IsNullOrEmpty(newName))
                {
                    lbl_newPantryName.Text = newName;
                    pnl_newPantry.Controls.Add(lbl_newPantryName);

                    Pantry newPantry = new Pantry(currentUser_Email, newName);
                    newPantry.CreateNewPantry();
                }
                else
                {
                    flp_PantriesContainer.Controls.Remove(pnl_newPantry);
                }
            }
        }

        private void LoadChecklist()
        {
            List<Checklist> checklists = GetChecklists(currentUser_Email);

            foreach (Checklist checklist in checklists)
            {
                pnl_Checklist = new Panel();
                pnl_Checklist.Size = new Size(250, 200);
                pnl_Checklist.BorderStyle = BorderStyle.None;
                pnl_Checklist.BackColor = ColorTranslator.FromHtml("#D9D9D9");

                lbl_ChecklistName = new Label();
                lbl_ChecklistName.Text = checklist.Checklist_Name;
                lbl_ChecklistName.Size = new Size(250, 37);
                lbl_ChecklistName.BackColor = ColorTranslator.FromHtml("#31A78F");
                lbl_ChecklistName.Font = new Font("Comic Sans MS", 16, FontStyle.Regular);
                lbl_ChecklistName.TextAlign = ContentAlignment.MiddleCenter;

                pnl_Checklist.Controls.Add(lbl_ChecklistName);

                flp_ChecklistsContainer.Controls.Add(pnl_Checklist);

            }
        }

        private void pb_AddNewChecklist_Click(object sender, EventArgs e)
        {
            pnl_newChecklist = new Panel();
            pnl_newChecklist.Size = new Size(250, 200);
            pnl_newChecklist.BorderStyle = BorderStyle.None;
            pnl_newChecklist.BackColor = ColorTranslator.FromHtml("#D9D9D9");

            txt_newChecklistName = new TextBox();
            txt_newChecklistName.Text = "Enter Checklist name...";
            txt_newChecklistName.Size = new Size(250, 37);
            txt_newChecklistName.Location = new Point(0, 0);

            lbl_newChecklistName = new Label();
            lbl_newChecklistName.Text = "New Checklist";
            lbl_newChecklistName.Size = new Size(250, 37);
            lbl_newChecklistName.BackColor = ColorTranslator.FromHtml("#31A78F");
            lbl_newChecklistName.Font = new Font("Comic Sans MS", 16, FontStyle.Regular);
            lbl_newChecklistName.TextAlign = ContentAlignment.MiddleCenter;

            pnl_newChecklist.Controls.Add(txt_newChecklistName);

            // Add the new panel to the FlowLayoutPanel
            DialogResult result = MessageBox.Show("Are you sure you want to create a new Checklist?", "New Checklist", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                flp_ChecklistsContainer.Controls.Add(pnl_newChecklist);
                txt_newChecklistName.Focus();

                txt_newChecklistName.KeyDown += new KeyEventHandler(txt_newChecklistName_KeyDown);
            }
        }

        private void txt_newChecklistName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = txt_newChecklistName.Text;
                txt_newChecklistName.Dispose();
                if (!string.IsNullOrEmpty(newName))
                {
                    lbl_newChecklistName.Text = newName;
                    pnl_newChecklist.Controls.Add(lbl_newChecklistName);

                    Checklist newEntry = new Checklist(newName, currentUser_Email);
                    newEntry.CreateNewChecklist();
                }
                else
                {
                    pnl_newChecklist.Controls.Remove(txt_newChecklistName);
                }
            }
        }

        // Switching tab pages
        private void pb_Dashboard_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 0;
            pb_Dashboard.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            pb_Pantry.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Checklist.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#334E4C).png";
            pb_Pantry.ImageLocation = "../../Resources/Icons/condiments(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
        }

       

        private void pb_Pantry_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 1;
            pb_Dashboard.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Pantry.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            pb_Checklist.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Pantry.ImageLocation = "../../Resources/Icons/condiments(#334E4C).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
        }

        private void pb_Checklist_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 2;
            pb_Dashboard.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Pantry.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Checklist.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Pantry.ImageLocation = "../../Resources/Icons/condiments(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#334E4C).png";
        }
    }
}
