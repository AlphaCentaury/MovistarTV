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
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.Services.SqlServerCE;
using IpTviewr.Common;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgMiniBar : UserControl
    {
        private EpgProgram[] EpgPrograms;
        private int EpgIndex;
        private int CurrentRequestId;

        private class LoadEpgProgramsData
        {
            public int RequestId;
            public string FullServiceName;
            public string FullAlternateServiceName;
            public DateTime ReferenceTime;
            public EpgProgram[] EpgPrograms;
        } // class LoadEpgProgramsData

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

        public EpgProgram SelectedEvent
        {
            get { return (EpgPrograms == null) ? null : EpgPrograms[EpgIndex]; }
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
                RefreshEpgPrograms(DateTime.Now);
            }
            catch
            {
            } // try-catch
        } // timerAutoRefresh_Tick

        public void ClearEpgPrograms()
        {
            timerLoadingData.Enabled = false;
            SetAutoRefreshTimer(false);
            EpgPrograms = null;
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
        } // ClearEpgPrograms

        public void DisplayEpgPrograms(Image channelLogo, string fullServiceName, string fullAlternateServiceName, DateTime referenceTime, string epgDatabase)
        {
            EpgDatabase = epgDatabase;
            FullServiceName = fullServiceName;
            FullAlternateServiceName = fullAlternateServiceName;
            ReferenceTime = new DateTime(referenceTime.Year, referenceTime.Month, referenceTime.Day, referenceTime.Hour, referenceTime.Minute, 0, 0);
            
            // clean-up UI
            ClearEpgPrograms();
            pictureChannelLogo.Image = channelLogo;

            LoadEpgProgramsAsync();
        } // DisplayEpgPrograms

        public void RefreshEpgPrograms(DateTime referenceTime)
        {
            ReferenceTime = new DateTime(referenceTime.Year, referenceTime.Month, referenceTime.Day, referenceTime.Hour, referenceTime.Minute, 0, 0);
            LoadEpgProgramsAsync();
        } // RefreshEpgPrograms

        public EpgProgram[] GetEpgPrograms()
        {
            if (EpgPrograms == null) return null;

            var result = new EpgProgram[EpgPrograms.Length];
            Array.Copy(EpgPrograms, result, EpgPrograms.Length);

            return result;
        } // GetEpgPrograms

        public void GoBack()
        {
            if (EpgIndex < 0) return;
            DisplayEpgProgram(EpgIndex - 1);
        } // GoBack

        public void GoForward()
        {
            if (EpgIndex > 2) return;
            DisplayEpgProgram(EpgIndex + 1);
        } // GoFoward

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.Back));

            GoBack();
        } // buttonBack_Click

        private void buttonForward_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, new EpgMiniBarButtonClickedEventArgs(Button.Forward));

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

        private void LoadEpgProgramsAsync()
        {
            if (IsDisabled) return;

            if (CurrentRequestId == int.MaxValue) CurrentRequestId = 0;

            timerLoadingData.Enabled = false;
            timerLoadingData.Enabled = true;

            var data = new LoadEpgProgramsData()
            {
                RequestId = ++CurrentRequestId,
                FullServiceName = this.FullServiceName,
                FullAlternateServiceName = this.FullAlternateServiceName,
                ReferenceTime = this.ReferenceTime
            };

            System.Threading.ThreadPool.QueueUserWorkItem((o) => LoadEpgPrograms(data), null);
        } // LoadEpgProgramsAsync

        private void LoadEpgPrograms(LoadEpgProgramsData data)
        {
            int serviceDbId;

            using (var cn = DbServices.GetConnection(EpgDatabase))
            {
                // TODO: EPG
                // serviceDbId = EpgDbQuery.GetDatabaseIdForServiceId(FullServiceName, cn);
                // data.EpgPrograms = EpgDbQuery.GetBeforeNowAndThenEvents(cn, serviceDbId, ReferenceTime.ToUniversalTime());

                // try alternate service if no EPG data
                if ((data.EpgPrograms == null) && (FullAlternateServiceName != null))
                {
                    // serviceDbId = EpgDbQuery.GetDatabaseIdForServiceId(FullAlternateServiceName, cn);
                    // data.EpgPrograms = EpgDbQuery.GetBeforeNowAndThenEvents(cn, serviceDbId, ReferenceTime.ToUniversalTime());
                } // if

                this.BeginInvoke(new Action<LoadEpgProgramsData>(DisplayEpgPrograms), data);
            } // using
        } // LoadEpgPrograms

        private void DisplayEpgPrograms(LoadEpgProgramsData data)
        {
            // ignore data if not from current request
            // as data is loading async, "old" load request may arrive if channel is quickly changed
            if (data.RequestId != CurrentRequestId) return;

            timerLoadingData.Enabled = false;
            SetAutoRefreshTimer(true);

            EpgPrograms = data.EpgPrograms;
            buttonFullView.Enabled = (EpgPrograms != null);

            if (EpgPrograms == null)
            {
                DisplayEpgProgram(0);
            }
            else
            {
                if ((EpgIndex != -1) && (EpgPrograms[EpgIndex] != null))
                {
                    DisplayEpgProgram(EpgIndex);
                }
                else if (EpgPrograms[1] != null)
                {
                    DisplayEpgProgram(1);
                }
                else if (EpgPrograms[0] != null)
                {
                    DisplayEpgProgram(0);
                }
                else if (EpgPrograms[2] != null)
                {
                    DisplayEpgProgram(2);
                }
                else
                {
                    DisplayEpgProgram(0);
                } // if-else
            } // if-else
        } // DisplayEpgPrograms

        private void DisplayEpgProgram(int index)
        {
            TimeSpan ellapsed;

            EpgIndex = index;

            epgProgressBar.Visible = (index == 1);
            labelEndTime.Visible = (index == 1);
            labelStartTime.Visible = (index == 1);
            labelFromTo.Visible = (index != 1);

            var EpgProgram = (EpgPrograms != null) ? EpgPrograms[EpgIndex] : null;

            buttonDetails.Enabled = DetailsButtonEnabled && (EpgProgram != null);
            if (EpgProgram == null)
            {
                labelProgramTitle.Text = Properties.Texts.EpgNoInformation;
                labelFromTo.Text = null;
                labelStartTime.Text = null;
                labelEllapsed.Text = null;
                EnableBackForward(false, false);
                return;
            } // if

            labelProgramTitle.Text = EpgProgram.Title;

            switch (EpgIndex)
            {
                case 0:
                    labelFromTo.Text = FormatString.DateTimeFromToMinutes(EpgProgram.LocalStartTime, EpgProgram.LocalEndTime, ReferenceTime);
                    ellapsed = (ReferenceTime - EpgProgram.LocalEndTime);
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramEnded, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(false, EpgPrograms[1] != null);
                    break;
                case 1:
                    labelStartTime.Text = string.Format("{0:HH:mm}", EpgProgram.LocalStartTime);
                    labelEndTime.Text = string.Format("{0:t}", EpgProgram.LocalEndTime);
                    ellapsed = (ReferenceTime - EpgProgram.LocalStartTime);
                    epgProgressBar.MaximumValue = EpgProgram.Duration.TotalMinutes;
                    epgProgressBar.Value = ellapsed.TotalMinutes;
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramStarted, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(EpgPrograms[0] != null, EpgPrograms[2] != null);
                    break;
                default:
                    labelFromTo.Text = FormatString.DateTimeFromToMinutes(EpgProgram.LocalStartTime, EpgProgram.LocalEndTime, ReferenceTime);
                    ellapsed = (EpgProgram.LocalStartTime - ReferenceTime);
                    labelEllapsed.Text = string.Format(Properties.Texts.ProgramWillStart, FormatString.TimeSpanTotalMinutes(ellapsed, FormatString.Format.Extended));
                    EnableBackForward(EpgPrograms[1] != null, false);
                    break;
            } // switch
        } // DisplayEpgProgram

        private void EnableBackForward(bool back, bool forward)
        {
            buttonBack.Enabled = back;
            buttonForward.Enabled = forward;

            NavigationButtonsChanged?.Invoke(this, new EpgMiniBarNavigationButtonsChangedEventArgs(back, forward));
        } // EnableBackForward

        private void SetAutoRefreshTimer(bool enabled)
        {
            // ensure timer ticks at :01
            timerAutoRefresh.Interval = (61 - DateTime.Now.Second) * 1000;

            timerAutoRefresh.Enabled = enabled & AutoRefresh;
        } // SetAutoRefreshTimer
    } // class EpgMiniBar
} // namespace
