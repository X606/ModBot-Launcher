
namespace ModBotInstaller
{
	partial class Form2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.InstallButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.LocalVersionLabel = new System.Windows.Forms.Label();
            this.LatestVersionLabel = new System.Windows.Forms.Label();
            this.Reinstall = new System.Windows.Forms.Button();
            this.installedModsView = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.installedModsTitle = new System.Windows.Forms.Label();
            this.modSettingsIconHover = new System.Windows.Forms.ToolTip(this.components);
            this.modBotSettingsButton = new System.Windows.Forms.Button();
            this.installingModBotLabel = new System.Windows.Forms.Label();
            this.ProgressBar = new ModBotInstaller.NewProgressBar();
            this.installedModsView.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // InstallButton
            // 
            this.InstallButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.InstallButton.FlatAppearance.BorderSize = 0;
            this.InstallButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InstallButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstallButton.ForeColor = System.Drawing.Color.White;
            this.InstallButton.Location = new System.Drawing.Point(387, 451);
            this.InstallButton.Name = "InstallButton";
            this.InstallButton.Size = new System.Drawing.Size(118, 23);
            this.InstallButton.TabIndex = 8;
            this.InstallButton.Text = "Install Mod-Bot";
            this.InstallButton.UseVisualStyleBackColor = false;
            this.InstallButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(12, 451);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(76, 23);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.White;
            this.StatusLabel.Location = new System.Drawing.Point(12, 9);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(493, 33);
            this.StatusLabel.TabIndex = 10;
            this.StatusLabel.Text = "Message";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LocalVersionLabel
            // 
            this.LocalVersionLabel.AutoSize = true;
            this.LocalVersionLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LocalVersionLabel.ForeColor = System.Drawing.Color.White;
            this.LocalVersionLabel.Location = new System.Drawing.Point(13, 404);
            this.LocalVersionLabel.Name = "LocalVersionLabel";
            this.LocalVersionLabel.Size = new System.Drawing.Size(105, 15);
            this.LocalVersionLabel.TabIndex = 11;
            this.LocalVersionLabel.Text = "Local Version:";
            // 
            // LatestVersionLabel
            // 
            this.LatestVersionLabel.AutoSize = true;
            this.LatestVersionLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LatestVersionLabel.ForeColor = System.Drawing.Color.White;
            this.LatestVersionLabel.Location = new System.Drawing.Point(12, 428);
            this.LatestVersionLabel.Name = "LatestVersionLabel";
            this.LatestVersionLabel.Size = new System.Drawing.Size(112, 15);
            this.LatestVersionLabel.TabIndex = 12;
            this.LatestVersionLabel.Text = "Latest Version:";
            // 
            // Reinstall
            // 
            this.Reinstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Reinstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.Reinstall.FlatAppearance.BorderSize = 0;
            this.Reinstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reinstall.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reinstall.ForeColor = System.Drawing.Color.White;
            this.Reinstall.Location = new System.Drawing.Point(263, 450);
            this.Reinstall.Name = "Reinstall";
            this.Reinstall.Size = new System.Drawing.Size(118, 23);
            this.Reinstall.TabIndex = 14;
            this.Reinstall.Text = "Reinstall";
            this.Reinstall.UseVisualStyleBackColor = false;
            this.Reinstall.Click += new System.EventHandler(this.Reinstall_Click);
            // 
            // installedModsView
            // 
            this.installedModsView.AutoScroll = true;
            this.installedModsView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.installedModsView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.installedModsView.Controls.Add(this.panel1);
            this.installedModsView.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.installedModsView.Location = new System.Drawing.Point(7, 92);
            this.installedModsView.Name = "installedModsView";
            this.installedModsView.Padding = new System.Windows.Forms.Padding(5);
            this.installedModsView.Size = new System.Drawing.Size(500, 300);
            this.installedModsView.TabIndex = 15;
            this.installedModsView.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(8, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 58);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 6F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(321, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 9);
            this.label4.TabIndex = 6;
            this.label4.Text = "Version: 1.55.7.0.4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 6F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(321, 42);
            this.label3.MaximumSize = new System.Drawing.Size(128, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 9);
            this.label3.TabIndex = 5;
            this.label3.Text = "By: Cool mod person man";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.UseMnemonic = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.ErrorImage = null;
            this.pictureBox3.Image = global::ModBotInstaller.Properties.Resources.SettingsIcon;
            this.pictureBox3.InitialImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(435, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(25, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            this.modSettingsIconHover.SetToolTip(this.pictureBox3, "Mod settings for [Mod name]");
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 7F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(59, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 36);
            this.label2.TabIndex = 2;
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.UseMnemonic = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.ErrorImage = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.pictureBox1.Image = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.pictureBox1.InitialImage = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(58, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mod name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.UseMnemonic = false;
            // 
            // installedModsTitle
            // 
            this.installedModsTitle.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installedModsTitle.ForeColor = System.Drawing.Color.White;
            this.installedModsTitle.Location = new System.Drawing.Point(9, 63);
            this.installedModsTitle.Name = "installedModsTitle";
            this.installedModsTitle.Size = new System.Drawing.Size(130, 26);
            this.installedModsTitle.TabIndex = 17;
            this.installedModsTitle.Text = "Installed mods:";
            this.installedModsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // modSettingsIconHover
            // 
            this.modSettingsIconHover.ToolTipTitle = "Mod Settings";
            // 
            // modBotSettingsButton
            // 
            this.modBotSettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.modBotSettingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.modBotSettingsButton.FlatAppearance.BorderSize = 0;
            this.modBotSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.modBotSettingsButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modBotSettingsButton.ForeColor = System.Drawing.Color.White;
            this.modBotSettingsButton.Location = new System.Drawing.Point(94, 451);
            this.modBotSettingsButton.Name = "modBotSettingsButton";
            this.modBotSettingsButton.Size = new System.Drawing.Size(118, 23);
            this.modBotSettingsButton.TabIndex = 19;
            this.modBotSettingsButton.Text = "Mod-Bot Settings";
            this.modBotSettingsButton.UseVisualStyleBackColor = false;
            this.modBotSettingsButton.Click += new System.EventHandler(this.modBotSettingsButton_Click);
            // 
            // installingModBotLabel
            // 
            this.installingModBotLabel.AutoSize = true;
            this.installingModBotLabel.Font = new System.Drawing.Font("Consolas", 11F);
            this.installingModBotLabel.ForeColor = System.Drawing.Color.White;
            this.installingModBotLabel.Location = new System.Drawing.Point(164, 369);
            this.installingModBotLabel.Name = "installingModBotLabel";
            this.installingModBotLabel.Size = new System.Drawing.Size(176, 18);
            this.installingModBotLabel.TabIndex = 20;
            this.installingModBotLabel.Text = "Installing Mod-Bot...";
            this.installingModBotLabel.Visible = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(0)))));
            this.ProgressBar.Location = new System.Drawing.Point(12, 396);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(493, 23);
            this.ProgressBar.TabIndex = 1;
            this.ProgressBar.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(517, 486);
            this.Controls.Add(this.installingModBotLabel);
            this.Controls.Add(this.modBotSettingsButton);
            this.Controls.Add(this.installedModsTitle);
            this.Controls.Add(this.installedModsView);
            this.Controls.Add(this.Reinstall);
            this.Controls.Add(this.LatestVersionLabel);
            this.Controls.Add(this.LocalVersionLabel);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.InstallButton);
            this.Controls.Add(this.ProgressBar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mod-Bot Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.installedModsView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private NewProgressBar ProgressBar;
		private System.Windows.Forms.Button InstallButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.Label LocalVersionLabel;
		private System.Windows.Forms.Label LatestVersionLabel;
		private System.Windows.Forms.Button Reinstall;
        private System.Windows.Forms.FlowLayoutPanel installedModsView;
        private System.Windows.Forms.Label installedModsTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolTip modSettingsIconHover;
        private System.Windows.Forms.Button modBotSettingsButton;
        private System.Windows.Forms.Label installingModBotLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}