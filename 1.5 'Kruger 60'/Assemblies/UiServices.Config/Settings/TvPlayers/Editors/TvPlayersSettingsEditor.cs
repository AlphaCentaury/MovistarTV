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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.UiServices.Configuration.Properties;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers.Editors
{
    public partial class TvPlayersSettingsEditor : UserControl, IConfigurationItemEditor
    {
        private int _manualUpdateLock;
        private List<TvPlayer> _players;

        public TvPlayersSettingsEditor()
        {
            InitializeComponent();
        } // constructor

        public TvPlayersSettings Settings
        {
            get;
            set;
        } // Settings

        #region IConfigurationItemEditor implementation

        UserControl IConfigurationItemEditor.UserInterfaceItem => this;

        bool IConfigurationItemEditor.SupportsWinFormsValidation => false;

        public bool IsDataChanged
        {
            get;
            protected set;
        } // IsDataChanged

        public bool IsAppRestartNeeded => false;

        bool IConfigurationItemEditor.Validate()
        {
            return true;
        } // IConfigurationItemEditor.Validate

        IConfigurationItem IConfigurationItemEditor.GetNewData()
        {
            Settings.Players = _players.ToArray();

            return Settings;
        } // IConfigurationItemEditor.GetNewData

        void IConfigurationItemEditor.EditorClosing(out bool cancelClose)
        {
            cancelClose = false;
        } // IConfigurationItemEditor.EditorClosing

        void IConfigurationItemEditor.EditorClosed(bool userCancel)
        {
            // no op
        } // IConfigurationItemEditor.EditorClosed

        #endregion

        private void TvPlayersSettingsEditor_Load(object sender, EventArgs e)
        {
            listViewPlayers.SmallImageList = TvPlayersSettingsRegistration.Settings.PlayerIcons;
            listViewPlayers.LargeImageList = TvPlayersSettingsRegistration.Settings.PlayerIcons;

            buttonEdit.Enabled = false;
            buttonDelete.Enabled = false;
            buttonSetDefault.Enabled = false;

            var tilesPerRow = 2;
            listViewPlayers.TileSize = new Size((listViewPlayers.Width - SystemInformation.VerticalScrollBarWidth - 6) / tilesPerRow,
                Settings.PlayerIcons.ImageSize.Height + 4);

            _manualUpdateLock++;
            labelDefaultPlayer.Text = Settings.GetDefaultPlayer()?.Name ?? SettingsTexts.TvPlayersNoDefaultPlayer;
            checkBoxShortcut.Checked = !Settings.DirectLaunch;
            _manualUpdateLock--;

            _players = new List<TvPlayer>(Settings.Players);
            FillList(false, true);
        } // TvPlayersSettingsEditor_Load

        private void listViewPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var player = (listViewPlayers.SelectedItems.Count > 0) ? (TvPlayer)listViewPlayers.SelectedItems[0].Tag : null;

            buttonEdit.Enabled = player != null;
            buttonDelete.Enabled = player != null;
            buttonSetDefault.Enabled = player != null;

            if (player == null) return;

            buttonDelete.Enabled = (player.Id != Settings.DefaultPlayerId) && (listViewPlayers.Items.Count > 1);
            buttonSetDefault.Enabled = player.Id != Settings.DefaultPlayerId;
        } // listViewPlayers_SelectedIndexChanged

        private void listViewPlayers_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit.PerformClick();
        } // listViewPlayers_DoubleClick

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var item = (listViewPlayers.SelectedItems.Count > 0) ? listViewPlayers.SelectedItems[0] : null;
            if (item == null) return;

            using var editor = new TvPlayerEditorDialog
            {
                Player = (TvPlayer) item.Tag
            };
            if (editor.ShowDialog(this) != DialogResult.OK) return;
            if (!editor.IsDataChanged) return;

            FillList(true, false);
            IsDataChanged = true;
        } // buttonEdit_Click

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var item = (listViewPlayers.SelectedItems.Count > 0) ? listViewPlayers.SelectedItems[0] : null;
            if (item == null) return;

            var player = (TvPlayer)item.Tag;

            if (MessageBox.Show(this,
                string.Format(SettingsTexts.TvPlayerDelete, player.Name),
                SettingsTexts.TvPlayerDeleteCaption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;

            item.Selected = false;
            _players.Remove(player);
            IsDataChanged = true;
            FillList(false, false);
        } // buttonDelete_Click

        private void buttonSetDefault_Click(object sender, EventArgs e)
        {
            var item = (listViewPlayers.SelectedItems.Count > 0) ? listViewPlayers.SelectedItems[0] : null;
            if (item == null) return;

            var player = (TvPlayer)item.Tag;
            Settings.DefaultPlayerId = player.Id;
            IsDataChanged = true;
            labelDefaultPlayer.Text = player.Name;

            // update list
            FillList(true, true);
        } // buttonSetDefault_Click

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using var editor = new TvPlayerEditorDialog();

            if (editor.ShowDialog(this) != DialogResult.OK) return;
            if (!editor.IsDataChanged) return;

            _players.Add(editor.Player);
            FillList(false, false);
            listViewPlayers.Items[listViewPlayers.Items.Count - 1].Selected = true;
        } // buttonAdd_Click

        private void checkBoxShortcut_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DirectLaunch = !checkBoxShortcut.Checked;
            IsDataChanged = true;
        } // checkBoxShortcut_CheckedChanged

        private void FillList(bool keepSelection, bool selectDefault)
        {
            FillList(listViewPlayers, _players, Settings.DefaultPlayerId, keepSelection, selectDefault, false);
        } // FillList

        internal static void FillList(ListView list, ICollection<TvPlayer> players, Guid defaultPlayerId, bool keepSelection, bool selectDefault, bool ignoreMissingPlayers)
        {
            var selectedIndex = (list.SelectedItems.Count > 0) ? list.SelectedItems[0].Index : -1;

            list.BeginUpdate();
            list.Items.Clear();

            foreach (var player in players)
            {
                var item = GetTvPlayerListItem(player, defaultPlayerId, list.Font, selectDefault);
                if (!ignoreMissingPlayers || (item.ImageKey != TvPlayersSettings.PlayerNotFoundKey))
                {
                    list.Items.Add(item);
                }
            } // foreach

            if ((selectedIndex >= 0) && (keepSelection))
            {
                list.Items[selectedIndex].Selected = true;
            } // if

            list.EndUpdate();
        } // FillList

        private static ListViewItem GetTvPlayerListItem(TvPlayer player, Guid defaultPlayerId, Font listFont, bool selectDefault)
        {
            var item = new ListViewItem(player.Name)
            {
                Tag = player,
                ImageKey = TvPlayersSettingsRegistration.Settings.GetPlayerIconKey(player.Path)
            };

            if (player.Id != defaultPlayerId) return item;

            item.Font = new Font(listFont, FontStyle.Bold);
            if (selectDefault)
            {
                item.Selected = true;
            } // if

            return item;
        } // GetTvPlayerListItem
    } // class TvPlayersSettingsEditor
} // namespace
