
namespace ModBotInstaller
{
    partial class SettingsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.changeBetaLocationNoteLabel = new System.Windows.Forms.Label();
            this.changeBetaLocationButton = new System.Windows.Forms.Button();
            this.enableLocalBetaVersionCheckbox = new System.Windows.Forms.CheckBox();
            this.skipFirstPageCheckbox = new System.Windows.Forms.CheckBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.modBotSettingsLabel = new System.Windows.Forms.Label();
            this.autoUpdateModsCheckbox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.autoUpdateModsCheckbox);
            this.panel1.Controls.Add(this.changeBetaLocationNoteLabel);
            this.panel1.Controls.Add(this.changeBetaLocationButton);
            this.panel1.Controls.Add(this.enableLocalBetaVersionCheckbox);
            this.panel1.Controls.Add(this.skipFirstPageCheckbox);
            this.panel1.Controls.Add(this.applyButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.modBotSettingsLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(397, 336);
            this.panel1.TabIndex = 0;
            // 
            // changeBetaLocationNoteLabel
            // 
            this.changeBetaLocationNoteLabel.AutoSize = true;
            this.changeBetaLocationNoteLabel.Font = new System.Drawing.Font("Consolas", 8F);
            this.changeBetaLocationNoteLabel.ForeColor = System.Drawing.Color.Chartreuse;
            this.changeBetaLocationNoteLabel.Location = new System.Drawing.Point(226, 117);
            this.changeBetaLocationNoteLabel.Name = "changeBetaLocationNoteLabel";
            this.changeBetaLocationNoteLabel.Size = new System.Drawing.Size(79, 13);
            this.changeBetaLocationNoteLabel.TabIndex = 13;
            this.changeBetaLocationNoteLabel.Text = "(Valid path)";
            this.changeBetaLocationNoteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // changeBetaLocationButton
            // 
            this.changeBetaLocationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.changeBetaLocationButton.FlatAppearance.BorderSize = 0;
            this.changeBetaLocationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeBetaLocationButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeBetaLocationButton.ForeColor = System.Drawing.Color.White;
            this.changeBetaLocationButton.Location = new System.Drawing.Point(79, 112);
            this.changeBetaLocationButton.Name = "changeBetaLocationButton";
            this.changeBetaLocationButton.Size = new System.Drawing.Size(141, 23);
            this.changeBetaLocationButton.TabIndex = 12;
            this.changeBetaLocationButton.Text = "Change beta location";
            this.changeBetaLocationButton.UseVisualStyleBackColor = false;
            this.changeBetaLocationButton.Click += new System.EventHandler(this.changeBetaLocationButton_Click);
            // 
            // enableLocalBetaVersionCheckbox
            // 
            this.enableLocalBetaVersionCheckbox.AutoSize = true;
            this.enableLocalBetaVersionCheckbox.ForeColor = System.Drawing.Color.White;
            this.enableLocalBetaVersionCheckbox.Location = new System.Drawing.Point(111, 87);
            this.enableLocalBetaVersionCheckbox.Name = "enableLocalBetaVersionCheckbox";
            this.enableLocalBetaVersionCheckbox.Size = new System.Drawing.Size(176, 17);
            this.enableLocalBetaVersionCheckbox.TabIndex = 11;
            this.enableLocalBetaVersionCheckbox.Text = "Enable local beta version";
            this.enableLocalBetaVersionCheckbox.UseMnemonic = false;
            this.enableLocalBetaVersionCheckbox.UseVisualStyleBackColor = true;
            this.enableLocalBetaVersionCheckbox.CheckedChanged += new System.EventHandler(this.enableLocalBetaVersionCheckbox_CheckedChanged);
            // 
            // skipFirstPageCheckbox
            // 
            this.skipFirstPageCheckbox.AutoSize = true;
            this.skipFirstPageCheckbox.ForeColor = System.Drawing.Color.White;
            this.skipFirstPageCheckbox.Location = new System.Drawing.Point(90, 55);
            this.skipFirstPageCheckbox.Name = "skipFirstPageCheckbox";
            this.skipFirstPageCheckbox.Size = new System.Drawing.Size(230, 17);
            this.skipFirstPageCheckbox.TabIndex = 10;
            this.skipFirstPageCheckbox.Text = "Skip target folder location select";
            this.skipFirstPageCheckbox.UseMnemonic = false;
            this.skipFirstPageCheckbox.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.applyButton.FlatAppearance.BorderSize = 0;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Location = new System.Drawing.Point(288, 300);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(96, 23);
            this.applyButton.TabIndex = 9;
            this.applyButton.Text = "&Apply";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.ForeColor = System.Drawing.Color.White;
            this.cancelButton.Location = new System.Drawing.Point(11, 300);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 23);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // modBotSettingsLabel
            // 
            this.modBotSettingsLabel.AutoSize = true;
            this.modBotSettingsLabel.Font = new System.Drawing.Font("Consolas", 12F);
            this.modBotSettingsLabel.ForeColor = System.Drawing.Color.White;
            this.modBotSettingsLabel.Location = new System.Drawing.Point(87, 8);
            this.modBotSettingsLabel.Name = "modBotSettingsLabel";
            this.modBotSettingsLabel.Size = new System.Drawing.Size(234, 19);
            this.modBotSettingsLabel.TabIndex = 0;
            this.modBotSettingsLabel.Text = "Mod Bot Launcher Settings";
            this.modBotSettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // autoUpdateModsCheckbox
            // 
            this.autoUpdateModsCheckbox.AutoSize = true;
            this.autoUpdateModsCheckbox.ForeColor = System.Drawing.Color.White;
            this.autoUpdateModsCheckbox.Location = new System.Drawing.Point(137, 149);
            this.autoUpdateModsCheckbox.Name = "autoUpdateModsCheckbox";
            this.autoUpdateModsCheckbox.Size = new System.Drawing.Size(122, 17);
            this.autoUpdateModsCheckbox.TabIndex = 14;
            this.autoUpdateModsCheckbox.Text = "Auto-update mods";
            this.autoUpdateModsCheckbox.UseMnemonic = false;
            this.autoUpdateModsCheckbox.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(397, 336);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mod-Bot Launcher Settings";
            this.Load += new System.EventHandler(this.SettingsWindow_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label modBotSettingsLabel;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox skipFirstPageCheckbox;
        private System.Windows.Forms.Button changeBetaLocationButton;
        private System.Windows.Forms.CheckBox enableLocalBetaVersionCheckbox;
        private System.Windows.Forms.Label changeBetaLocationNoteLabel;
        private System.Windows.Forms.CheckBox autoUpdateModsCheckbox;
    }
}