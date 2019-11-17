// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IpTviewr.ChannelList
{
    partial class ChannelListForm
    {
        #region 'Provider' menu event handlers

        private void menuItemProviderSelect_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemProviderSelect_Click, sender, e);
        } // menuItemProviderSelect_Click

        private void menuItemProviderDetails_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemProviderDetails_Click, sender, e);
        } // menuItemProviderDetails_Click

        #endregion

        #region 'Provider' menu event handlers implementation

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private void Implementation_menuItemProviderSelect_Click(object sender, EventArgs e)
        {
            // can't select a new provider if a services scan is in progress; the user must manually cancel it first
            if (IsScanActive()) return;
            SelectProvider();
        } // Implementation_menuItemProviderSelect_Click

        private void Implementation_menuItemProviderDetails_Click(object sender, EventArgs e)
        {
            if (_selectedServiceProvider == null) return;

            using (var dlg = new PropertiesDialog()
            {
                Caption = Properties.Texts.SPProperties,
                ItemProperties = _selectedServiceProvider.DumpProperties(),
                Description = _selectedServiceProvider.DisplayName,
                ItemIcon = _selectedServiceProvider.Logo.GetImage(LogoSize.Size64, true),
            })
            {
                dlg.ShowDialog(this);
            } // using
        } // Implementation_menuItemProviderDetails_Click

        private void SelectProvider()
        {
            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = _selectedServiceProvider;
                var result = dialog.ShowDialog(this);
                BasicGoogleTelemetry.SendScreenHit(this);
                if (result != DialogResult.OK) return;

                _selectedServiceProvider = dialog.SelectedServiceProvider;
                ServiceProviderChanged();
            } // dialog
        } // SelectProvider

        #endregion

        #region Auxiliary methods: providers

        private void ServiceProviderChanged()
        {
            Properties.Settings.Default.LastSelectedServiceProvider = (_selectedServiceProvider != null) ? _selectedServiceProvider.Key : null;
            Properties.Settings.Default.Save();

            labelProviderName.Text = Properties.Texts.NotSelectedServiceProvider;
            labelProviderDescription.Text = null;
            pictureProviderLogo.Image = null;
            menuItemProviderDetails.Enabled = false;
            menuItemChannelRefreshList.Enabled = false;
            menuItemChannelEditList.Enabled = false;
            SetBroadcastDiscovery(null);

            if (_selectedServiceProvider == null) return;

            labelProviderName.Text = _selectedServiceProvider.DisplayName;
            labelProviderDescription.Text = _selectedServiceProvider.DisplayDescription;
            pictureProviderLogo.Image = _selectedServiceProvider.Logo.GetImage(LogoSize.Size32, true);

            menuItemProviderDetails.Enabled = true;
            menuItemChannelRefreshList.Enabled = true;
            menuItemChannelEditList.Enabled = true;

            SetBroadcastDiscovery(null);
            LoadBroadcastDiscovery(true);
        } // ServiceProviderChanged

        #endregion
    } // partial class ChannelListForm
} // namespace
