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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Common.Telemetry;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Discovery.BroadcastList;
using IpTviewr.Common;

namespace IpTviewr.UiServices.EPG
{
    public partial class EpgBasicGridDialog : CommonBaseForm
    {
        private IList<UiBroadcastService> _servicesList;
        private UiBroadcastService _initialService;
        private UiBroadcastService _selectedService;
        private IEpgLinkedList[] _epgPrograms;
        private EpgDataStore _datastore;
        private int _selectedRowIndex;
        private DateTime _localReferenceTime;
        private bool _isGridReady;

        public static DialogResult ShowGrid(CommonBaseForm parentForm, IList<UiBroadcastService> list, UiBroadcastService currentService, EpgDataStore datastore)
        {
            using (var dialog = new EpgBasicGridDialog())
            {
                dialog._servicesList = list;
                dialog._initialService = currentService;
                dialog._datastore = datastore;
                return dialog.ShowDialog(parentForm);
            } // using
        }  // ShowGrid

        public EpgBasicGridDialog()
        {
            InitializeComponent();
            Icon = Properties.Resources.Epg;
        } // constructor

        #region Event handlers

        private void EpgBasicGridDialog_Load(object sender, EventArgs e)
        {
            _epgPrograms = new IEpgLinkedList[_servicesList.Count];
            ChangeSelectedRow(-1);

            var workerOptions = new BackgroundWorkerOptions()
            {
                OutputData = _epgPrograms,
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
            if (!_isGridReady) return;

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
                    var row = dataGridPrograms.Rows[_selectedRowIndex];
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
            foreach (var service in _servicesList)
            {
                var name = UiBroadcastListManager.GetColumnData(service, UiBroadcastListColumn.NumberAndName);
                var rowIndex = dataGridPrograms.Rows.Add(name);

                if (service.Key == _initialService?.Key)
                {
                    serviceRowIndex = rowIndex;
                } // if

                // TODO: use ListManager view options for hidden and inactive programs (to show or no to show)
                if ((service.IsHidden) || (service.IsInactive))
                {
                    dataGridPrograms.Rows[rowIndex].DefaultCellStyle.ForeColor = SystemColors.GrayText;
                } // if
            } // foreach

            for (var index = 0; index < _epgPrograms.Length; index++)
            {
                int cellIndex;
                var row = dataGridPrograms.Rows[index];

                var node = _epgPrograms[index]?.Requested;
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

            _selectedService = null;
            _isGridReady = true;

            if (serviceRowIndex >= 0)
            {
                dataGridPrograms.CurrentCell = dataGridPrograms.Rows[serviceRowIndex].Cells[1];
            }
            else
            {
                _selectedRowIndex = -1;
            } // if-else
        } // FillGrid

        private void AsyncGetEpgPrograms(BackgroundWorkerOptions options, IBackgroundWorkerDialog dialog)
        {
            IEpgLinkedList servicePrograms;

            dialog.SetProgressText("Requesting data for channels...");

            _localReferenceTime = DateTime.Now;
            var programs = _datastore.GetAllPrograms(_localReferenceTime, 0, 2);

            dialog.SetProgressText("Sorting information...");

            var index = -1;
            foreach (var service in _servicesList)
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

                _epgPrograms[index] = servicePrograms;
            } // foreach
        }  // AsyncGetEpgPrograms

        private void DisplayProgramData(int cellIndex)
        {
            if (cellIndex < 1)
            {
                epgMiniGuide.Visible = false;
            }
            else
            {
                epgMiniGuide.Visible = true;
                epgMiniGuide.GoTo(cellIndex);
            } // if-else
        } // DisplayProgramData

        private void ChangeSelectedRow(int rowIndex)
        {
            if (rowIndex == _selectedRowIndex) return;
            _selectedRowIndex = rowIndex;
            _selectedService = (rowIndex >= 0)? _servicesList[rowIndex] : null;

            if (rowIndex == -1)
            {
                epgMiniGuide.Visible = false;
                return;
            } // if

            var epgPrograms = _epgPrograms[rowIndex];
            var singleServiceDataStore = new EpgSingleServiceDataStore(_selectedService.FullServiceName, epgPrograms);

            epgMiniGuide.LoadEpgPrograms(_selectedService, _localReferenceTime, false);
            epgMiniGuide.SetEpgDataStore(singleServiceDataStore, false);
            epgMiniGuide.Visible = true;
        } // ChangeSelectedRow
    } // class EpgBasicGridDialog
} // namespace
