using System.Windows.Forms;

namespace Othello.Forms
{
    partial class GameSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettingsForm));
            this.m_BoardSizeButton = new System.Windows.Forms.Button();
            this.m_PlayAgainstComputerButton = new System.Windows.Forms.Button();
            this.m_PlayAgainstPlayerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_BoardSizeButton
            // 
            this.m_BoardSizeButton.AutoSize = true;
            this.m_BoardSizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_BoardSizeButton.Location = new System.Drawing.Point(10, 11);
            this.m_BoardSizeButton.Margin = new System.Windows.Forms.Padding(2);
            this.m_BoardSizeButton.Name = "m_BoardSizeButton";
            this.m_BoardSizeButton.Size = new System.Drawing.Size(333, 68);
            this.m_BoardSizeButton.TabIndex = 0;
            this.m_BoardSizeButton.Text = "Board size: 6x6 (Press this button to increase board size)";
            this.m_BoardSizeButton.Click += new System.EventHandler(this.m_BoardSizeButton_Click);
            // 
            // m_PlayAgainstComputerButton
            // 
            this.m_PlayAgainstComputerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_PlayAgainstComputerButton.Location = new System.Drawing.Point(186, 84);
            this.m_PlayAgainstComputerButton.Name = "m_PlayAgainstComputerButton";
            this.m_PlayAgainstComputerButton.Size = new System.Drawing.Size(157, 69);
            this.m_PlayAgainstComputerButton.TabIndex = 0;
            this.m_PlayAgainstComputerButton.Text = "Play With A Friend";
            this.m_PlayAgainstComputerButton.Click += new System.EventHandler(this.m_PlayAgainstComputerButton_Click);
            // 
            // m_PlayAgainstPlayerButton
            // 
            this.m_PlayAgainstPlayerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_PlayAgainstPlayerButton.Location = new System.Drawing.Point(10, 84);
            this.m_PlayAgainstPlayerButton.Name = "m_PlayAgainstPlayerButton";
            this.m_PlayAgainstPlayerButton.Size = new System.Drawing.Size(170, 69);
            this.m_PlayAgainstPlayerButton.TabIndex = 0;
            this.m_PlayAgainstPlayerButton.Text = "Play Against The Computer (As a single player)";
            this.m_PlayAgainstPlayerButton.Click += new System.EventHandler(this.m_PlayAgainstPlayerButton_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(354, 167);
            this.Controls.Add(this.m_BoardSizeButton);
            this.Controls.Add(this.m_PlayAgainstPlayerButton);
            this.Controls.Add(this.m_PlayAgainstComputerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        // Members
        private Button m_BoardSizeButton;
        private  Button m_PlayAgainstComputerButton;
        private  Button m_PlayAgainstPlayerButton;
    }
}