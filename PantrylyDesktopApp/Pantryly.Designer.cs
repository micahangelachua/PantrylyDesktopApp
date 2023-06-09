﻿namespace PantrylyDesktopApp
{
    partial class Pantryly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pantryly));
            this.lbl_LoginLogo = new System.Windows.Forms.Label();
            this.txt_Email = new System.Windows.Forms.TextBox();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.btn_Login = new System.Windows.Forms.Button();
            this.lbl_LoginEmail = new System.Windows.Forms.Label();
            this.lbl_LoginPassword = new System.Windows.Forms.Label();
            this.btn_Signup = new System.Windows.Forms.Button();
            this.pnl_WinTitleAndControls = new System.Windows.Forms.Panel();
            this.pb_LoginMinimize = new System.Windows.Forms.PictureBox();
            this.pb_LoginClose = new System.Windows.Forms.PictureBox();
            this.pnl_WinTitleAndControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_LoginMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_LoginClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_LoginLogo
            // 
            this.lbl_LoginLogo.AutoSize = true;
            this.lbl_LoginLogo.Font = new System.Drawing.Font("Pacifico", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LoginLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(108)))), ((int)(((byte)(78)))));
            this.lbl_LoginLogo.Location = new System.Drawing.Point(218, 74);
            this.lbl_LoginLogo.Name = "lbl_LoginLogo";
            this.lbl_LoginLogo.Size = new System.Drawing.Size(296, 124);
            this.lbl_LoginLogo.TabIndex = 0;
            this.lbl_LoginLogo.Text = "Pantryly";
            // 
            // txt_Email
            // 
            this.txt_Email.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.txt_Email.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Email.Location = new System.Drawing.Point(184, 254);
            this.txt_Email.Name = "txt_Email";
            this.txt_Email.Size = new System.Drawing.Size(369, 29);
            this.txt_Email.TabIndex = 0;
            // 
            // txt_Password
            // 
            this.txt_Password.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.txt_Password.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Password.Location = new System.Drawing.Point(184, 330);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(369, 29);
            this.txt_Password.TabIndex = 1;
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(78)))), ((int)(((byte)(76)))));
            this.btn_Login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Login.FlatAppearance.BorderSize = 0;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("Comic Sans MS", 12F);
            this.btn_Login.ForeColor = System.Drawing.Color.White;
            this.btn_Login.Location = new System.Drawing.Point(292, 371);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(149, 34);
            this.btn_Login.TabIndex = 2;
            this.btn_Login.Text = "LOG IN";
            this.btn_Login.UseVisualStyleBackColor = false;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // lbl_LoginEmail
            // 
            this.lbl_LoginEmail.AutoSize = true;
            this.lbl_LoginEmail.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LoginEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(78)))), ((int)(((byte)(76)))));
            this.lbl_LoginEmail.Location = new System.Drawing.Point(180, 227);
            this.lbl_LoginEmail.Name = "lbl_LoginEmail";
            this.lbl_LoginEmail.Size = new System.Drawing.Size(62, 24);
            this.lbl_LoginEmail.TabIndex = 6;
            this.lbl_LoginEmail.Text = "Email:";
            // 
            // lbl_LoginPassword
            // 
            this.lbl_LoginPassword.AutoSize = true;
            this.lbl_LoginPassword.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LoginPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(78)))), ((int)(((byte)(76)))));
            this.lbl_LoginPassword.Location = new System.Drawing.Point(180, 303);
            this.lbl_LoginPassword.Name = "lbl_LoginPassword";
            this.lbl_LoginPassword.Size = new System.Drawing.Size(93, 24);
            this.lbl_LoginPassword.TabIndex = 7;
            this.lbl_LoginPassword.Text = "Password:";
            // 
            // btn_Signup
            // 
            this.btn_Signup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(116)))));
            this.btn_Signup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Signup.FlatAppearance.BorderSize = 0;
            this.btn_Signup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Signup.Font = new System.Drawing.Font("Comic Sans MS", 12F);
            this.btn_Signup.Location = new System.Drawing.Point(633, 8);
            this.btn_Signup.Name = "btn_Signup";
            this.btn_Signup.Size = new System.Drawing.Size(88, 34);
            this.btn_Signup.TabIndex = 8;
            this.btn_Signup.Text = "Sign Up";
            this.btn_Signup.UseVisualStyleBackColor = false;
            this.btn_Signup.Click += new System.EventHandler(this.btn_Signup_Click);
            // 
            // pnl_WinTitleAndControls
            // 
            this.pnl_WinTitleAndControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(78)))), ((int)(((byte)(76)))));
            this.pnl_WinTitleAndControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_WinTitleAndControls.Controls.Add(this.pb_LoginMinimize);
            this.pnl_WinTitleAndControls.Controls.Add(this.btn_Signup);
            this.pnl_WinTitleAndControls.Controls.Add(this.pb_LoginClose);
            this.pnl_WinTitleAndControls.Location = new System.Drawing.Point(0, 0);
            this.pnl_WinTitleAndControls.Name = "pnl_WinTitleAndControls";
            this.pnl_WinTitleAndControls.Size = new System.Drawing.Size(733, 50);
            this.pnl_WinTitleAndControls.TabIndex = 9;
            // 
            // pb_LoginMinimize
            // 
            this.pb_LoginMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_LoginMinimize.Image = ((System.Drawing.Image)(resources.GetObject("pb_LoginMinimize.Image")));
            this.pb_LoginMinimize.Location = new System.Drawing.Point(42, 12);
            this.pb_LoginMinimize.Name = "pb_LoginMinimize";
            this.pb_LoginMinimize.Size = new System.Drawing.Size(25, 25);
            this.pb_LoginMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_LoginMinimize.TabIndex = 17;
            this.pb_LoginMinimize.TabStop = false;
            // 
            // pb_LoginClose
            // 
            this.pb_LoginClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pb_LoginClose.Image = ((System.Drawing.Image)(resources.GetObject("pb_LoginClose.Image")));
            this.pb_LoginClose.Location = new System.Drawing.Point(11, 12);
            this.pb_LoginClose.Name = "pb_LoginClose";
            this.pb_LoginClose.Size = new System.Drawing.Size(25, 25);
            this.pb_LoginClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_LoginClose.TabIndex = 16;
            this.pb_LoginClose.TabStop = false;
            // 
            // Pantryly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(228)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(733, 447);
            this.Controls.Add(this.lbl_LoginLogo);
            this.Controls.Add(this.pnl_WinTitleAndControls);
            this.Controls.Add(this.lbl_LoginPassword);
            this.Controls.Add(this.lbl_LoginEmail);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.txt_Email);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Pantryly";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pantryly: Log In";
            this.pnl_WinTitleAndControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_LoginMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_LoginClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_LoginLogo;
        private System.Windows.Forms.TextBox txt_Email;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Label lbl_LoginEmail;
        private System.Windows.Forms.Label lbl_LoginPassword;
        private System.Windows.Forms.Button btn_Signup;
        private System.Windows.Forms.Panel pnl_WinTitleAndControls;
        private System.Windows.Forms.PictureBox pb_LoginMinimize;
        private System.Windows.Forms.PictureBox pb_LoginClose;
    }
}

