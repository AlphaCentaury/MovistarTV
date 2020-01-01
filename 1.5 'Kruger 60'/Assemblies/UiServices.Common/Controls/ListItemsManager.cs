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

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    public class ListItemsManager
    {
        private readonly ListBox _listBox;
        private readonly Control _removeControl;
        private readonly Control _upControl;
        private readonly Control _downControl;

        public ListItemsManager(ListBox listBox, Control removeControl, Control upControl, Control downControl)
        {
            if ((listBox == null) || (removeControl == null) || (upControl == null) || (downControl == null))
            {
                throw new ArgumentNullException();
            } // if

            if ((listBox.SelectionMode == SelectionMode.MultiSimple) || (listBox.SelectionMode == SelectionMode.MultiExtended))
            {
                throw new ArgumentException(nameof(listBox));
            } // if

            _listBox = listBox;
            _removeControl = removeControl;
            _upControl = upControl;
            _downControl = downControl;

            _removeControl.Enabled = false;
            _upControl.Enabled = false;
            _downControl.Enabled = false;

            _listBox.DisplayMember = "Value";
            _listBox.ValueMember = "Key";
            _listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        }  // constructor

        public bool IsReadOnly { get; set; }

        [PublicAPI]
        public int Add(string item)
        {
            var index = _listBox.Items.Add(item);
            _listBox.SelectedIndex = index;

            return index;
        } // Add

        [PublicAPI]
        public void Add(IEnumerable<string> items)
        {
            Add(items.ToArray());
        } // Add

        [PublicAPI]
        public void Add(IList<string> items)
        {
            var add = new object[items.Count];
            for (var index = 0; index < items.Count; index++)
            {
                add[index] = items[index];
            } // for
            AddItems(add);
        } // Add

        [PublicAPI]
        public void Add(string[] items)
        {
            var add = new object[items.Length];
            Array.Copy(items, add, items.Length);
            AddItems(add);
        } // Add

        [PublicAPI]
        public List<string> GetListItems()
        {
            return (from item in _listBox.Items.Cast<string>()
                    select item).ToList();
        } // GetListItems

        [PublicAPI]
        public void RemoveSelection()
        {
            if (IsReadOnly) return;

            var index = _listBox.SelectedIndex;
            if (index < 0) throw new InvalidOperationException();

            _listBox.Items.RemoveAt(index);

            if (index >= _listBox.Items.Count) index -= 1;
            _listBox.SelectedIndex = index;
        } // RemoveSelection

        [PublicAPI]
        public void MoveSelectionUp()
        {
            if (IsReadOnly) return;

            var index = _listBox.SelectedIndex;
            if (index < 0) throw new InvalidOperationException();

            var upItem = _listBox.Items[index - 1];
            var current = _listBox.Items[index];

            _listBox.Items[index - 1] = current;
            _listBox.Items[index] = upItem;

            _listBox.SelectedIndex = index - 1;
        } // MoveUp
        
        [PublicAPI]
        public void MoveSelectionDown()
        {
            if (IsReadOnly) return;

            var index = _listBox.SelectedIndex;
            if ((index + 1) >= _listBox.Items.Count) throw new InvalidOperationException();

            var current = _listBox.Items[index];
            var downItem = _listBox.Items[index + 1];

            _listBox.Items[index + 1] = current;
            _listBox.Items[index] = downItem;

            _listBox.SelectedIndex = index + 1;
        } // MoveDown

        [PublicAPI]
        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = _listBox.SelectedIndex;
            _removeControl.Enabled = (index >= 0) && (_listBox.Items.Count > 0) && (!IsReadOnly);
            _upControl.Enabled = (index > 0) && (_listBox.Items.Count > 1) && (!IsReadOnly);
            _downControl.Enabled = ((index + 1) < _listBox.Items.Count) && (!IsReadOnly);
        } // ListBox_SelectedIndexChanged

        private void AddItems(object[] items)
        {
            var index = _listBox.SelectedIndex;
            _listBox.Items.AddRange(items);
            _listBox.SelectedIndex = index;
        } // AddItems
    } // class ListItemsManager
} // namespace
