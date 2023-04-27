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
        private Panel pnl_newPantry;
        private TextBox txt_newPantryName;
        private Label lbl_newPantryName;
        string currentUser_id = "";
        public UserDashboard(string user_id)
        {
            currentUser_id = user_id;
            InitializeComponent();
            FormUtils.MakeWindowFormRounded(this);
            FormUtils.AddDraggableWindowTitle(pnl_WinTitleAndControls);
            FormUtils.MakeButtonRounded(btn_DashboardClose);
            FormUtils.AddCloseButton(btn_DashboardClose);
            FormUtils.MakeButtonRounded(btn_DashboardMinimize);
            FormUtils.AddMinimizeButton(btn_DashboardMinimize);
        }

        SQLiteConnection sql_con;
        SQLiteCommand sql_cmd;
        SQLiteDataAdapter DB;

        DataSet UserPantriesDS = new DataSet();
        DataTable UserPantriesDT = new DataTable();

        DataSet UserChecklistDS = new DataSet();
        DataTable UserChecklistDT = new DataTable();


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

        private void GetUserPantries(string user_id)
        {
            //each pantry has an FK of the user id. it will retrieve all pantries that have the user id as foreign key.
        }

        private void GetUserChecklist(string user_id)
        {

        }

        public void OpenPantry(string pantry_id)
        {
            
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
                    // add query to execute for adding the pantry to the table
                }
                else
                {
                    flp_PantriesContainer.Controls.Remove(pnl_newPantry);
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
