// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using IpTviewr.Services.EPG;
using IpTviewr.Services.EPG.Serialization;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.Core.IpTvProvider;

namespace IpTviewr.UiServices.EPG
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
            var epgProgram = (listPrograms.SelectedItems.Count != 0) ? (EpgProgram)listPrograms.SelectedItems[0].Tag : null;
            EpgProgramDetails.Visible = (epgProgram != null);
            EpgProgramDetails.DisplayData(Service, epgProgram, ReferenceTime, "Programa");

            buttonDisplayChannel.Enabled = (epgProgram != null) && (epgProgram.LocalStartTime <= ReferenceTime) && (epgProgram.LocalEndTime > ReferenceTime);
            buttonRecordChannel.Enabled = (epgProgram != null) && (epgProgram.LocalEndTime >= ReferenceTime);
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
            EpgProgramDetails.Visible = false;

            ThreadPool.QueueUserWorkItem(LoadEpg);
        } // StartLoadEpg

        private void LoadEpg(object state)
        {
            ReferenceTime = DateTime.Now;
            var start = new DateTime(ReferenceTime.Year, ReferenceTime.Month, ReferenceTime.Day).AddDays(DaysDelta);
            var end = start.AddDays(1);
            // TODO: EPG
            //var epgPrograms = EpgDbSerialization.GetServiceEvents(EpgDatabase, FullServiceName, FullAlternateServiceName, start, end);
            //this.BeginInvoke(new Action<IList<EpgProgram>>(ShowEpg), epgPrograms);
        } // LoadEpg

        private void ShowEpg(IList<EpgProgram> epgPrograms)
        {
            EpgProgram last;
            ListViewItem item, select;

            last = null;
            select = null;

            listPrograms.BeginUpdate();
            listPrograms.Items.Clear();

            foreach (var epgProgram in epgPrograms)
            {
                if ((last != null) && (last.LocalEndTime != epgProgram.LocalStartTime))
                {
                    item = new ListViewItem(last.LocalEndTime.ToShortTimeString());
                    item.UseItemStyleForSubItems = false;
                    item.SubItems.Add("Información de programa no disponible");
                    item.SubItems[1].Font = ItalicListFont;
                }
                else
                {
                    item = new ListViewItem(epgProgram.LocalStartTime.ToShortTimeString());
                    item.UseItemStyleForSubItems = false;
                    item.Font = BoldListFont;
                    item.Tag = epgProgram;
                    item.SubItems.Add(epgProgram.Title);
                    item.ToolTipText = epgProgram.Title;
                } // if-else
                listPrograms.Items.Add(item);
                last = epgProgram;

                if ((select == null) && (epgProgram.LocalStartTime <= ReferenceTime) && (epgProgram.LocalEndTime > ReferenceTime))
                {
                    select = item;
                } // if
            } // foreach

            if (epgPrograms.Count == 0)
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
