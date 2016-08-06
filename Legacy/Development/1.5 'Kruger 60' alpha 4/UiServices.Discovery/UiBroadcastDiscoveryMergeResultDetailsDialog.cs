// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Discovery
{
    public partial class UiBroadcastDiscoveryMergeResultDetailsDialog : Form
    {
        public UiBroadcastDiscoveryMergeResultDetailsDialog()
        {
            InitializeComponent();
        } // constructor

        public UiBroadcastDiscoveryMergeResult MergeResult
        {
            get;
            set;
        } // MergeResult

        private void UiBroadcastDiscoveryMergeResultDetailsDialog_Load(object sender, EventArgs e)
        {
            List<ListViewItem> items;

            if (MergeResult == null) return;

            items = new List<ListViewItem>();
            AddServices(items, MergeResult.NewServices, listViewDetails.Groups["Added"]);
            AddServices(items, MergeResult.RemovedServices, listViewDetails.Groups["Removed"]);
            AddServices(items, MergeResult.ChangedServices, listViewDetails.Groups["Changed"]);

            if (items.Count > 0)
            {
                var q = from item in items
                        orderby item.Text
                        select item;

                listViewDetails.BeginUpdate();
                listViewDetails.Items.AddRange(q.ToArray());
                listViewDetails.EndUpdate();
            }
            else
            {
                listViewDetails.ShowGroups = false;
                listViewDetails.Items.Add(Properties.Texts.MergeResultDetailsNoData);
                buttonCopy.Enabled = false;
            } // if-else
        } // UiBroadcastDiscoveryMergeResultDetailsDialog_Load

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            var output = new StringBuilder();

            output.AppendLine("Display name\tService Name\tStatus");

            for (int index = 0; index < listViewDetails.Items.Count; index++)
            {
                var item = listViewDetails.Items[index];
                output.Append(item.SubItems[0].Text);
                output.Append('\t');
                output.Append(item.SubItems[1].Text);
                output.Append('\t');
                output.Append(item.Group.Name);
                output.AppendLine();
            } // for

            Clipboard.SetText(output.ToString());
        } // buttonCopy_Click

        private void AddServices(List<ListViewItem> items, IList<UiBroadcastService> services, ListViewGroup group)
        {
            if ((services == null) || (services.Count == 0)) return;

            foreach(var service in services)
            {
                var item = new ListViewItem(service.DisplayName);
                item.Group = group;
                item.SubItems.Add(service.ServiceName);

                items.Add(item);
            } // foreach service
        } // AddServices
    } // class UiBroadcastDiscoveryMergeResultDetailsDialog
} // namespace
