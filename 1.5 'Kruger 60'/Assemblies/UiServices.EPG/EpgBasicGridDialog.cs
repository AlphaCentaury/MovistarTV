// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.Services.SqlServerCE;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Discovery.BroadcastList;
using IpTviewr.Common;
using System.Threading;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgBasicGridDialog : CommonBaseForm
    {
        private IList<UiBroadcastService> ServicesList;
        private UiBroadcastService InitialService;
        private UiBroadcastService SelectedService;
        private IEpgLinkedList[] EpgPrograms;
        private EpgDataStore Datastore;
        private int SelectedRowIndex;
        private DateTime LocalReferenceTime;
        private bool IsGridReady;

        public static DialogResult ShowGrid(CommonBaseForm parentForm, IList<UiBroadcastService> list, UiBroadcastService currentService, EpgDataStore datastore)
        {
            using (var dialog = new EpgBasicGridDialog())
            {
                dialog.ServicesList = list;
                dialog.InitialService = currentService;
                dialog.Datastore = datastore;
                return dialog.ShowDialog(parentForm);
            } // using
        }  // ShowGrid

        public EpgBasicGridDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Epg;
        } // constructor

        #region Event handlers

        private void EpgBasicGridDialog_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            EpgPrograms = new IEpgLinkedList[ServicesList.Count];
            ChangeSelectedRow(-1);

            var workerOptions = new BackgroundWorkerOptions()
            {
                OutputData = EpgPrograms,
                BackgroundTask = AsyncGetEpgPrograms,
                AfterTask = FillGrid,
                AllowAutoClose = true,
                TaskDescription = Properties.Texts.EpgDataLoadingList,
                AllowCancelButton = true,
            };

            var result = BackgroundWorkerDialog.RunWorkerAsync(this, workerOptions);
            var close = false;
            if (workerOptions.OutputException != null)
            {
                HandleException(new ExceptionEventData(Properties.Texts.ObtainingListException, workerOptions.OutputException));
                close = true;
            } // if
            if (result != DialogResult.OK)
            {
                close = true;
            } // if

            if (close)
            {
                Visible = false;
                Close();
            } // if
        } // EpgBasicGridDialog_Load

        private void dataGridPrograms_SelectionChanged(object sender, EventArgs e)
        {
            if (!IsGridReady) return;

            var cell = (dataGridPrograms.SelectedCells.Count > 0) ? dataGridPrograms.SelectedCells[0] : null;
            if (cell == null)
            {
                ChangeSelectedRow(-1);
            }
            else
            {
                ChangeSelectedRow(cell.RowIndex);

                // don't allow to select the service
                if (cell.ColumnIndex == 0)
                {
                    var row = dataGridPrograms.Rows[SelectedRowIndex];
                    row.Cells[1].Selected = true;

                    // changing the selected cell will cause SelectionChanged to be fired
                    // thus making unnecesary to call DisplayProgramData()
                    return;
                } // if

                DisplayProgramData(cell.ColumnIndex);
            } // if-else
        } // dataGridPrograms_SelectionChanged

        #endregion

        private void FillGrid(BackgroundWorkerOptions options, IBackgroundWorkerDialog dialog)
        {
            if (dialog.QueryCancel()) return;
            dialog.SetProgressText("Filling the list...");

            var serviceRowIndex = -1;
            foreach (var service in ServicesList)
            {
                var name = UiBroadcastListManager.GetColumnData(service, UiBroadcastListColumn.NumberAndName);
                var rowIndex = dataGridPrograms.Rows.Add(name);

                if (service.Key == InitialService?.Key)
                {
                    serviceRowIndex = rowIndex;
                } // if

                // TODO: use ListManager view options for hidden and inactive programs (to show or no to show)
                if ((service.IsHidden) || (service.IsInactive))
                {
                    dataGridPrograms.Rows[rowIndex].DefaultCellStyle.ForeColor = SystemColors.GrayText;
                } // if
            } // foreach

            for (int index = 0; index < EpgPrograms.Length; index++)
            {
                int cellIndex;
                var row = dataGridPrograms.Rows[index];

                var node = EpgPrograms[index]?.Requested;
                cellIndex = 1;
                while ((node != null) && (cellIndex < 4))
                {
                    var cell = row.Cells[cellIndex];
                    cell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    cell.Value = node.Program.Title;
                    node = node.Next;
                    cellIndex++;
                } // while

                // mark remaining cells as empty
                for (; cellIndex < 4; cellIndex++)
                {
                    var cell = row.Cells[cellIndex];
                    cell.Style.ForeColor = SystemColors.GrayText;
                    cell.Value = Properties.Texts.EpgNoInformationShort;
                    cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                } // for cellIndex
            } // for index

            SelectedService = null;
            IsGridReady = true;

            if (serviceRowIndex >= 0)
            {
                dataGridPrograms.CurrentCell = dataGridPrograms.Rows[serviceRowIndex].Cells[1];
            }
            else
            {
                SelectedRowIndex = -1;
            } // if-else
        } // FillGrid

        private void AsyncGetEpgPrograms(BackgroundWorkerOptions options, IBackgroundWorkerDialog dialog)
        {
            IEpgLinkedList servicePrograms;

            dialog.SetProgressText("Requesting data for channels...");

            LocalReferenceTime = DateTime.Now;
            var programs = Datastore.GetAllPrograms(LocalReferenceTime, 0, 2);

            dialog.SetProgressText("Sorting information...");

            var index = -1;
            foreach (var service in ServicesList)
            {
                index++;

                // TODO: do NOT assume .imagenio.es
                var fullServiceName = service.ServiceName + ".imagenio.es";
                var fullAlternateServiceName = service.ReplacementService?.ServiceName + ".imagenio.es";

                if (!programs.TryGetValue(fullServiceName, out servicePrograms))
                {
                    if (!programs.TryGetValue(fullAlternateServiceName, out servicePrograms))
                    {
                        continue;
                    } // if
                } // if

                EpgPrograms[index] = servicePrograms;
            } // foreach
        }  // AsyncGetEpgPrograms

        private void DisplayProgramData(int cellIndex)
        {
            if (cellIndex < 1)
            {
                pictureProgramThumbnail.Visible = false;
                epgMiniGuide.Visible = false;
            }
            else
            {
                //pictureProgramThumbnail.SetImage(Properties.Resources.EpgLoadingProgramImage);
                pictureProgramThumbnail.Visible = true;

                // TODO: load program image (async)
                pictureProgramThumbnail.SetImage(Properties.Resources.EpgNoProgramImage);

                epgMiniGuide.Visible = true;
                epgMiniGuide.GoTo(cellIndex);
            } // if-else
        } // DisplayProgramData

        private void ChangeSelectedRow(int rowIndex)
        {
            if (rowIndex == SelectedRowIndex) return;
            SelectedRowIndex = rowIndex;
            SelectedService = (rowIndex >= 0)? ServicesList[rowIndex] : null;

            if (rowIndex == -1)
            {
                pictureProgramThumbnail.Visible = false;
                epgMiniGuide.Visible = false;
                return;
            } // if

            var epgPrograms = EpgPrograms[rowIndex];
            var singleServiceDatastore = new EpgSingleServiceDatastore(SelectedService.FullServiceName, EpgPrograms[SelectedRowIndex]);

            epgMiniGuide.LoadEpgPrograms(SelectedService, LocalReferenceTime, false);
            epgMiniGuide.SetEpgDataStore(singleServiceDatastore, false);
            epgMiniGuide.Visible = true;
        } // ChangeSelectedRow
    } // class EpgBasicGridDialog
} // namespace
