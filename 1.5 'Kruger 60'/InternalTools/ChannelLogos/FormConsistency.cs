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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Common;
using IpTviewr.Internal.Tools.UiFramework;
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public partial class FormConsistency : MdiRibbonChildForm
    {
        private ConsistencyCheck _currentCheck;
        private ConsistencyChecksData _data;

        public FormConsistency()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
            buttonRun.Enabled = false;
        } // constructor

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var baseFolder = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\.."));
            textBoxLogosFolder.Text = Path.Combine(baseFolder, @"Logos");
            checkLogosFolder.Checked = true;
        } // OnLoad

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (AppConfig.Current != null) return;

            RibbonMdiForm.SetStatusText("Loading configuration...");
            var result = AppConfig.Load(null, RibbonMdiForm.SetStatusText);
            if (result.IsError)
            {
                BaseProgram.HandleException(this, result.Caption, result.Message, result.InnerException);
            } // if
            RibbonMdiForm.SetStatusText("Configuration loaded");
        } // OnShown

        private void comboCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRun.Enabled = (comboCheck.SelectedIndex >=0) && !(comboCheck.SelectedItem.ToString().StartsWith("--"));
        } // comboCheck_SelectedIndexChanged

        private void buttonRun_Click(object sender, EventArgs e)
        {
            // init list
            var width = (listViewResults.Width - SystemInformation.VerticalScrollBarWidth - 4) / 3;
            listViewResults.BeginUpdate();
            listViewResults.Columns.Clear();
            listViewResults.Items.Clear();
            listViewResults.Columns.Add("Activity").Width = width;
            listViewResults.Columns.Add("Details").Width = width;
            listViewResults.Columns.Add("Data").Width = width;
            listViewResults.EndUpdate();

            if (_currentCheck != null)
            {
                _currentCheck.ProgressChanged -= Check_ProgressChanged;
            } // if

            var test = int.Parse(comboCheck.SelectedItem.ToString().Substring(0, 2));
            _currentCheck = test switch
            {
                1 => new ConsistencyCheckMissingServiceMappings(),
                2 => new ConsistencyCheckUnusedServiceMappingEntries(),
                11 => new ConsistencyCheckMissingLogoFiles(),
                12 => new ConsistencyCheckUnusedLogoFiles(),
                _ => (ConsistencyCheck)null
            };

            if (_currentCheck == null) return;

            _data ??= new ConsistencyChecksData(this, !checkBoxUseCache.Checked, checkLogosFolder.Checked ? textBoxLogosFolder.Text : null);
            buttonDiscardData.Enabled = true;

            _currentCheck.ProgressChanged += Check_ProgressChanged;
            _currentCheck.Execute(_data);
            ShowResults(_currentCheck);
        } // buttonRun_Click

        private void buttonDiscardData_Click(object sender, EventArgs e)
        {
            _data = null;
            buttonDiscardData.Enabled = false;
        } // buttonDiscardData_Click

        private void contextMenuListView_Opening(object sender, CancelEventArgs e)
        {
            var selectedItem = (listViewResults.SelectedIndices.Count == 0) ? null : listViewResults.SelectedItems[0];
            if ((selectedItem == null) || (selectedItem.SubItems.Count == 0))
            {
                menuItemListContextCopyActivity.Enabled = false;
                menuItemListContextCopyFirstDetail.Enabled = false;
                menuItemListContextCopyRow.Enabled = false;
                menuItemListContextCopyMappingEntry.Enabled = false;
                return;
            } // if

            menuItemListContextCopyActivity.Enabled = true;
            menuItemListContextCopyFirstDetail.Enabled = (selectedItem.SubItems.Count > 1);
            menuItemListContextCopyRow.Enabled = true;
            menuItemListContextCopyMappingEntry.Enabled = _currentCheck is ConsistencyCheckMissingServiceMappings;
        } // contextMenuListView_Opening

        private void menuItemListContextCopyActivity_Click(object sender, EventArgs e)
        {
            CopySubItemToClipboard(listViewResults, 0);
        } // menuItemListContextCopyActivity_Click

        private void menuItemListContextCopyFirstDetail_Click(object sender, EventArgs e)
        {
            CopySubItemToClipboard(listViewResults, 1);
        } // menuItemListContextCopyFirstDetail_Click

        private static void CopySubItemToClipboard(ListView list, int index)
        {
            var selectedItem = (list.SelectedIndices.Count == 0) ? null : list.SelectedItems[0];
            if (selectedItem == null) return;

            if (index > selectedItem.SubItems.Count) return;

            Clipboard.SetText(selectedItem.SubItems[index].Text);
        } // CopySubItemToClipboard

        private void menuItemListContextCopyRow_Click(object sender, EventArgs e)
        {
            if (listViewResults.SelectedIndices.Count == 0) return;
            var selectedItem = listViewResults.SelectedItems[0];

            var result = new StringBuilder();
            result.Append(selectedItem.ImageKey);
            foreach (ListViewItem.ListViewSubItem subItem in selectedItem.SubItems)
            {
                result.Append('\t');
                result.Append(subItem);
            } // foreach subItem

            Clipboard.SetText(result.ToString());
        } // menuItemListContextCopyRow_Click

        private void menuItemListContextCopyMappingEntry_Click(object sender, EventArgs e)
        {
            if (listViewResults.SelectedIndices.Count == 0) return;
            var selectedItem = listViewResults.SelectedItems[0];

            var entry = $"<Mapping serviceName=\"{selectedItem.SubItems[1].Text}\" logo=\"***\" remarks=\"{selectedItem.SubItems[2].Text}\" />";
            Clipboard.SetText(entry);
        } // menuItemListContextCopyMappingEntry_Click

        private void menuItemListContextCopyMissingEntries_Click(object sender, EventArgs e)
        {
            var entries = new StringBuilder();
            foreach (var item in listViewResults.Items.Cast<ListViewItem>())
            {
                if (item.Text != "Missing logo") continue;

                entries.AppendFormat("<Mapping serviceName=\"{0}\" logo=\"***\" remarks=\"{1}\" />", item.SubItems[1].Text, item.SubItems[2].Text);
                entries.AppendLine();
            } // foreach

            Clipboard.SetText(entries.ToString());
        } // menuItemListContextCopyMissingEntries_Click


        private void LoadDisplayProgress(string text)
        {
            labelStatus.Text = text;
            labelStatus.GetCurrentParent().Refresh();
        } // LoadDisplayProgress

        private void Check_ProgressChanged(object sender, ConsistencyCheck.ProgressChangedEventArgs e)
        {
            LoadDisplayProgress(e.Messages[0]);

            var item = new ListViewItem(e.Messages)
            {
                ImageKey = ConsistencyCheck.Severity.Info.ToString()
            };
            listViewResults.Items.Add(item);
            item.EnsureVisible();
        } // Check_ProgressChanged

        private void ShowResults(ConsistencyCheck consistencyCheck)
        {
            listViewResults.BeginUpdate();
            listViewResults.Columns.Clear();
            listViewResults.Items.Clear();

            if (consistencyCheck.Results.Count > 0)
            {

                var maxColumns = consistencyCheck.Results.Select(result => result.Data.Length).Max();

                foreach (var result in consistencyCheck.Results)
                {
                    var item = new ListViewItem(result.Data)
                    {
                        ImageKey = result.Severity.ToString()
                    };
                    for (var missing = 0; missing < maxColumns - result.Data.Length; missing++)
                    {
                        item.SubItems.Add((string)null);
                    } // for missing

                    item.SubItems.Add((result.Timestamp - consistencyCheck.StartTime).ToString());
                    listViewResults.Items.Add(item);
                } // foreach result

                listViewResults.Columns.Add("Activity").AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                for (var i = 1; i < maxColumns; i++)
                {
                    listViewResults.Columns.Add("Details").AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                } // for i

                listViewResults.Columns.Add("Elapsed").AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            } // if

            listViewResults.EndUpdate();
        } // ShowResults

        #region Implementation of IRibbonMdiChild

        public override Guid TypeGuid => Guid.Parse(LogosConsistenceTool.ToolGuid);

        #endregion

        private void checkLogosFolder_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLogosFolder.Enabled = checkLogosFolder.Checked;
        } // checkLogosFolder_CheckedChanged
    } // class FormConsistency
} // namespace
