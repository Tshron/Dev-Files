using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using FacebookAppServer;
using FBUser;

namespace FacebookAppUI
{
    public partial class AppForm
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
        private TextBox newText = new TextBox();
        private PictureBox picture;
        private Label label;
        private TextBox contentBox;
        private Button likeButton;
        private Button commentButton;
        private TableLayoutPanel aboutInnerPanel;
        private Label albumTitle;
        private Label albumDescription;
        
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
            this.label.Text = Server.User != null ? Server.User.m_About.Name : null;
            this.Controls.Add(label);
            this.label.BringToFront();
            this.Refresh();
        }

        private void displayUserThumbnail()
        {
            this.picture = new System.Windows.Forms.PictureBox();
            picture.BackgroundImage = Server.User != null ? Server.User.m_About.ProfilePicture : null;
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
            picture.ImageLocation = Server.User != null ? Server.User.m_About.Cover : null;
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
            this.Controls.Add(displayFilterBar(Server.GetFilterBarStatus()));
            buildOuterFeedPanel();
            int counter = 0;
            foreach (Tuple<Post, string, Image> post in i_FeedPostsCollection)
            {
                if(string.IsNullOrEmpty(post.Item1.PostContent))
                {
                    continue;
                }

                innerPanel = buildPostCard(post, counter);
                counter++;
            }

            outerPanel.BringToFront();
        }

        private void buildOuterFeedPanel()
        {
            outerPanel = new Panel();
            outerPanel.Name = "feedPanel";
            outerPanel.AutoScroll = true;
            outerPanel.Size = new Size(640, 560);
            outerPanel.BackColor = Color.FromArgb(233, 235, 238);
            outerPanel.Location = new Point(300, 30);
            filterBarControl.BringToFront();
            this.Controls.Add(outerPanel);
        }

        private Control displayFilterBar(Tuple<bool, bool> i_FilterCondition)
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
            picture.ImageLocation = i_FilterCondition.Item1 ? "../../img/likeOn.png" : "../../img/likeOff.png";
            picture.Size = new Size(75, 30);
            picture.Location = new Point(95, 0);
            picture.Tag = i_FilterCondition.Item1;
            picture.MouseClick += filterByLike_MouseClick;
            filterBarControl.Controls.Add(picture);
            picture.BringToFront();
            
            picture = new PictureBox();
            picture.Name = "friendFilter";
            picture.ImageLocation = i_FilterCondition.Item2 ? "../../img/bestFriendsOn.png" : "../../img/bestFriendsOff.png";
            picture.Size = new Size(75, 30);
            picture.Tag = i_FilterCondition.Item2;
            picture.Location = new Point(175, 0);
            picture.MouseClick += filterByFriend_MouseClick;
            filterBarControl.Controls.Add(picture);
            picture.BringToFront();

            return filterBarControl;
        }

        private Panel buildPostCard(Tuple<Post, string, Image> i_PostToBuild, int i_Counter)
        {
            buildInnerPanel(i_PostToBuild, i_Counter);
            
            buildNameLabel(i_PostToBuild, i_Counter);
            
            buildTimeLabel(i_PostToBuild.Item1, i_Counter);
            
            buildPostAuthorPicture(i_PostToBuild.Item1, i_Counter);

            buildContentBox(i_PostToBuild.Item1, i_Counter);

            buildContentPicture(i_PostToBuild.Item1, i_Counter);

            buildAmountsLabel(i_PostToBuild.Item1, i_Counter);

            buildLikeButton(i_PostToBuild.Item1, i_Counter);

            buildCommentButton(i_PostToBuild.Item1, i_Counter);
            
            return innerPanel;
        }

        private void buildInnerPanel(Tuple<Post, string, Image> i_PostToBuild, int i_Counter)
        {
            innerPanel = new Panel();
            innerPanel.BackColor = Color.White;
            innerPanel.Tag = i_PostToBuild;
            innerPanel.Name = "postPanel" + i_Counter;
            innerPanel.Size = new System.Drawing.Size(450, 370);
            innerPanel.TabIndex = 1 + i_Counter;
            innerPanel.BorderStyle = BorderStyle.FixedSingle;
            outerPanel.Controls.Add(innerPanel);
            innerPanel.Location = i_Counter == 0
                                      ? new Point(70, 10  + outerPanel.AutoScrollPosition.Y)
                                      : setLocation(innerPanel, "postPanel" + (i_Counter - 1), 70, 5);
        }

        private void buildNameLabel(Tuple<Post, string, Image> i_PostToBuild, int i_Counter)
        {
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
        }

        private void buildTimeLabel(Post i_Post, int i_Counter)
        {
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
            TimeSpan timeSincePost = DateTime.Now.Subtract(i_Post.PostCreateTime.Value);
            if (timeSincePost.Days > 0)
            {
                if (timeSincePost.Days > 365)
                {
                    date = string.Format("{0:MMM dd , yyyy}", i_Post.PostCreateTime);
                }
                else
                {
                    date = string.Format("{0:MMM dd}", i_Post.PostCreateTime);
                }
            }
            else
            {
                date = string.Format("{0} hours {1} min ago", timeSincePost.Hours, timeSincePost.Minutes);
            }

            label.Text = date;
            innerPanel.Controls.Add(label);
            label.Location = setLocation(label, "nameLabel", 75, 5);
        }

        private void buildPostAuthorPicture(Post i_Post, int i_Counter)
        {
            picture = new PictureBox();
            picture.Size = new System.Drawing.Size(50, 50);
            picture.BackgroundImage = i_Post.PostProfileImage;
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
        }

        private void buildContentPicture(Post i_Post, int i_Counter)
        {
            picture = new PictureBox();
            picture.TabIndex = 5;
            picture.Size = new System.Drawing.Size(435, 170);
            picture.Name = "postPicture";
            picture.ImageLocation = i_Post.PostContentImageUrl;
            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            innerPanel.Controls.Add(picture);
            picture.Location = setLocation(picture, "contentBox", 7, 5);
        }

        private void buildContentBox(Post i_Post, int i_Counter)
        {
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
            contentBox.Multiline = true;
            contentBox.ScrollBars = ScrollBars.Vertical;
            contentBox.Name = "contentBox";
            contentBox.ReadOnly = true;
            contentBox.Text = i_Post.PostContent;
            contentBox.TextAlign = HorizontalAlignment.Right;
            contentBox.BorderStyle = BorderStyle.None;
            innerPanel.Controls.Add(contentBox);
            contentBox.Location = setLocation(contentBox, "timeLabel", 84, 10);
        }

        private void buildCommentButton(Post i_Post, int i_Counter)
        {
            commentButton = new Button();
            commentButton.BackgroundImage = Image.FromFile("../../img/comment.png");
            commentButton.BackgroundImageLayout = ImageLayout.Stretch;
            commentButton.Size = new Size(110, 35);
            commentButton.Name = "comment" + i_Counter;
            commentButton.TabIndex = 8;
            commentButton.ForeColor = Color.FromArgb(0, 48, 88, 152);
            commentButton.MouseClick += commentButton_MouseClick;
            innerPanel.Controls.Add(commentButton);
            commentButton.Location = setLocation(commentButton, "likesAndComments" + i_Counter, 240, 10);
        }

        private void buildAmountsLabel(Post i_Post, int i_Counter)
        {
            label = new Label();
            label.Name = "likesAndComments" + i_Counter;
            label.TabIndex = 6;
            label.AutoSize = true;
            label.Tag = i_Post;
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
            innerPanel.Controls.Add(label);
            label.Location = setLocation(label, "postPicture", 6, 5);
        }

        private Button buildLikeButton(Post i_PostToBuildFor, int i_Counter)
        {
            likeButton = new Button();
            likeButton.Tag = i_PostToBuildFor.IsLikedByUser;
            likeButton.BackgroundImage = i_PostToBuildFor.IsLikedByUser ? Image.FromFile("../../img/LikeCommenton.png") : Image.FromFile("../../img/likeButton.png");
            likeButton.BackgroundImageLayout = ImageLayout.Stretch;
            likeButton.Size = new Size(110, 35);
            likeButton.Name = "like" + i_Counter;
            likeButton.ForeColor = Color.FromArgb(0, 48, 88, 152);
            likeButton.MouseClick += likeButton_MouseClick;
            innerPanel.Controls.Add(likeButton);
            likeButton.Location = setLocation(likeButton, "likesAndComments" + i_Counter, 90, 10);

            return likeButton;
        }
        
        private void sortFeedByFriends()
        {
            this.Controls.Remove(outerPanel);
            this.Controls.Remove(filterBarControl);
            displayFeed(Server.GetFollowedFriendsFeed());
        }

        private void sortFeedByLike()
        {
            this.Controls.Remove(outerPanel);
            this.Controls.Remove(filterBarControl);
            displayFeed(Server.GetSortedFeedList());
        }
        #endregion

        #region About
        private void showAbout()
        {
            initRightControl();
            List<string> abouts = new List<string>()
                                      {
                                          Server.User.m_About.Name,
                                          Server.User.m_About.Email,
                                          Server.User.m_About.Birthday,
                                          Server.User.m_About.Hometown,
                                          Server.User.m_FriendsList.Count.ToString()
                                      };

            aboutInnerPanel = new TableLayoutPanel();
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
                buildAboutsIcons(counter);
                buildAboutLabel(counter, detail);
                counter++;
            }

            rightControl.Controls.Add(aboutInnerPanel);
            this.Controls.Add(rightControl);
            rightControl.BringToFront();
        }

        private void buildAboutsIcons(int i_Counter)
        {
            picture = new PictureBox();
            picture.Size = new Size(76, 70);
            picture.Name = "aboutDetail" + i_Counter;
            picture.Bounds = Rectangle.Round(picture.Bounds);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, 70, 70);
            picture.Region = new Region(gp);
            picture.BackgroundImage = Image.FromFile("../../img/aboutIcon" + i_Counter + ".png");
            picture.BackgroundImageLayout = ImageLayout.Stretch;
            picture.Margin = new Padding(0, 10, 0, 0);
            int column = (i_Counter / 4) == 0 ? 0 : 2;
            aboutInnerPanel.Controls.Add(picture, column, ((i_Counter - 1) % 3));
        }

        private void buildAboutLabel(int i_Counter, string i_AboutDetail)
        {
            label = new Label();
            label.Text = i_AboutDetail;
            label.AutoSize = true;
            label.Margin = new Padding(10, 35, 0, 0);
            int column = (i_Counter / 4) == 0 ? 0 : 2;
            aboutInnerPanel.Controls.Add(label, column + 1, ((i_Counter - 1) % 3));
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
                outerPanel.Size = new Size(500, 85);
                outerPanel.BackColor = Color.FromArgb(0, 233, 235, 238);
                picture = new PictureBox();
                picture.BackgroundImage = Server.User.m_FriendsList[i].m_About.ProfilePicture;
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Name = "friend" + i;
                picture.Size = new Size(85, 85);
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.TabIndex = 0;

                label = new Label();
                label.Text = Server.User.m_FriendsList[i].m_About.Name;
                label.Location = new Point(90, 0);
                label.TextAlign = ContentAlignment.MiddleLeft;
                label.Size = new Size(300, 85);
                label.Font = new Font("Helvetica", 14.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)177);
                label.ForeColor = Color.FromArgb(0, 61, 82, 149);
                outerPanel.Controls.Add(picture);

                picture = new PictureBox();
                picture.BackgroundImage = Server.User.m_FriendsList[i].Follow ? Image.FromFile("../../img/follow.png") : Image.FromFile("../../img/unfollow.png");
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Location = new Point(350, 22);
                picture.Size = new Size(40, 40);
                picture.Name = i.ToString();
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.MouseClick += favoriteFriend_Click;
                outerPanel.Controls.Add(picture);

                picture = new PictureBox();
                picture.BackgroundImage = Image.FromFile("../../../GreetingCard/img/envelope1.png");
                picture.BackgroundImageLayout = ImageLayout.Stretch;
                picture.Location = new Point(410, 22);
                picture.Size = new Size(65, 45);
                picture.Name = i.ToString();
                picture.Tag = Server.User.m_FriendsList[i];
                picture.SizeMode = PictureBoxSizeMode.StretchImage;
                picture.MouseClick += sendCard_MouseClick;
                outerPanel.Controls.Add(picture);

                outerPanel.Controls.Add(label);
                rightLayout.Controls.Add(outerPanel);
            }

            rightControl.Controls.Add(rightLayout);
            this.Controls.Add(rightControl);
            rightControl.BringToFront();
        }

        private void sendCard_MouseClick(object sender, MouseEventArgs e)
        {
            openGreetingCardSelector((sender as PictureBox).Tag as FBUser.FBUser);
        }
        #endregion
       
        #region Albums
        private void buildPhotos(Album i_Album, int i_AlbumId)
        {
            albumTitle = new Label();
            albumTitle.Text = i_Album.Name;
            albumTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold, GraphicsUnit.Pixel, (byte)177);
            albumTitle.Location = new Point(360, 0);
            albumTitle.Name = i_AlbumId.ToString();
            albumTitle.Tag = "AlbumTitle";
            albumTitle.Size = new Size(450, 40);
            albumTitle.TextAlign = ContentAlignment.MiddleCenter;
            albumTitle.BackColor = Color.FromArgb(233, 235, 238);
            albumTitle.MouseDoubleClick += changeText_Click;
            this.Controls.Add(albumTitle);
            albumTitle.BringToFront();

            albumDescription = new Label();
            albumDescription.Text = i_Album.Description != null ? i_Album.Description : "click to add description";
            albumDescription.Font = new Font("Segoe UI", 12, FontStyle.Bold, GraphicsUnit.Pixel, (byte)177);
            albumDescription.Location = new Point(360, 40);
            albumDescription.Name = i_AlbumId.ToString();
            albumDescription.Size = new Size(450, 40);
            albumDescription.Tag = "AlbumDescription";
            albumDescription.TextAlign = ContentAlignment.MiddleCenter;
            albumDescription.BackColor = Color.FromArgb(233, 235, 238);
            albumDescription.MouseDoubleClick += changeText_Click;
            this.Controls.Add(albumDescription);
            albumDescription.BringToFront();

            initGallary();
            rightControl.Size = new Size(dataFrame.Width, dataFrame.Height - 80);
            rightControl.Location = new Point(360, 80);
            
            for (int i = 0; i < i_Album.Photos.Count; i++)
            {
                string name = Server.User.m_Album[i_AlbumId].Photos[i].Name;
                string picture = Server.User.m_Album[i_AlbumId].Photos[i].PictureNormalURL;
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
                string name = Server.User.m_Album[i].Name;
                string picture = Server.User.m_Album[i].PictureAlbumURL;
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
            Photo photo = Server.User.m_Album[albumId].Photos[photoId];

            picture = new PictureBox();
            picture.ImageLocation = photo.PictureNormalURL;
            picture.Name = "picture";
            picture.Size = new Size(440, 440);
            picture.Location = new Point(40, 40);
            picture.SizeMode = PictureBoxSizeMode.Zoom;

            rightControl.Controls.Add(picture);

            label = new Label();
            label.Name = "like and comments";
            label.Text = string.Format("{0} likes, {1} comments", photo.Like, photo.Comments);
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

        #region GreetingCards
        private void openGreetingCardSelector(FBUser.FBUser i_Friend)
        {
            initPanel();
            int counter = 0;
            foreach(string cardName in Server.GetAllCardsOptions())
            {
                picture = new PictureBox();
                picture.ImageLocation = "../../../GreetingCard/img/cardPick" + counter + ".png";
                picture.BackgroundImageLayout = ImageLayout.Zoom;
                picture.Size = new Size(80, 80);
                picture.Name = cardName;
                picture.Location = new Point(50, 10 + (110 * counter));
                picture.Tag = i_Friend;
                picture.MouseClick += CardPicker_MouseClick;
                outerPanel.Controls.Add(picture);

                label = new Label();
                label.Text = cardName + " card";
                label.Size = new Size(100, 20);
                label.Location = new Point(picture.Location.X + 100, picture.Location.Y + 35);
                outerPanel.Controls.Add(label);
                counter++;
            }

            this.Controls.Add(outerPanel);
            outerPanel.BringToFront();
        }

        private void CardPicker_MouseClick(object sender, MouseEventArgs e)
        {
            displayCard(sender as PictureBox);
        }

        #endregion

        private void initRightControl()
        {
            rightControl = new Control();
            rightControl.Size = dataFrame;
            rightControl.Location = new Point(360, 0);
            rightControl.BackColor = Color.FromArgb(233, 235, 238);
        }

        private void initPanel()
        {
            outerPanel = new Panel();
            outerPanel.Size = dataFrame;
            outerPanel.Location = new Point(360, 0);
            outerPanel.BackColor = Color.FromArgb(233, 235, 238);
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