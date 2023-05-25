using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
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

        private Panel pnl_NewPantryItem;
        private TextBox txt_NewPantryItemName;
        private Label lbl_NewPantryItemName;
        private Label lbl_PantryItemQty;
        private PictureBox pb_DecPantryItem;
        private PictureBox pb_IncPantryItem;
        private PictureBox pb_Ellipsis;

        private Panel pnl_Pantry;
        private Label lbl_PantryName;

        private Panel pnl_Checklist;
        private Label lbl_ChecklistName;

        private Panel pnl_ChecklistEntry;
        private Label lbl_ChecklistEntryName;
        private Label lbl_ChecklistEntryDateCreated;

        private CheckBox chk_NewChecklistItemName;
        private TextBox txt_NewChecklistItemName;
        private CheckBox chk_CrossedChecklistItemName;

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
               
            }
        }

        //Show All Pantries of the current user
        private void LoadPantries()
        {
            userPantries.Clear();
            userPantries = currentUser.GetUserPantries(currentUser.Email);

            foreach (Control control in flp_PantriesContainer.Controls)
            {
                flp_PantriesContainer.Controls.OfType<Label>().ToList().ForEach(x => flp_PantriesContainer.Controls.Remove(x));
                flp_PantriesContainer.Controls.OfType<Panel>().ToList().ForEach(x => flp_PantriesContainer.Controls.Remove(x));
            }

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
                pnl_Pantry.Click += pnl_Pantry_Click;
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
                    LoadChecklist();
                }
                else
                {
                    flp_PantriesContainer.Controls.Remove(pnl_newPantry);
                }
            }
        }

        /*
         * TODO: Dynamically add panels inside a FlowLayoutPanel for pantry items.
         * FlowLayoutPanel:
         *   [
         *     - Pantry Name (Label)
         *     - Ellipsis/MoreOptions Button (PictureBox)
         *     - Panel:
         *         [
         *           - Item Name (Label)
         *           - Item Quantity (Label)
         *           - Unit of Measurement(?) (Label)
         *           - Add Button (PictureBox)
         *           - Deduct Button (PictureBox)
         *           - Ellipsis/MoreOptions Button (PictureBox)
         *         ]
         *   ]
         */
        // Event for clicking on a pantry panel in Dashboard.
        private void pnl_Pantry_Click(object sender, EventArgs e)
        {
            lbl_UserOverviewPantries.Visible = false;
            lbl_UserOverviewChecklists.Visible = false;
            flp_PantriesContainer.Visible = false;
            flp_ChecklistsContainer.Visible = false;

            Panel pantryPanel = new Panel();
            pantryPanel.BackColor = Color.White;
            pantryPanel.Dock = DockStyle.Fill;

            Label pantryNameLabel = new Label();
            pantryNameLabel.Text = ((Panel)sender).Controls[0].Text; // Get the pantry name from the clicked panel
            pantryNameLabel.Font = new Font("Arial", 20, FontStyle.Bold);
            pantryNameLabel.AutoSize = true;
            pantryNameLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            Button closeButton = new Button();
            closeButton.Text = "X";
            closeButton.Font = new Font("Arial", 16, FontStyle.Bold);
            closeButton.AutoSize = true;
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.Click += new EventHandler(CloseButton_Click);

            pantryPanel.Controls.Add(pantryNameLabel);
            pantryPanel.Controls.Add(closeButton);

            tp_UserOverview.Controls.Add(pantryPanel);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            // Remove the pantry details panel from the user overview tab page
            tp_UserOverview.Controls.RemoveAt(tp_UserOverview.Controls.Count - 1);

            lbl_UserOverviewPantries.Visible = true;
            lbl_UserOverviewChecklists.Visible = true;
            flp_PantriesContainer.Visible = true;
            flp_ChecklistsContainer.Visible = true;
        }
        #endregion
        

        #region Checklists
        //----------------- Checklist Dashboard : Start -------------------------
        private void LoadChecklist()
        {
            userChecklists.Clear();
            userChecklists = currentUser.GetUserChecklists(currentUser.Email);

            foreach (Control control in flp_ChecklistsContainer.Controls)
            {
                flp_ChecklistsContainer.Controls.OfType<Label>().ToList().ForEach(x => flp_ChecklistsContainer.Controls.Remove(x));
                flp_ChecklistsContainer.Controls.OfType<Panel>().ToList().ForEach(x => flp_ChecklistsContainer.Controls.Remove(x));
            }

            foreach (Checklist checklist in userChecklists)
            {
                pnl_Checklist = new Panel();
                pnl_Checklist.Size = new Size(250, 200);
                pnl_Checklist.BorderStyle = BorderStyle.None;
                pnl_Checklist.BackColor = ColorTranslator.FromHtml("#D9D9D9");

                lbl_ChecklistName = new Label();
                lbl_ChecklistName.Text = checklist.Checklist_Name;
                lbl_ChecklistName.Tag = checklist.ChecklistID;
                
                lbl_ChecklistName.Size = new Size(250, 37);
                lbl_ChecklistName.BackColor = ColorTranslator.FromHtml("#31A78F");
                lbl_ChecklistName.Font = new Font("Ink Free", 16, FontStyle.Regular);
                lbl_ChecklistName.TextAlign = ContentAlignment.MiddleCenter;

                pnl_Checklist.Controls.Add(lbl_ChecklistName);
                
                flp_ChecklistsContainer.Controls.Add(pnl_Checklist);

                foreach (Panel panel in flp_ChecklistsContainer.Controls.OfType<Panel>())
                {
                    panel.Click += pb_Checklist_Click;
                    panel.Click += pnl_Checklist_Click;
                }
            }
        }

        private void pnl_Checklist_Click(object sender, EventArgs e)
        {
            LoadChecklistsEntries();
            Panel clickedPanel = (Panel)sender;
            string checklistName = null;
            foreach (Control control in clickedPanel.Controls)
            {
                if (control is Label)
                {
                    checklistName = ((Label)control).Text;
                    string checklistId = ((Label)control).Tag.ToString();
                    selectedChecklist = new Checklist(checklistId);

                    /*
                     * HOW THIS WORKS:
                     * - flp_ChecklistItems.Controls - return collection of all controls in this FlowLayoutPanel.
                     * - OfType<CheckBox> - works as a filter, so return just the checkboxes.
                     * - ToList() - place filtered collection in a list.
                     * - ForEach(x => flp_ChecklistItems.Controls.Remove(x)) - pretty much a foreach loop for removing the
                     * filtered collection, in this case it's the checkboxes.
                     */
                    flp_ChecklistItems.Controls.OfType<CheckBox>().ToList().ForEach(x => flp_ChecklistItems.Controls.Remove(x));
                    flp_ChecklistItems.Controls.OfType<Label>().ToList().ForEach(x => flp_ChecklistItems.Controls.Remove(x));
                    flp_CrossedChecklistItems.Controls.OfType<CheckBox>().ToList().ForEach(x => flp_CrossedChecklistItems.Controls.Remove(x));
                    flp_CrossedChecklistItems.Controls.OfType<Label>().ToList().ForEach(x => flp_CrossedChecklistItems.Controls.Remove(x));

                    selectedChecklist_ChecklistItems = selectedChecklist.GetChecklistItems(checklistId);

                    ExpandSelectedChecklist(selectedChecklist);

                    break;
                }
            }

            /*
             * HOW THIS WORKS:
             * - panelsToClear - list of Panel controls.
             * - FirstOrDefault() - return only the first Panel that matches the specified condition. In this case it's
             * any label control that has a "Text" property whose value is the same as checklistName.
             */
            var panelsToClear = flp_ChecklistEntryContainer.Controls.OfType<Panel>().ToList();
            var checklistEntryPanel = panelsToClear.FirstOrDefault(x => x.Controls.OfType<Label>().Any(label => label.Text == checklistName));
            panelsToClear.ForEach(x => x.BackColor = ColorTranslator.FromHtml("#D9D9D9"));
            if (checklistEntryPanel != null)
            {
                checklistEntryPanel.BackColor = Color.FromArgb(128, 255, 224, 116);
            }
            /*
             * ?.Refresh() - a null-conditional operator, bale only execute this if checklistEntryPanel
             * is not null.
             */
            checklistEntryPanel?.Refresh();
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

                    LoadChecklist(); 
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
            userChecklists.Clear();
            userChecklists = currentUser.GetUserChecklists(currentUser.Email);

            foreach (Control control in flp_ChecklistEntryContainer.Controls)
            {
                flp_ChecklistEntryContainer.Controls.OfType<Label>().ToList().ForEach(x => flp_ChecklistEntryContainer.Controls.Remove(x));
                flp_ChecklistEntryContainer.Controls.OfType<Panel>().ToList().ForEach(x => flp_ChecklistEntryContainer.Controls.Remove(x));
            }

            foreach (Checklist checklist in userChecklists)
            {
                pnl_ChecklistEntry = new Panel();
                pnl_ChecklistEntry.Size = new Size(244, 30);
                pnl_ChecklistEntry.BorderStyle = BorderStyle.None;
                pnl_ChecklistEntry.BackColor = ColorTranslator.FromHtml("#D9D9D9");

                pnl_ChecklistEntry.Tag = checklist.ChecklistID;

                lbl_ChecklistEntryName = new Label();
                lbl_ChecklistEntryName.Text = checklist.Checklist_Name;
                lbl_ChecklistEntryName.Size = new Size(115, 25);
                lbl_ChecklistEntryName.BackColor = Color.Transparent;
                lbl_ChecklistEntryName.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                lbl_ChecklistEntryName.TextAlign = ContentAlignment.MiddleLeft;
                lbl_ChecklistEntryName.Location = new Point(0, 0);

                lbl_ChecklistEntryName.Tag = checklist.ChecklistID;

                lbl_ChecklistEntryDateCreated = new Label();
                lbl_ChecklistEntryDateCreated.Text = checklist.Checklist_DateCreated;
                lbl_ChecklistEntryDateCreated.Size = new Size(115, 25);
                lbl_ChecklistEntryDateCreated.BackColor = Color.Transparent;
                lbl_ChecklistEntryDateCreated.Font = new Font("Ink Free", 12, FontStyle.Regular);
                lbl_ChecklistEntryDateCreated.TextAlign = ContentAlignment.MiddleRight;
                lbl_ChecklistEntryDateCreated.Location = new Point(lbl_ChecklistEntryName.Right + 15, lbl_ChecklistEntryName.Top);

                pnl_ChecklistEntry.Controls.Add(lbl_ChecklistEntryName);
                pnl_ChecklistEntry.Controls.Add(lbl_ChecklistEntryDateCreated);

                flp_ChecklistEntryContainer.Controls.Add(pnl_ChecklistEntry);

                foreach (Panel panel in flp_ChecklistEntryContainer.Controls.OfType<Panel>())
                {
                    panel.Click += pnl_Checklist_Click;
                }
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
            selectedChecklist_ChecklistItems.Clear();
            selectedChecklist_ChecklistItems = selectedChecklist.GetChecklistItems(selectedChecklist.ChecklistID);

            foreach (Control control in flp_ChecklistItems.Controls)
            {
                flp_ChecklistItems.Controls.OfType<CheckBox>().ToList().ForEach(x => flp_ChecklistItems.Controls.Remove(x));
            }
            foreach (Control control in flp_CrossedChecklistItems.Controls)
            {
                flp_CrossedChecklistItems.Controls.OfType<CheckBox>().ToList().ForEach(x => flp_CrossedChecklistItems.Controls.Remove(x));
            }

            foreach (ChecklistItems item in selectedChecklist_ChecklistItems)
            {
                CheckBox chk_NewChecklistItemName = new CheckBox();
                chk_NewChecklistItemName.AutoSize = true;
                chk_NewChecklistItemName.Text = item.Description;
                chk_NewChecklistItemName.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                
                chk_NewChecklistItemName.TextAlign = ContentAlignment.MiddleLeft;
                chk_NewChecklistItemName.Tag = item.ChecklistItemID;

                chk_NewChecklistItemName.CheckedChanged += checkBox_CheckedChanged;
                

                if (item.ChecklistItem_isDone == 0) 
                {
                    chk_NewChecklistItemName.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
                    flp_ChecklistItems.Controls.Add(chk_NewChecklistItemName);
                } 
                else 
                {
                    chk_NewChecklistItemName.Font = new Font("Comic Sans MS", 14, FontStyle.Strikeout);
                    flp_CrossedChecklistItems.Controls.Add(chk_NewChecklistItemName);
                }
                
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            ChecklistItems item = new ChecklistItems(checkBox.Tag.ToString());
            if (checkBox.Checked)
            {
                item.UpdateChecklistIsDone(1);
                flp_ChecklistItems.Controls.Remove(checkBox);

                flp_CrossedChecklistItems.Controls.Add(checkBox);
                checkBox.Font = new Font("Comic Sans MS", 14, FontStyle.Strikeout);
            }
            else
            {
                item.UpdateChecklistIsDone(0);
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
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#334E4C).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#FFE074).png";
        }

        private void pb_Checklist_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 1;
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#334E4C).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#FFE074).png";
        }

        private void pb_Settings_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 2;
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#334E4C).png";
        }

        #endregion

        private void pb_ChecklistDelete_Click(object sender, EventArgs e)
        {
            selectedChecklist.DeleteChecklistItems();
            selectedChecklist.DeleteChecklist();

            LoadChecklist();
            LoadChecklistsEntries();

            selectedChecklist = new Checklist(userChecklists[0].ChecklistID);
            ExpandSelectedChecklist(selectedChecklist);
        }
    }
}
