using System.Windows.Forms;

namespace FacebookAppUI
{
    public partial class commentForm
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
            this.pleaseType = new System.Windows.Forms.Label();
            this.commentBox = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pleaseType
            // 
            this.pleaseType.AutoSize = true;
            this.pleaseType.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pleaseType.Location = new System.Drawing.Point(154, 32);
            this.pleaseType.Name = "pleaseType";
            this.pleaseType.Size = new System.Drawing.Size(221, 24);
            this.pleaseType.TabIndex = 0;
            this.pleaseType.Text = "Type in your comment";
            // 
            // commentBox
            // 
            this.commentBox.Location = new System.Drawing.Point(48, 78);
            this.commentBox.Multiline = true;
            this.commentBox.Name = "commentBox";
            this.commentBox.Size = new System.Drawing.Size(448, 104);
            this.commentBox.TabIndex = 1;
            // 
            // Send
            // 
            this.Send.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Send.Location = new System.Drawing.Point(206, 214);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(130, 38);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // commentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(564, 290);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Controls.Add(this.Send);
            this.Controls.Add(this.commentBox);
            this.Controls.Add(this.pleaseType);
            this.Name = "commentForm";
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label pleaseType;
        private System.Windows.Forms.TextBox commentBox;
        private System.Windows.Forms.Button Send;
    }
}