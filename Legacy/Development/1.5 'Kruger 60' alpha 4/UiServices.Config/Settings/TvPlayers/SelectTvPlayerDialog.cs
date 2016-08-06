// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.UiServices.Configuration.Settings.TvPlayers.Editors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Configuration.Settings.TvPlayers
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

            TvPlayersSettingsEditor.FillList(listViewPlayers, new List<TvPlayer>(settings.Players), settings.DefaultPlayerId, false, true);
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
