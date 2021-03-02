
namespace ModBotInstaller
{
    partial class UpdateAvailable
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
            System.Windows.Forms.Label modBotLauncherUpdateAvailableLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAvailable));
            this.panel1 = new System.Windows.Forms.Panel();
            this.installButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            modBotLauncherUpdateAvailableLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // modBotLauncherUpdateAvailableLabel
            // 
            modBotLauncherUpdateAvailableLabel.AutoSize = true;
            modBotLauncherUpdateAvailableLabel.Font = new System.Drawing.Font("Consolas", 12F);
            modBotLauncherUpdateAvailableLabel.ForeColor = System.Drawing.Color.Lime;
            modBotLauncherUpdateAvailableLabel.Location = new System.Drawing.Point(16, 10);
            modBotLauncherUpdateAvailableLabel.Name = "modBotLauncherUpdateAvailableLabel";
            modBotLauncherUpdateAvailableLabel.Size = new System.Drawing.Size(306, 19);
            modBotLauncherUpdateAvailableLabel.TabIndex = 0;
            modBotLauncherUpdateAvailableLabel.Text = "Mod Bot Launcher update available";
            modBotLauncherUpdateAvailableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.installButton);
            this.panel1.Controls.Add(this.continueButton);
            this.panel1.Controls.Add(modBotLauncherUpdateAvailableLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 136);
            this.panel1.TabIndex = 0;
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.installButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.installButton.FlatAppearance.BorderSize = 0;
            this.installButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installButton.Font = new System.Drawing.Font("Consolas", 10F);
            this.installButton.ForeColor = System.Drawing.Color.White;
            this.installButton.Location = new System.Drawing.Point(112, 46);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(107, 24);
            this.installButton.TabIndex = 9;
            this.installButton.Text = "Install";
            this.installButton.UseVisualStyleBackColor = false;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.continueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.continueButton.FlatAppearance.BorderSize = 0;
            this.continueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continueButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueButton.ForeColor = System.Drawing.Color.White;
            this.continueButton.Location = new System.Drawing.Point(112, 96);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(107, 23);
            this.continueButton.TabIndex = 8;
            this.continueButton.Text = "Continue anyway";
            this.continueButton.UseVisualStyleBackColor = false;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // UpdateAvailable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(344, 136);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateAvailable";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mod-Bot Launcher Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Button continueButton;
    }
}