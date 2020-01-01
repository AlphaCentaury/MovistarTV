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
    public class ListItemsManager<TValue>
    {
        private readonly ListBox _listBox;
        private readonly Control _removeControl;
        private readonly Control _upControl;
        private readonly Control _downControl;
        private IDictionary<TValue, string> _dictionary;

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

        public void SetValueDictionary(IList<KeyValuePair<TValue, string>> items, IEqualityComparer<TValue> comparer)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            _dictionary = new Dictionary<TValue, string>(items.Count, comparer);
            foreach (var item in items)
            {
                _dictionary.Add(item);
            } // if
        } // SetValueDictionary

        [PublicAPI]
        public void SetItemsDictionary(IDictionary<TValue, string> dictionary)
        {
            _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
        } // SetItemsDictionary

        public int Add(TValue key, string text)
        {
            return Add(new KeyValuePair<TValue, string>(key, text));
        } // Add

        public int Add(KeyValuePair<TValue, string> item)
        {
            var index = _listBox.Items.Add(item);
            _listBox.SelectedIndex = index;

            return index;
        } // Add

        public void Add(IEnumerable<KeyValuePair<TValue, string>> items)
        {
            Add(items.ToArray());
        } // Add

        public void Add(IList<KeyValuePair<TValue, string>> items)
        {
            var add = new object[items.Count];
            for (var index = 0; index < items.Count; index++)
            {
                add[index] = items[index];
            } // for
            AddItems(add);
        } // Add

        public void Add(KeyValuePair<TValue, string>[] items)
        {
            var add = new object[items.Length];
            Array.Copy(items, add, items.Length);
            AddItems(add);
        } // Add

        public int Add(TValue key)
        {
            if (_dictionary == null) throw new InvalidOperationException();

            return Add(key, _dictionary[key]);
        } // Add

        public void Add(IList<TValue> values)
        {
            if (_dictionary == null) throw new InvalidOperationException();

            var add = new object[values.Count];
            for (var index = 0; index < values.Count; index++)
            {
                var value = values[index];
                add[index] = new KeyValuePair<TValue, string>(value, _dictionary[value]);
            } // for
            AddItems(add);
        } // Add

        public List<TValue> GetListValues()
        {
            return (from item in _listBox.Items.Cast<KeyValuePair<TValue, string>>()
                    select item.Key).ToList();
        } // GetListValues

        [PublicAPI]
        public List<string> GetListKeys()
        {
            return (from item in _listBox.Items.Cast<KeyValuePair<TValue, string>>()
                select item.Value).ToList();
        } // GetListValues

        [PublicAPI]
        public IList<KeyValuePair<TValue, string>> GetListItems()
        {
            return (from item in _listBox.Items.Cast<KeyValuePair<TValue, string>>()
                    select item).ToList();
        } // GetListItems

        public void RemoveSelection()
        {
            if (IsReadOnly) return;

            var index = _listBox.SelectedIndex;
            if (index < 0) throw new InvalidOperationException();

            _listBox.Items.RemoveAt(index);

            if (index >= _listBox.Items.Count) index -= 1;
            _listBox.SelectedIndex = index;
        } // RemoveSelection

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
    } // class ListItemsManager<TValue>
} // namespace
