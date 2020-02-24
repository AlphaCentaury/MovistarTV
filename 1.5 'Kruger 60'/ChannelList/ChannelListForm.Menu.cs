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

using IpTviewr.ChannelList.Properties;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Discovery.BroadcastList;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using IpTviewr.UiServices.Configuration.Push;
using IpTviewr.UiServices.Forms;

namespace IpTviewr.ChannelList
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "WinForms designer creates wrong member names")]
    partial class ChannelListForm
    {
        #region 'IPTViewr' menu init

        private void InitIpTviewrMenu()
        {
            var providerTexts = AppConfig.Current.IpTvService.Texts.Provider;

            menuItemIpTviewrProvider.Text = providerTexts.MenuEntry;
            menuItemIpTviewrProviderSelect.Text = providerTexts.MenuSelect;
            menuItemIpTviewrProviderDetails.Text = providerTexts.MenuDetails;
        } // InitIpTviewrMenu

        #endregion

        #region 'IPTViewr > Packages' menu event handlers

        private void menuItemPackagesSelect_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemPackagesSelect_Click, sender, e);
        } // menuItemPackagesSelect_Click

        private void menuItemPackagesManage_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemPackagesManage_Click, sender, e);
        } // menuItemPackagesManage_Click

        #endregion

        #region 'IPTViewr > Provider' menu event handlers

        private void menuItemIpTviewrProviderSelect_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemProviderSelect_Click, sender, e);
        } // menuItemIpTviewrProviderSelect_Click

        private void menuItemIpTviewrProviderDetails_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemProviderDetails_Click, sender, e);
        } // menuItemIpTviewrProviderDetails_Click

        #endregion

        #region 'IPTViewr > Recent' menu event handlers

        private void menuItemIpTviewrRecent_DropDownOpening(object sender, EventArgs e)
        {
            // TODO: update recent list
        }  // menuItemIpTviewrRecent_DropDownOpening

        private void menuItemIpTviewrRecent_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemIpTviewrRecent");
        }  // menuItemIpTviewrRecent_Click

        #endregion

        #region #region 'IPTViewr > Export' menu event handlers

        private void menuItemIpTviewrExport_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemIpTviewrExport");
        } // menuItemIpTviewrExport_Click

        #endregion

        #region 'IPTViewr > Settings' menu event handlers

        private void menuItemIpTviewrSettings_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemIpTviewrSettings_Click, sender, e);
        } // menuItemIpTviewrSettings_Click

        #endregion

        #region 'IPTViewr > Exit' menu event handlers

        private void menuItemIpTviewrExit_Click(object sender, EventArgs e)
        {
            Close();
        } // menuItemIpTviewrExit_Click

        #endregion

        #region 'IPTViewr > Provider' menu event handlers implementation

        private void Implementation_menuItemProviderSelect_Click(object sender, EventArgs e)
        {
            // can't select a new provider if a services scan is in progress; the user must manually cancel it first
            if (IsScanActive()) return;
            SelectProvider();
        } // Implementation_menuItemProviderSelect_Click

        private void Implementation_menuItemProviderDetails_Click(object sender, EventArgs e)
        {
            if (_selectedServiceProvider == null) return;
            DiscoveryDialogs.ShowServiceProviderDetails(this, _selectedServiceProvider);
        } // Implementation_menuItemProviderDetails_Click

        private void SelectProvider()
        {
            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = _selectedServiceProvider;
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK) return;

                _selectedServiceProvider = dialog.SelectedServiceProvider;
                ServiceProviderChanged();
            } // dialog
        } // SelectProvider

        #endregion

        #region 'IPTViewr > Package' menu event handlers implementation

        private void Implementation_menuItemPackagesSelect_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemPackagesSelect");
        } // Implementation_menuItemPackagesSelect_Click

        private void Implementation_menuItemPackagesManage_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemPackagesManage");
        } // Implementation_menuItemPackagesManage_Click

        #endregion

        #region 'IPTViewr > Settings' menu event handlers implementation

        private void Implementation_menuItemIpTviewrSettings_Click(object sender, EventArgs e)
        {
            var applyChanges = new Dictionary<Guid, Action>(1)
            {
                {UiBroadcastListSettingsRegistration.SettingsGuid, () => { _listManager.Settings = UiBroadcastListSettingsRegistration.Settings; }}
            };

            ConfigurationForm.ShowConfigurationForm(this, true, applyChanges);
        } // menuItemIpTviewrSettings_Click

        #endregion
        
        #region 'Help' menu event handlers

        private void menuItemHelpDocumentation_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpDocumentation_Click, sender, e);
        } // menuItemHelpDocumentation_Click

        private void menuItemHelpHomePage_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpHomePage_Click, sender, e);
        } // menuItemHelpHomePage_Click

        private void menuItemHelpReportIssue_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpReportIssue_Click, sender, e);
        } // menuItemHelpReportIssue_Click

        private void menuItemHelpCheckUpdates_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpCheckUpdates_Click, sender, e);
        } // menuItemHelpCheckUpdates_Click

        private void menuItemHelpTelemetry_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpTelemetry_Click, sender, e);
        } // menuItemHelpTelemetry_Click

        private void menuItemHelpAbout_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpAbout_Click, sender, e);
        } // menuItemHelpAbout_Click

        #endregion

        #region 'Help' menu event handlers implementation

        private void Implementation_menuItemHelpDocumentation_Click(object sender, EventArgs e)
        {
            OpenUrl(InvariantTexts.UrlDocumentation);
        } // Implementation_menuItemHelpDocumentation_Click

        private void Implementation_menuItemHelpHomePage_Click(object sender, EventArgs e)
        {
            OpenUrl(InvariantTexts.UrlHomePage);
        } // Implementation_menuItemHelpHomePage_Click

        private void Implementation_menuItemHelpReportIssue_Click(object sender, EventArgs e)
        {
            OpenUrl(InvariantTexts.UrlReportIssue);
        } // Implementation_menuItemHelpReportIssue_Click

        private void Implementation_menuItemHelpCheckUpdates_Click(object sender, EventArgs e)
        {
            PushManager.CheckForUpdates(this, new MyApplication.PushUpdateContext());
        } // Implementation_menuItemHelpCheckUpdates_Click

        private void Implementation_menuItemHelpTelemetry_Click(object sender, EventArgs e)
        {
            HelpDialog.ShowRtfHelp(this, Texts.AppTelemetry);
        } // Implementation_menuItemHelpTelemetry_Click

    private void Implementation_menuItemHelpAbout_Click(object sender, EventArgs e)
        {
            using (var box = new AboutBox())
            {
                box.ApplicationData = new AboutBoxApplicationData()
                {
                    LargeIcon = Resources.AboutIcon,
                    Name = Texts.AppName,
                    Version = Texts.AppVersion,
                    Status = Texts.AppStatus,
                    LicenseTextRtf = Texts.SolutionLicenseRtf
                };
                box.ShowDialog(this);
            } // using box
        } // menuItemHelpAbout_Click_Implementation

        #endregion
    } // partial class ChannelListForm
} // namespace
