using System.Drawing;
using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    public partial class ErrorForm
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
            this.textToUser = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textToUser
            // 
            this.textToUser.AutoSize = true;
            this.textToUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textToUser.ForeColor = System.Drawing.Color.Red;
            this.textToUser.Location = new System.Drawing.Point(30, 30);
            this.textToUser.Name = "textToUser";
            this.textToUser.Size = new System.Drawing.Size(349, 25);
            this.textToUser.TabIndex = 0;
            this.textToUser.Text = "You can\'t pick the same color twice";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(146, 89);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(120, 34);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.Ok_Click);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(98)))), ((int)(((byte)(95)))));
            this.ClientSize = new System.Drawing.Size(412, 151);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.textToUser);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ErrorForm";
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ErrorForm_Closed(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

        private System.Windows.Forms.Label textToUser;
        private Button OkButton;
    }
}