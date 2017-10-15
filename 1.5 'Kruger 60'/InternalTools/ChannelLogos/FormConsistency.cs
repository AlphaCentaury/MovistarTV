// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public partial class FormConsistency : Form
    {
        public FormConsistency()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
            buttonRun.Enabled = false;
        } // constructor

        private void comboCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonRun.Enabled = !(comboCheck.SelectedItem.ToString() ?? "").StartsWith("--");
        } // comboCheck_SelectedIndexChanged

        private void buttonRun_Click(object sender, EventArgs e)
        {
            ConsistencyCheck check;

            // init list
            var maxColumns = 3;
            listViewResults.Items.Clear();
            var width = (listViewResults.Width - SystemInformation.VerticalScrollBarWidth - 4) / maxColumns;
            listViewResults.Columns.Add("Activity").Width = width;
            for (int i = 1; i < maxColumns; i++)
            {
                listViewResults.Columns.Add("Details").AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            } // for i

            var test = int.Parse(comboCheck.SelectedItem.ToString().Substring(0, 2));
            switch (test)
            {
                case 1: check = new ConsistencyCheckMissingServiceLogos(); break;
                case 2: check = new ConsistencyCheckUnusedServiceMappingEntries(); break;
                case 3: check = new ConsistencyCheckMissingLogoFiles(); break;
                case 4: check = new ConsistencyCheckUnusedLogoFiles(); break;
                default:
                    return;
            } // switch

            check.ProgressChanged += Check_ProgressChanged;
            check.Execute(this);
            ShowResults(check);
        } // buttonRun_Click

        private void contextMenuListView_Opening(object sender, CancelEventArgs e)
        {
            var selectedItem = (listViewResults.SelectedIndices.Count == 0)? null : listViewResults.SelectedItems[0];
            if ((selectedItem == null) || (selectedItem.SubItems.Count == 0))
            {
                menuItemListContextCopyActivity.Enabled = false;
                menuItemListContextCopyFirstDetail.Enabled = false;
                menuItemListContextCopyRow.Enabled = false;
                return;
            } // if

            menuItemListContextCopyActivity.Enabled = true;
            menuItemListContextCopyFirstDetail.Enabled = (selectedItem.SubItems.Count > 1);
            menuItemListContextCopyRow.Enabled = true;
        } // contextMenuListView_Opening

        private void menuItemListContextCopyActivity_Click(object sender, EventArgs e)
        {
            CopySubitemToClipboard(listViewResults, 0);
        } // menuItemListContextCopyActivity_Click

        private void menuItemListContextCopyFirstDetail_Click(object sender, EventArgs e)
        {
            CopySubitemToClipboard(listViewResults, 1);
        } // menuItemListContextCopyFirstDetail_Click

        private bool CopySubitemToClipboard(ListView list, int index)
        {
            var selectedItem = (list.SelectedIndices.Count == 0) ? null : list.SelectedItems[0];
            if (selectedItem == null) return false;

            if (index > selectedItem.SubItems.Count) return false;

            Clipboard.SetText(selectedItem.SubItems[index].Text);
            return true;
        } // CopySubitemToClipboard

        private void menuItemListContextCopyRow_Click(object sender, EventArgs e)
        {
            if (listViewResults.SelectedIndices.Count == 0) return;
            var selectedItem = listViewResults.SelectedItems[0];

            var result = new StringBuilder();
            result.Append(selectedItem.ImageKey);
            foreach (var subitem in selectedItem.SubItems)
            {
                result.Append('\t');
                result.Append(subitem);
            } // foreach subitem

            Clipboard.SetText(result.ToString());
        } // menuItemListContextCopyRow_Click

        private void LoadDisplayProgress(string text)
        {
            labelStatus.Text = text;
            labelStatus.GetCurrentParent().Refresh();
        } // LoadDisplayProgress

        private void Check_ProgressChanged(object sender, ConsistencyCheck.ProgressChangedEventArgs e)
        {
            LoadDisplayProgress(e.Messages[0]);

            var item = new ListViewItem(e.Messages);
            item.ImageKey = ConsistencyCheck.Severity.Info.ToString();
            listViewResults.Items.Add(item);
            item.EnsureVisible();
        } // Check_ProgressChanged

        private void ShowResults(ConsistencyCheck consistencyCheck)
        {
            int maxColumns = 0;

            listViewResults.BeginUpdate();
            listViewResults.Columns.Clear();
            listViewResults.Items.Clear();

            foreach (var result in consistencyCheck.Results)
            {
                maxColumns = Math.Max(result.Data.Length, maxColumns);
            } // foreach result

            foreach (var result in consistencyCheck.Results)
            {
                var item = new ListViewItem(result.Data);
                item.ImageKey = result.Severity.ToString();
                for (int missing = 0; missing < maxColumns - result.Data.Length; missing++)
                {
                    item.SubItems.Add((string)null);
                } // for missing
                item.SubItems.Add((result.Timestamp - consistencyCheck.StartTime).ToString());
                listViewResults.Items.Add(item);
            } // foreach result

            listViewResults.Columns.Add("Activity").AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            for (int i = 1; i < maxColumns; i++)
            {
                listViewResults.Columns.Add("Details").AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            } // for i
            listViewResults.Columns.Add("Ellapsed").AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            listViewResults.EndUpdate();
        } // ShowResults
    } // class FormConsistency
} // namespace
