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

using IpTviewr.UiServices.Configuration.Settings.TvPlayers.Editors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers
{
    public partial class SelectTvPlayerDialog : Form
    {
        public SelectTvPlayerDialog()
        {
            InitializeComponent();
        } // constructor

        public TvPlayer SelectedPlayer
        {
            get;
            private set;
        } // SelectedPlayer

        private void SelectTvPlayerDialog_Load(object sender, EventArgs e)
        {
            var settings = TvPlayersSettingsRegistration.Settings;

            buttonOk.Enabled = false;

            listViewPlayers.SmallImageList = settings.PlayerIcons;
            listViewPlayers.LargeImageList = settings.PlayerIcons;
            listViewPlayers.TileSize = new Size((listViewPlayers.Width - SystemInformation.VerticalScrollBarWidth - 6),
                settings.PlayerIcons.ImageSize.Height + 4);

            TvPlayersSettingsEditor.FillList(listViewPlayers, settings.Players, settings.DefaultPlayerId, false, true, true);
        } // SelectTvPlayerDialog_Load

        private void listViewPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPlayer = (listViewPlayers.SelectedItems.Count > 0) ? (TvPlayer)listViewPlayers.SelectedItems[0].Tag : null;
            buttonOk.Enabled = (SelectedPlayer != null);
        } // listViewPlayers_SelectedIndexChanged

        private void listViewPlayers_DoubleClick(object sender, EventArgs e)
        {
            buttonOk.PerformClick();
        } // listViewPlayers_DoubleClick
    } // class SelectTvPlayerDialog
} // namespace
