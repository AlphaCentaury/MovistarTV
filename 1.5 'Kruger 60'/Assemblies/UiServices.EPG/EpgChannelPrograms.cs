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

using IpTviewr.Core;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgChannelPrograms : CommonBaseForm
    {
        DateTime _referenceTime;
        private Font _boldListFont, _italicListFont;

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

            _boldListFont?.Dispose();
            _italicListFont?.Dispose();
        } // DisposeForm

        private void EpgChannelPrograms_Load(object sender, EventArgs e)
        {
            pictureChannelLogo.Image = Service.Logo.GetImage(LogoSize.Size48);
            labelChannelName.Text = $"{Service.DisplayLogicalNumber} - {Service.DisplayName}";

            _boldListFont = new Font(listPrograms.Font, FontStyle.Bold);
            _italicListFont = new Font(listPrograms.Font, FontStyle.Italic);

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
            EpgProgramDetails.DisplayData(Service, epgProgram, _referenceTime, "Programa");

            buttonDisplayChannel.Enabled = (epgProgram != null) && (epgProgram.LocalStartTime <= _referenceTime) && (epgProgram.LocalEndTime > _referenceTime);
            buttonRecordChannel.Enabled = (epgProgram != null) && (epgProgram.LocalEndTime >= _referenceTime);
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
            _referenceTime = DateTime.Now;
            var start = new DateTime(_referenceTime.Year, _referenceTime.Month, _referenceTime.Day).AddDays(DaysDelta);
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
                    item = new ListViewItem(last.LocalEndTime.ToShortTimeString())
                    {
                        UseItemStyleForSubItems = false
                    };
                    item.SubItems.Add("Información de programa no disponible");
                    item.SubItems[1].Font = _italicListFont;
                }
                else
                {
                    item = new ListViewItem(epgProgram.LocalStartTime.ToShortTimeString())
                    {
                        UseItemStyleForSubItems = false,
                        Font = _boldListFont,
                        Tag = epgProgram
                    };
                    item.SubItems.Add(epgProgram.Title);
                    item.ToolTipText = epgProgram.Title;
                } // if-else
                listPrograms.Items.Add(item);
                last = epgProgram;

                if ((select == null) && (epgProgram.LocalStartTime <= _referenceTime) && (epgProgram.LocalEndTime > _referenceTime))
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
