// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

// v1.0 RC 0: Moved from ChannelList > PropertiesDlg.cs

using Project.DvbIpTv.UiServices.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace Project.DvbIpTv.UiServices.Forms
{
    public partial class PropertiesDialog : Form
    {
        public IEnumerable<Property> Properties { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public Image Logo { get; set; }

        public PropertiesDialog()
        {
            InitializeComponent();
        } // constructor

        private void PropertiesDialog_Load(object sender, EventArgs e)
        {
            this.Text = Caption;
            this.labelDescription.Text = (Description ?? Texts.PropertiesDlgCaptionDefault);
            this.pictureBoxEx1.Image = Logo;
        } // PropertiesDialog_Load

        private void PropertiesDialog_Shown(object sender, EventArgs e)
        {
            foreach (var property in Properties)
            {
                AddProperty(property.Key, property.Value);
            } // foreach
        } // PropertiesDialog_Shown

        private void AddProperty(string name, string value)
        {
            ListViewItem item;

            item = listViewProperties.Items.Add(name ?? Texts.PropertiesDlgNameNull);
            item.UseItemStyleForSubItems = false;
            if (value != null)
            {
                item.SubItems.Add(value);
            }
            else
            {
                item.SubItems.Add(Texts.PropertiesDlgValueNull);
                item.SubItems[1].BackColor = Color.LightYellow;
            } // if-else
        }  // AddProperty

        #region List context menu

        private void contextMenuList_Opening(object sender, CancelEventArgs e)
        {
            var selection = (listViewProperties.SelectedItems.Count > 0);
            contextMenuListCopyValue.Enabled = selection;
            contextMenuListCopyName.Enabled = selection;
            contextMenuListCopyRow.Enabled = selection;
        } // contextMenuList_Opening

        private void contextMenuListCopyValue_Click(object sender, EventArgs e)
        {
            var selectedRow = (listViewProperties.SelectedItems.Count > 0) ? listViewProperties.SelectedItems[0] : null;
            if (selectedRow == null) return;

            Clipboard.SetText(selectedRow.SubItems[1].Text, TextDataFormat.UnicodeText);
        } // contextMenuListCopyValue_Click

        private void contextMenuListCopyName_Click(object sender, EventArgs e)
        {
            var selectedRow = (listViewProperties.SelectedItems.Count > 0) ? listViewProperties.SelectedItems[0] : null;
            if (selectedRow == null) return;

            Clipboard.SetText(selectedRow.SubItems[0].Text, TextDataFormat.UnicodeText);
        } // contextMenuListCopyName_Click

        private void contextMenuListCopyRow_Click(object sender, EventArgs e)
        {
            var selectedRow = (listViewProperties.SelectedItems.Count > 0) ? listViewProperties.SelectedItems[0] : null;
            if (selectedRow == null) return;

            Clipboard.SetText(string.Format("{0}\t{1}", selectedRow.SubItems[0].Text, selectedRow.SubItems[1].Text), TextDataFormat.UnicodeText);
        } // contextMenuListCopyRow_Click

        private void contextMenuListCopyAll_Click(object sender, EventArgs e)
        {
            StringBuilder buffer;

            buffer = new StringBuilder();
            foreach (ListViewItem item in listViewProperties.Items)
            {
                buffer.AppendFormat("{0}\t{1}\r\n", item.SubItems[0].Text, item.SubItems[1].Text);
            } // foreach item

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyAll_Click

        #endregion
    } // class PropertiesDlg
} // namespace
