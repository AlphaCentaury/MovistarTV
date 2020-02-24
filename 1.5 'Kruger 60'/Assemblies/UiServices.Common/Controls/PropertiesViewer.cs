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

using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Controls;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace IpTviewr.UiServices.Common.Forms
{
    [DefaultEvent(nameof(PropertySelected))]

    public partial class PropertiesViewer : CommonBaseUserControl
    {
        private ICollection<Property> _properties;
        private int _propertyColumnDefaultWidth;
        private int _valueColumnDefaultWidth;

        public PropertiesViewer()
        {
            InitializeComponent();
            _propertyColumnDefaultWidth = ColumnProperty.Width;
            _valueColumnDefaultWidth = ColumnValue.Width;
        } // constructor

        public event EventHandler<PropertySelectedEventArgs> PropertySelected;

        [DefaultValue(null)]
        public ICollection<Property> Properties
        {
            get => _properties;
            set
            {
                if (ReferenceEquals(_properties, value)) return;

                _properties = value;
                listViewProperties.BeginUpdate();
                listViewProperties.Items.Clear();
                if (value != null)
                {
                    listViewProperties.Items.AddRange(GetItems());
                    if (AutoResizeColumns)
                    {
                        ColumnProperty.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        if (ColumnProperty.Width < _propertyColumnDefaultWidth) ColumnProperty.Width = _propertyColumnDefaultWidth;

                        ColumnValue.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        if (ColumnValue.Width < _valueColumnDefaultWidth) ColumnValue.Width = _valueColumnDefaultWidth;
                    } // if
                }
                else
                {
                    ColumnProperty.Width = _propertyColumnDefaultWidth;
                    ColumnValue.Width = _valueColumnDefaultWidth;
                } // if-else
                listViewProperties.EndUpdate();
            } // set
        } // Properties

        [DefaultValue(false)] public bool AutoResizeColumns { get; set; }

        private void listViewProperties_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (listViewProperties.SelectedItems.Count > 0) ? listViewProperties.SelectedItems[0] : null;
            if (item == null) return;

            PropertySelected?.Invoke(this, new PropertySelectedEventArgs
            {
                Name = item.Text,
                Value = item.UseItemStyleForSubItems? item.SubItems[1].Text : null,
                Index = item.Index
            });
        } // listViewProperties_SelectedIndexChanged

        private ListViewItem[] GetItems()
        {
            var items = new ListViewItem[_properties.Count];

            var index = 0;
            foreach (var property in _properties)
            {
                var item = new ListViewItem(property.Key ?? Common.Properties.PropertiesDialog.NameNull);
                if (property.Value != null)
                {
                    item.SubItems.Add(property.Value);
                }
                else
                {
                    item.SubItems.Add(Common.Properties.PropertiesDialog.ValueNull);
                    item.SubItems[1].ForeColor = SystemColors.InfoText;
                    item.SubItems[1].BackColor = SystemColors.Info;
                    item.UseItemStyleForSubItems = false;
                } // if-else

                items[index++] = item;
            } // foreach;

            return items;
        }  // GetItem

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

            Clipboard.SetText($"{selectedRow.SubItems[0].Text}\t{selectedRow.SubItems[1].Text}", TextDataFormat.UnicodeText);
        } // contextMenuListCopyRow_Click

        private void contextMenuListCopyAll_Click(object sender, EventArgs e)
        {
            var buffer = new StringBuilder();
            foreach (ListViewItem item in listViewProperties.Items)
            {
                buffer.AppendFormat("{0}\t{1}", item.SubItems[0].Text, item.SubItems[1].Text);
                buffer.AppendLine();
            } // foreach item

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyAll_Click

        #endregion
    } // class PropertiesDlg
} // namespace
