// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.IO;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers.Editors
{
    public partial class TvPlayerEditorDialog : Form
    {
        public TvPlayerEditorDialog()
        {
            InitializeComponent();
        } // constructor

        public TvPlayer Player
        {
            get;
            set;
        } // Player

        public bool IsDataChanged
        {
            get;
            set;
        } // IsDataChanged

        private void TvPlayerEditorDialog_Load(object sender, EventArgs e)
        {
            if (Player != null)
            {
                textPlayerName.Text = Player.Name;
                textPlayerPath.Text = Player.Path;
                argumentsEditor.Arguments = Player.Arguments;
            }
            else
            {
                argumentsEditor.Arguments = new[]
                {
                    "{param:Channel.Url}"
                };
            } // if-else

            argumentsEditor.OpenBraceText = TvPlayer.ParameterOpenBrace;
            argumentsEditor.CloseBraceText = TvPlayer.ParameterCloseBrace;
            argumentsEditor.ParametersList = Properties.SettingsTexts.TvPlayerParametersList;
        } // TvPlayerEditorDialog_Load

        private void TvPlayerEditorDialog_Shown(object sender, EventArgs e)
        {
            if (Player == null)
            {
                buttonSelectPlayer.PerformClick();
            }
            else
            {
                SetPlayerIcon();
            } // if-else
        } // TvPlayerEditorDialog_Shown

        private void buttonSelectPlayer_Click(object sender, EventArgs e)
        {
            if (Player == null)
            {
                selectPlayerDialog.InitialDirectory = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            }
            else
            {
                selectPlayerDialog.InitialDirectory = Path.GetDirectoryName(textPlayerPath.Text);
                selectPlayerDialog.FileName = textPlayerPath.Text;
            } // if-else

            if (selectPlayerDialog.ShowDialog(this) != DialogResult.OK)
            {
                if (Player == null)
                {
                    Close();
                } // if
                return;
            } // if

            if (Player == null)
            {
                Player = new TvPlayer
                {
                    Id = Guid.NewGuid(),
                    Arguments = argumentsEditor.Arguments
                };
            } // if

            IsDataChanged = true;
            if (textPlayerPath.Text != selectPlayerDialog.FileName)
            {
                textPlayerName.Text = GetPlayerName(selectPlayerDialog.FileName);
            } // if
            textPlayerPath.Text = selectPlayerDialog.FileName;
            SetPlayerIcon();
        } // buttonSelectPlayer_Click

        private void textPlayerName_TextChanged(object sender, EventArgs e)
        {
            IsDataChanged = true;
        } // textPlayerName_TextChanged

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Player.Name = textPlayerName.Text;
            Player.Path = textPlayerPath.Text;
            if (argumentsEditor.IsDataChanged)
            {
                Player.Arguments = argumentsEditor.Arguments;
            } // if

            if (Player.Arguments.Length == 0)
            {
                if (MessageBox.Show(this,
                    Properties.SettingsTexts.TvPlayerArgumentsListEmpty,
                    Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    buttonOk.DialogResult = DialogResult.None;
                    DialogResult = DialogResult.None;
                    return;
                } // if
            } // if

            buttonOk.DialogResult = DialogResult.OK;
            DialogResult = DialogResult.OK;
        } // buttonOk_Click

        private void SetPlayerIcon()
        {
            var settings = TvPlayersSettingsRegistration.Settings;
            var icon = settings.GetPlayerIcon(textPlayerPath.Text);
            {
                picturePlayerIcon.Image = icon;
            } // using
        } // SetPlayerIcon

        private static string GetPlayerName(string playerPath)
        {
            string playerName = null;

            try
            {
                var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(playerPath);
                playerName = info.FileDescription;
                if (string.IsNullOrEmpty(playerName)) playerName = info.ProductName;
            }
            catch
            {
                // ignore
            } // try-catch

            if (string.IsNullOrEmpty(playerName)) playerName = Path.GetFileNameWithoutExtension(playerPath);

            return playerName;
        } // GetPlayerName
    } // class TvPlayerEditorDialog
} // namespace
