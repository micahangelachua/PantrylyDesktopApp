namespace PantrylyDesktopApp
{
    partial class UserDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserDashboard));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pb_Checklist = new System.Windows.Forms.PictureBox();
            this.pb_UserPicture = new System.Windows.Forms.PictureBox();
            this.pb_Pantry = new System.Windows.Forms.PictureBox();
            this.pb_Dashboard = new System.Windows.Forms.PictureBox();
            this.lbl_AddNewPantry = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_DasboardLogo = new System.Windows.Forms.Label();
            this.pnl_AddNewPantry = new System.Windows.Forms.Panel();
            this.pb_AddNewPantry = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.flp_PantriesContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_WinTitleAndControls = new System.Windows.Forms.Panel();
            this.btn_DashboardMinimize = new System.Windows.Forms.Button();
            this.btn_DashboardClose = new System.Windows.Forms.Button();
            this.tc_UserDashboard = new System.Windows.Forms.TabControl();
            this.tp_UserOverview = new System.Windows.Forms.TabPage();
            this.flp_ChecklistsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_AddNewChecklist = new System.Windows.Forms.Panel();
            this.lbl_AddNewChecklist = new System.Windows.Forms.Label();
            this.pb_AddNewChecklist = new System.Windows.Forms.PictureBox();
            this.tp_UserPantries = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tp_UserChecklists = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_UserFname = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Checklist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_UserPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Pantry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Dashboard)).BeginInit();
            this.pnl_AddNewPantry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_AddNewPantry)).BeginInit();
            this.flp_PantriesContainer.SuspendLayout();
            this.pnl_WinTitleAndControls.SuspendLayout();
            this.tc_UserDashboard.SuspendLayout();
            this.tp_UserOverview.SuspendLayout();
            this.flp_ChecklistsContainer.SuspendLayout();
            this.pnl_AddNewChecklist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_AddNewChecklist)).BeginInit();
            this.tp_UserPantries.SuspendLayout();
            this.tp_UserChecklists.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(78)))), ((int)(((byte)(76)))));
            this.panel1.Controls.Add(this.pb_Checklist);
            this.panel1.Controls.Add(this.pb_UserPicture);
            this.panel1.Controls.Add(this.pb_Pantry);
            this.panel1.Controls.Add(this.pb_Dashboard);
            this.panel1.Location = new System.Drawing.Point(0, 62);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 827);
            this.panel1.TabIndex = 0;
            // 
            // pb_Checklist
            // 
            this.pb_Checklist.Image = ((System.Drawing.Image)(resources.GetObject("pb_Checklist.Image")));
            this.pb_Checklist.Location = new System.Drawing.Point(37, 540);
            this.pb_Checklist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pb_Checklist.Name = "pb_Checklist";
            this.pb_Checklist.Size = new System.Drawing.Size(133, 114);
            this.pb_Checklist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Checklist.TabIndex = 12;
            this.pb_Checklist.TabStop = false;
            this.pb_Checklist.Click += new System.EventHandler(this.pb_Checklist_Click);
            // 
            // pb_UserPicture
            // 
            this.pb_UserPicture.Image = ((System.Drawing.Image)(resources.GetObject("pb_UserPicture.Image")));
            this.pb_UserPicture.Location = new System.Drawing.Point(37, 37);
            this.pb_UserPicture.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pb_UserPicture.Name = "pb_UserPicture";
            this.pb_UserPicture.Size = new System.Drawing.Size(133, 114);
            this.pb_UserPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_UserPicture.TabIndex = 9;
            this.pb_UserPicture.TabStop = false;
            // 
            // pb_Pantry
            // 
            this.pb_Pantry.Image = ((System.Drawing.Image)(resources.GetObject("pb_Pantry.Image")));
            this.pb_Pantry.Location = new System.Drawing.Point(37, 400);
            this.pb_Pantry.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pb_Pantry.Name = "pb_Pantry";
            this.pb_Pantry.Size = new System.Drawing.Size(133, 114);
            this.pb_Pantry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Pantry.TabIndex = 11;
            this.pb_Pantry.TabStop = false;
            this.pb_Pantry.Click += new System.EventHandler(this.pb_Pantry_Click);
            // 
            // pb_Dashboard
            // 
            this.pb_Dashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.pb_Dashboard.Image = ((System.Drawing.Image)(resources.GetObject("pb_Dashboard.Image")));
            this.pb_Dashboard.Location = new System.Drawing.Point(37, 256);
            this.pb_Dashboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pb_Dashboard.Name = "pb_Dashboard";
            this.pb_Dashboard.Size = new System.Drawing.Size(133, 114);
            this.pb_Dashboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Dashboard.TabIndex = 10;
            this.pb_Dashboard.TabStop = false;
            this.pb_Dashboard.Click += new System.EventHandler(this.pb_Dashboard_Click);
            // 
            // lbl_AddNewPantry
            // 
            this.lbl_AddNewPantry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(102)))), ((int)(((byte)(78)))));
            this.lbl_AddNewPantry.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AddNewPantry.Location = new System.Drawing.Point(0, 0);
            this.lbl_AddNewPantry.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AddNewPantry.Name = "lbl_AddNewPantry";
            this.lbl_AddNewPantry.Size = new System.Drawing.Size(333, 46);
            this.lbl_AddNewPantry.TabIndex = 1;
            this.lbl_AddNewPantry.Text = "ADD NEW PANTRY";
            this.lbl_AddNewPantry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 24F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(102)))), ((int)(((byte)(78)))));
            this.label2.Location = new System.Drawing.Point(8, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 56);
            this.label2.TabIndex = 2;
            this.label2.Text = "PANTRIES";
            // 
            // lbl_DasboardLogo
            // 
            this.lbl_DasboardLogo.AutoSize = true;
            this.lbl_DasboardLogo.Font = new System.Drawing.Font("Pacifico", 28F);
            this.lbl_DasboardLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(102)))), ((int)(((byte)(78)))));
            this.lbl_DasboardLogo.Location = new System.Drawing.Point(1431, 4);
            this.lbl_DasboardLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_DasboardLogo.Name = "lbl_DasboardLogo";
            this.lbl_DasboardLogo.Size = new System.Drawing.Size(203, 82);
            this.lbl_DasboardLogo.TabIndex = 3;
            this.lbl_DasboardLogo.Text = "Pantryly";
            // 
            // pnl_AddNewPantry
            // 
            this.pnl_AddNewPantry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.pnl_AddNewPantry.Controls.Add(this.lbl_AddNewPantry);
            this.pnl_AddNewPantry.Controls.Add(this.pb_AddNewPantry);
            this.pnl_AddNewPantry.Location = new System.Drawing.Point(4, 4);
            this.pnl_AddNewPantry.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_AddNewPantry.Name = "pnl_AddNewPantry";
            this.pnl_AddNewPantry.Size = new System.Drawing.Size(333, 246);
            this.pnl_AddNewPantry.TabIndex = 5;
            // 
            // pb_AddNewPantry
            // 
            this.pb_AddNewPantry.Image = ((System.Drawing.Image)(resources.GetObject("pb_AddNewPantry.Image")));
            this.pb_AddNewPantry.Location = new System.Drawing.Point(129, 105);
            this.pb_AddNewPantry.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pb_AddNewPantry.Name = "pb_AddNewPantry";
            this.pb_AddNewPantry.Size = new System.Drawing.Size(76, 62);
            this.pb_AddNewPantry.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_AddNewPantry.TabIndex = 6;
            this.pb_AddNewPantry.TabStop = false;
            this.pb_AddNewPantry.Click += new System.EventHandler(this.pb_AddNewPantry_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(167)))), ((int)(((byte)(143)))));
            this.label3.Location = new System.Drawing.Point(9, 405);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 56);
            this.label3.TabIndex = 6;
            this.label3.Text = "CHECKLISTS";
            // 
            // flp_PantriesContainer
            // 
            this.flp_PantriesContainer.AutoScroll = true;
            this.flp_PantriesContainer.Controls.Add(this.pnl_AddNewPantry);
            this.flp_PantriesContainer.Location = new System.Drawing.Point(9, 148);
            this.flp_PantriesContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flp_PantriesContainer.Name = "flp_PantriesContainer";
            this.flp_PantriesContainer.Size = new System.Drawing.Size(1605, 254);
            this.flp_PantriesContainer.TabIndex = 10;
            // 
            // pnl_WinTitleAndControls
            // 
            this.pnl_WinTitleAndControls.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnl_WinTitleAndControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(78)))), ((int)(((byte)(76)))));
            this.pnl_WinTitleAndControls.Controls.Add(this.btn_DashboardMinimize);
            this.pnl_WinTitleAndControls.Controls.Add(this.btn_DashboardClose);
            this.pnl_WinTitleAndControls.Location = new System.Drawing.Point(0, 0);
            this.pnl_WinTitleAndControls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_WinTitleAndControls.Name = "pnl_WinTitleAndControls";
            this.pnl_WinTitleAndControls.Size = new System.Drawing.Size(1885, 62);
            this.pnl_WinTitleAndControls.TabIndex = 11;
            // 
            // btn_DashboardMinimize
            // 
            this.btn_DashboardMinimize.BackColor = System.Drawing.Color.Lime;
            this.btn_DashboardMinimize.FlatAppearance.BorderSize = 0;
            this.btn_DashboardMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DashboardMinimize.Location = new System.Drawing.Point(51, 15);
            this.btn_DashboardMinimize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DashboardMinimize.Name = "btn_DashboardMinimize";
            this.btn_DashboardMinimize.Size = new System.Drawing.Size(27, 25);
            this.btn_DashboardMinimize.TabIndex = 12;
            this.btn_DashboardMinimize.UseVisualStyleBackColor = false;
            // 
            // btn_DashboardClose
            // 
            this.btn_DashboardClose.BackColor = System.Drawing.Color.Red;
            this.btn_DashboardClose.FlatAppearance.BorderSize = 0;
            this.btn_DashboardClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DashboardClose.Location = new System.Drawing.Point(16, 15);
            this.btn_DashboardClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_DashboardClose.Name = "btn_DashboardClose";
            this.btn_DashboardClose.Size = new System.Drawing.Size(27, 25);
            this.btn_DashboardClose.TabIndex = 11;
            this.btn_DashboardClose.UseVisualStyleBackColor = false;
            // 
            // tc_UserDashboard
            // 
            this.tc_UserDashboard.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tc_UserDashboard.Controls.Add(this.tp_UserOverview);
            this.tc_UserDashboard.Controls.Add(this.tp_UserPantries);
            this.tc_UserDashboard.Controls.Add(this.tp_UserChecklists);
            this.tc_UserDashboard.Location = new System.Drawing.Point(205, 39);
            this.tc_UserDashboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tc_UserDashboard.Name = "tc_UserDashboard";
            this.tc_UserDashboard.SelectedIndex = 0;
            this.tc_UserDashboard.Size = new System.Drawing.Size(1684, 858);
            this.tc_UserDashboard.TabIndex = 12;
            // 
            // tp_UserOverview
            // 
            this.tp_UserOverview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(228)))), ((int)(((byte)(216)))));
            this.tp_UserOverview.Controls.Add(this.lbl_UserFname);
            this.tp_UserOverview.Controls.Add(this.flp_ChecklistsContainer);
            this.tp_UserOverview.Controls.Add(this.label2);
            this.tp_UserOverview.Controls.Add(this.flp_PantriesContainer);
            this.tp_UserOverview.Controls.Add(this.lbl_DasboardLogo);
            this.tp_UserOverview.Controls.Add(this.label3);
            this.tp_UserOverview.Location = new System.Drawing.Point(4, 28);
            this.tp_UserOverview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tp_UserOverview.Name = "tp_UserOverview";
            this.tp_UserOverview.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tp_UserOverview.Size = new System.Drawing.Size(1676, 826);
            this.tp_UserOverview.TabIndex = 0;
            this.tp_UserOverview.Text = "Overview";
            // 
            // flp_ChecklistsContainer
            // 
            this.flp_ChecklistsContainer.Controls.Add(this.pnl_AddNewChecklist);
            this.flp_ChecklistsContainer.Location = new System.Drawing.Point(9, 464);
            this.flp_ChecklistsContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flp_ChecklistsContainer.Name = "flp_ChecklistsContainer";
            this.flp_ChecklistsContainer.Size = new System.Drawing.Size(1605, 254);
            this.flp_ChecklistsContainer.TabIndex = 12;
            // 
            // pnl_AddNewChecklist
            // 
            this.pnl_AddNewChecklist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.pnl_AddNewChecklist.Controls.Add(this.lbl_AddNewChecklist);
            this.pnl_AddNewChecklist.Controls.Add(this.pb_AddNewChecklist);
            this.pnl_AddNewChecklist.Location = new System.Drawing.Point(4, 4);
            this.pnl_AddNewChecklist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_AddNewChecklist.Name = "pnl_AddNewChecklist";
            this.pnl_AddNewChecklist.Size = new System.Drawing.Size(333, 246);
            this.pnl_AddNewChecklist.TabIndex = 11;
            // 
            // lbl_AddNewChecklist
            // 
            this.lbl_AddNewChecklist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(167)))), ((int)(((byte)(143)))));
            this.lbl_AddNewChecklist.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AddNewChecklist.Location = new System.Drawing.Point(0, 0);
            this.lbl_AddNewChecklist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AddNewChecklist.Name = "lbl_AddNewChecklist";
            this.lbl_AddNewChecklist.Size = new System.Drawing.Size(333, 46);
            this.lbl_AddNewChecklist.TabIndex = 1;
            this.lbl_AddNewChecklist.Text = "ADD NEW CHECKLIST";
            this.lbl_AddNewChecklist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pb_AddNewChecklist
            // 
            this.pb_AddNewChecklist.Image = ((System.Drawing.Image)(resources.GetObject("pb_AddNewChecklist.Image")));
            this.pb_AddNewChecklist.Location = new System.Drawing.Point(129, 103);
            this.pb_AddNewChecklist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pb_AddNewChecklist.Name = "pb_AddNewChecklist";
            this.pb_AddNewChecklist.Size = new System.Drawing.Size(76, 62);
            this.pb_AddNewChecklist.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_AddNewChecklist.TabIndex = 12;
            this.pb_AddNewChecklist.TabStop = false;
            // 
            // tp_UserPantries
            // 
            this.tp_UserPantries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(228)))), ((int)(((byte)(216)))));
            this.tp_UserPantries.Controls.Add(this.label1);
            this.tp_UserPantries.Location = new System.Drawing.Point(4, 28);
            this.tp_UserPantries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tp_UserPantries.Name = "tp_UserPantries";
            this.tp_UserPantries.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tp_UserPantries.Size = new System.Drawing.Size(1676, 826);
            this.tp_UserPantries.TabIndex = 1;
            this.tp_UserPantries.Text = "Pantries";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Pacifico", 28F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(102)))), ((int)(((byte)(78)))));
            this.label1.Location = new System.Drawing.Point(1431, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 82);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pantryly";
            // 
            // tp_UserChecklists
            // 
            this.tp_UserChecklists.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(228)))), ((int)(((byte)(216)))));
            this.tp_UserChecklists.Controls.Add(this.label4);
            this.tp_UserChecklists.Location = new System.Drawing.Point(4, 28);
            this.tp_UserChecklists.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tp_UserChecklists.Name = "tp_UserChecklists";
            this.tp_UserChecklists.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tp_UserChecklists.Size = new System.Drawing.Size(1676, 826);
            this.tp_UserChecklists.TabIndex = 2;
            this.tp_UserChecklists.Text = "Checklists";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Pacifico", 28F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(102)))), ((int)(((byte)(78)))));
            this.label4.Location = new System.Drawing.Point(1431, 4);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(203, 82);
            this.label4.TabIndex = 4;
            this.label4.Text = "Pantryly";
            // 
            // lbl_UserFname
            // 
            this.lbl_UserFname.AutoSize = true;
            this.lbl_UserFname.Location = new System.Drawing.Point(18, 18);
            this.lbl_UserFname.Name = "lbl_UserFname";
            this.lbl_UserFname.Size = new System.Drawing.Size(20, 16);
            this.lbl_UserFname.TabIndex = 13;
            this.lbl_UserFname.Text = "Hi";
            // 
            // UserDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(228)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(1884, 886);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_WinTitleAndControls);
            this.Controls.Add(this.tc_UserDashboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UserDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserDashboard";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Checklist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_UserPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Pantry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Dashboard)).EndInit();
            this.pnl_AddNewPantry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_AddNewPantry)).EndInit();
            this.flp_PantriesContainer.ResumeLayout(false);
            this.pnl_WinTitleAndControls.ResumeLayout(false);
            this.tc_UserDashboard.ResumeLayout(false);
            this.tp_UserOverview.ResumeLayout(false);
            this.tp_UserOverview.PerformLayout();
            this.flp_ChecklistsContainer.ResumeLayout(false);
            this.pnl_AddNewChecklist.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_AddNewChecklist)).EndInit();
            this.tp_UserPantries.ResumeLayout(false);
            this.tp_UserPantries.PerformLayout();
            this.tp_UserChecklists.ResumeLayout(false);
            this.tp_UserChecklists.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_AddNewPantry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_DasboardLogo;
        private System.Windows.Forms.PictureBox pb_Checklist;
        private System.Windows.Forms.PictureBox pb_UserPicture;
        private System.Windows.Forms.PictureBox pb_Pantry;
        private System.Windows.Forms.PictureBox pb_Dashboard;
        private System.Windows.Forms.Panel pnl_AddNewPantry;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flp_PantriesContainer;
        private System.Windows.Forms.Panel pnl_WinTitleAndControls;
        private System.Windows.Forms.Button btn_DashboardMinimize;
        private System.Windows.Forms.Button btn_DashboardClose;
        private System.Windows.Forms.TabControl tc_UserDashboard;
        private System.Windows.Forms.TabPage tp_UserOverview;
        private System.Windows.Forms.Panel pnl_AddNewChecklist;
        private System.Windows.Forms.Label lbl_AddNewChecklist;
        private System.Windows.Forms.TabPage tp_UserPantries;
        private System.Windows.Forms.TabPage tp_UserChecklists;
        private System.Windows.Forms.FlowLayoutPanel flp_ChecklistsContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pb_AddNewPantry;
        private System.Windows.Forms.PictureBox pb_AddNewChecklist;
        private System.Windows.Forms.Label lbl_UserFname;
    }
}