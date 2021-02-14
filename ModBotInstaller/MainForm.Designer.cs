
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
            this.SuspendLayout();
            // 
            // BigTitle
            // 
            this.BigTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BigTitle.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BigTitle.ForeColor = System.Drawing.Color.LawnGreen;
            this.BigTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BigTitle.Location = new System.Drawing.Point(15, 110);
            this.BigTitle.Name = "BigTitle";
            this.BigTitle.Size = new System.Drawing.Size(490, 24);
            this.BigTitle.TabIndex = 1;
            this.BigTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // InstallLocationDisplay
            // 
            this.InstallLocationDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallLocationDisplay.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstallLocationDisplay.ForeColor = System.Drawing.Color.White;
            this.InstallLocationDisplay.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.InstallLocationDisplay.Location = new System.Drawing.Point(12, 134);
            this.InstallLocationDisplay.Name = "InstallLocationDisplay";
            this.InstallLocationDisplay.Size = new System.Drawing.Size(493, 22);
            this.InstallLocationDisplay.TabIndex = 5;
            this.InstallLocationDisplay.Text = "Filler text bla bla bla bla";
            this.InstallLocationDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChangeLocationButton
            // 
            this.ChangeLocationButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ChangeLocationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.ChangeLocationButton.FlatAppearance.BorderSize = 0;
            this.ChangeLocationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChangeLocationButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeLocationButton.ForeColor = System.Drawing.Color.White;
            this.ChangeLocationButton.Location = new System.Drawing.Point(221, 159);
            this.ChangeLocationButton.Name = "ChangeLocationButton";
            this.ChangeLocationButton.Size = new System.Drawing.Size(75, 23);
            this.ChangeLocationButton.TabIndex = 6;
            this.ChangeLocationButton.Text = "Change";
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
            this.ContinueButton.Location = new System.Drawing.Point(430, 271);
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
            this.CloseButton.Location = new System.Drawing.Point(19, 271);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(517, 306);
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
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label BigTitle;
		private System.Windows.Forms.Label InstallLocationDisplay;
		private System.Windows.Forms.Button ChangeLocationButton;
		private System.Windows.Forms.Button ContinueButton;
		private System.Windows.Forms.Button CloseButton;
    }
}

