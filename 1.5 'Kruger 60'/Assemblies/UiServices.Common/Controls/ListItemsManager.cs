// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
            if (items == null) throw new ArgumentNullException("items");

            _dictionary = new Dictionary<TValue, string>(items.Count, comparer);
            foreach (var item in items)
            {
                _dictionary.Add(item);
            } // if
        } // SetValueDictionary

        public void SetValueDictionary(IDictionary<TValue, string> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            _dictionary = dictionary;
        } // SetValueDictionary

        public int Add(TValue value, string text)
        {
            return Add(new KeyValuePair<TValue, string>(value, text));
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
            for (var index = 0; index < items.Count;index++ )
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

        public int Add(TValue value)
        {
            if (_dictionary == null) throw new InvalidOperationException();

            return Add(value, _dictionary[value]);
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
            var count = _listBox.Items.Count;
            var result = new List<TValue>(count);

            for (var index = 0; index < count; index++)
            {
                var item = (KeyValuePair<TValue, string>)_listBox.Items[index];
                result.Add(item.Key);
            } // for

            return result;
        } // GetListValues

        public IList<KeyValuePair<TValue, string>> GetListItems()
        {
            var count = _listBox.Items.Count;
            var result = new List<KeyValuePair<TValue, string>>(count);

            for (var index = 0; index < count; index++)
            {
                var item = (KeyValuePair<TValue, string>)_listBox.Items[index];
                result.Add(item);
            } // for

            return result;
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
            if ((index +1) >= _listBox.Items.Count) throw new InvalidOperationException();

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
