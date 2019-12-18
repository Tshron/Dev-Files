﻿using System;
using System.Windows.Forms;

namespace BullsAndCowsFormUI
{
    public partial class WinLoseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinLoseForm));
            this.textbox = new System.Windows.Forms.Label();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textbox
            // 
            this.textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(98)))), ((int)(((byte)(95)))));
            this.textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox.ForeColor = System.Drawing.Color.Red;
            this.textbox.Location = new System.Drawing.Point(92, 49);
            this.textbox.Name = "textbox";
            this.textbox.Size = new System.Drawing.Size(267, 96);
            this.textbox.TabIndex = 3;
            this.textbox.Text = "      Game over :(\r\nYou are out of guess";
            // 
            // ExitButton
            // 
            this.ExitButton.Image = ((System.Drawing.Image)(resources.GetObject("ExitButton.Image")));
            this.ExitButton.Location = new System.Drawing.Point(155, 148);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(112, 69);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.goNextButtons_Click);
            // 
            // WinLoseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(98)))), ((int)(((byte)(95)))));
            this.ClientSize = new System.Drawing.Size(425, 266);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.textbox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinLoseForm";
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label textbox;
        private System.Windows.Forms.Button ExitButton;
    }
}