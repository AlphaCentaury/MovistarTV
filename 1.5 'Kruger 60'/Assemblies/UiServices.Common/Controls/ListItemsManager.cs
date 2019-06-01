// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    public class ListItemsManager<TValue>
    {
        private ListBox ListBox;
        private Control RemoveControl, UpControl, DownControl;
        private IDictionary<TValue, string> Dictionary;

        public ListItemsManager(ListBox listBox, Control removeControl, Control upControl, Control downControl)
        {
            if ((listBox == null) || (removeControl == null) || (upControl == null) || (downControl == null))
            {
                throw new ArgumentNullException();
            } // if

            if ((listBox.SelectionMode == SelectionMode.MultiSimple) || (listBox.SelectionMode == SelectionMode.MultiExtended))
            {
                throw new ArgumentException("listBox");
            } // if

            ListBox = listBox;
            RemoveControl = removeControl;
            UpControl = upControl;
            DownControl = downControl;

            RemoveControl.Enabled = false;
            UpControl.Enabled = false;
            DownControl.Enabled = false;

            ListBox.DisplayMember = "Value";
            ListBox.ValueMember = "Key";
            ListBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
        }  // constructor

        public void SetValueDictionary(IList<KeyValuePair<TValue, string>> items, IEqualityComparer<TValue> comparer)
        {
            if (items == null) throw new ArgumentNullException("items");

            Dictionary = new Dictionary<TValue, string>(items.Count, comparer);
            foreach (var item in items)
            {
                Dictionary.Add(item);
            } // if
        } // SetValueDictionary

        public void SetValueDictionary(IDictionary<TValue, string> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");
            Dictionary = dictionary;
        } // SetValueDictionary

        public int Add(TValue value, string text)
        {
            return Add(new KeyValuePair<TValue, string>(value, text));
        } // Add

        public int Add(KeyValuePair<TValue, string> item)
        {
            var index = ListBox.Items.Add(item);
            ListBox.SelectedIndex = index;

            return index;
        } // Add

        public void Add(IEnumerable<KeyValuePair<TValue, string>> items)
        {
            Add(items.ToArray());
        } // Add

        public void Add(IList<KeyValuePair<TValue, string>> items)
        {
            var add = new object[items.Count];
            for (int index = 0; index < items.Count;index++ )
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
            if (Dictionary == null) throw new InvalidOperationException();

            return Add(value, Dictionary[value]);
        } // Add

        public void Add(IList<TValue> values)
        {
            if (Dictionary == null) throw new InvalidOperationException();

            var add = new object[values.Count];
            for (int index = 0; index < values.Count; index++)
            {
                var value = values[index];
                add[index] = new KeyValuePair<TValue, string>(value, Dictionary[value]);
            } // for
            AddItems(add);
        } // Add

        public List<TValue> GetListValues()
        {
            var count = ListBox.Items.Count;
            var result = new List<TValue>(count);

            for (int index = 0; index < count; index++)
            {
                var item = (KeyValuePair<TValue, string>)ListBox.Items[index];
                result.Add(item.Key);
            } // for

            return result;
        } // GetListValues

        public IList<KeyValuePair<TValue, string>> GetListItems()
        {
            var count = ListBox.Items.Count;
            var result = new List<KeyValuePair<TValue, string>>(count);

            for (int index = 0; index < count; index++)
            {
                var item = (KeyValuePair<TValue, string>)ListBox.Items[index];
                result.Add(item);
            } // for

            return result;
        } // GetListItems

        public void RemoveSelection()
        {
            var index = ListBox.SelectedIndex;
            if (index < 0) throw new InvalidOperationException();

            ListBox.Items.RemoveAt(index);

            if (index >= ListBox.Items.Count) index -= 1;
            ListBox.SelectedIndex = index;
        } // RemoveSelection

        public void MoveSelectionUp()
        {
            var index = ListBox.SelectedIndex;
            if (index < 0) throw new InvalidOperationException();

            var upItem = ListBox.Items[index - 1];
            var current = ListBox.Items[index];

            ListBox.Items[index - 1] = current;
            ListBox.Items[index] = upItem;

            ListBox.SelectedIndex = index - 1;
        } // MoveUp

        public void MoveSelectionDown()
        {
            var index = ListBox.SelectedIndex;
            if ((index +1) >= ListBox.Items.Count) throw new InvalidOperationException();

            var current = ListBox.Items[index];
            var downItem = ListBox.Items[index + 1];

            ListBox.Items[index + 1] = current;
            ListBox.Items[index] = downItem;

            ListBox.SelectedIndex = index + 1;
        } // MoveDown

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = ListBox.SelectedIndex;
            RemoveControl.Enabled = (index >= 0) && (ListBox.Items.Count > 0);
            UpControl.Enabled = (index > 0) && (ListBox.Items.Count > 1);
            DownControl.Enabled = ((index + 1) < ListBox.Items.Count);
        } // ListBox_SelectedIndexChanged

        private void AddItems(object[] items)
        {
            var index = ListBox.SelectedIndex;
            ListBox.Items.AddRange(items);
            ListBox.SelectedIndex = index;
        } // AddItems
    } // class ListItemsManager<TValue>
} // namespace
