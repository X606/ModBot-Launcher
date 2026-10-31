
namespace ModBotInstaller
{
	partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BigTitle = new System.Windows.Forms.Label();
            this.InstallLocationDisplay = new System.Windows.Forms.Label();
            this.ChangeLocationButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.detectedGamesLabel = new System.Windows.Forms.Label();
            this.detectedGamesView = new System.Windows.Forms.FlowLayoutPanel();
            this.gameInstance_TEMPLATE_Panel = new System.Windows.Forms.Panel();
            this.gameInstance_TEMPLATE_Platform = new System.Windows.Forms.Label();
            this.gameInstance_TEMPLATE_SelectButton = new System.Windows.Forms.Button();
            this.gameInstance_TEMPLATE_Icon = new System.Windows.Forms.PictureBox();
            this.gameInstance_TEMPLATE_Title = new System.Windows.Forms.Label();
            this.detectedGamesView.SuspendLayout();
            this.gameInstance_TEMPLATE_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameInstance_TEMPLATE_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // BigTitle
            // 
            this.BigTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BigTitle.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BigTitle.ForeColor = System.Drawing.Color.LawnGreen;
            this.BigTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BigTitle.Location = new System.Drawing.Point(12, 20);
            this.BigTitle.Name = "BigTitle";
            this.BigTitle.Size = new System.Drawing.Size(493, 30);
            this.BigTitle.TabIndex = 1;
            this.BigTitle.Text = "big text here";
            this.BigTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // InstallLocationDisplay
            // 
            this.InstallLocationDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallLocationDisplay.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstallLocationDisplay.ForeColor = System.Drawing.Color.White;
            this.InstallLocationDisplay.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.InstallLocationDisplay.Location = new System.Drawing.Point(9, 55);
            this.InstallLocationDisplay.Name = "InstallLocationDisplay";
            this.InstallLocationDisplay.Size = new System.Drawing.Size(493, 22);
            this.InstallLocationDisplay.TabIndex = 5;
            this.InstallLocationDisplay.Text = "Filler text bla bla bla bla";
            this.InstallLocationDisplay.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ChangeLocationButton
            // 
            this.ChangeLocationButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ChangeLocationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ChangeLocationButton.FlatAppearance.BorderSize = 0;
            this.ChangeLocationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeLocationButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeLocationButton.ForeColor = System.Drawing.Color.White;
            this.ChangeLocationButton.Location = new System.Drawing.Point(197, 85);
            this.ChangeLocationButton.Name = "ChangeLocationButton";
            this.ChangeLocationButton.Size = new System.Drawing.Size(135, 23);
            this.ChangeLocationButton.TabIndex = 6;
            this.ChangeLocationButton.Text = "Custom..";
            this.ChangeLocationButton.UseVisualStyleBackColor = false;
            this.ChangeLocationButton.Click += new System.EventHandler(this.ChangeLocationButton_Click);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ContinueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ContinueButton.FlatAppearance.BorderSize = 0;
            this.ContinueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContinueButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueButton.ForeColor = System.Drawing.Color.White;
            this.ContinueButton.Location = new System.Drawing.Point(430, 354);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 23);
            this.ContinueButton.TabIndex = 7;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = false;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(12, 354);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // detectedGamesLabel
            // 
            this.detectedGamesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detectedGamesLabel.Font = new System.Drawing.Font("Consolas", 10F);
            this.detectedGamesLabel.ForeColor = System.Drawing.Color.White;
            this.detectedGamesLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.detectedGamesLabel.Location = new System.Drawing.Point(12, 123);
            this.detectedGamesLabel.Name = "detectedGamesLabel";
            this.detectedGamesLabel.Size = new System.Drawing.Size(490, 20);
            this.detectedGamesLabel.TabIndex = 11;
            this.detectedGamesLabel.Text = "Detected Clone Drone games:\r\n";
            this.detectedGamesLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // detectedGamesView
            // 
            this.detectedGamesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detectedGamesView.AutoScroll = true;
            this.detectedGamesView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.detectedGamesView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.detectedGamesView.Controls.Add(this.gameInstance_TEMPLATE_Panel);
            this.detectedGamesView.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.detectedGamesView.Location = new System.Drawing.Point(12, 146);
            this.detectedGamesView.Name = "detectedGamesView";
            this.detectedGamesView.Padding = new System.Windows.Forms.Padding(5);
            this.detectedGamesView.Size = new System.Drawing.Size(494, 198);
            this.detectedGamesView.TabIndex = 16;
            this.detectedGamesView.WrapContents = false;
            // 
            // gameInstance_TEMPLATE_Panel
            // 
            this.gameInstance_TEMPLATE_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.gameInstance_TEMPLATE_Panel.Controls.Add(this.gameInstance_TEMPLATE_Platform);
            this.gameInstance_TEMPLATE_Panel.Controls.Add(this.gameInstance_TEMPLATE_SelectButton);
            this.gameInstance_TEMPLATE_Panel.Controls.Add(this.gameInstance_TEMPLATE_Icon);
            this.gameInstance_TEMPLATE_Panel.Controls.Add(this.gameInstance_TEMPLATE_Title);
            this.gameInstance_TEMPLATE_Panel.Location = new System.Drawing.Point(8, 8);
            this.gameInstance_TEMPLATE_Panel.Name = "gameInstance_TEMPLATE_Panel";
            this.gameInstance_TEMPLATE_Panel.Size = new System.Drawing.Size(474, 56);
            this.gameInstance_TEMPLATE_Panel.TabIndex = 18;
            this.gameInstance_TEMPLATE_Panel.Visible = false;
            // 
            // gameInstance_TEMPLATE_Platform
            // 
            this.gameInstance_TEMPLATE_Platform.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.gameInstance_TEMPLATE_Platform.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.gameInstance_TEMPLATE_Platform.ForeColor = System.Drawing.Color.ForestGreen;
            this.gameInstance_TEMPLATE_Platform.Location = new System.Drawing.Point(62, 32);
            this.gameInstance_TEMPLATE_Platform.Name = "gameInstance_TEMPLATE_Platform";
            this.gameInstance_TEMPLATE_Platform.Size = new System.Drawing.Size(112, 17);
            this.gameInstance_TEMPLATE_Platform.TabIndex = 18;
            this.gameInstance_TEMPLATE_Platform.Text = "Steam";
            this.gameInstance_TEMPLATE_Platform.UseMnemonic = false;
            // 
            // gameInstance_TEMPLATE_SelectButton
            // 
            this.gameInstance_TEMPLATE_SelectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gameInstance_TEMPLATE_SelectButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.gameInstance_TEMPLATE_SelectButton.Enabled = false;
            this.gameInstance_TEMPLATE_SelectButton.FlatAppearance.BorderSize = 0;
            this.gameInstance_TEMPLATE_SelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gameInstance_TEMPLATE_SelectButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameInstance_TEMPLATE_SelectButton.ForeColor = System.Drawing.Color.White;
            this.gameInstance_TEMPLATE_SelectButton.Location = new System.Drawing.Point(363, 15);
            this.gameInstance_TEMPLATE_SelectButton.Name = "gameInstance_TEMPLATE_SelectButton";
            this.gameInstance_TEMPLATE_SelectButton.Size = new System.Drawing.Size(101, 25);
            this.gameInstance_TEMPLATE_SelectButton.TabIndex = 17;
            this.gameInstance_TEMPLATE_SelectButton.Text = "Select";
            this.gameInstance_TEMPLATE_SelectButton.UseVisualStyleBackColor = false;
            // 
            // gameInstance_TEMPLATE_Icon
            // 
            this.gameInstance_TEMPLATE_Icon.ErrorImage = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.gameInstance_TEMPLATE_Icon.Image = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.gameInstance_TEMPLATE_Icon.InitialImage = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.gameInstance_TEMPLATE_Icon.Location = new System.Drawing.Point(3, 3);
            this.gameInstance_TEMPLATE_Icon.Name = "gameInstance_TEMPLATE_Icon";
            this.gameInstance_TEMPLATE_Icon.Size = new System.Drawing.Size(50, 50);
            this.gameInstance_TEMPLATE_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gameInstance_TEMPLATE_Icon.TabIndex = 1;
            this.gameInstance_TEMPLATE_Icon.TabStop = false;
            // 
            // gameInstance_TEMPLATE_Title
            // 
            this.gameInstance_TEMPLATE_Title.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.gameInstance_TEMPLATE_Title.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.gameInstance_TEMPLATE_Title.ForeColor = System.Drawing.Color.White;
            this.gameInstance_TEMPLATE_Title.Location = new System.Drawing.Point(61, 10);
            this.gameInstance_TEMPLATE_Title.Name = "gameInstance_TEMPLATE_Title";
            this.gameInstance_TEMPLATE_Title.Size = new System.Drawing.Size(287, 22);
            this.gameInstance_TEMPLATE_Title.TabIndex = 0;
            this.gameInstance_TEMPLATE_Title.Text = "Clone Drone in the Danger Zone";
            this.gameInstance_TEMPLATE_Title.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.gameInstance_TEMPLATE_Title.UseMnemonic = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(517, 389);
            this.Controls.Add(this.detectedGamesView);
            this.Controls.Add(this.detectedGamesLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.ChangeLocationButton);
            this.Controls.Add(this.InstallLocationDisplay);
            this.Controls.Add(this.BigTitle);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mod-Bot Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.detectedGamesView.ResumeLayout(false);
            this.gameInstance_TEMPLATE_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gameInstance_TEMPLATE_Icon)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label BigTitle;
		private System.Windows.Forms.Label InstallLocationDisplay;
		private System.Windows.Forms.Button ChangeLocationButton;
		private System.Windows.Forms.Button ContinueButton;
		private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label detectedGamesLabel;
        private System.Windows.Forms.FlowLayoutPanel detectedGamesView;
        private System.Windows.Forms.Panel gameInstance_TEMPLATE_Panel;
        private System.Windows.Forms.Button gameInstance_TEMPLATE_SelectButton;
        private System.Windows.Forms.PictureBox gameInstance_TEMPLATE_Icon;
        private System.Windows.Forms.Label gameInstance_TEMPLATE_Title;
        private System.Windows.Forms.Label gameInstance_TEMPLATE_Platform;
    }
}

