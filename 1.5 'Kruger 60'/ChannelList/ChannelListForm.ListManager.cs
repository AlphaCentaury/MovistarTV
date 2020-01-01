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

using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Discovery.BroadcastList;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.ChannelList
{
    partial class ChannelListForm
    {
        #region ListManager event handlers

        private void ListManager_StatusChanged(object sender, ListStatusChangedEventArgs e)
        {
            SafeCall(ListManager_StatusChanged_Implementation, sender, e);
        } // ListManager_StatusChanged

        private void ListManager_SelectionChanged(object sender, ListSelectionChangedEventArgs e)
        {
            SafeCall(ListManager_SelectionChanged_Implementation, sender, e);
        } // ListManager_SelectionChanged

        private void ListManager_StatusChanged_Implementation(object sender, ListStatusChangedEventArgs e)
        {
            _listManager.ListView.Enabled = e.HasItems;
            menuItemChannelFavorites.Enabled = e.HasItems && EnableMenuItemChannelFavorites;
            menuItemChannelListView.Enabled = e.HasItems;
            menuItemChannelEditList.Enabled = e.HasItems && EnableMenuItemChannelEditList;
            menuItemChannelVerify.Enabled = e.HasItems;
        } // ListManager_StatusChanged_Implementation

        private void ListManager_SelectionChanged_Implementation(object sender, ListSelectionChangedEventArgs e)
        {
            // save selection
            // TODO: save ListManager.SelectedService in user-config
            Properties.Settings.Default.LastSelectedService = (e.Item != null) ? e.Item.Key : null;
            Properties.Settings.Default.Save();

            var enable = e.Item != null;
            var enable2 = enable && !e.Item.IsHidden;
            menuItemChannelShow.Enabled = enable2;
            menuItemChannelShowWith.Enabled = enable2;
            menuItemChannelFavoritesAdd.Enabled = enable2;
            menuItemChannelDetails.Enabled = enable;
            menuItemRecordingsRecord.Enabled = enable2;

            // EPG
            EnableEpgMenus(enable);
            if (enable)
            {
                ShowEpgMiniGuide(true);
            }
            else
            {
                epgMiniGuide.ClearEpgPrograms();
            } // if-else
        } // ListManager_SelectionChanged_Implementation

        #endregion

        #region Context menu

        private void SetupContextMenuList()
        {
            contextMenuListMode.Text = menuItemChannelListView.Text;
            contextMenuListMode.Image = menuItemChannelListView.Image;
            contextMenuListProperties.Text = menuItemChannelDetails.Text;
            contextMenuListProperties.Image = menuItemChannelDetails.Image;
        } // SetupContextMenuList

        private void contextMenuList_Opening(object sender, CancelEventArgs e)
        {
            // sync Properties context item with main menu counterpart
            contextMenuListShow.Enabled = menuItemChannelShow.Enabled;
            contextMenuListRecord.Enabled = menuItemRecordingsRecord.Enabled;
            contextMenuListShowWith.Enabled = menuItemChannelShowWith.Enabled;
            contextMenuListProperties.Enabled = menuItemChannelDetails.Enabled;
        } // contextMenuList_Opening

        private void contextMenuListShow_Click(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel, true);
        } // contextMenuListShow_Click

        private void contextMenuListShowWith_Click(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel, false);
        } // contextMenuListShowWith_Click

        private void contextMenuListMode_Click(object sender, EventArgs e)
        {
            _listManager.ShowSettingsEditor(this, true);
        } // contextMenuListMode_Click

        private void contextMenuListCopy_DropDownOpening(object sender, EventArgs e)
        {
            contextMenuListCopyRow.Enabled = _listManager.SelectedService != null;
            contextMenuListCopyAll.Enabled = _listManager.HasItems;
        } // contextMenuListCopy_DropDownOpening

        private void contextMenuListCopyURL_Click(object sender, EventArgs e)
        {
            var service = _listManager.SelectedService;
            if (service == null) return;

            Clipboard.SetText(service.LocationUrl, TextDataFormat.UnicodeText);
        } // contextMenuListCopyURL_Click

        private void contextMenuListCopyRow_Click(object sender, EventArgs e)
        {
            StringBuilder buffer;

            var service = _listManager.SelectedService;
            if (service == null) return;

            buffer = new StringBuilder();
            DumpHeader(buffer);
            DumpBroadcastService(service, buffer);

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyRow_Click

        private void contextMenuListCopyAll_Click(object sender, EventArgs e)
        {
            StringBuilder buffer;

            buffer = new StringBuilder();

            DumpHeader(buffer);
            foreach (var service in _listManager.BroadcastServices)
            {
                DumpBroadcastService(service, buffer);
                buffer.AppendLine();
            } // foreach item

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyAll_Click

        private void DumpHeader(StringBuilder buffer)
        {
            buffer.Append("Number");
            buffer.Append("\t");
            buffer.Append("Name");
            buffer.Append("\t");
            buffer.Append("Description");
            buffer.Append("\t");
            buffer.Append("Type");
            buffer.Append("\t");
            buffer.Append("LocationUrl");
            buffer.Append("\t");
            buffer.Append("FullServiceId");
            buffer.Append("\t");
            buffer.Append("ReplacementServiceId");
            buffer.AppendLine();
        } // DumpHeader

        private void DumpBroadcastService(UiBroadcastService service, StringBuilder buffer)
        {
            buffer.Append(service.DisplayLogicalNumber);
            buffer.Append("\t");
            buffer.Append(service.DisplayName);
            buffer.Append("\t");
            buffer.Append(service.DisplayDescription);
            buffer.Append("\t");
            buffer.Append(service.DisplayServiceType);
            buffer.Append("\t");
            buffer.Append(service.DisplayLocationUrl);
            buffer.Append("\t");
            buffer.Append(service.FullServiceName);
            buffer.Append("\t");
            var replacement = service.ReplacementService;
            if (replacement != null)
            {
                if (string.IsNullOrEmpty(replacement.DomainName))
                {
                    buffer.Append(replacement.ServiceName);
                    buffer.Append('.');
                    buffer.Append(service.DomainName);
                }
                else
                {
                    buffer.Append(replacement.ServiceName);
                    buffer.Append('.');
                    buffer.Append(replacement.DomainName);
                } // if-else
            } // if
        } // DumpBroadcastService

        private void contextMenuListExportM3u_Click(object sender, EventArgs e)
        {
            SafeCall(contextMenuListExportM3u_Click_Implementation, sender, e);
        }

        private void contextMenuListExportM3u_Click_Implementation(object sender, EventArgs e)
        {
            string filename;

            using (var fileDialog = new SaveFileDialog()
            {
                AddExtension = true,
                AutoUpgradeEnabled = true,
                CheckPathExists = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                OverwritePrompt = true,
                RestoreDirectory = true,
                ShowHelp = false,
                SupportMultiDottedExtensions = true,
                Title = contextMenuListExportM3u.Text,
                ValidateNames = true,
                DefaultExt = "m3u",
                Filter = ".m3u|*.m3u",
            })
            {
                if (fileDialog.ShowDialog(this) != DialogResult.OK) return;
                filename = fileDialog.FileName;
            } // using

            AppTelemetry.CustomEvent(GetType().Name, "Feature", "contextMenuListExportM3u", "contextMenuListExportM3u.Text");

            var sortedServices = from service in _listManager.BroadcastServices
                                 orderby service.DisplayLogicalNumber
                                 select service;

            var output = new StringBuilder();
            output.AppendLine("#EXTM3U");

            foreach (var service in sortedServices)
            {
                output.AppendFormat("#EXTINF:-1,[{0}] {1}", service.DisplayLogicalNumber, service.DisplayName);
                output.AppendLine();
                output.AppendLine(service.DisplayLocationUrl);
            } // foreach service

            File.WriteAllText(filename, output.ToString(), Encoding.UTF8);
        } // contextMenuListExportM3u_Click_Implementation

        #endregion
    } // partial class ChannelListForm
} // namespace
