using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private TextBox txt_PantryItemQty;
        private PictureBox pb_DecPantryItem;
        private PictureBox pb_IncPantryItem;
        private PictureBox pb_Ellipsis;
        private PictureBox pb_EditPantryItem;
        private PictureBox pb_DeletePantryItem;

        private Panel pnl_Pantry;
        private Label lbl_PantryName;

        private Panel pnl_Checklist;
        private Label lbl_ChecklistName;

        private Panel pnl_ChecklistEntry;
        private Label lbl_ChecklistEntryName;
        private Label lbl_ChecklistEntryDateCreated;

        private Panel pnl_NewChecklistItem;
        private CheckBox chk_NewChecklistItemName;
        private TextBox txt_NewChecklistItemName;
        private CheckBox chk_CrossedChecklistItemName;

        private User currentUser;
        private List<Checklist> userChecklists = new List<Checklist>();
        private List<Pantry> userPantries = new List<Pantry>();

        private Checklist selectedChecklist;
        private List<ChecklistItems> selectedChecklist_ChecklistItems = new List<ChecklistItems>();
        private Pantry selectedPantry;
        private List<PantryItems> selectedPantry_PantryItems = new List<PantryItems>();


        #endregion

        #region ONLOAD
        //Constructor
        public UserDashboard(string id)
        {
            currentUser = new User(id); //method that assigns private variables for current user

            InitializeComponent();
            FormUtils.MakeWindowFormRounded(this);
            FormUtils.AddDraggableWindowTitle(pnl_WinTitleAndControls);
            FormUtils.AddCloseButton(pb_DashboardClose);
            FormUtils.AddMinimizeButton(pb_DashboardMinimize);
            FormUtils.MakeButtonRounded(btn_EditUserInfo);
            FormUtils.MakeButtonRounded(btn_Cancel);
            FormUtils.MakeButtonRounded(btn_Confirm);

            lbl_UserFname.Text = currentUser.FirstName;
        
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            LoadPantries();
            LoadChecklist();
            LoadChecklistsEntries();
            pnl_AddPantryItem.Width = flowLayoutPanel1.ClientSize.Width - 50;
        }

        #endregion 

        #region Pantry
        //------------LOAD PANTRY
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
                pnl_Pantry.Tag = pantry.PantryID;

                lbl_PantryName = new Label();
                lbl_PantryName.Text = pantry.Pantry_Name;
                lbl_PantryName.Size = new Size(250, 37);
                lbl_PantryName.BackColor = ColorTranslator.FromHtml("#D4664E");
                lbl_PantryName.Font = new Font("Ink Free", 16, FontStyle.Regular);
                lbl_PantryName.TextAlign = ContentAlignment.MiddleCenter;
                lbl_PantryName.Tag = pantry.PantryID;

                pnl_Pantry.Controls.Add(lbl_PantryName);

                flp_PantriesContainer.Controls.Add(pnl_Pantry);
                pnl_Pantry.Click += pnl_Pantry_Click;
            }
        }

        //------------CREATE PANTRY
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
                    LoadPantries();
                }
                else
                {
                    flp_PantriesContainer.Controls.Remove(pnl_newPantry);
                }
            }
        }

        //------------EXPAND SELECTED PANTRY
        private void pnl_Pantry_Click(object sender, EventArgs e)
        {
            tc_UserDashboard.SelectedIndex = 3;
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#FFE074).png";
            Panel clickedPanel = (Panel)sender;
            string pantryName = null;

            foreach (Control control in clickedPanel.Controls)
            {
                if (control is Label)
                {
                    pantryName = ((Label)control).Text;
                    string pantryId = ((Label)control).Tag.ToString();
                    selectedPantry = new Pantry(pantryId);

                    ExpandSelectedPantry(selectedPantry);
                }
            }
        }

        private void ExpandSelectedPantry(Pantry pantry)
        {
            lbl_SelectedPantryName.Text = pantry.Pantry_Name;
            LoadPantryItems();
        }

        //------------DELETE PANTRY
        private void pb_DeleteSelectedPantry_Click(object sender, EventArgs e)
        {
            // Delete the selected Pantry
            selectedPantry.DeletePantry();
            LoadPantries();
            selectedPantry = null;
            selectedPantry_PantryItems.Clear();
            tc_UserDashboard.SelectedIndex = 0;
        }
        #endregion

        #region PantryItems
        //------------LOAD PANTRY ITEMS
        private void LoadPantryItems()
        {
            selectedPantry_PantryItems.Clear();
            selectedPantry_PantryItems = selectedPantry.GetPantryItems(selectedPantry.PantryID);

            foreach (Control control in flp_SelectedPantryItems.Controls)
            {
                flp_SelectedPantryItems.Controls.OfType<Panel>().ToList().ForEach(x => flp_SelectedPantryItems.Controls.Remove(x));
            }
            
            foreach(PantryItems item in selectedPantry_PantryItems)
            {
                pnl_NewPantryItem = new Panel();
                pnl_NewPantryItem.Size = new Size((flp_SelectedPantryItems.ClientSize.Width - 50), 100);
                pnl_NewPantryItem.BackColor = ColorTranslator.FromHtml("#FCF5EF");
                pnl_NewPantryItem.Margin = new Padding(0, 0, 0, 25);

                lbl_NewPantryItemName = new Label();
                lbl_NewPantryItemName.Size = new Size(250, 37);
                lbl_NewPantryItemName.Text = item.PantryItemName;
                lbl_NewPantryItemName.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
                lbl_NewPantryItemName.ForeColor = ColorTranslator.FromHtml("#334E4C");
                lbl_NewPantryItemName.Location = new Point((pnl_NewPantryItem.Width - lbl_NewPantryItemName.Width) / 2, 15);
                lbl_NewPantryItemName.TextAlign = ContentAlignment.MiddleCenter;

                lbl_PantryItemQty = new Label();
                lbl_PantryItemQty.Size = new Size(26, 30);
                lbl_PantryItemQty.Text = item.PantryItem_Quantity.ToString();
                lbl_PantryItemQty.Location = new Point((pnl_NewPantryItem.Width - lbl_PantryItemQty.Width) / 2, 56);
                lbl_PantryItemQty.Font = new Font("Comic Sans MS", 16, FontStyle.Regular);
                lbl_PantryItemQty.ForeColor = ColorTranslator.FromHtml("#334E4C");

                pb_DecPantryItem = new PictureBox();
                pb_DecPantryItem.ImageLocation = "../../Resources/Icons/minus(#334E4C).png";
                pb_DecPantryItem.Size = new Size(25, 25);
                pb_DecPantryItem.Location = new Point(lbl_PantryItemQty.Location.X - 50, 59);
                pb_DecPantryItem.SizeMode = PictureBoxSizeMode.Zoom;
                pb_DecPantryItem.Cursor = Cursors.Hand;
                pb_DecPantryItem.Tag = item.PantryItemID;

                pb_IncPantryItem = new PictureBox();
                pb_IncPantryItem.ImageLocation = "../../Resources/Icons/add(#334E4C).png";
                pb_IncPantryItem.Size = new Size(25, 25);
                pb_IncPantryItem.Location = new Point(lbl_PantryItemQty.Location.X + 50, 59);
                pb_IncPantryItem.SizeMode = PictureBoxSizeMode.Zoom;
                pb_IncPantryItem.Cursor = Cursors.Hand;
                pb_IncPantryItem.Tag = item.PantryItemID;

                pb_EditPantryItem = new PictureBox();
                pb_EditPantryItem.ImageLocation = "../../Resources/Icons/edit(#334e4c).png";
                pb_EditPantryItem.Size = new Size(25, 25);
                pb_EditPantryItem.Location = new Point(1000, 10);
                pb_EditPantryItem.SizeMode = PictureBoxSizeMode.Zoom;
                pb_EditPantryItem.Cursor = Cursors.Hand;
                pb_EditPantryItem.Tag = item.PantryItemID;

                pb_DeletePantryItem = new PictureBox();
                pb_DeletePantryItem.ImageLocation = "../../Resources/Icons/bin(#334e4c).png";
                pb_DeletePantryItem.Size = new Size(25, 25);
                pb_DeletePantryItem.Location = new Point(1030, 10);
                pb_DeletePantryItem.SizeMode = PictureBoxSizeMode.Zoom;
                pb_DeletePantryItem.Cursor = Cursors.Hand;
                pb_DeletePantryItem.Tag = item.PantryItemID;

                pnl_NewPantryItem.Controls.Add(lbl_NewPantryItemName);
                pnl_NewPantryItem.Controls.Add(lbl_PantryItemQty);
                pnl_NewPantryItem.Controls.Add(pb_DecPantryItem);
                pnl_NewPantryItem.Controls.Add(pb_IncPantryItem);
                pnl_NewPantryItem.Controls.Add(pb_EditPantryItem);
                pnl_NewPantryItem.Controls.Add(pb_DeletePantryItem);

                pb_DecPantryItem.Click += new EventHandler(pb_DecPantryItem_Click);
                pb_IncPantryItem.Click += new EventHandler(pb_IncPantryItem_Click);

                pb_DeletePantryItem.Click += new EventHandler(pb_DeletePantryItemClick);
                pb_EditPantryItem.Click += new EventHandler(pb_EditPantryItemClick);

                flp_SelectedPantryItems.Controls.Add(pnl_NewPantryItem);
            }
        }

        //------------CREATE PANTRY ITEM
        private void pb_AddPantryItem_Click(object sender, EventArgs e)
        {
            pnl_NewPantryItem = new Panel();
            pnl_NewPantryItem.Size = new Size(1129, 100);
            pnl_NewPantryItem.BackColor = ColorTranslator.FromHtml("#FCF5EF");
            pnl_NewPantryItem.Margin = new Padding(25, 25, 25, 25);
            pnl_NewPantryItem.Anchor = AnchorStyles.Left | AnchorStyles.Right;

            txt_NewPantryItemName = new TextBox();
            txt_NewPantryItemName.Text = "Enter Item name...";
            txt_NewPantryItemName.Size = new Size(250, 37);
            txt_NewPantryItemName.Location = new Point(442, 34);
            txt_NewPantryItemName.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);

            lbl_NewPantryItemName = new Label();
            lbl_NewPantryItemName.Size = new Size(250, 37);
            lbl_NewPantryItemName.Text = "New Item";
            lbl_NewPantryItemName.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
            lbl_NewPantryItemName.ForeColor = ColorTranslator.FromHtml("#334E4C");
            lbl_NewPantryItemName.Location = new Point((pnl_NewPantryItem.Width - lbl_NewPantryItemName.Width) / 2, 15);
            lbl_NewPantryItemName.TextAlign = ContentAlignment.MiddleCenter;

            txt_PantryItemQty = new TextBox();
            txt_PantryItemQty.Size = new Size(100, 37);
            txt_PantryItemQty.Text = "0";
            txt_PantryItemQty.Location = new Point(100, 34);
            txt_PantryItemQty.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);

            lbl_PantryItemQty = new Label();
            lbl_PantryItemQty.Size = new Size(26, 30);
            lbl_PantryItemQty.Text = "0";
            lbl_PantryItemQty.Location = new Point((pnl_NewPantryItem.Width - lbl_PantryItemQty.Width) / 2, 56);
            lbl_PantryItemQty.Font = new Font("Comic Sans MS", 16, FontStyle.Regular);
            lbl_PantryItemQty.ForeColor = ColorTranslator.FromHtml("#334E4C");

            pb_DecPantryItem = new PictureBox();
            pb_DecPantryItem.ImageLocation = "../../Resources/Icons/minus(#334E4C).png";
            pb_DecPantryItem.Size = new Size(25, 25);
            pb_DecPantryItem.Location = new Point(480, 59);
            pb_DecPantryItem.SizeMode = PictureBoxSizeMode.Zoom;
            pb_DecPantryItem.Cursor = Cursors.Hand;

            pb_IncPantryItem = new PictureBox();
            pb_IncPantryItem.ImageLocation = "../../Resources/Icons/add(#334E4C).png";
            pb_IncPantryItem.Size = new Size(25, 25);
            pb_IncPantryItem.Location = new Point(563, 59);
            pb_IncPantryItem.SizeMode = PictureBoxSizeMode.Zoom;
            pb_IncPantryItem.Cursor = Cursors.Hand;

            pb_EditPantryItem = new PictureBox();
            pb_EditPantryItem.ImageLocation = "../../Resources/Icons/edit(#334e4c).png";
            pb_EditPantryItem.Size = new Size(25, 25);
            pb_EditPantryItem.Location = new Point(1000, 10);
            pb_EditPantryItem.SizeMode = PictureBoxSizeMode.Zoom;
            pb_EditPantryItem.Cursor = Cursors.Hand;

            pb_DeletePantryItem = new PictureBox();
            pb_DeletePantryItem.ImageLocation = "../../Resources/Icons/bin(#334e4c).png";
            pb_DeletePantryItem.Size = new Size(25, 25);
            pb_DeletePantryItem.Location = new Point(1030, 10);
            pb_DeletePantryItem.SizeMode = PictureBoxSizeMode.Zoom;
            pb_DeletePantryItem.Cursor = Cursors.Hand;

            pnl_NewPantryItem.Controls.Add(txt_NewPantryItemName);
            pnl_NewPantryItem.Controls.Add(txt_PantryItemQty);

            pb_DecPantryItem.Click += new EventHandler(pb_DecPantryItem_Click);
            pb_IncPantryItem.Click += new EventHandler(pb_IncPantryItem_Click);

            DialogResult result = MessageBox.Show("Are you sure you want to add a new item?", "New Item", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                flp_SelectedPantryItems.Controls.Add(pnl_NewPantryItem);
                txt_NewPantryItemName.Focus();
                txt_NewPantryItemName.KeyDown += new KeyEventHandler(txt_NewPantryItemName_KeyDown);
                txt_PantryItemQty.KeyDown += new KeyEventHandler(txt_NewPantryItemName_KeyDown);
            }
        }

        private void txt_NewPantryItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string newName = txt_NewPantryItemName.Text;
                string qty = txt_PantryItemQty.Text;

                txt_NewPantryItemName.Dispose();
                txt_PantryItemQty.Dispose();

                if (!string.IsNullOrEmpty(newName))
                {
                    if (qty != "0")
                    {
                        lbl_NewPantryItemName.Text = newName;
                        lbl_PantryItemQty.Text = qty;

                        PantryItems newItem = new PantryItems(newName, selectedPantry.PantryID, int.Parse(qty));
                        newItem.AddItemToPantry();

                        LoadPantryItems();
                    }
                    else
                    {
                        MessageBox.Show("Qty cannot be 0");
                        flp_SelectedPantryItems.Controls.Remove(pnl_NewPantryItem);
                    }
                }
                else
                {
                    flp_SelectedPantryItems.Controls.Remove(pnl_NewPantryItem); 
                }
            }
        }

        //------------DELETE PANTRY ITEM
        private void pb_DeletePantryItemClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            PantryItems item = new PantryItems(pictureBox.Tag.ToString());
            item.Delete();
            LoadPantryItems();
        }

        //------------UPDATE PANTRY ITEM (NAME)
        private void pb_EditPantryItemClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            PantryItems item = new PantryItems(pictureBox.Tag.ToString());

            Panel itemPanel = null;
            Label itemLabel = null;

            foreach (Control control in flp_SelectedPantryItems.Controls)
            {
                if (control is Panel panel && panel.Controls.Contains(pictureBox))
                {
                    itemPanel = panel;
                    itemLabel = panel.Controls.OfType<Label>().FirstOrDefault();
                    break;
                }
            }

            if (itemPanel != null)
            {
                itemLabel.Dispose();

                txt_NewPantryItemName = new TextBox();
                txt_NewPantryItemName.Text = item.PantryItemName;
                txt_NewPantryItemName.Size = new Size(250, 37);
                txt_NewPantryItemName.Location = new Point(442, 34);
                txt_NewPantryItemName.Font = new Font("Comic Sans MS", 18, FontStyle.Regular);
                txt_NewPantryItemName.Tag = item.PantryItemID;

                itemPanel.Controls.Add(txt_NewPantryItemName);
                txt_NewPantryItemName.Focus();
                txt_NewPantryItemName.KeyDown += new KeyEventHandler(PantryItemNameUpdate);
            }
        }

        private void PantryItemNameUpdate(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TextBox textbox = (TextBox)sender;
                PantryItems item = new PantryItems(textbox.Tag.ToString());
                item.UpdateName(textbox.Text);

                LoadPantryItems();
            }
        }

        //------------UPDATE PANTRY ITEM (QUANTITY)
        private void pb_DecPantryItem_Click(object sender, EventArgs e)
        {
            // Decrement lbl_PantryItemQty
            PictureBox pictureBox = (PictureBox)sender;
            PantryItems item = new PantryItems(pictureBox.Tag.ToString());
            item.DecreaseQty();
            LoadPantryItems();
        }

        private void pb_IncPantryItem_Click(object sender, EventArgs e)
        {
            // Increment lbl_PantryItemQty
            PictureBox pictureBox = (PictureBox)sender;
            PantryItems item = new PantryItems(pictureBox.Tag.ToString());
            item.IncreaseQty();
            LoadPantryItems();
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
                     * - Where(x => x.Name != "pnl_AddChecklistItemContainer" - any control that isnt named "pnl_ChecklistItemContainer".
                     * - ToList() - place filtered collection in a list.
                     * - ForEach(x => flp_ChecklistItems.Controls.Remove(x)) - pretty much a foreach loop for removing the
                     * filtered collection, in this case it's the checkboxes.
                     */
                    flp_ChecklistItems.Controls.OfType<Panel>().Where(x => x.Name != "pnl_AddChecklistItemContainer").ToList().ForEach(x => flp_ChecklistItems.Controls.Remove(x));
                    flp_CrossedChecklistItems.Controls.OfType<Panel>().ToList().ForEach(x => flp_CrossedChecklistItems.Controls.Remove(x));

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

        private void pb_ChecklistDelete_Click(object sender, EventArgs e)
        {
            selectedChecklist.DeleteChecklist();

            LoadChecklist();
            LoadChecklistsEntries();

            selectedChecklist = new Checklist(userChecklists[0].ChecklistID);
            ExpandSelectedChecklist(selectedChecklist);
        }
        
        private void pb_EditChecklistTitle_Click(object sender, EventArgs e)
        {
            
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
                pnl_NewChecklistItem = new Panel();
                pnl_NewChecklistItem.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                pnl_NewChecklistItem.Size = new Size(flp_ChecklistItems.ClientSize.Width - 25, 50);
                pnl_NewChecklistItem.Margin = new Padding(25, 0, 0, 10);

                CheckBox chk_NewChecklistItemName = new CheckBox();
                chk_NewChecklistItemName.AutoSize = true;
                chk_NewChecklistItemName.Text = item.Description;
                chk_NewChecklistItemName.Location = new Point(5, 10);
                chk_NewChecklistItemName.BackColor = ColorTranslator.FromHtml("#D9D9D9");
                chk_NewChecklistItemName.Tag = item.ChecklistItemID;

                PictureBox pb_ChecklistItemEdit = new PictureBox();
                pb_ChecklistItemEdit.ImageLocation = "../../Resources/Icons/edit(#334E4C).png";
                pb_ChecklistItemEdit.Location = new Point(pnl_NewChecklistItem.Width - 60, 10);
                pb_ChecklistItemEdit.Size = new Size(25, 25);
                pb_ChecklistItemEdit.SizeMode = PictureBoxSizeMode.Zoom;
                pb_ChecklistItemEdit.Tag = item.ChecklistItemID;

                PictureBox pb_ChecklistItemDelete = new PictureBox();
                pb_ChecklistItemDelete.ImageLocation = "../../Resources/Icons/bin(#334E4C).png";
                pb_ChecklistItemDelete.Location = new Point(pnl_NewChecklistItem.Width - 30, 10);
                pb_ChecklistItemDelete.Size = new Size(25, 25);
                pb_ChecklistItemDelete.SizeMode = PictureBoxSizeMode.Zoom;
                pb_ChecklistItemDelete.Tag = item.ChecklistItemID;

                pnl_NewChecklistItem.Controls.Add(chk_NewChecklistItemName);
                pnl_NewChecklistItem.Controls.Add(pb_ChecklistItemEdit);
                pnl_NewChecklistItem.Controls.Add(pb_ChecklistItemDelete);

                chk_NewChecklistItemName.CheckedChanged += checkBox_CheckedChanged;
                pb_ChecklistItemEdit.Click += pb_ChecklistItemEdit_Click;
                pb_ChecklistItemDelete.Click += pb_ChecklistItemDelete_Click;

                if (item.ChecklistItem_isDone == 0) 
                {
                    chk_NewChecklistItemName.Font = new Font("Comic Sans MS", 14, FontStyle.Regular);
                    flp_ChecklistItems.Controls.Add(pnl_NewChecklistItem);
                } 
                else 
                {
                    chk_NewChecklistItemName.Font = new Font("Comic Sans MS", 14, FontStyle.Strikeout);
                    flp_CrossedChecklistItems.Controls.Add(pnl_NewChecklistItem);
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

        private void pb_ChecklistItemEdit_Click(object sender, EventArgs e)
        {
            //Edit Checklist Item

        }

        private void pb_ChecklistItemDelete_Click(object sender, EventArgs e)
        {
            //Delete Checklist Item
            PictureBox pictureBox = (PictureBox)sender;
            ChecklistItems item = new ChecklistItems(pictureBox.Tag.ToString());
            item.Delete();
            LoadChecklistItems();
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
            if (selectedChecklist == null)
            {
                selectedChecklist = currentUser.GetUserChecklists(currentUser.Email)[0];
                ExpandSelectedChecklist(selectedChecklist);
            }

            tc_UserDashboard.SelectedIndex = 1;
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#334E4C).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#FFE074).png";
        }

        private void pb_Settings_Click(object sender, EventArgs e)
        {
            LoadUserInformation();
            tc_UserDashboard.SelectedIndex = 2;
            pb_Dashboard.ImageLocation = "../../Resources/Icons/dashboard(#FFE074).png";
            pb_Checklist.ImageLocation = "../../Resources/Icons/to-do-list(#FFE074).png";
            pb_Settings.ImageLocation = "../../Resources/Icons/settings(#334E4C).png";
        }

        #endregion

        #region UserSettings

        private void LoadUserInformation()
        {
            lbl_UserFirstname.Text = currentUser.FirstName;
            lbl_UserLastname.Text = currentUser.LastName;
            dtp_UserBirthdate.Text = currentUser.Birthday;
        }
        private void btn_EditUserInfo_Click(object sender, EventArgs e)
        {
            txt_UserFirstname.Visible = true;
            txt_UserLastname.Visible = true;
            dtp_UserBirthdate.Enabled = true;
            label6.Visible = true;
            pnl_UserPassword.Visible = true;
            btn_Cancel.Visible = true;
            btn_Confirm.Visible = true;
            btn_EditUserInfo.Visible = false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            txt_UserFirstname.Text = string.Empty;
            txt_UserLastname.Text = string.Empty;
            txt_UserPassword.Text = string.Empty;

            txt_UserFirstname.Visible = false;
            txt_UserLastname.Visible = false;
            dtp_UserBirthdate.Enabled = false;
            label6.Visible = false;
            pnl_UserPassword.Visible = false;
            btn_Cancel.Visible = false;
            btn_Confirm.Visible = false;
            btn_EditUserInfo.Visible = true;
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if(currentUser.Password == txt_UserPassword.Text && txt_UserPassword.Text.Trim() != "" && txt_UserLastname.Text.Trim() != "" && txt_UserFirstname.Text.Trim() != "")
            {
                currentUser.UserInformationUpdate(txt_UserFirstname.Text, txt_UserLastname.Text, dtp_UserBirthdate.Text);

                txt_UserFirstname.Text = string.Empty;
                txt_UserLastname.Text = string.Empty;
                txt_UserPassword.Text = string.Empty;

                txt_UserFirstname.Visible = false;
                txt_UserLastname.Visible = false;
                dtp_UserBirthdate.Enabled = false;
                label6.Visible = false;
                pnl_UserPassword.Visible = false;
                btn_Cancel.Visible = false;
                btn_Confirm.Visible = false;
                btn_EditUserInfo.Visible = true;

                DialogResult msgConfirm = MessageBox.Show("Please Log back in", "SUCCESS");
                this.Close();
            }
            else
            {
                MessageBox.Show("Can't update, wrong password");
            }
           


        }
        #endregion

    }
}
