// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers.Editors
{
    public partial class TvPlayersSettingsEditor : UserControl, IConfigurationItemEditor
    {
        private int ManualUpdateLock;
        private List<TvPlayer> Players;

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

        UserControl IConfigurationItemEditor.UserInterfaceItem
        {
            get { return this; }
        } // IConfigurationItemEditor.UserInterfaceItem

        bool IConfigurationItemEditor.SupportsWinFormsValidation
        {
            get { return false; }
        } // IConfigurationItemEditor.SupportsWinFormsValidation

        public bool IsDataChanged
        {
            get;
            protected set;
        } // IsDataChanged

        public bool IsAppRestartNeeded
        {
            get { return false; }
        } // IsAppRestartNeeded

        bool IConfigurationItemEditor.Validate()
        {
            return true;
        } // IConfigurationItemEditor.Validate

        IConfigurationItem IConfigurationItemEditor.GetNewData()
        {
            Settings.Players = Players.ToArray();

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

            ManualUpdateLock++;
            labelDefaultPlayer.Text = Settings.GetDefaultPlayer().Name;
            checkBoxShortcut.Checked = !Settings.DirectLaunch;
            ManualUpdateLock--;

            Players = new List<TvPlayer>(Settings.Players);
            FillList(false, true);
        } // TvPlayersSettingsEditor_Load

        private void listViewPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var player = (listViewPlayers.SelectedItems.Count > 0) ? (TvPlayer)listViewPlayers.SelectedItems[0].Tag : null;

            buttonEdit.Enabled = player != null;
            buttonDelete.Enabled = player != null;
            buttonSetDefault.Enabled = player != null;

            if (player == null) return;

            buttonDelete.Enabled = player.Id != Settings.DefaultPlayerId;
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

            var player = (TvPlayer)item.Tag;
            using (var editor = new TvPlayerEditorDialog())
            {
                editor.Player = player;
                editor.ShowDialog(this);
                if (editor.IsDataChanged)
                {
                    FillList(true, false);
                    IsDataChanged = true;
                } // if
            } // using
        } // buttonEdit_Click

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var item = (listViewPlayers.SelectedItems.Count > 0) ? listViewPlayers.SelectedItems[0] : null;
            if (item == null) return;

            var player = (TvPlayer)item.Tag;

            if (MessageBox.Show(this,
                string.Format(Properties.SettingsTexts.TvPlayerDelete, player.Name),
                Properties.SettingsTexts.TvPlayerDeleteCaption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;

            item.Selected = false;
            Players.Remove(player);
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
            using (var editor = new TvPlayerEditorDialog())
            {
                editor.ShowDialog(this);
                if (editor.IsDataChanged)
                {
                    Players.Add(editor.Player);
                    FillList(false, false);
                    listViewPlayers.Items[listViewPlayers.Items.Count - 1].Selected = true;
                } // if
            } // using
        } // buttonAdd_Click

        private void checkBoxShortcut_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DirectLaunch = !checkBoxShortcut.Checked;
            IsDataChanged = true;
        } // checkBoxShortcut_CheckedChanged

        private void FillList(bool keepSelection, bool selectDefault)
        {
            FillList(listViewPlayers, Players, Settings.DefaultPlayerId, keepSelection, selectDefault);
        } // FillList

        internal static void FillList(ListView list, IList<TvPlayer> players, Guid defaultPlayerId, bool keepSelection, bool selectDefault)
        {
            ListViewItem[] items;
            int index;
            int selectedIndex;

            selectedIndex = (list.SelectedItems.Count > 0) ? list.SelectedItems[0].Index : -1;

            items = new ListViewItem[players.Count];
            index = 0;
            foreach (var player in players)
            {
                items[index++] = GetTvPlayerListItem(player, defaultPlayerId, list.Font, selectDefault);
            } // foreach

            list.Items.Clear();
            list.Items.AddRange(items);
            if ((selectedIndex >= 0) && (keepSelection))
            {
                list.Items[selectedIndex].Selected = true;
            } // if
        } // FillList

        private static ListViewItem GetTvPlayerListItem(TvPlayer player, Guid defaultPlayerId, Font listFont, bool selectDefault)
        {
            var item = new ListViewItem(player.Name);
            item.Tag = player;
            item.ImageKey = TvPlayersSettingsRegistration.Settings.GetPlayerIconKey(player.Path);
            if (player.Id == defaultPlayerId)
            {
                item.Font = new Font(listFont, FontStyle.Bold);
                if (selectDefault)
                {
                    item.Selected = true;
                } // if
            } // if

            return item;
        } // GetTvPlayerListItem
    } // class TvPlayersSettingsEditor
} // namespace
