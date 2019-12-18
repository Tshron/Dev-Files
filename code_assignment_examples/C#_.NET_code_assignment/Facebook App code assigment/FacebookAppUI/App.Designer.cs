using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using FacebookAppServer;
using FBUser;

namespace FacebookAppUI
{
    public partial class App
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

        private Panel outerPanel;
        private Panel innerPanel;
        private Control rightControl = new Control();
        private Control filterBarControl = new Control();
        private FlowLayoutPanel rightLayout = new FlowLayoutPanel();
        private PictureBox picture;
        private Label label;
        private TextBox contentBox;
        private Button likeButton;
        private Button commentButton;
        private bool filterLike = false;
        private bool filterFriends = false;

        #region Windows Form Designer generated code
        
        private void InitializeComponent()
        {
            this.picture = new PictureBox();
            this.label = new Label();
            displayMenuButtons();
            displayPhotoCover();
            displayUserThumbnail();
            displayUserName();
                        
            Image background = Image.FromFile("../../img/backgroundApp.png");
            this.BackgroundImage = background;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(background.Width, background.Height);
            this.components = new System.ComponentModel.Container();
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        private void addMenuButtons(int i_Width, int i_Height, string i_Picture, MouseEventHandler i_Function)
        {
            Image image = Image.FromFile(i_Picture);
            picture = new PictureBox();
            this.picture.BackgroundImage = image;
            this.picture.Location = new System.Drawing.Point(i_Width, i_Height);
            this.picture.Name = "HomeButton";
            this.picture.Size = new System.Drawing.Size(image.Width, image.Height);
            this.picture.SizeMode = PictureBoxSizeMode.StretchImage;
            this.picture.MouseClick += i_Function;
            this.Controls.Add(picture);
            
        }
        private void displayMenuButtons()
        {
            addMenuButtons(50, 260, "../../img/home.png", openFeed_Click);
            addMenuButtons(50, 320, "../../img/about.png", openAbout_Click);
            addMenuButtons(50, 380, "../../img/friends.png", openFriends_Click);
            addMenuButtons(50, 440, "../../img/photos.png", showAlbums_Click);
            addMenuButtons(50, 500, "../../img/exit.png", exitButton_MouseClick);
        }
        private void exitButton_MouseClick(object sender, MouseEventArgs e)
        {
            exitApp();
        }

        private void displayUserName()
        {
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Helvetica", 14.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label.ForeColor = System.Drawing.SystemColors.Window;
            this.label.Location = new System.Drawing.Point(65, 150);
            this.label.Name = "UserName";
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Size = new System.Drawing.Size(93, 32);
            this.label.TabIndex = 0;
            this.label.Text = Server.User != null ? Server.User.m_About.m_Name : null;
            this.Controls.Add(label);
            this.label.BringToFront();
            this.Refresh();
        }

        private void displayUserThumbnail()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            picture.BackgroundImage = Server.User != null ? Server.User.m_About.m_ProfilePicture : null;
            picture.Name = "UserThumbnail";
            picture.Bounds = Rectangle.Round(Bounds);
            picture.Size = new Size(50, 50);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Location = new Point(15, 125);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, picture.Width - 3, picture.Height - 3);
            Region rg = new Region(gp);
            picture.Region = rg;
            this.Controls.Add(picture);
            this.picture.BringToFront();
            this.Refresh();
        }

        private void displayPhotoCover()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            picture.ImageLocation = Server.User != null ? Server.User.m_About.m_Cover : null;
            picture.Name = "UserThumbnail";
            picture.Bounds = Rectangle.Round(Bounds);
            picture.Size = new Size(300, 150);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Location = new Point(0, 0);
            this.Controls.Add(picture);
            this.Refresh();
        }
        #endregion

        #region Feed
        public void displayFeed(List<Tuple<Post, string, Image>> i_FeedPostsCollection)
        {
            displayFilterBar();
            outerPanel = new Panel();
            outerPanel.Name = "feedPanel";
            outerPanel.AutoScroll = true;
            outerPanel.Size = new Size(640, 560);
            outerPanel.BackColor = Color.FromArgb(233, 235, 238);
            outerPanel.Location = new Point(300, 30);
            filterBarControl.BringToFront();
            int counter = 0;
            this.Controls.Add(outerPanel);
            foreach (Tuple<Post, string, Image> post in i_FeedPostsCollection)
            {
                if(string.IsNullOrEmpty(post.Item1.PostContent))
                {
                    continue;
                }

                innerPanel = buildPostCard(post, counter);
                innerPanel.Location = counter == 0 ? new Point(70, 10 + (375 * counter)) : setLocationAfter("postPanel" + (counter - 1), "feedPanel", true, 70, 5);
                counter++;
            }

            outerPanel.BringToFront();
        }
        private void displayFilterBar()
        {
            filterBarControl = new Control();
            filterBarControl.Size = new Size(640, 30);
            filterBarControl.Location = new Point(300, 0);
            filterBarControl.BackColor = Color.White;

            picture = new PictureBox();
            picture.ImageLocation = "../../img/filterPanel.png";
            picture.Size = new Size(600, 30);
            picture.BackgroundImageLayout = ImageLayout.Zoom;
            picture.Location = new Point(0, 0);
            filterBarControl.Controls.Add(picture);
            
            picture = new PictureBox();
            picture.Name = "likeFilter";
            picture.ImageLocation = filterLike ? "../../img/likeOn.png" : "../../img/likeOff.png";
            picture.Size = new Size(75, 30);
            picture.Location = new Point(95, 0);
            picture.MouseClick += filterByLike_MouseClick;
            filterBarControl.Controls.Add(picture);
            picture.BringToFront();
            
            picture = new PictureBox();
            picture.Name = "friendFilter";
            picture.ImageLocation = filterFriends ? "../../img/bestFriendsOn.png" : "../../img/bestFriendsOff.png";
            picture.Size = new Size(75, 30);
            picture.Location = new Point(175, 0);
            picture.MouseClick += filterByFriend_MouseClick;
            filterBarControl.Controls.Add(picture);
            picture.BringToFront();

            this.Controls.Add(filterBarControl);
        }
        private Panel buildPostCard(Tuple<Post, string, Image> i_PostToBuild, int i_Counter)
        {
            innerPanel = new Panel();
            innerPanel.BackColor = Color.White;
            innerPanel.Name = "postPanel" + i_Counter;
            innerPanel.Size = new System.Drawing.Size(450, 370);
            innerPanel.TabIndex = 1 + i_Counter;
            innerPanel.BorderStyle = BorderStyle.FixedSingle;
            outerPanel.Controls.Add(innerPanel);

            label = new Label();
            label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label.ForeColor = Color.FromArgb(0, 48, 88, 152);
            label.Font = new System.Drawing.Font(
                "Arial",
                10.2F,
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point,
                (byte)0);
            label.Location = new System.Drawing.Point(70, 15);
            label.Margin = new System.Windows.Forms.Padding(8, 15, 3, 0);
            label.Name = "nameLabel";
            label.AutoSize = true;
            label.TabIndex = 2;
            label.Text = i_PostToBuild.Item2;
            innerPanel.Controls.Add(label);

            label = new Label();
            label.AutoSize = true;
            label.Font = new System.Drawing.Font(
                "Arial",
                8.8F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                (byte)0);
            label.Margin = new System.Windows.Forms.Padding(0, 100, 3, 0);
            label.Name = "timeLabel";
            label.AutoSize = true;
            label.TabIndex = 3;
            string date = string.Empty;
            TimeSpan timeSincePost = DateTime.Now.Subtract(i_PostToBuild.Item1.PostCreateTime.Value);
            if (timeSincePost.Days > 0)
            {
                if (timeSincePost.Days > 365)
                {
                    date = string.Format("{0:MMM dd , yyyy}", i_PostToBuild.Item1.PostCreateTime);
                }
                else
                {
                    date = string.Format("{0:MMM dd}", i_PostToBuild.Item1.PostCreateTime);
                }
            }
            else
            {
                date = string.Format("{0} hours {1} min ago", timeSincePost.Hours, timeSincePost.Minutes);
            }

            label.Text = date;
            innerPanel.Controls.Add(label);
            label.Location = setLocationAfter("nameLabel", "postPanel" + i_Counter, false, 75, 5);

            picture = new PictureBox();
            picture.Size = new System.Drawing.Size(50, 50);
            picture.BackgroundImage = i_PostToBuild.Item1.postProfileImage;
            picture.Bounds = Rectangle.Round(Bounds);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(10, 10, 50, 50);
            Region rg = new Region(gp);
            picture.Region = rg;
            picture.TabIndex = 9;
            picture.Location = new Point(0, 0);
            picture.Name = "postRelatePicture";
            innerPanel.Controls.Add(picture);

            contentBox = new TextBox();
            contentBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            contentBox.BackColor = Color.White;
            contentBox.Font = new System.Drawing.Font(
                "Arial",
                9.8F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                (byte)0);
            contentBox.TabIndex = 4;
            contentBox.Size = new System.Drawing.Size(380, 50);
            contentBox.Location = setLocationAfter("timeLabel", "postPanel" + i_Counter, false, 84, 10);
            contentBox.Multiline = true;
            contentBox.ScrollBars = ScrollBars.Vertical;
            contentBox.Name = "contentBox";
            contentBox.ReadOnly = true;
            contentBox.Text = i_PostToBuild.Item1.PostContent;
            contentBox.TextAlign = HorizontalAlignment.Right;
            contentBox.BorderStyle = BorderStyle.None;
            innerPanel.Controls.Add(contentBox);

            picture = new PictureBox();
            picture.TabIndex = 5;
            picture.Size = new System.Drawing.Size(435, 170);
            picture.Location = setLocationAfter("contentBox", "postPanel" + i_Counter, false, 7, 5);
            picture.Name = "postPicture";
            picture.ImageLocation = i_PostToBuild.Item1.PostContentImageUrl;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            innerPanel.Controls.Add(picture);

            label = new Label();
            label.Name = "likesAndComments";
            label.TabIndex = 6;
            label.AutoSize = true;
            label.Tag = i_PostToBuild.Item1;
            label.Text = string.Format(
                "{0} likes {1} comments",
                (label.Tag as Post).AmountOfLikes,
                (label.Tag as Post).AmountOfComments);
            label.Font = new System.Drawing.Font(
                "Arial",
                8.8F,
                System.Drawing.FontStyle.Regular,
                System.Drawing.GraphicsUnit.Point,
                ((byte)(0)));
            label.Location = setLocationAfter("postPicture", "postPanel" + i_Counter, false, 6, 5);
            innerPanel.Controls.Add(label);

            likeButton = buildLikeButton(i_PostToBuild.Item1, i_Counter);

            commentButton = new Button();
            commentButton.BackgroundImage = Image.FromFile("../../img/comment.png");
            commentButton.BackgroundImageLayout = ImageLayout.Stretch;
            commentButton.Size = new Size(110, 35);
            commentButton.Name = "comment";
            commentButton.TabIndex = 8;
            commentButton.Location = setLocationAfter("likesAndComments", "postPanel" + i_Counter, false, 240, 10);
            commentButton.ForeColor = Color.FromArgb(0, 48, 88, 152);
            commentButton.MouseClick += commentButton_MouseClick;
            innerPanel.Controls.Add(commentButton);

            return innerPanel;
        }
        private Button buildLikeButton(Post i_PostToBuildFor, int i_Counter)
        {
            likeButton = new Button();
            likeButton.Tag = i_PostToBuildFor.IsLikedByUser;
            likeButton.BackgroundImage = i_PostToBuildFor.IsLikedByUser ? Image.FromFile("../../img/LikeCommenton.png") : Image.FromFile("../../img/likeButton.png");
            likeButton.BackgroundImageLayout = ImageLayout.Stretch;
            likeButton.Size = new Size(110, 35);
            likeButton.Name = "like" + i_Counter;
            likeButton.Location = setLocationAfter("likesAndComments", "postPanel" + i_Counter, false, 90, 10);
            likeButton.ForeColor = Color.FromArgb(0, 48, 88, 152);
            likeButton.MouseClick += likeButton_MouseClick;
            innerPanel.Controls.Add(likeButton);
            return likeButton;
        }
        private void setFilterBarState(PictureBox i_FilterMode)
        {
            bool filterToEnable;
            bool filterToDisable;
            bool isLikeFilter = i_FilterMode.Name.Equals("likeFilter");
            if (isLikeFilter)
            {
                filterToEnable = filterLike;
                filterToDisable = filterFriends;
            }
            else
            {
                filterToEnable = filterFriends;
                filterToDisable = filterLike;
            }

            if (!filterToEnable)
            {
                if (filterToDisable)
                {
                    this.Controls.Remove(filterBarControl);
                    i_FilterMode.ImageLocation = isLikeFilter
                                                     ? "../../img/bestFriendsOff.png"
                                                     : "../../ img / likeOff.png";
                    filterLike = !filterLike;
                    filterFriends = !filterFriends;
                    this.Controls.Add(filterBarControl);
                }

                if (isLikeFilter)
                {
                    sortFeedByLike();
                }
                else
                {
                    sortFeedByFriends();
                }
            }
            else
            {
                if (isLikeFilter)
                {
                    filterLike = false;
                }
                else
                {
                    filterFriends = false;
                }

                this.Controls.Remove(outerPanel);
                this.Controls.Remove(filterBarControl);
                displayFeed(Server.User.m_Feed);
            }
        }
        private void userReactToPost(Button i_RelevantButton, bool i_Like)
        {
            label = (Label)i_RelevantButton.Parent.Controls.Find("likesAndComments", false).First();
            Post relevantPost = (label.Tag as Post);
            if (i_Like)
            {
                relevantPost.AmountOfLikes += relevantPost.IsLikedByUser ? (-1) : 1;
                relevantPost.IsLikedByUser = !relevantPost.IsLikedByUser;
                label.Parent.Controls.Remove(i_RelevantButton);
                i_RelevantButton = buildLikeButton(relevantPost, int.Parse(i_RelevantButton.Name.Substring(i_RelevantButton.Name.Length - 1)));
                label.Parent.Controls.Add(i_RelevantButton);
            }
            else
            {
                Form commentWindow = new commentForm();
                commentWindow.ShowDialog();
                if (commentWindow.DialogResult == DialogResult.OK)
                {
                    commentWindow.Tag = false;
                    relevantPost.AmountOfComments += 1;
                }
            }

            updateAmounts(label);
            i_RelevantButton.Parent.Controls.Remove(label);
            i_RelevantButton.Parent.Controls.Add(label);
        }
        private void updateAmounts(Control i_LabelToUpdate)
        {
            Post post = i_LabelToUpdate.Tag as Post;
            i_LabelToUpdate.Text = string.Format("{0} likes {1} comments", post.AmountOfLikes, post.AmountOfComments);
        }
        private Point setLocationAfter(string i_NeighborControlToSetAfter, string i_fatherControl, bool i_IsRoot, int i_XCoordinate, int i_SpaceBetween)
        {
            Control neighbor = i_IsRoot
                                   ? this.Controls.Find(i_fatherControl, false).First().Controls
                                       .Find(i_NeighborControlToSetAfter, false).First()
                                   : this.Controls.Find("feedPanel", false).First().Controls
                                       .Find(i_fatherControl, false).First().Controls.Find(
                                           i_NeighborControlToSetAfter,
                                           false).First();
            Point location = new Point(i_XCoordinate, neighbor.Location.Y + neighbor.Size.Height + i_SpaceBetween);

            return location;
        }
        private void sortFeedByFriends()
        {
            filterFriends = true;
            List<Tuple<Post, string, Image>> BestFriendsPost = new List<Tuple<Post, string, Image>>();
            foreach (Friend friend in Server.User.m_FriendsList)
            {
                if (!friend.m_Follow)
                {
                    continue;
                }

                foreach (Post post in friend.FriendPosts)
                {
                    BestFriendsPost.Add(new Tuple<Post, string, Image>(post, friend.m_About.m_Name, friend.m_About.m_ProfilePicture));
                }
            }

            this.Controls.Remove(outerPanel);
            this.Controls.Remove(filterBarControl);
            displayFeed(BestFriendsPost);
        }
        private void sortFeedByLike()
        {
            filterLike = true;
            List<Tuple<Post, string, Image>> feedListSortedBy =
                Server.User.m_Feed.OrderByDescending(o => o.Item1.AmountOfLikes).ToList();
            this.Controls.Remove(outerPanel);
            this.Controls.Remove(filterBarControl);
            displayFeed(feedListSortedBy);
        }
        #endregion

        #region About
        private void showAbout()
        {
            initRightControl();
            string[] abouts =
                {
                    Server.User.m_About.m_Name, Server.User.m_About.m_Email, Server.User.m_About.m_Birthday, Server.User.m_About.m_Hometown, Server.User.m_FriendsList.Count.ToString()
                };

            TableLayoutPanel aboutInnerPanel = new TableLayoutPanel();
            aboutInnerPanel.Dock = DockStyle.Fill;
            aboutInnerPanel.BackColor = Color.FromArgb(233, 235, 238);
            aboutInnerPanel.AllowDrop = true;
            aboutInnerPanel.AutoScroll = true;
            aboutInnerPanel.ColumnCount = 4;
            aboutInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.80488F));
            aboutInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.19512F));
            aboutInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            aboutInnerPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 296F));
            aboutInnerPanel.Location = new System.Drawing.Point(97, 65);
            aboutInnerPanel.Name = "aboutInnerPanel";
            aboutInnerPanel.RowCount = 4;
            aboutInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.08537F));
            aboutInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.91463F));
            aboutInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.39024F));
            aboutInnerPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.30488F));

            int counter = 1;
            foreach (string detail in abouts)
            {
                picture = new PictureBox();
                picture.Size = new Size(76, 70);
                picture.Name = "aboutDetail" + counter;
                picture.BackgroundImage = Image.FromFile("../../img/aboutIcon" + counter + ".png");
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Margin = new Padding(0, 10, 0, 0);
                int column = (counter / 4) == 0 ? 0 : 2;
                aboutInnerPanel.Controls.Add(picture, column, ((counter - 1) % 3));

                label = new Label();
                label.Text = detail;
                label.AutoSize = true;
                label.Margin = new Padding(10, 35, 0, 0);
                aboutInnerPanel.Controls.Add(label, column + 1, ((counter - 1) % 3));
                counter++;
            }

            rightControl.Controls.Add(aboutInnerPanel);
            this.Controls.Add(rightControl);
            rightControl.BringToFront();
        }
        #endregion

        #region Friend
        private void buildFriendsList()
        {
            hideAllControls();
            initRightControl();
            initRightLayout();

            for (int i = 0; i < Server.User.m_FriendsList.Count; i++)
            {
                outerPanel = new Panel();
                outerPanel.Size = new Size(400, 85);
                outerPanel.BackColor = Color.FromArgb(0, 233, 235, 238);
                picture = new PictureBox();
                picture.BackgroundImage = Server.User.m_FriendsList[i].m_About.m_ProfilePicture;
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Name = "friend" + i;
                picture.Size = new Size(85, 85);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.TabIndex = 0;

                label = new Label();
                label.Text = Server.User.m_FriendsList[i].m_About.m_Name;
                label.Location = new Point(90, 0);
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Size = new Size(300, 85);
                label.Font = new Font("Helvetica", 14.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)177);
                label.ForeColor = Color.FromArgb(0, 61, 82, 149);
                outerPanel.Controls.Add(picture);

                picture = new PictureBox();
                picture.BackgroundImage = Server.User.m_FriendsList[i].m_Follow ? Image.FromFile("../../img/follow.png") : Image.FromFile("../../img/unfollow.png");
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Location = new Point(350, 22);
                picture.Size = new Size(40, 40);
                picture.Name = i.ToString();
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.MouseClick += favoriteFriend_Click;
                outerPanel.Controls.Add(picture);
                
                outerPanel.Controls.Add(label);
                rightLayout.Controls.Add(outerPanel);
            }

            rightControl.Controls.Add(rightLayout);
            this.Controls.Add(rightControl);
            rightControl.BringToFront();
        }
        #endregion

        #region Albums
        private void buildPhotos<T>(List<T> i_Photos, int i_AlbumId)
        {
            initGallary();

            for (int i = 0; i < i_Photos.Count; i++)
            {
                string name = Server.User.m_Album[i_AlbumId].m_Photos[i].m_Name;
                string picture = Server.User.m_Album[i_AlbumId].m_Photos[i].m_PictureNormalURL;
                buildGallaryItem(i, picture, name);
                this.picture.Name = i_AlbumId + ":" + i;
                this.picture.MouseClick += showPhoto_Click;
                rightLayout.Controls.Add(outerPanel);
            }

            addToGallary();
        }
        private void buildGallary<T>(List<T> i_Items)
        {
            initGallary();

            for (int i = 0; i < i_Items.Count; i++)
            {
                string name = Server.User.m_Album[i].m_Name;
                string picture = Server.User.m_Album[i].m_PictureAlbumURL;
                buildGallaryItem(i, picture, name);
                this.picture.Name = i.ToString();
                this.picture.MouseClick += showPictures_Click;
                rightLayout.Controls.Add(outerPanel);
            }

            addToGallary();
        }
        private void buildGallaryItem(int i_Index, string i_PictureUrl, string i_Title)
        {
            picture = new PictureBox();
            if (i_PictureUrl == "default")
            {
                picture.BackgroundImage = Image.FromFile("../../img/emptyPhoto.png");
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                picture.ImageLocation = i_PictureUrl;
            }

            picture.Size = new Size(150, 150);
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            
            label = new Label();
            label.Text = i_Title;
            label.Font = new Font("Segoe UI", 10, FontStyle.Bold, GraphicsUnit.Pixel, (byte)177);
            label.Location = new Point(0, 155);
            label.Size = new Size(150, 60);
            
            outerPanel = new Panel();
            outerPanel.Size = new Size(150, 180);
            outerPanel.Controls.Add(picture);
            outerPanel.Controls.Add(label);
        }
        private void initGallary()
        {
            initRightLayout();
            initRightControl();
        }
        private void addToGallary()
        {
            rightControl.Controls.Add(rightLayout);
            this.Controls.Add(rightControl);
            rightControl.BringToFront();
        }
        private void showPhoto(string i_PictureName)
        {
            this.rightControl.Show();

            initRightControl();

            string pictureLocation = i_PictureName;
            int albumId = int.Parse(pictureLocation.Split(':')[0]);
            int photoId = int.Parse(pictureLocation.Split(':')[1]);
            Photo photo = Server.User.m_Album[albumId].m_Photos[photoId];

            picture = new PictureBox();
            picture.ImageLocation = photo.m_PictureNormalURL;
            picture.Name = "picture";
            picture.Size = new Size(440, 440);
            picture.Location = new Point(40, 40);
            picture.SizeMode = PictureBoxSizeMode.Zoom;

            rightControl.Controls.Add(picture);

            label = new Label();
            label.Name = "like and comments";
            label.Text = string.Format("{0} likes, {1} comments", photo.m_Like, photo.m_Comments);
            label.Location = new Point(40, 525);
            label.Size = new Size(150, 30);
            rightControl.Controls.Add(label);
            
            label = new Label();
            label.Name = albumId.ToString();
            label.Size = new Size(150, 30);
            label.Location = new Point(400, 525);
            label.Text = "Back to album";
            label.MouseClick += showPictures_Click;
            rightControl.Controls.Add(label);

            this.Controls.Add(rightControl);
            this.rightControl.BringToFront();
        }
        #endregion

        private void initRightControl()
        {
            rightControl = new Control();
            rightControl.Size = dataFrame;
            rightControl.Location = new Point(360, 0);
            rightControl.BackColor = Color.FromArgb(233, 235, 238);
        }
        private void initRightLayout()
        {
            rightLayout = new FlowLayoutPanel();
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.BackColor = Color.FromArgb(233, 235, 238);
            rightLayout.AllowDrop = true;
            rightLayout.AutoScroll = true;
        }
    }
}