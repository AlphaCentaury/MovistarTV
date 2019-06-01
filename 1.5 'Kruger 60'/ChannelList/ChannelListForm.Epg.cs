// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.EPG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            menuItemEpgNow.Enabled = enable & enable_Epg;
            menuItemEpgToday.Enabled = enable & enable_Epg;
            menuItemEpgTomorrow.Enabled = enable & enable_Epg;
            menuItemEpgPrevious.Enabled = false;
            menuItemEpgNext.Enabled = false;
            menuItemEpgBasicGrid.Enabled = enable_Epg && (SelectedServiceProvider != null);
        } // EnableEpgMenus

        private void ShowEpgMiniGuide(bool display)
        {
            epgMiniGuide.Visible = display;
            if (!display) return;

            // display mini bar
            // TODO: epgMiniGuide.DetailsEnabled
            epgMiniGuide.DetailsEnabled = false; //(IpTvProvider.Current.EpgInfo.Capabilities & EpgInfoProviderCapabilities.ExtendedInfo) != 0;
            epgMiniGuide.LoadEpgPrograms(ListManager.SelectedService, DateTime.Now, EpgDatastore);
        }  // ShowEpgMiniGuide

        private void ShowEpgNowThenForm()
        {
            // TODO: ShowEpgNowThenForm
            // EpgNowThenDialog.ShowEpgPrograms(ListManager.SelectedService, epgMiniGuide.GetEpgPrograms(), this, epgMiniGuide.LocalReferenceTime);
        } // ShowEpgNowThenForm

        private void ShowEpgBasicGrid()
        {
            EpgBasicGridDialog.ShowGrid(this, ListManager.GetDisplayedBroadcastList(), ListManager.SelectedService, EpgDatastore);
        } // ShowEpgBasicGrid

        private void ShowEpgExtendedInfo()
        {
            // TODO: ShowEpgExtendedInfo
            // EpgExtendedInfoDialog.ShowExtendedInfo(this, ListManager.SelectedService, epgMiniGuide.SelectedEvent);
        } // ShowEpgExtendedInfo

        private void UpdateEpgData()
        {
            if (!enable_Epg) return;

            // TODO: call EpgDownloader with appropriate EpgDatastore
        } // UpdateEgpData

        private void ShowEpgList(int daysDelta)
        {
            if (ListManager.SelectedService == null) return;

            // TODO: ShowEpgList
            /*
            using (var form = new EpgChannelPrograms())
            {
                // TODO: unify code with mini-bar code

                // TODO: get dbFile from config
                form.EpgDatabase = Path.Combine(AppUiConfiguration.Current.Folders.Cache, "EPG.sdf");

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
