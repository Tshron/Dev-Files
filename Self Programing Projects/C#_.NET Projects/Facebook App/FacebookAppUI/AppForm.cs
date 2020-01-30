using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FacebookAppServer;
using FBUser;

namespace FacebookAppUI
{
    public partial class AppForm : Form
    {
        private Size dataFrame = new Size(615, 610);
        private IEnumerator<IEnumerator<Post>> feedEnumerator;
        private int postCounter;

        public AppForm()
        {
            Server.User.StartTrackingOnFriends(Server.AppSettings.FriendsToFollow);
            InitializeComponent();
            panelBar = new PanelBar.PanelBar(Server.User.m_Feed);
            DisplayFeed();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Server.AppSettings.SaveToFile();
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

            buildPhotos(Server.User.m_Album[albumId], albumId);
        }

        private void changeText_Click(object sender, EventArgs e)
        {
                label = sender as Label;
                label.Hide();
                newText.Show();
                newText.Size = label.Size;
                newText.Name = label.Name;
                newText.Location = label.Location;
                newText.Text = label.Text;
                newText.Font = label.Font;
                newText.Tag = label.Tag;
                newText.MouseDoubleClick += updateTitle_Event;
                this.Controls.Add(newText);
                newText.BringToFront();
        }

        private void updateTitle_Event(object sender, EventArgs e)
        {
            newText = sender as TextBox;
            newText.Hide();

            Server.UpdateFields().ForEach((action) =>
            {
                if ("update" + label.Tag == action.Method.Name)
                {
                    action(int.Parse(label.Name), newText.Text);
                }
            });

            label = new Label();
            label.Text = newText.Text;
            label.Font = newText.Font;
            label.Location = newText.Location;
            label.Name = newText.Name;
            label.Size = newText.Size;
            label.Tag = newText.Tag;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.BackColor = Color.FromArgb(233, 235, 238);
            label.MouseDoubleClick += changeText_Click;
            this.Controls.Add(label);
            label.BringToFront();
            label.Show();
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
            Server.User.ChangeFriendStatus(idFriend);
            buildFriendsList();
        }

        private void openFeed_Click(object sender, MouseEventArgs e)
        {
            hideAllControls();
            openFeed();
        }

        private void openFeed()
        {
            this.Controls.Remove(filterBarControl);
            this.Controls.Remove(outerPanel);
            DisplayFeed();
        }

        private void openAbout_Click(object sender, MouseEventArgs e)
        {
            hideAllControls();
            showAbout();
        }

        private void userReactToPost(Button i_RelevantButton, bool i_Like)
        {
            int serialNumOfPost = i_Like
                                      ? int.Parse(i_RelevantButton.Name.Substring(4, 1))
                                      : int.Parse(i_RelevantButton.Name.Substring(7, 1));
            
            if(i_Like)
            {
                Server.ActOnLike((i_RelevantButton.Parent.Tag as Post));
            }
            else
            {
                if (enableCommentOnPost())
                {
                    Server.NotifyAboutComment(i_RelevantButton.Parent.Tag as Post);
                }
            } 
            
            outerPanel.Controls.Remove(outerPanel.Controls.Find("postPanel" + serialNumOfPost, false).First());
            buildPostCard(Server.GetPost((i_RelevantButton.Parent.Tag as Post).PostId), serialNumOfPost);
        }

        private bool enableCommentOnPost()
        {
            bool commentCompleted = false;
            Form commentWindow = new commentForm();
            commentWindow.ShowDialog();
            if (commentWindow.DialogResult == DialogResult.OK)
            {
                commentCompleted = true;
            }

            return commentCompleted;
        }

        private Point setLocation(
            Control i_ControlToSet,
            string i_NeighborControlToSetAfter,
            int i_XCoordinate,
            int i_SpaceBetween)
        {
            Control neighbor = i_ControlToSet.Parent.Controls.Find(i_NeighborControlToSetAfter, false).First();

            return new Point(i_XCoordinate, neighbor.Location.Y + neighbor.Size.Height + i_SpaceBetween);
        }

        private void loadMorePosts()
        {
            outerPanel.Controls.Remove(outerPanel.Controls.Find("loadMore", false).First());
            BuildTrio();
        }

        private void displayCard(PictureBox i_CardPictureBox)
        {
            Form card = Server.GetGreetingCard(i_CardPictureBox.Name, i_CardPictureBox.Tag as FBUser.FBUser);
            card.ShowDialog();
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
