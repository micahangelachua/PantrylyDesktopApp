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

        #region VARIABLES
        private Panel pnl_newPantry;
        private TextBox txt_newPantryName;
        private Label lbl_newPantryName;
        private Panel pnl_newChecklist;
        private TextBox txt_newChecklistName;
        private Label lbl_newChecklistName;

        private Panel pnl_Pantry;
        private Label lbl_PantryName;
        private TextBox txt_PantryId;

        private Panel pnl_Checklist;
        private Label lbl_ChecklistName;
        private TextBox txt_ChecklistId;

        private Panel pnl_ChecklistEntry;
        private Label lbl_ChecklistEntryName;
        private Label lbl_ChecklistEntryDateCreated;

        private CheckBox chk_NewChecklistItemName;
        private TextBox txt_NewChecklistItemName;
        private CheckBox chk_CrossedChecklistItemName;

        private Label lbl_ItemId;//will use to get each checklist item....

        private User currentUser;
        private List<Checklist> userChecklists = new List<Checklist>();
        private List<Pantry> userPantries = new List<Pantry>();

        private Checklist selectedChecklist;
        private List<ChecklistItems> selectedChecklist_ChecklistItems = new List<ChecklistItems>();
        private Pantry selectedPantry;
       

        #endregion
        //Constructor
        public UserDashboard(string id)
        {
            currentUser = new User(id); //method that assigns private variables for current user

            userChecklists = currentUser.GetUserChecklists(currentUser.Email); //Get user's checklists
            userPantries = currentUser.GetUserPantries(currentUser.Email); // Get user's pantries

            InitializeComponent();
            FormUtils.MakeWindowFormRounded(this);
            FormUtils.AddDraggableWindowTitle(pnl_WinTitleAndControls);
            FormUtils.AddCloseButton(pb_DashboardClose);
            FormUtils.AddMinimizeButton(pb_DashboardMinimize);

            lbl_UserFname.Text = currentUser.FirstName;
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            LoadPantries();
            LoadChecklist();
            LoadChecklistsEntries();
            //LoadChecklistItems();
        }

        #region Pantry
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
            lbl_newPantryName.Font = new Font("Ink Free", 16, FontStyle.Regular);
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

            foreach (Pantry pantry in userPantries)
            {
                pnl_Pantry = new Panel();
                pnl_Pantry.Size = new Size(250, 200);
                pnl_Pantry.BorderStyle = BorderStyle.None;
                pnl_Pantry.BackColor = ColorTranslator.FromHtml("#D9D9D9");

                lbl_PantryName = new Label();
                lbl_PantryName.Text = pantry.Pantry_Name;
                lbl_PantryName.Size = new Size(250, 37);
                lbl_PantryName.BackColor = ColorTranslator.FromHtml("#D4664E");
                lbl_PantryName.Font = new Font("Ink Free", 16, FontStyle.Regular);
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

                    Pantry newPantry = new Pantry(currentUser.Email, newName);
                    newPantry.CreateNewPantry();
                }
                else
                {
                    flp_PantriesContainer.Controls.Remove(pnl_newPantry);
                }
            }
        }
        #endregion

        #region Checklists
        //----------------- Checklist Dashboard : Start -------------------------
        private void LoadChecklist()
        {
            foreach (Checklist checklist in userChecklists)
            {
                pnl_Checklist = new Panel();
                pnl_Checklist.Size = new Size(250, 200);
                pnl_Checklist.BorderStyle = BorderStyle.None;
                pnl_Checklist.BackColor = ColorTranslator.FromHtml("#D9D9D9");

                lbl_ChecklistName = new Label();
                lbl_ChecklistName.Text = checklist.Checklist_Name;

                
                txt_ChecklistId = new TextBox();
                txt_ChecklistId.Size = new Size(250, 37);
                txt_ChecklistId.Enabled = false;
                txt_ChecklistId.Text = checklist.ChecklistID;
                txt_ChecklistId.Hide();
                

                lbl_ChecklistName.Size = new Size(250, 37);
                lbl_ChecklistName.BackColor = ColorTranslator.FromHtml("#31A78F");
                lbl_ChecklistName.Font = new Font("Ink Free", 16, FontStyle.Regular);
                lbl_ChecklistName.TextAlign = ContentAlignment.MiddleCenter;

                pnl_Checklist.Controls.Add(txt_ChecklistId);

                pnl_Checklist.Controls.Add(lbl_ChecklistName);
                
                flp_ChecklistsContainer.Controls.Add(pnl_Checklist);

                foreach (Panel panel in flp_ChecklistsContainer.Controls.OfType<Panel>())
                {
                    panel.Click += Panel_Click;
                    panel.Click += pb_Checklist_Click;
                }
            }
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = (Panel)sender;
            string checklistName = null;
            foreach (Control control in clickedPanel.Controls)
            {
                if (control is Label)
                {
                    checklistName = ((Label)control).Text;
                    break;
                }
            }

            foreach (Control control in clickedPanel.Controls)
            {
                if (control is TextBox)
                {   
                    //selectedChecklist_ChecklistItems.Clear();
                    
                    string checklistId = ((TextBox)control).Text;
                    selectedChecklist = new Checklist(checklistId);
                    
                    selectedChecklist_ChecklistItems = selectedChecklist.GetChecklistItems(checklistId);

                    ExpandSelectedChecklist(selectedChecklist);
                    break;
                }
            }

            Panel checklistEntryPanel = null;
            foreach (Control control in flp_ChecklistEntryContainer.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control innerControl in control.Controls)
                    {
                        if (innerControl is Label && ((Label)innerControl).Text == checklistName)
                        {
                            checklistEntryPanel = (Panel)control;
                            break;
                        }
                    }
                }
            }

            if (checklistEntryPanel != null)
            {
                foreach (Control control in flp_ChecklistEntryContainer.Controls)
                {
                    if (control is Panel)
                    {
                        control.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                    }
                }

                checklistEntryPanel.BackColor = ColorTranslator.FromHtml("#FFFF00");
                checklistEntryPanel.Refresh();
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
            lbl_newChecklistName.Font = new Font("Ink Free", 16, FontStyle.Regular);
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

                    Checklist newEntry = new Checklist(newName, currentUser.Email);
                    newEntry.CreateNewChecklist();
                }
                else
                {
                    pnl_newChecklist.Controls.Remove(txt_newChecklistName);
                }
            }
        }

        //----------------- Checklist Dashboard : End -------------------------

        //
        //
        //
        //----------------- Checklist TabPage : Start-------------------------
        private void LoadChecklistsEntries() 
        {
            foreach (Checklist checklist in userChecklists)
            {
                pnl_ChecklistEntry = new Panel();
                pnl_ChecklistEntry.Size = new Size(230, 40);
                pnl_ChecklistEntry.BorderStyle = BorderStyle.None;
                pnl_ChecklistEntry.BackColor = ColorTranslator.FromHtml("#D9D9D9");

                lbl_ChecklistEntryName = new Label();
                lbl_ChecklistEntryName.Text = checklist.Checklist_Name;
                lbl_ChecklistEntryName.Size = new Size(115, 25);
                lbl_ChecklistEntryName.BackColor = Color.Transparent;
                lbl_ChecklistEntryName.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                lbl_ChecklistEntryName.TextAlign = ContentAlignment.MiddleLeft;
                lbl_ChecklistEntryName.Location = new Point(0, 0);

                lbl_ChecklistEntryDateCreated = new Label();
                lbl_ChecklistEntryDateCreated.Text = checklist.Checklist_DateCreated;
                lbl_ChecklistEntryDateCreated.Size = new Size(115, 25);
                lbl_ChecklistEntryDateCreated.BackColor = Color.Transparent;
                lbl_ChecklistEntryDateCreated.Font = new Font("Ink Free", 12, FontStyle.Regular);
                lbl_ChecklistEntryDateCreated.TextAlign = ContentAlignment.MiddleLeft;
                lbl_ChecklistEntryDateCreated.Location = new Point(lbl_ChecklistEntryName.Right + 15, lbl_ChecklistEntryName.Top);

                pnl_ChecklistEntry.Controls.Add(lbl_ChecklistEntryName);
                pnl_ChecklistEntry.Controls.Add(lbl_ChecklistEntryDateCreated);

                flp_ChecklistEntryContainer.Controls.Add(pnl_ChecklistEntry);
            }
        }

        private void ExpandSelectedChecklist(Checklist checklist)
        {
            lbl_ChecklistDetailsName.Text = checklist.Checklist_Name;
            LoadChecklistItems();
        }

        #endregion

        #region ChecklistItems
        private void LoadChecklistItems()
        {
            foreach (ChecklistItems item in selectedChecklist_ChecklistItems)
            {
                CheckBox chk_NewChecklistItemName = new CheckBox();
                chk_NewChecklistItemName.AutoSize = true;
                chk_NewChecklistItemName.Text = item.Description;
                chk_NewChecklistItemName.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                chk_NewChecklistItemName.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
                chk_NewChecklistItemName.TextAlign = ContentAlignment.MiddleLeft;


                chk_NewChecklistItemName.CheckedChanged += checkBox_CheckedChanged;

                flp_ChecklistItems.Controls.Add(chk_NewChecklistItemName);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked)
            {
                flp_ChecklistItems.Controls.Remove(checkBox);

                flp_CrossedChecklistItems.Controls.Add(checkBox);
                checkBox.Font = new Font("Comic Sans MS", 14, FontStyle.Strikeout);
            }
            else
            {
                flp_CrossedChecklistItems.Controls.Remove(checkBox);

                flp_ChecklistItems.Controls.Add(checkBox);
                checkBox.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
            }
        }

        private void pb_AddItem_Click(object sender, EventArgs e)
        {
            txt_NewChecklistItemName = new TextBox();
            txt_NewChecklistItemName.Text = "Enter Item name...";
            txt_NewChecklistItemName.Size = new Size(250, 37);
            txt_NewChecklistItemName.Location = new Point(0, 0);

            chk_NewChecklistItemName = new CheckBox();
            chk_NewChecklistItemName.Text = "New Item";
            chk_NewChecklistItemName.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            chk_NewChecklistItemName.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
            chk_NewChecklistItemName.TextAlign = ContentAlignment.MiddleLeft;
            chk_NewChecklistItemName.Size = new Size(250, 37);
            chk_NewChecklistItemName.Location = new Point(0, 0);

            flp_ChecklistItems.Controls.Add(txt_NewChecklistItemName);
            
            txt_NewChecklistItemName.Focus();
            txt_NewChecklistItemName.KeyDown += new KeyEventHandler(txt_NewChecklistItemName_KeyDown);
        }

        private void txt_NewChecklistItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = txt_NewChecklistItemName.Text;
                txt_NewChecklistItemName.Dispose();
                if (!string.IsNullOrEmpty(newName))
                {
                    chk_NewChecklistItemName.Text = newName;

                    ChecklistItems checklistItem = new ChecklistItems(newName, selectedChecklist.ChecklistID);
                    checklistItem.AddItemToChecklist();

                    flp_ChecklistItems.Controls.Add(chk_NewChecklistItemName);
                    chk_NewChecklistItemName.CheckedChanged += checkBox_CheckedChanged;
                }
                else
                {
                    flp_ChecklistItems.Controls.Remove(txt_NewChecklistItemName);
                }
            }
        }
        #endregion

        #region SwitchingTabPages
        private void pb_Dashboard_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 0;
            pb_UserPicture.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#334E4C).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#FFE074).png";
        }

        private void pb_Checklist_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 1;
            pb_UserPicture.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#334E4C).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#FFE074).png";
        }

        private void pb_Settings_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 2;
            pb_UserPicture.BackColor = ColorTranslator.FromHtml("#334E4C");
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#334E4C).png";
        }

        private void pb_UserPicture_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 3;
            pb_UserPicture.BackColor = ColorTranslator.FromHtml("#D9D9D9");
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#FFE074).png";
        }
        #endregion
    }
}
