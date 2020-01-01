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
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Forms;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.ChannelList
{
    partial class ChannelListForm
    {
        #region Auxiliary methods: providers

        private void ServiceProviderChanged()
        {
            Properties.Settings.Default.LastSelectedServiceProvider = _selectedServiceProvider?.Key;
            Properties.Settings.Default.Save();

            labelProviderName.Text = AppConfig.Current.IpTvService.Texts.Provider.NoSelection;
            labelProviderDescription.Text = null;
            pictureProviderLogo.Image = null;
            menuItemIpTviewrProviderDetails.Enabled = false;
            menuItemChannelRefreshList.Enabled = false;
            menuItemChannelEditList.Enabled = false;
            SetBroadcastDiscovery(null);

            if (_selectedServiceProvider == null) return;

            labelProviderName.Text = _selectedServiceProvider.DisplayName;
            labelProviderDescription.Text = _selectedServiceProvider.DisplayDescription;
            pictureProviderLogo.Image = _selectedServiceProvider.Logo.GetImage(LogoSize.Size32);

            menuItemIpTviewrProviderDetails.Enabled = true;
            menuItemChannelRefreshList.Enabled = true;
            menuItemChannelEditList.Enabled = true;

            SetBroadcastDiscovery(null);
            LoadBroadcastDiscovery(true);
        } // ServiceProviderChanged

        #endregion
    } // partial class ChannelListForm
} // namespace
