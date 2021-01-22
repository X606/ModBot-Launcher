
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
			this.InstallButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.ProgressBar = new ModBotInstaller.NewProgressBar();
			this.LocalVersionLabel = new System.Windows.Forms.Label();
			this.LatestVersionLabel = new System.Windows.Forms.Label();
			this.SkipSelectScreen = new System.Windows.Forms.CheckBox();
			this.Reinstall = new System.Windows.Forms.Button();
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
			this.InstallButton.Location = new System.Drawing.Point(387, 242);
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
			this.CloseButton.Location = new System.Drawing.Point(12, 242);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(76, 23);
			this.CloseButton.TabIndex = 9;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = false;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.StatusLabel.ForeColor = System.Drawing.Color.White;
			this.StatusLabel.Location = new System.Drawing.Point(12, 90);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(493, 59);
			this.StatusLabel.TabIndex = 10;
			this.StatusLabel.Text = "Message";
			this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ProgressBar
			// 
			this.ProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
			this.ProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(0)))));
			this.ProgressBar.Location = new System.Drawing.Point(12, 271);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(493, 23);
			this.ProgressBar.TabIndex = 1;
			// 
			// LocalVersionLabel
			// 
			this.LocalVersionLabel.AutoSize = true;
			this.LocalVersionLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LocalVersionLabel.ForeColor = System.Drawing.Color.White;
			this.LocalVersionLabel.Location = new System.Drawing.Point(12, 9);
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
			this.LatestVersionLabel.Location = new System.Drawing.Point(12, 34);
			this.LatestVersionLabel.Name = "LatestVersionLabel";
			this.LatestVersionLabel.Size = new System.Drawing.Size(112, 15);
			this.LatestVersionLabel.TabIndex = 12;
			this.LatestVersionLabel.Text = "Latest Version:";
			// 
			// SkipSelectScreen
			// 
			this.SkipSelectScreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SkipSelectScreen.AutoSize = true;
			this.SkipSelectScreen.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SkipSelectScreen.ForeColor = System.Drawing.Color.White;
			this.SkipSelectScreen.Location = new System.Drawing.Point(16, 217);
			this.SkipSelectScreen.Name = "SkipSelectScreen";
			this.SkipSelectScreen.Size = new System.Drawing.Size(134, 17);
			this.SkipSelectScreen.TabIndex = 13;
			this.SkipSelectScreen.Text = "Skip select screen";
			this.SkipSelectScreen.UseVisualStyleBackColor = true;
			this.SkipSelectScreen.CheckedChanged += new System.EventHandler(this.SkipSelectScreen_CheckedChanged);
			// 
			// Reinstall
			// 
			this.Reinstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Reinstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
			this.Reinstall.FlatAppearance.BorderSize = 0;
			this.Reinstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Reinstall.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Reinstall.ForeColor = System.Drawing.Color.White;
			this.Reinstall.Location = new System.Drawing.Point(263, 242);
			this.Reinstall.Name = "Reinstall";
			this.Reinstall.Size = new System.Drawing.Size(118, 23);
			this.Reinstall.TabIndex = 14;
			this.Reinstall.Text = "Reinstall";
			this.Reinstall.UseVisualStyleBackColor = false;
			this.Reinstall.Click += new System.EventHandler(this.Reinstall_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
			this.ClientSize = new System.Drawing.Size(517, 306);
			this.Controls.Add(this.Reinstall);
			this.Controls.Add(this.SkipSelectScreen);
			this.Controls.Add(this.LatestVersionLabel);
			this.Controls.Add(this.LocalVersionLabel);
			this.Controls.Add(this.StatusLabel);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.InstallButton);
			this.Controls.Add(this.ProgressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form2";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form2";
			this.TopMost = true;
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
			this.Load += new System.EventHandler(this.Form2_Load);
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
		private System.Windows.Forms.CheckBox SkipSelectScreen;
		private System.Windows.Forms.Button Reinstall;
	}
}