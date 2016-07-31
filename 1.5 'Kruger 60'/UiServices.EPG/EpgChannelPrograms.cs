// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using Project.DvbIpTv.Common.Telemetry;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Services.EPG.Serialization;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Project.DvbIpTv.UiServices.Common.Forms;
using Project.DvbIpTv.Core.IpTvProvider;

namespace Project.DvbIpTv.UiServices.EPG
{
    public partial class EpgChannelPrograms : Form
    {
        DateTime ReferenceTime;
        private Font BoldListFont, ItalicListFont;

        public int DaysDelta
        {
            get;
            set;
        } // DaysDelta

        public UiBroadcastService Service
        {
            get;
            set;
        } // ChannelLog

        public string FullServiceName
        {
            get;
            set;
        } // FullServiceName

        public string FullAlternateServiceName
        {
            get;
            set;
        } // FullAlternateServiceName

        public string EpgDatabase
        {
            get;
            set;
        } // EpgDatabase

        public EpgChannelPrograms()
        {
            InitializeComponent();
        } // constructor

        private void DisposeForm(bool disposing)
        {
            if (!disposing) return;

            if (BoldListFont != null) BoldListFont.Dispose();
            if (ItalicListFont != null) ItalicListFont.Dispose();
        } // DisposeForm

        private void EpgChannelPrograms_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            pictureChannelLogo.Image = Service.Logo.GetImage(LogoSize.Size48, true);
            labelChannelName.Text = string.Format("{0} - {1}", Service.DisplayLogicalNumber, Service.DisplayName);

            BoldListFont = new Font(listPrograms.Font, FontStyle.Bold);
            ItalicListFont = new Font(listPrograms.Font, FontStyle.Italic);

            comboBoxDate.SelectedIndex = DaysDelta;
            buttonDisplayChannel.Enabled = false;
            buttonRecordChannel.Enabled = false;
        } // EpgChannelPrograms_Load

        private void comboBoxDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            DaysDelta = comboBoxDate.SelectedIndex;
            StartLoadEpg();
        } // comboBoxDate_SelectedIndexChanged

        private void listPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            var epgEvent = (listPrograms.SelectedItems.Count != 0) ? (EpgEvent)listPrograms.SelectedItems[0].Tag : null;
            epgEventDetails.Visible = (epgEvent != null);
            epgEventDetails.DisplayData(Service, epgEvent, ReferenceTime, "Programa");

            buttonDisplayChannel.Enabled = (epgEvent != null) && (epgEvent.LocalStartTime <= ReferenceTime) && (epgEvent.LocalEndTime > ReferenceTime);
            buttonRecordChannel.Enabled = (epgEvent != null) && (epgEvent.LocalEndTime >= ReferenceTime);
        } // listPrograms_SelectedIndexChanged

        private void buttonDisplayChannel_Click(object sender, EventArgs e)
        {
            ExternalTvPlayer.ShowTvChannel(this, Service);
        } // buttonDisplayChannel_Click

        private void buttonRecordChannel_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "buttonRecordChannel");
        } // buttonRecordChannel_Click

        private void StartLoadEpg()
        {
            if (listPrograms.Items.Count > 0)
            {
                listPrograms.Items.Clear();
                var item = listPrograms.Items.Add("");
                item.SubItems.Add("Leyendo información de guía de programas...");
            } // if

            comboBoxDate.Enabled = false;
            epgEventDetails.Visible = false;

            ThreadPool.QueueUserWorkItem(LoadEpg);
        } // StartLoadEpg

        private void LoadEpg(object state)
        {
            ReferenceTime = DateTime.Now;
            var start = new DateTime(ReferenceTime.Year, ReferenceTime.Month, ReferenceTime.Day).AddDays(DaysDelta);
            var end = start.AddDays(1);
            var epgEvents = EpgDbSerialization.GetServiceEvents(EpgDatabase, FullServiceName, FullAlternateServiceName, start, end);
            this.BeginInvoke(new Action<IList<EpgEvent>>(ShowEpg), epgEvents);
        } // LoadEpg

        private void ShowEpg(IList<EpgEvent> epgEvents)
        {
            EpgEvent last;
            ListViewItem item, select;

            last = null;
            select = null;

            listPrograms.BeginUpdate();
            listPrograms.Items.Clear();

            foreach (var epgEvent in epgEvents)
            {
                if ((last != null) && (last.LocalEndTime != epgEvent.LocalStartTime))
                {
                    item = new ListViewItem(last.LocalEndTime.ToShortTimeString());
                    item.UseItemStyleForSubItems = false;
                    item.SubItems.Add("Información de programa no disponible");
                    item.SubItems[1].Font = ItalicListFont;
                }
                else
                {
                    item = new ListViewItem(epgEvent.LocalStartTime.ToShortTimeString());
                    item.UseItemStyleForSubItems = false;
                    item.Font = BoldListFont;
                    item.Tag = epgEvent;
                    item.SubItems.Add(epgEvent.Title);
                    item.ToolTipText = epgEvent.Title;
                } // if-else
                listPrograms.Items.Add(item);
                last = epgEvent;

                if ((select == null) && (epgEvent.LocalStartTime <= ReferenceTime) && (epgEvent.LocalEndTime > ReferenceTime))
                {
                    select = item;
                } // if
            } // foreach

            if (epgEvents.Count == 0)
            {
                item = new ListViewItem("--:--");
                item.SubItems.Add("Información de programas no disponible para este canal");
                item.UseItemStyleForSubItems = false;
                listPrograms.Items.Add(item);
            } // if
            columnHeaderTitle.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (select != null)
            {
                select.Selected = (DaysDelta == 0);
                select.EnsureVisible();
                select.IndentCount = 3;
                listPrograms.Focus();
            } // if

            listPrograms.EndUpdate();

            comboBoxDate.Enabled = true;
        } // ShowEpg
    } // class EpgChannelPrograms
} // namespace
