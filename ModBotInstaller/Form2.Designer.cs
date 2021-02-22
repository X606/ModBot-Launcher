
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
            this.LocalVersionLabel = new System.Windows.Forms.Label();
            this.LatestVersionLabel = new System.Windows.Forms.Label();
            this.Reinstall = new System.Windows.Forms.Button();
            this.installedModsView = new System.Windows.Forms.FlowLayoutPanel();
            this.installedModsItem_TEMPLATE = new System.Windows.Forms.Panel();
            this.installedModsItem_TEMPLATE_UpdateStatus = new System.Windows.Forms.Label();
            this.installedModsItem_TEMPLATE_UpdateProgressBar = new ModBotInstaller.NewProgressBar();
            this.installedModsItem_TEMPLATE_UpdateButton = new System.Windows.Forms.Button();
            this.installedModsItem_TEMPLATE_ModID = new System.Windows.Forms.Label();
            this.installedModsItem_TEMPLATE_ModVersion = new System.Windows.Forms.Label();
            this.installedModsItem_TEMPLATE_ModAuthor = new System.Windows.Forms.Label();
            this.installedModsItem_TEMPLATE_SettingsIcon = new System.Windows.Forms.PictureBox();
            this.installedModsItem_TEMPLATE_ModDescription = new System.Windows.Forms.Label();
            this.installedModsItem_TEMPLATE_ModImage = new System.Windows.Forms.PictureBox();
            this.installedModsItem_TEMPLATE_ModName = new System.Windows.Forms.Label();
            this.installedModsTitle = new System.Windows.Forms.Label();
            this.modBotSettingsButton = new System.Windows.Forms.Button();
            this.installingModBotLabel = new System.Windows.Forms.Label();
            this.installedModsLoading = new System.Windows.Forms.Label();
            this.ProgressBar = new ModBotInstaller.NewProgressBar();
            this.button1 = new System.Windows.Forms.Button();
            this.installedModsView.SuspendLayout();
            this.installedModsItem_TEMPLATE.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.installedModsItem_TEMPLATE_SettingsIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedModsItem_TEMPLATE_ModImage)).BeginInit();
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
            this.Reinstall.Location = new System.Drawing.Point(263, 451);
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
            this.installedModsView.Controls.Add(this.installedModsItem_TEMPLATE);
            this.installedModsView.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.installedModsView.Location = new System.Drawing.Point(9, 92);
            this.installedModsView.Name = "installedModsView";
            this.installedModsView.Padding = new System.Windows.Forms.Padding(5);
            this.installedModsView.Size = new System.Drawing.Size(500, 300);
            this.installedModsView.TabIndex = 15;
            this.installedModsView.WrapContents = false;
            // 
            // installedModsItem_TEMPLATE
            // 
            this.installedModsItem_TEMPLATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_UpdateStatus);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_UpdateProgressBar);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_UpdateButton);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_ModID);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_ModVersion);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_ModAuthor);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_SettingsIcon);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_ModDescription);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_ModImage);
            this.installedModsItem_TEMPLATE.Controls.Add(this.installedModsItem_TEMPLATE_ModName);
            this.installedModsItem_TEMPLATE.Location = new System.Drawing.Point(8, 8);
            this.installedModsItem_TEMPLATE.Name = "installedModsItem_TEMPLATE";
            this.installedModsItem_TEMPLATE.Size = new System.Drawing.Size(465, 98);
            this.installedModsItem_TEMPLATE.TabIndex = 0;
            this.installedModsItem_TEMPLATE.Visible = false;
            // 
            // installedModsItem_TEMPLATE_UpdateStatus
            // 
            this.installedModsItem_TEMPLATE_UpdateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.installedModsItem_TEMPLATE_UpdateStatus.AutoSize = true;
            this.installedModsItem_TEMPLATE_UpdateStatus.Font = new System.Drawing.Font("Consolas", 7F);
            this.installedModsItem_TEMPLATE_UpdateStatus.ForeColor = System.Drawing.Color.White;
            this.installedModsItem_TEMPLATE_UpdateStatus.Location = new System.Drawing.Point(373, 56);
            this.installedModsItem_TEMPLATE_UpdateStatus.Name = "installedModsItem_TEMPLATE_UpdateStatus";
            this.installedModsItem_TEMPLATE_UpdateStatus.Size = new System.Drawing.Size(90, 12);
            this.installedModsItem_TEMPLATE_UpdateStatus.TabIndex = 24;
            this.installedModsItem_TEMPLATE_UpdateStatus.Text = "Update available!";
            this.installedModsItem_TEMPLATE_UpdateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.installedModsItem_TEMPLATE_UpdateStatus.UseMnemonic = false;
            // 
            // installedModsItem_TEMPLATE_UpdateProgressBar
            // 
            this.installedModsItem_TEMPLATE_UpdateProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.installedModsItem_TEMPLATE_UpdateProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(0)))));
            this.installedModsItem_TEMPLATE_UpdateProgressBar.Location = new System.Drawing.Point(253, 70);
            this.installedModsItem_TEMPLATE_UpdateProgressBar.Name = "installedModsItem_TEMPLATE_UpdateProgressBar";
            this.installedModsItem_TEMPLATE_UpdateProgressBar.Progress = 0F;
            this.installedModsItem_TEMPLATE_UpdateProgressBar.Size = new System.Drawing.Size(207, 21);
            this.installedModsItem_TEMPLATE_UpdateProgressBar.TabIndex = 23;
            // 
            // installedModsItem_TEMPLATE_UpdateButton
            // 
            this.installedModsItem_TEMPLATE_UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.installedModsItem_TEMPLATE_UpdateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.installedModsItem_TEMPLATE_UpdateButton.FlatAppearance.BorderSize = 0;
            this.installedModsItem_TEMPLATE_UpdateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.installedModsItem_TEMPLATE_UpdateButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installedModsItem_TEMPLATE_UpdateButton.ForeColor = System.Drawing.Color.White;
            this.installedModsItem_TEMPLATE_UpdateButton.Location = new System.Drawing.Point(386, 70);
            this.installedModsItem_TEMPLATE_UpdateButton.Name = "installedModsItem_TEMPLATE_UpdateButton";
            this.installedModsItem_TEMPLATE_UpdateButton.Size = new System.Drawing.Size(64, 21);
            this.installedModsItem_TEMPLATE_UpdateButton.TabIndex = 22;
            this.installedModsItem_TEMPLATE_UpdateButton.Text = "Update";
            this.installedModsItem_TEMPLATE_UpdateButton.UseVisualStyleBackColor = false;
            // 
            // installedModsItem_TEMPLATE_ModID
            // 
            this.installedModsItem_TEMPLATE_ModID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.installedModsItem_TEMPLATE_ModID.AutoSize = true;
            this.installedModsItem_TEMPLATE_ModID.Font = new System.Drawing.Font("Consolas", 7F);
            this.installedModsItem_TEMPLATE_ModID.ForeColor = System.Drawing.Color.White;
            this.installedModsItem_TEMPLATE_ModID.Location = new System.Drawing.Point(3, 80);
            this.installedModsItem_TEMPLATE_ModID.Name = "installedModsItem_TEMPLATE_ModID";
            this.installedModsItem_TEMPLATE_ModID.Size = new System.Drawing.Size(125, 12);
            this.installedModsItem_TEMPLATE_ModID.TabIndex = 7;
            this.installedModsItem_TEMPLATE_ModID.Text = "Mod ID: 11111-AAAAA-BBBB";
            this.installedModsItem_TEMPLATE_ModID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // installedModsItem_TEMPLATE_ModVersion
            // 
            this.installedModsItem_TEMPLATE_ModVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.installedModsItem_TEMPLATE_ModVersion.AutoSize = true;
            this.installedModsItem_TEMPLATE_ModVersion.Font = new System.Drawing.Font("Consolas", 7F);
            this.installedModsItem_TEMPLATE_ModVersion.ForeColor = System.Drawing.Color.White;
            this.installedModsItem_TEMPLATE_ModVersion.Location = new System.Drawing.Point(3, 68);
            this.installedModsItem_TEMPLATE_ModVersion.Name = "installedModsItem_TEMPLATE_ModVersion";
            this.installedModsItem_TEMPLATE_ModVersion.Size = new System.Drawing.Size(100, 12);
            this.installedModsItem_TEMPLATE_ModVersion.TabIndex = 6;
            this.installedModsItem_TEMPLATE_ModVersion.Text = "Version: 1.55.7.0.4";
            this.installedModsItem_TEMPLATE_ModVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // installedModsItem_TEMPLATE_ModAuthor
            // 
            this.installedModsItem_TEMPLATE_ModAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.installedModsItem_TEMPLATE_ModAuthor.AutoSize = true;
            this.installedModsItem_TEMPLATE_ModAuthor.Font = new System.Drawing.Font("Consolas", 7F);
            this.installedModsItem_TEMPLATE_ModAuthor.ForeColor = System.Drawing.Color.White;
            this.installedModsItem_TEMPLATE_ModAuthor.Location = new System.Drawing.Point(3, 56);
            this.installedModsItem_TEMPLATE_ModAuthor.Name = "installedModsItem_TEMPLATE_ModAuthor";
            this.installedModsItem_TEMPLATE_ModAuthor.Size = new System.Drawing.Size(120, 12);
            this.installedModsItem_TEMPLATE_ModAuthor.TabIndex = 5;
            this.installedModsItem_TEMPLATE_ModAuthor.Text = "By: Cool mod person man";
            this.installedModsItem_TEMPLATE_ModAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.installedModsItem_TEMPLATE_ModAuthor.UseMnemonic = false;
            // 
            // installedModsItem_TEMPLATE_SettingsIcon
            // 
            this.installedModsItem_TEMPLATE_SettingsIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.installedModsItem_TEMPLATE_SettingsIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.installedModsItem_TEMPLATE_SettingsIcon.ErrorImage = null;
            this.installedModsItem_TEMPLATE_SettingsIcon.Image = global::ModBotInstaller.Properties.Resources.SettingsIcon;
            this.installedModsItem_TEMPLATE_SettingsIcon.InitialImage = null;
            this.installedModsItem_TEMPLATE_SettingsIcon.Location = new System.Drawing.Point(435, 3);
            this.installedModsItem_TEMPLATE_SettingsIcon.Name = "installedModsItem_TEMPLATE_SettingsIcon";
            this.installedModsItem_TEMPLATE_SettingsIcon.Size = new System.Drawing.Size(25, 25);
            this.installedModsItem_TEMPLATE_SettingsIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.installedModsItem_TEMPLATE_SettingsIcon.TabIndex = 4;
            this.installedModsItem_TEMPLATE_SettingsIcon.TabStop = false;
            // 
            // installedModsItem_TEMPLATE_ModDescription
            // 
            this.installedModsItem_TEMPLATE_ModDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.installedModsItem_TEMPLATE_ModDescription.AutoEllipsis = true;
            this.installedModsItem_TEMPLATE_ModDescription.Font = new System.Drawing.Font("Consolas", 6.5F);
            this.installedModsItem_TEMPLATE_ModDescription.ForeColor = System.Drawing.Color.White;
            this.installedModsItem_TEMPLATE_ModDescription.Location = new System.Drawing.Point(59, 23);
            this.installedModsItem_TEMPLATE_ModDescription.Name = "installedModsItem_TEMPLATE_ModDescription";
            this.installedModsItem_TEMPLATE_ModDescription.Size = new System.Drawing.Size(335, 30);
            this.installedModsItem_TEMPLATE_ModDescription.TabIndex = 2;
            this.installedModsItem_TEMPLATE_ModDescription.Text = resources.GetString("installedModsItem_TEMPLATE_ModDescription.Text");
            this.installedModsItem_TEMPLATE_ModDescription.UseMnemonic = false;
            // 
            // installedModsItem_TEMPLATE_ModImage
            // 
            this.installedModsItem_TEMPLATE_ModImage.ErrorImage = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.installedModsItem_TEMPLATE_ModImage.Image = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.installedModsItem_TEMPLATE_ModImage.InitialImage = global::ModBotInstaller.Properties.Resources.NoImageAvailable;
            this.installedModsItem_TEMPLATE_ModImage.Location = new System.Drawing.Point(3, 3);
            this.installedModsItem_TEMPLATE_ModImage.Name = "installedModsItem_TEMPLATE_ModImage";
            this.installedModsItem_TEMPLATE_ModImage.Size = new System.Drawing.Size(50, 50);
            this.installedModsItem_TEMPLATE_ModImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.installedModsItem_TEMPLATE_ModImage.TabIndex = 1;
            this.installedModsItem_TEMPLATE_ModImage.TabStop = false;
            // 
            // installedModsItem_TEMPLATE_ModName
            // 
            this.installedModsItem_TEMPLATE_ModName.AutoSize = true;
            this.installedModsItem_TEMPLATE_ModName.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.installedModsItem_TEMPLATE_ModName.ForeColor = System.Drawing.Color.White;
            this.installedModsItem_TEMPLATE_ModName.Location = new System.Drawing.Point(58, 3);
            this.installedModsItem_TEMPLATE_ModName.Name = "installedModsItem_TEMPLATE_ModName";
            this.installedModsItem_TEMPLATE_ModName.Size = new System.Drawing.Size(72, 17);
            this.installedModsItem_TEMPLATE_ModName.TabIndex = 0;
            this.installedModsItem_TEMPLATE_ModName.Text = "Mod name";
            this.installedModsItem_TEMPLATE_ModName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.installedModsItem_TEMPLATE_ModName.UseMnemonic = false;
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
            this.installingModBotLabel.CausesValidation = false;
            this.installingModBotLabel.Font = new System.Drawing.Font("Consolas", 11F);
            this.installingModBotLabel.ForeColor = System.Drawing.Color.White;
            this.installingModBotLabel.Location = new System.Drawing.Point(164, 189);
            this.installingModBotLabel.Name = "installingModBotLabel";
            this.installingModBotLabel.Size = new System.Drawing.Size(176, 18);
            this.installingModBotLabel.TabIndex = 20;
            this.installingModBotLabel.Text = "Installing Mod-Bot...";
            this.installingModBotLabel.Visible = false;
            // 
            // installedModsLoading
            // 
            this.installedModsLoading.AutoSize = true;
            this.installedModsLoading.Font = new System.Drawing.Font("Consolas", 11F);
            this.installedModsLoading.ForeColor = System.Drawing.Color.White;
            this.installedModsLoading.Location = new System.Drawing.Point(194, 67);
            this.installedModsLoading.Name = "installedModsLoading";
            this.installedModsLoading.Size = new System.Drawing.Size(128, 18);
            this.installedModsLoading.TabIndex = 21;
            this.installedModsLoading.Text = "Loading mods...";
            this.installedModsLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.installedModsLoading.Visible = false;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(104)))), ((int)(((byte)(0)))));
            this.ProgressBar.Location = new System.Drawing.Point(12, 216);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Progress = 0F;
            this.ProgressBar.Size = new System.Drawing.Size(493, 23);
            this.ProgressBar.TabIndex = 1;
            this.ProgressBar.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(263, 451);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(242, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Install all 8252 missing dependencies";
            this.button1.UseMnemonic = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(517, 486);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.installedModsLoading);
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
            this.installedModsItem_TEMPLATE.ResumeLayout(false);
            this.installedModsItem_TEMPLATE.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.installedModsItem_TEMPLATE_SettingsIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.installedModsItem_TEMPLATE_ModImage)).EndInit();
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
        private System.Windows.Forms.Panel installedModsItem_TEMPLATE;
        private System.Windows.Forms.Label installedModsItem_TEMPLATE_ModName;
        private System.Windows.Forms.PictureBox installedModsItem_TEMPLATE_ModImage;
        private System.Windows.Forms.Label installedModsItem_TEMPLATE_ModDescription;
        private System.Windows.Forms.PictureBox installedModsItem_TEMPLATE_SettingsIcon;
        private System.Windows.Forms.Button modBotSettingsButton;
        private System.Windows.Forms.Label installingModBotLabel;
        private System.Windows.Forms.Label installedModsItem_TEMPLATE_ModVersion;
        private System.Windows.Forms.Label installedModsItem_TEMPLATE_ModAuthor;
        private System.Windows.Forms.Label installedModsLoading;
        private System.Windows.Forms.Label installedModsItem_TEMPLATE_ModID;
        private System.Windows.Forms.Button installedModsItem_TEMPLATE_UpdateButton;
        private NewProgressBar installedModsItem_TEMPLATE_UpdateProgressBar;
        private System.Windows.Forms.Label installedModsItem_TEMPLATE_UpdateStatus;
        private System.Windows.Forms.Button button1;
    }
}