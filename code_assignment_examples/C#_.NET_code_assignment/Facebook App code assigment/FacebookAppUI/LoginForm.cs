using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookAppServer;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookAppUI
{
    public partial class LoginForm : Form
    {
        private LoginResult m_LoginResult;
        private AppForm m_applicationPage;
        private User m_User;

        private PictureBox loading = new PictureBox();

        public LoginForm()
        {
            Server.ServerSettings.LoadFromFile();
            if (Server.Error != string.Empty)
            {
                errorComponent();
            }
            else
            {
                Server.AppSettings.LoadFromFile();
                InitializeComponent();
            }
        }

        private void logininasClicked(object sender, EventArgs e)
        {
            m_LoginResult = Server.Connect(Server.AppSettings.AccessToken);
            initServer();
        }

        private void loginButtonClicked(object sender, EventArgs e)
        {
            m_LoginResult = Server.Login();
            initServer();
        }

        private void initServer()
        {
            Task[] tasks = new Task[1];
            showWaitingBar();
            m_User = m_LoginResult.LoggedInUser;
            int lastProcess = Server.CurrentProcess;
            if (m_LoginResult.AccessToken != string.Empty)
            {
                tasks[0] = Task.Run(() => Server.SetServer(m_User));
                while (Server.CurrentProcess != Server.ProcessesAtLoading)
                {
                    if (lastProcess != Server.CurrentProcess)
                    {
                        changeProcessBar();
                        lastProcess = Server.CurrentProcess;
                    }
                }

                Task.WaitAll(tasks);
                Thread.Sleep(100);

                loginApproved();
            }
        }

        private void changeProcessBar()
        {
            Image loadingButtonImage = Image.FromFile(string.Format("../../img/loadingBar/{0}loading.png", Server.CurrentProcess));
            loading.BackgroundImage = loadingButtonImage;
            loading.Refresh();
        }

        private void loginApproved()
        {
            Server.AppSettings.AccessToken = m_LoginResult.AccessToken;
            m_applicationPage = new AppForm();
            this.Hide();
            m_applicationPage.ShowDialog();
            if (m_applicationPage.DialogResult == DialogResult.Abort)
            {
                this.Close();
            }
        }

        private void showWaitingBar()
        {
            runAsButton.Hide();
            lastLoginUserName.Hide();
            profileImage.Hide();
            loginButton.Hide();
            exitButton.Hide();
            loading = new PictureBox();
            Image loadingButtonImage = Image.FromFile("../../img/loadingBar/0loading.png");
            loading.BackgroundImage = loadingButtonImage;
            loading.Size = new Size(loadingButtonImage.Width, loadingButtonImage.Height);
            loading.Location = new System.Drawing.Point(323, 400);
            loading.SizeMode = PictureBoxSizeMode.StretchImage;
            loading.TabIndex = 0;
            this.Controls.Add(loading);
            
            this.Refresh();
        }

        private void exitApp()
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }
    }
}