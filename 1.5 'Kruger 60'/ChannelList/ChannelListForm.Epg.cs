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

using IpTviewr.UiServices.EPG;
using System;

namespace IpTviewr.ChannelList
{
    partial class ChannelListForm
    {
        #region 'EPG' menu and mini guide event handlers

        private void menuItemEpgBasicGrid_Click(object sender, EventArgs e)
        {
            SafeCall(ShowEpgBasicGrid);
        } // menuItemEpgBasicGrid_Click

        private void menuItemEpgNow_Click(object sender, EventArgs e)
        {
            SafeCall(ShowEpgNowThenForm);
        } // menuItemEpgNow_Click

        private void menuItemEpgToday_Click(object sender, EventArgs e)
        {
            ShowEpgList(0);
        } // menuItemEpgToday_Click

        private void menuItemEpgTomorrow_Click(object sender, EventArgs e)
        {
            ShowEpgList(1);
        } // menuItemEpgTomorrow_Click

        private void menuItemEpgPrevious_Click(object sender, EventArgs e)
        {
            SafeCall(epgMiniGuide.GoBack);
        } // menuItemEpgPrevious_Click

        private void menuItemEpgNext_Click(object sender, EventArgs e)
        {
            SafeCall(epgMiniGuide.GoForward);
        } // menuItemEpgNext_Click

        private void epgMiniGuide_ButtonClicked(object sender, EpgMiniBarButtonClickedEventArgs e)
        {
            switch (e.Button)
            {
                case EpgMiniGuide.Button.EpgGrid:
                    SafeCall(ShowEpgBasicGrid);
                    break;
            } // switch
        } // epgMiniGuide_ButtonClicked

        private void epgMiniGuide_NavigationButtonsChanged(object sender, EpgMiniBarNavigationButtonsChangedEventArgs e)
        {
            menuItemEpgPrevious.Enabled = e.IsBackEnabled;
            menuItemEpgNext.Enabled = e.IsForwardEnabled;
        } // epgMiniGuide_NavigationButtonsChanged

        #endregion

        #region 'EPG' menu and mini guide event handlers implementation



        #endregion

        #region Auxiliary methods: EPG

        private void EnableEpgMenus(bool enable)
        {
            menuItemEpgNow.Enabled = enable & _enableEpg;
            menuItemEpgToday.Enabled = enable & _enableEpg;
            menuItemEpgTomorrow.Enabled = enable & _enableEpg;
            menuItemEpgPrevious.Enabled = false;
            menuItemEpgNext.Enabled = false;
            menuItemEpgBasicGrid.Enabled = _enableEpg && (_selectedServiceProvider != null);
        } // EnableEpgMenus

        private void ShowEpgMiniGuide(bool display)
        {
            epgMiniGuide.Visible = display;
            if (!display) return;

            // display mini bar
            // TODO: epgMiniGuide.DetailsEnabled
            epgMiniGuide.DetailsEnabled = false; //(IpTvService.Current.EpgInfo.Capabilities & EpgInfoProviderCapabilities.ExtendedInfo) != 0;
            epgMiniGuide.LoadEpgPrograms(_listManager.SelectedService, DateTime.Now);
            epgMiniGuide.SetEpgDataStore(_epgDataStore);
        }  // ShowEpgMiniGuide

        private void ShowEpgNowThenForm()
        {
            // TODO: ShowEpgNowThenForm
            // EpgNowThenDialog.ShowEpgPrograms(ListManager.SelectedService, epgMiniGuide.GetEpgPrograms(), this, epgMiniGuide.LocalReferenceTime);
        } // ShowEpgNowThenForm

        private void ShowEpgBasicGrid()
        {
            EpgBasicGridDialog.ShowGrid(this, _listManager.GetDisplayedBroadcastList(), _listManager.SelectedService, _epgDataStore);
        } // ShowEpgBasicGrid

        private void ShowEpgExtendedInfo()
        {
            // TODO: ShowEpgExtendedInfo
            // EpgExtendedInfoDialog.ShowExtendedInfo(this, ListManager.SelectedService, epgMiniGuide.SelectedEvent);
        } // ShowEpgExtendedInfo

        private void ShowEpgList(int daysDelta)
        {
            if (_listManager.SelectedService == null) return;

            // TODO: ShowEpgList
            /*
            using (var form = new EpgChannelPrograms())
            {
                // TODO: unify code with mini-bar code

                // TODO: get dbFile from config
                form.EpgDatabase = Path.Combine(AppConfig.Current.Folders.Cache, "EPG.sdf");

                // TODO: do NOT assume .imagenio.es
                form.FullServiceName = ListManager.SelectedService.ServiceName + ".imagenio.es";
                var replacement = ListManager.SelectedService.ReplacementService;
                form.FullAlternateServiceName = (replacement == null) ? null : replacement.ServiceName + ".imagenio.es";

                form.DaysDelta = daysDelta;
                form.Service = ListManager.SelectedService;

                form.ShowDialog(this);
            } // using form
            */
        } // ShowEpgList

        #endregion
    } // partial class ChannelListForm
} // namespace
