// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Common.Telemetry;
using IpTviewr.Core.IpTvProvider;
using IpTviewr.Services.EPG;
using IpTviewr.Services.EPG.Serialization;
using IpTviewr.Services.SqlServerCE;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Discovery.BroadcastList;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgBasicGridDialog : Form
    {
        private IList<UiBroadcastService> ServicesList;
        private UiBroadcastService CurrentService;
        private UiBroadcastService SelectedService;
        private EpgProgram[,] EpgPrograms;
        private int CurrentRowIndex, SelectedRowIndex;
        private DateTime ReferenceTime;

        public EpgBasicGridDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Epg;
        } // constructor

        public static DialogResult ShowGrid(IWin32Window owner, IList<UiBroadcastService> list, UiBroadcastService currentService)
        {
            using (var dialog = new EpgBasicGridDialog())
            {
                dialog.ServicesList = list;
                dialog.CurrentService = currentService;
                return dialog.ShowDialog(owner);
            } // using
        }  // ShowGrid

        private void EpgBasicGridDialog_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            CurrentRowIndex = -1;
            foreach (var service in ServicesList)
            {
                var name = UiBroadcastListManager.GetColumnData(service, UiBroadcastListColumn.NumberAndName);
                var rowIndex = dataGridPrograms.Rows.Add(name);

                if ((CurrentService != null) && (service.Key == CurrentService.Key))
                {
                    CurrentRowIndex = rowIndex;
                } // if

                if ((service.IsHidden) || (service.IsInactive))
                {
                    dataGridPrograms.Rows[rowIndex].DefaultCellStyle.ForeColor = SystemColors.GrayText;
                } // if
            } // foreach

            EpgPrograms = new EpgProgram[ServicesList.Count, 3];

            EpgProgramDisplay.Visible = false;
            buttonDisplayChannel.Enabled = false;
            buttonRecordChannel.Enabled = false;
        } // EpgBasicGridDialog_Load

        private void EpgBasicGridDialog_Shown(object sender, EventArgs e)
        {
            var workerOptions = new BackgroundWorkerOptions()
            {
                OutputData = EpgPrograms,
                BackgroundTask = AsyncBuildList,
                AllowAutoClose = true,
                TaskDescription = Properties.Texts.EpgDataLoadingList,
                AllowCancelButton = true,
            };
            if (BackgroundWorkerDialog.RunWorkerAsync(this, workerOptions) != DialogResult.OK)
            {
                return;
            } // if

            // TODO: implement HandleException
            /*
            if (workerOptions.OutputException != null)
            {
                HandleException(TasksTexts.ObtainingListException, workerOptions.OutputException);
                return;
            } // if
            */

            for (int index = 0; index < EpgPrograms.GetLength(0); index++)
            {
                var row = dataGridPrograms.Rows[index];
                for (int cellIndex = 0; cellIndex < 3; cellIndex++)
                {
                    var epgProgram = EpgPrograms[index, cellIndex];
                    var cell = row.Cells[cellIndex + 1];
                    if (epgProgram != null)
                    {
                        cell.Value = epgProgram.Title;
                    }
                    else
                    {
                        cell.Style.BackColor = SystemColors.Control;
                        cell.Value = Properties.Texts.EpgNoInformation;
                        cell.ErrorText = Properties.Texts.EpgNoData;
                    } // if-else
                } // for cellIndex
            } // for index

            SelectedService = null;
            if (CurrentRowIndex >= 0)
            {
                dataGridPrograms.CurrentCell = dataGridPrograms.Rows[CurrentRowIndex].Cells[1];
            } // if
        } // EpgBasicGridDialog_Shown

        private void AsyncBuildList(BackgroundWorkerOptions options, IBackgroundWorkerDialog dialog)
        {
            var result = options.OutputData as EpgProgram[,];

            using (var cn = DbServices.GetConnection(null)) // TODO: EPG AppUiConfiguration.Current.EpgDatabaseFile))
            {
                var serviceEvents = new Dictionary<string, EpgProgram[]>(ServicesList.Count, StringComparer.InvariantCultureIgnoreCase);
                foreach (var service in ServicesList)
                {
                    // TODO: do not assume imagenio.es
                    serviceEvents[service.ServiceName + ".imagenio.es"] = null;
                } // foreach

                var now = DateTime.Now;
                ReferenceTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                // TODO: EPG
                /*
                var events = EpgDbQuery.GetAllServicesNowEvent(cn, ReferenceTime);

                foreach (var epgServiceEvent in events)
                {
                    var epgPrograms = new EpgProgram[3];
                    epgPrograms[0] = epgServiceEvent.EpgProgram;
                    serviceEvents[epgServiceEvent.FullServiceName] = epgPrograms;
                } // foreach

                foreach (var serviceName in serviceEvents.Keys.ToList())
                {
                    EpgProgram[] epgPrograms;
                    DateTime start;

                    if (dialog.QueryCancel()) return;

                    var serviceDbId = EpgDbQuery.GetDatabaseIdForServiceId(serviceName, cn);
                    if (serviceDbId <= 0) continue;

                    EpgPrograms = serviceEvents[serviceName];
                    start = (epgPrograms != null) ? start = EpgPrograms[0].LocalEndTime : ReferenceTime;

                    var afterEvents = EpgDbQuery.GetDateRange(cn, serviceDbId, start, null, 2);
                    if (afterEvents.Count > 0)
                    {
                        if (EpgPrograms == null)
                        {
                            EpgPrograms = new EpgProgram[3];
                            serviceEvents[serviceName] = EpgPrograms;
                        } // if
                        for (int epgIndex = 0; epgIndex < afterEvents.Count; epgIndex++)
                        {
                            EpgPrograms[epgIndex + 1] = afterEvents[epgIndex];
                        } // for epgIndex
                    } // if
                } // foreach

                for (int index = 0; index < ServicesList.Count; index++)
                {
                    EpgProgram[] epgPrograms;

                    if (dialog.QueryCancel()) return;

                    // TODO: do not assume imagenio.es
                    var service = ServicesList[index];
                    EpgPrograms = serviceEvents[service.ServiceName + ".imagenio.es"];
                    if (EpgPrograms == null)
                    {
                        if (service.Data.ServiceInformation.ReplacementService != null)
                        {
                            foreach (var replacement in service.Data.ServiceInformation.ReplacementService)
                            {
                                if (replacement.Kind != "5") continue;
                                if (replacement.TextualIdentifier == null) continue;
                                if (serviceEvents.TryGetValue(replacement.TextualIdentifier.ServiceName + ".imagenio.es", out EpgPrograms))
                                {
                                    if (EpgPrograms != null)
                                    {
                                        break;
                                    } // if
                                } // if
                            } // foreach
                        } // if
                    } // if

                    if (EpgPrograms == null) continue;

                    for (int epgIndex = 0; epgIndex < EpgPrograms.Length; epgIndex++)
                    {
                        EpgPrograms[index, epgIndex] = EpgPrograms[epgIndex];
                    } // if
                } // for index
                */
            } // using cn
        }  // AsyncBuildList

        private void dataGridPrograms_SelectionChanged(object sender, EventArgs e)
        {
            EpgProgram epgProgram;
            int columnIndex;
            string caption;

            var cell = (dataGridPrograms.SelectedCells.Count > 0) ? dataGridPrograms.SelectedCells[0] : null;
            if (cell == null)
            {
                SelectedService = null;
                SelectedRowIndex = -1;
                columnIndex = -1;
                epgProgram = null;
            }
            else
            {
                SelectedRowIndex = cell.RowIndex;
                SelectedService = ServicesList[SelectedRowIndex];

                columnIndex = cell.ColumnIndex;
                epgProgram = (columnIndex > 0)? EpgPrograms[cell.RowIndex, columnIndex - 1] : null;

                switch (columnIndex)
                {
                    case 1: caption = Properties.Texts.EpgProgramNowCaption; break;
                    case 2: caption = Properties.Texts.EpgProgramThenCaption; break;
                    case 3: caption = Properties.Texts.EpgProgramAfterCaption; break;
                    default:
                        caption = null;
                        break;
                } // switch

                EpgProgramDisplay.DisplayData(ServicesList[SelectedRowIndex], epgProgram, ReferenceTime, caption);
            } // if-else

            EpgProgramDisplay.Visible = (columnIndex > 0);
            buttonDisplayChannel.Enabled = (columnIndex == 1);
            buttonRecordChannel.Enabled = (columnIndex >= 1) && (epgProgram != null);
        } // dataGridPrograms_SelectionChanged

        private void buttonDisplayChannel_Click(object sender, EventArgs e)
        {
            ExternalTvPlayer.ShowTvChannel(this, SelectedService);
        } // buttonDisplayChannel_Click

        private void buttonRecordChannel_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "buttonRecordChannel");
        } // buttonRecordChannel_Click
    } // class EpgBasicGridDialog
} // namespace
