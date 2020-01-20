using System.Drawing;

namespace BullsAndCowsFormUI
{
    public partial class StartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartForm));
            this.StartButton = new System.Windows.Forms.Button();
            this.NumberOfChancesCounter = new System.Windows.Forms.Label();
            this.PlusButton = new System.Windows.Forms.Button();
            this.MinuesButton = new System.Windows.Forms.Button();
            this.StartFormHeader = new System.Windows.Forms.PictureBox();
            this.ExitButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.StartFormHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartButton.Image = ((System.Drawing.Image)(resources.GetObject("StartButton.Image")));
            this.StartButton.Location = new System.Drawing.Point(339, 167);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(104, 65);
            this.StartButton.TabIndex = 0;
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // NumberOfChancesCounter
            // 
            this.NumberOfChancesCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(94)))), ((int)(((byte)(93)))));
            this.NumberOfChancesCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumberOfChancesCounter.Location = new System.Drawing.Point(203, 93);
            this.NumberOfChancesCounter.Name = "NumberOfChancesCounter";
            this.NumberOfChancesCounter.Size = new System.Drawing.Size(88, 69);
            this.NumberOfChancesCounter.TabIndex = 2;
            this.NumberOfChancesCounter.Text = " 4";
            // 
            // PlusButton
            // 
            this.PlusButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(98)))), ((int)(((byte)(95)))));
            this.PlusButton.FlatAppearance.BorderSize = 0;
            this.PlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlusButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PlusButton.Location = new System.Drawing.Point(297, 100);
            this.PlusButton.Name = "PlusButton";
            this.PlusButton.Size = new System.Drawing.Size(46, 47);
            this.PlusButton.TabIndex = 4;
            this.PlusButton.Text = " +";
            this.PlusButton.UseVisualStyleBackColor = false;
            this.PlusButton.Click += new System.EventHandler(this.buttonNumberOfChances_Click);
            // 
            // MinuesButton
            // 
            this.MinuesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(98)))), ((int)(((byte)(95)))));
            this.MinuesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinuesButton.Location = new System.Drawing.Point(132, 100);
            this.MinuesButton.Name = "MinuesButton";
            this.MinuesButton.Size = new System.Drawing.Size(46, 47);
            this.MinuesButton.TabIndex = 5;
            this.MinuesButton.Text = "-";
            this.MinuesButton.UseVisualStyleBackColor = false;
            this.MinuesButton.Click += new System.EventHandler(this.buttonNumberOfChances_Click);
            // 
            // StartFormHeader
            // 
            this.StartFormHeader.Image = ((System.Drawing.Image)(resources.GetObject("StartFormHeader.Image")));
            this.StartFormHeader.Location = new System.Drawing.Point(83, 6);
            this.StartFormHeader.Name = "StartFormHeader";
            this.StartFormHeader.Size = new System.Drawing.Size(301, 75);
            this.StartFormHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StartFormHeader.TabIndex = 6;
            this.StartFormHeader.TabStop = false;
            // 
            // ExitButton
            // 
            this.ExitButton.Image = ((System.Drawing.Image)(resources.GetObject("ExitButton.Image")));
            this.ExitButton.Location = new System.Drawing.Point(33, 167);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(104, 65);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(94)))), ((int)(((byte)(93)))));
            this.ClientSize = new System.Drawing.Size(470, 244);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.StartFormHeader);
            this.Controls.Add(this.MinuesButton);
            this.Controls.Add(this.PlusButton);
            this.Controls.Add(this.NumberOfChancesCounter);
            this.Controls.Add(this.StartButton);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bulls And Cows";
            ((System.ComponentModel.ISupportInitialize)(this.StartFormHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label NumberOfChancesCounter;
        private System.Windows.Forms.Button PlusButton;
        private System.Windows.Forms.Button MinuesButton;
        private System.Windows.Forms.PictureBox StartFormHeader;
        private System.Windows.Forms.Button ExitButton;
    }
}