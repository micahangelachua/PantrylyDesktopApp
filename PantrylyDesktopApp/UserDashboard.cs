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
            // TODO: Make the rounded edges smoother (anti-aliasing?)
            // Rounded corners for the form 
            GraphicsPath path = new GraphicsPath();
            int arcSize = 20;
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            path.AddArc(rect.X, rect.Y, arcSize, arcSize, 180, 90);
            path.AddArc(rect.X + rect.Width - arcSize, rect.Y, arcSize, arcSize, 270, 90);
            path.AddArc(rect.X + rect.Width - arcSize, rect.Y + rect.Height - arcSize, arcSize, arcSize, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - arcSize, arcSize, arcSize, 90, 90);
            Region = new Region(path);
            this.FormBorderStyle = FormBorderStyle.None;

            // make btnDashboardClose circle.
            GraphicsPath closePath = new GraphicsPath();
            closePath.AddEllipse(0, 0, btn_DashboardClose.Width, btn_DashboardClose.Height);
            btn_DashboardClose.Region = new Region(closePath);

            // make btnDashboardMinimize circle.
            GraphicsPath minimizePath = new GraphicsPath();
            minimizePath.AddEllipse(0, 0, btn_DashboardMinimize.Width, btn_DashboardMinimize.Height);
            btn_DashboardMinimize.Region = new Region(minimizePath);
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

        private void btnAddNewPantry_Click(object sender, EventArgs e)
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

        // Variables to use for dragging around window
        private bool isDragging = false;
        private Point lastCursorPos;

        private void pnl_WinTitleAndControls_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastCursorPos = Cursor.Position;
        }

        private void pnl_WinTitleAndControls_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point deltaCursorPos = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(this.Location.X + deltaCursorPos.X, this.Location.Y + deltaCursorPos.Y);
                lastCursorPos = Cursor.Position;
            }
        }

        private void pnl_WinTitleAndControls_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        private void btn_DashboardClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_DashboardMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
