using System;
using System.Drawing;
using System.Windows.Forms;
using FacebookAppServer;

namespace FacebookAppUI
{
    public partial class App : Form
    {
        
        private Size dataFrame = new Size(615, 610);

        public App()
        {
            Server.User.StartTrackingOnFriends(AppSettings.AppSetting.FriendsToFollow);
            InitializeComponent();
            displayFeed(Server.User.m_Feed);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            AppSettings.SaveToFile();
        }

        private void commentButton_MouseClick(object sender, MouseEventArgs e)
        {
            userReactToPost(sender as Button, false);
        }

        private void likeButton_MouseClick(object sender, MouseEventArgs e)
        {
            userReactToPost(sender as Button, true);
        }

        private void showAlbums_Click(object sender, EventArgs e)
        {
            hideAllControls();
            buildGallary(Server.User.m_Album);
        }

        private void showPictures_Click(object sender, MouseEventArgs e)
        {
            hideAllControls();
            int albumId;
            try
            {
                albumId = int.Parse((sender as PictureBox).Name);
            }
            catch
            {
                try
                {
                    albumId = int.Parse((sender as Label).Name);
                }
                catch
                {
                    albumId = 0;
                }
            }

            buildPhotos(Server.User.m_Album[albumId].m_Photos, albumId);
        }

        private void showPhoto_Click(object sender, MouseEventArgs e)
        {
            hideAllControls();
            showPhoto((sender as PictureBox).Name);
        }      

        private void openFriends_Click(object sender, MouseEventArgs e)
        {
            buildFriendsList();
        }

        private void favoriteFriend_Click(object sender, MouseEventArgs e)
        {
            int idFriend = int.Parse((sender as PictureBox).Name);
            Server.User.m_FriendsList[idFriend].m_Follow = !Server.User.m_FriendsList[idFriend].m_Follow;
            buildFriendsList();
        }

        private void openFeed_Click(object sender, MouseEventArgs e)
        {
            hideAllControls();
            this.Controls.Remove(outerPanel);
            filterFriends = false;
            filterLike = false;
            displayFeed(Server.User.m_Feed);
        }

        private void openAbout_Click(object sender, MouseEventArgs e)
        {
            hideAllControls();
            showAbout();
        }

        private void filterByLike_MouseClick(object sender, MouseEventArgs e)
        {
            setFilterBarState(sender as PictureBox);
        }

        private void filterByFriend_MouseClick(object sender, MouseEventArgs e)
        {
            setFilterBarState(sender as PictureBox);
        }

        
        private void hideAllControls()
        {
            this.filterBarControl.Hide();
            this.outerPanel.Hide();
            this.rightControl.Hide();
        }

        private void exitApp()
        {
            this.DialogResult = DialogResult.Abort;
        }
    }
}
