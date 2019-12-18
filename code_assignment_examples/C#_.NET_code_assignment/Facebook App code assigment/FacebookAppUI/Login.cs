using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookAppServer;
using FacebookWrapper;

namespace FacebookAppUI
{
    public partial class Login : Form
    {
        private LoginResult m_LoginResult;
        private App m_applicationPage;

        public Login()
        {
            ServerSettings.LoadFromFile();
            if(Server.Error != null)
            {
                errorComponent();
            }
            else
            {
                AppSettings.LoadFromFile();
                InitializeComponent();
            }
        }
        
        private void logininasClicked(object sender, EventArgs e)
        {
            m_LoginResult = Server.Connect(AppSettings.AppSetting.AccessToken);
            loginApproved();
        }

        private void loginButtonClicked(object sender, EventArgs e)
        {
            m_LoginResult = Server.Login();
            loginApproved();
        }

        private void loginApproved()
        {
            showWaitingBar();
            Server.InitEntity(m_LoginResult.LoggedInUser);
            if (m_LoginResult.AccessToken != string.Empty)
            {
                AppSettings.AppSetting.AccessToken = m_LoginResult.AccessToken;
                m_applicationPage = new App();
                this.Hide();
                m_applicationPage.ShowDialog();
                if(m_applicationPage.DialogResult == DialogResult.Abort)
                {
                   this.Close();
                }
            }
            else
            {
                //MessageBox.Show(User.m_Message);
            }
        }

        private void showWaitingBar()
        {
            runAsButton.Hide();
            lastLoginUserName.Hide();
            profileImage.Hide();
            loginButton.Hide();
           
            PictureBox loading = new PictureBox();
            Image loadingButtonImage = Image.FromFile("../../img/loading.png");
            loading.BackgroundImage = loadingButtonImage;
            loading.Size = new Size(loadingButtonImage.Width, loadingButtonImage.Height);
            loading.Location = new System.Drawing.Point(323, 400);
            loading.SizeMode = PictureBoxSizeMode.StretchImage;
            loading.TabIndex = 0;
            this.Controls.Add(loading);
            this.Refresh();
        }
    }
}