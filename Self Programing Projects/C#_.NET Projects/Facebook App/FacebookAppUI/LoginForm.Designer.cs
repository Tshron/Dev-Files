using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookAppServer;

namespace FacebookAppUI
{
    public partial class LoginForm
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

        private void errorComponent()
        {
            Image background = Image.FromFile("../../img/login/FacebookLoginBackground.png");

            Label label = new Label();
            label.Text = string.Format("System Error:\n{0}\nThis modified require restart", Server.Error);
            label.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(177)));
            label.BackColor = Color.FromArgb(61, 82, 149);
            label.ForeColor = Color.FromArgb(237, 20, 20);
            label.Size = new Size(750, 300);
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Location = new Point(100, 270);
            this.Controls.Add(label);

            this.BackgroundImage = background;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(background.Width, background.Height);
            this.Controls.Add(this.loginButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.loginButton = new System.Windows.Forms.PictureBox();
            this.runAsButton = new PictureBox();
            this.SuspendLayout();

            if ((Server.AppSettings.UserName != null) && (Server.AppSettings.UserPicture != null))
            {
                AddLoginAs();
            }

            /// 
            /// Login
            ///
            Image loginButtonImage = Image.FromFile("../../img/login/login.png");
            this.loginButton.BackgroundImage = loginButtonImage;
            this.loginButton.Location = new System.Drawing.Point(323, 400);

            this.loginButton.Name = "LoginButton";
            this.loginButton.Size = new System.Drawing.Size(loginButtonImage.Width, loginButtonImage.Height);
            this.loginButton.SizeMode = PictureBoxSizeMode.StretchImage;
            this.loginButton.TabIndex = 0;
            this.loginButton.MouseLeave += new EventHandler(this.LoginInHoverLeave);
            this.loginButton.MouseEnter += new EventHandler(this.LoginInHoverEnter);
            this.loginButton.Click += new System.EventHandler(this.loginButtonClicked);
            ///
            /// exit
            ///
            exitButton = new PictureBox();
            exitButton.ImageLocation = "../../img/login/exitLogin.png";
            exitButton.Size = new Size(40,40);
            exitButton.Location = new Point(0, 0);
            exitButton.SizeMode = PictureBoxSizeMode.StretchImage;
            exitButton.MouseClick += exitButton_MouseClick;
            this.Controls.Add(exitButton);

            // 
            // Form1
            // 
            Image background = Image.FromFile("../../img/login/FacebookLoginBackground.png");
            this.BackgroundImage = background;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(background.Width, background.Height);
            this.Controls.Add(this.loginButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);

        }

        private void exitButton_MouseClick(object sender, MouseEventArgs e)
        {
            exitApp();
        }

        public void AddLoginAs()
        {
            ///
            ///  run as
            ///
            Image runAsButtonImage = Image.FromFile("../../img/login/loginas.png");
            this.runAsButton.BackgroundImage = runAsButtonImage;
            this.runAsButton.Location = new System.Drawing.Point(323, 350);
            this.runAsButton.Size = new System.Drawing.Size(runAsButtonImage.Width, runAsButtonImage.Height);
            this.runAsButton.Text = "Run as " + Server.AppSettings.UserName;
            this.runAsButton.TabIndex = 0;
            this.runAsButton.Click += new System.EventHandler(this.logininasClicked);

            lastLoginUserName = new Label();
            lastLoginUserName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            lastLoginUserName.BackColor = Color.FromArgb(81, 211, 111);
            lastLoginUserName.ForeColor = Color.White;
            lastLoginUserName.Size = new Size(108, runAsButtonImage.Height - 12);
            lastLoginUserName.TextAlign = ContentAlignment.MiddleLeft;
            lastLoginUserName.Location = new Point(457, 355);
            lastLoginUserName.Text = Server.AppSettings.UserName;
            lastLoginUserName.Click += new System.EventHandler(this.logininasClicked);

            profileImage = new PictureBox();
            this.profileImage.ImageLocation = Server.AppSettings.UserPicture;
            this.profileImage.Location = new System.Drawing.Point(336, 358);
            this.profileImage.Size = new System.Drawing.Size(30, 30);
            this.profileImage.TabIndex = 0;
            this.profileImage.SizeMode = PictureBoxSizeMode.StretchImage;
            this.profileImage.Click += new System.EventHandler(this.logininasClicked);

            this.Controls.Add(this.runAsButton);
            this.Controls.Add(this.profileImage);
            this.profileImage.BringToFront();
            this.Controls.Add(this.lastLoginUserName);
            lastLoginUserName.BringToFront();
        }

        private void LoginInHoverEnter(object sender, EventArgs e)
        {
            Image loginButtonImage = Image.FromFile("../../img/login/loginhover.png");
            this.loginButton.BackgroundImage = loginButtonImage;
        }

        private void LoginInHoverLeave(object sender, EventArgs e)
        {
            Image loginButtonImage = Image.FromFile("../../img/login/login.png");
            this.loginButton.BackgroundImage = loginButtonImage;
        }

        #endregion

        private PictureBox loginButton;
        private PictureBox runAsButton;
        private PictureBox profileImage = new PictureBox();
        private Label lastLoginUserName = new Label();
        private Label errorLabel = new Label();
        private PictureBox exitButton;
    }
}