// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Configuration.Settings.TvPlayers.Editors
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
                argumentsEditor.Arguments = new string[]
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
                Player = new TvPlayer();
                Player.Id = Guid.NewGuid();
                Player.Arguments = argumentsEditor.Arguments;
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
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
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

        private string GetPlayerName(string playerPath)
        {
            try
            {
                var info = System.Diagnostics.FileVersionInfo.GetVersionInfo(playerPath);
                return info.FileDescription;
            }
            catch
            {
                return Path.GetFileNameWithoutExtension(playerPath);
            } // try-catch
        } // GetPlayerName
    } // class TvPlayerEditorDialog
} // namespace
