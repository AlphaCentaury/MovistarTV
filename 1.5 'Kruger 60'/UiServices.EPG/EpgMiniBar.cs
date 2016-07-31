// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.IpTv.Services.EPG;
using Project.IpTv.Services.EPG.Serialization;
using Project.IpTv.Services.SqlServerCE;
using Project.IpTv.Common;

namespace Project.IpTv.UiServices.EPG
{
    public partial class EpgMiniBar : UserControl
    {
        private EpgEvent[] EpgEvents;
        private int EpgIndex;
        private int CurrentRequestId;

        private class LoadEpgEventsData
        {
            public int RequestId;
            public string FullServiceName;
            public string FullAlternateServiceName;
            public DateTime ReferenceTime;
            public EpgEvent[] EpgEvents;
        } // class LoadEpgEventsData

        public enum Button
        {
            Back,
            Forward,
            Details,
            FullView,
            EpgGrid
        } // enum Button

        public event EventHandler<EpgMiniBarButtonClickedEventArgs> ButtonClicked;
        public event EventHandler<EpgMiniBarNavigationButtonsChangedEventArgs> NavigationButtonsChanged;

        public EpgMiniBar()
        {
            InitializeComponent();
            AutoRefresh = true;
        } // constructor

        public EpgEvent SelectedEvent
        {
            get { return (EpgEvents == null) ? null : EpgEvents[EpgIndex]; }
        } // SelectedEvent

        public bool IsDisabled
        {
            get;
            set;
        } // IsDisabled

        [DefaultValue(true)]
        public bool AutoRefresh
        {
            get;
            set;
        } // AutoRefresh

        public bool DetailsButtonEnabled
        {
            get;
            set;
        }  // DetailsButtonEnabled

        public string EpgDatabase
        {
            get;
            private set;
        } // EpgDatabase

        public string FullServiceName
        {
            get;
            private set;
        } // FullServiceName

        public string FullAlternateServiceName
        {
            get;
            private set;
        } // FullAlternateServiceName

        public DateTime ReferenceTime
        {
            get;
            private set;
        } // ReferenceTime

        private void timerLoadingData_Tick(object sender, EventArgs e)
        {
            timerLoadingData.Enabled = false;
            labelFromTo.Text = Properties.Texts.EpgDataLoading;
            labelFromTo.Visible = true;
        } // timerLoadingData_Tick

        private void timerAutoRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                RefreshEpgEvents(DateTime.Now);
            }
            catch
            {
            } // try-catch
        } // timerAutoRefresh_Tick

        public void ClearEpgEvents()
        {
            timerLoadingData.Enabled = false;
            SetAutoRefreshTimer(false);
            EpgEvents = null;
            EpgIndex = -1;

            pictureChannelLogo.Image = null;

            labelProgramTitle.Text = IsDisabled? Properties.Texts.EpgNoInformation : null;
            labelEllapsed.Text = null;
            labelFromTo.Visible = false;
            labelStartTime.Visible = false;
            labelEndTime.Visible = false;
            epgProgressBar.Visible = false;

            EnableBackForward(false, false);
            buttonEpgGrid.Enabled = true;
            buttonDetails.Enabled = false;
        } // ClearEpgEvents

        public void DisplayEpgEvents(Image channelLogo, string fullServiceName, string fullAlternateServiceName, DateTime referenceTime, string epgDatabase)
        {
            EpgDatabase = epgDatabase;
            FullServiceName = fullServiceName;
            FullAlternateServiceName = fullAlternateServiceName;
            ReferenceTime = new DateTime(referenceTime.Year, referenceTime.Month, referenceTime.Day, referenceTime.Hour, referenceTime.Minute, 0, 0);
            
            // clean-up UI
            ClearEpgEvents();
            pictureChannelLogo.Image = channelLogo;

            LoadEpgEventsAsync();
        } // DisplayEpgEvents

        public void RefreshEpgEvents(DateTime referenceTime)
        {
            ReferenceTime = new DateTime(referenceTime.Year, referenceTime.Month, referenceTime.Day, referenceTime.Hour, referenceTime.Minute, 0, 0);
            LoadEpgEventsAsync();
        } // RefreshEpgEvents

        public EpgEvent[] GetEpgEvents()
        {
            if (EpgEvents == null) return null;

            var result = new EpgEvent[EpgEvents.Length];
            Array.Copy(EpgEvents, result, EpgEvents.Length);

            return result;
        } // GetEpgEvents

        public void GoBack()
        {
            if (EpgIndex < 0) return;
            DisplayEpgEvent(EpgIndex - 1);
        } // GoBack

        public void GoForward()
        {
            if (EpgIndex > 2) return;
            DisplayEpgEvent(EpgIndex + 1);
        } // GoFoward

        private void buttonBack_Click(object sender, EventArgs e)
        {
            var buttonClicked = ButtonClicked;
            if (buttonClicked != null)
            {
                buttonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.Back));
            } // if

            GoBack();
        } // buttonBack_Click

        private void buttonForward_Click(object sender, EventArgs e)
        {
            var buttonClicked = ButtonClicked;
            if (buttonClicked != null)
            {
                buttonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.Forward));
            } // if

            GoForward();
        } // buttonForward_Click

        private void buttonDetails_Click(object sender, EventArgs e)
        {
            var buttonClicked = ButtonClicked;
            if (buttonClicked == null) return;

            buttonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.Details));
        } // buttonDetails_Click

        private void buttonFullView_Click(object sender, EventArgs e)
        {
            var buttonClicked = ButtonClicked;
            if (buttonClicked == null) return;

            buttonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.FullView));
        } // buttonFullView_Click

        private void buttonEpgGrid_Click(object sender, EventArgs e)
        {
            var buttonClicked = ButtonClicked;
            if (buttonClicked == null) return;

            buttonClicked(this, new EpgMiniBarButtonClickedEventArgs(Button.EpgGrid));
        } // buttonEpgGrid_Click

        private void LoadEpgEventsAsync()
        {
            if (IsDisabled) return;

            if (CurrentRequestId == int.MaxValue) CurrentRequestId = 0;

            timerLoadingData.Enabled = false;
            timerLoadingData.Enabled = true;

            var data = new LoadEpgEventsData()
            {
                RequestId = ++CurrentRequestId,
                FullServiceName = this.FullServiceName,
                FullAlternateServiceName = this.FullAlternateServiceName,
                ReferenceTime = this.ReferenceTime
            };

            System.Threading.ThreadPool.QueueUserWorkItem((o) => LoadEpgEvents(data), null);
        } // LoadEpgEventsAsync

        private void LoadEpgEvents(LoadEpgEventsData data)
        {
            int serviceDbId;

            using (var cn = DbServices.GetConnection(EpgDatabase))
            {
                serviceDbId = EpgDbQuery.GetDatabaseIdForServiceId(FullServiceName, cn);
                data.EpgEvents = EpgDbQuery.GetBeforeNowAndThenEvents(cn, serviceDbId, ReferenceTime.ToUniversalTime());

                // try alternate service if no EPG data
                if ((data.EpgEvents == null) && (FullAlternateServiceName != null))
                {
                    serviceDbId = EpgDbQuery.GetDatabaseIdForServiceId(FullAlternateServiceName, cn);
                    data.EpgEvents = EpgDbQuery.GetBeforeNowAndThenEvents(cn, serviceDbId, ReferenceTime.ToUniversalTime());
                } // if

                this.BeginInvoke(new Action<LoadEpgEventsData>(DisplayEpgEvents), data);
            } // using
        } // LoadEpgEvents

        private void DisplayEpgEvents(LoadEpgEventsData data)
        {
            // ignore data if not from current request
            // as data is loading async, "old" load request may arrive if channel is quickly changed
            if (data.RequestId != CurrentRequestId) return;

            timerLoadingData.Enabled = false;
            SetAutoRefreshTimer(true);

            EpgEvents = data.EpgEvents;
            buttonFullView.Enabled = (EpgEvents != null);

            if (EpgEvents == null)
            {
                DisplayEpgEvent(0);
            }
            else
            {
                if ((EpgIndex != -1) && (EpgEvents[EpgIndex] != null))
                {
                    DisplayEpgEvent(EpgIndex);
                }
                else if (EpgEvents[1] != null)
                {
                    DisplayEpgEvent(1);
                }
                else if (EpgEvents[0] != null)
                {
                    DisplayEpgEvent(0);
                }
                else if (EpgEvents[2] != null)
                {
                    DisplayEpgEvent(2);
                }
                else
                {
                    DisplayEpgEvent(0);
                } // if-else
            } // if-else
        } // DisplayEpgEvents

        private void DisplayEpgEvent(int index)
        {
            TimeSpan ellapsed;

            EpgIndex = index;

            epgProgressBar.Visible = (index == 1);
            labelEndTime.Visible = (index == 1);
            labelStartTime.Visible = (index == 1);
            labelFromTo.Visible = (index != 1);

            var epgEvent = (EpgEvents != null) ? EpgEvents[EpgIndex] : null;

            buttonDetails.Enabled = DetailsButtonEnabled && (epgEvent != null);
            if (epgEvent == null)
            {
                labelProgramTitle.Text = Properties.Texts.EpgNoInformation;
                labelFromTo.Text = null;
                labelStartTime.Text = null;
                labelEllapsed.Text = null;
                EnableBackForward(false, false);
                return;
            } // if

            labelProgramTitle.Text = epgEvent.Title;

            switch (EpgIndex)
            {
                case 0:
                    labelFromTo.Text = FormatString.DateTimeFromToMinutes(epgEvent.LocalStartTime, epgEvent.LocalEndTime, ReferenceTime);
                    ellapsed = (ReferenceTime - epgEvent.LocalEndTime);
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramEnded, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(false, EpgEvents[1] != null);
                    break;
                case 1:
                    labelStartTime.Text = string.Format("{0:HH:mm}", epgEvent.LocalStartTime);
                    labelEndTime.Text = string.Format("{0:t}", epgEvent.LocalEndTime);
                    ellapsed = (ReferenceTime - epgEvent.LocalStartTime);
                    epgProgressBar.MaximumValue = epgEvent.Duration.TotalMinutes;
                    epgProgressBar.Value = ellapsed.TotalMinutes;
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramStarted, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(EpgEvents[0] != null, EpgEvents[2] != null);
                    break;
                default:
                    labelFromTo.Text = FormatString.DateTimeFromToMinutes(epgEvent.LocalStartTime, epgEvent.LocalEndTime, ReferenceTime);
                    ellapsed = (epgEvent.LocalStartTime - ReferenceTime);
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramWillStart, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(EpgEvents[1] != null, false);
                    break;
            } // switch
        } // DisplayEpgEvent

        private void EnableBackForward(bool back, bool forward)
        {
            buttonBack.Enabled = back;
            buttonForward.Enabled = forward;

            if (NavigationButtonsChanged != null)
            {
                NavigationButtonsChanged(this, new EpgMiniBarNavigationButtonsChangedEventArgs(back, forward));
            } // if
        } // EnableBackForward

        private void SetAutoRefreshTimer(bool enabled)
        {
            // ensure timer ticks at :01
            timerAutoRefresh.Interval = (61 - DateTime.Now.Second) * 1000;

            timerAutoRefresh.Enabled = enabled & AutoRefresh;
        } // SetAutoRefreshTimer
    } // class EpgMiniBar
} // namespace
