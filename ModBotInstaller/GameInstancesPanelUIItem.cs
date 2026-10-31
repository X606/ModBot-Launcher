using ModBotInstaller.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ModBotInstaller
{
    public class GameInstancesPanelUIItem
    {
        Panel _gamePanel;

        PictureBox _gameIcon;

        Label _gameTitle;
        Label _gamePlatform;

        Button _selectButton;

        MainForm _owner;

        GameInstanceInfo _gameInfo;
        public GameInstanceInfo GameInfo => _gameInfo;

        public GameInstancesPanelUIItem(GameInstanceInfo gameInfo, Control parent, MainForm owner)
        {
            _owner = owner;
            _gameInfo = gameInfo;

            _gamePanel = new Panel
            {
                Parent = parent,
                Name = $"gameInstancesItem_{gameInfo.Release}_{gameInfo.Platform}",
                BorderStyle = BorderStyle.None,
                Location = new Point(8, 8),
                Size = new Size(474, 56),
                BackColor = Color.FromArgb(30, 30, 30),
            };
            parent.Controls.Add(_gamePanel);

            _gameIcon = new PictureBox
            {
                Name = _gamePanel.Name + "_ModImage",
                Parent = _gamePanel,
                Location = new Point(3, 3),
                Size = new Size(50, 50),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Anchor = AnchorStyles.Left,
                ErrorImage = Resources.NoImageAvailable,
                Image = Resources.NoImageAvailable,
                InitialImage = Resources.NoImageAvailable,
            };
            _gamePanel.Controls.Add(_gameIcon);

            if (_gameInfo.Release == GameReleaseType.DangerZone)
            {
                _gameIcon.Image = Resources.DangerZoneIcon;
            }
            else
            {
                string exeFilePath = Path.Combine(gameInfo.InstallationPath, Utils.GetExecutableName(gameInfo.Release, gameInfo.Platform));
                if (File.Exists(exeFilePath))
                {
                    _gameIcon.Image = Bitmap.FromHicon(Icon.ExtractAssociatedIcon(exeFilePath).Handle);
                }
                else
                {
                    _gameIcon.Image = Resources.NoImageAvailable;
                }
            }

            _gameTitle = new Label
            {
                Name = _gamePanel.Name + "_Title",
                Parent = _gamePanel,
                AutoSize = true,
                Font = new Font("Consolas", 12f, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(61, 10),
                Size = new Size(287, 62),
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.BottomLeft,
                UseMnemonic = false,
                Text = Utils.GetReleaseTitle(gameInfo.Release)
            };
            _gamePanel.Controls.Add(_gameTitle);

            _gamePlatform = new Label
            {
                Name = _gamePanel.Name + "_Platform",
                Parent = _gamePanel,
                Anchor = AnchorStyles.Left,
                AutoEllipsis = true,
                Font = new Font("Consolas", 9f),
                ForeColor = Utils.GetPlatformColor(gameInfo.Platform),
                Location = new Point(62, 32),
                Size = new Size(200, 20),
                AutoSize = false,
                UseMnemonic = false,
                TextAlign = ContentAlignment.TopLeft,
                Text = Utils.GetPlatformName(gameInfo.Platform)
            };
            _gamePanel.Controls.Add(_gamePlatform);

            _selectButton = new Button
            {
                Name = _gamePanel.Name + "_SelectButton",
                Parent = _gamePanel,
                Anchor = AnchorStyles.Right,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(65, 65, 65),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Consolas", 8.25f),
                Location = new Point(363, 15),
                Size = new Size(101, 25),
                UseVisualStyleBackColor = false,
                Text = "Select"
            };

            _selectButton.Click += onSelectButtonClicked;
            _selectButton.FlatAppearance.BorderSize = 0;
            _gamePanel.Controls.Add(_selectButton);

            RefreshState();
        }

        public void RefreshState()
        {
            bool isSelected = GameInstanceInfo.Compare(GameInstanceInfo.Current, _gameInfo);
            _selectButton.Enabled = !isSelected;
            _gamePanel.BorderStyle = isSelected ? BorderStyle.FixedSingle : BorderStyle.None;
        }

        void onSelectButtonClicked(object sender, EventArgs e)
        {
            _owner.SelectGameInstance(_gameInfo);
        }
    }
}