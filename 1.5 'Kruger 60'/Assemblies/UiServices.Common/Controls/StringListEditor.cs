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

using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Common.Properties;
using System;
//using System.Collections.Specialized;

namespace IpTviewr.UiServices.Common.Controls
{
    public partial class StringListEditor : ListEditor
    {
        private readonly ListItemsManager _manager;

        // TODO: implement
        // public event NotifyCollectionChangedEventHandler ItemsChanged;

        public StringListEditor()
        {
            InitializeComponent();
            _manager = new ListItemsManager(listItems, buttonRemove, buttonMoveUp, buttonMoveDown);
        } // constructor

        public string[] Items
        {
            get
            {
                var arguments = new string[listItems.Items.Count];
                for (var index = 0; index < arguments.Length; index++)
                {
                    arguments[index] = listItems.Items[index].ToString();
                } // for

                return arguments;
            }
            set
            {
                if (value != null)
                {
                    listItems.Items.AddRange(value);
                }
                else
                {
                    listItems.Items.Clear();
                } // if-else
            } // set
        } // Arguments

        protected override void ButtonEdit_Click(object sender, EventArgs e)
        {
            SafeCall(() =>
            {
                if (!EditItem(listItems.SelectedItem.ToString(), out string newItem)) return;
                var index = listItems.SelectedIndex;
                listItems.Items[index] = newItem;
                IsDataChanged = true;
            });
        } // ButtonEdit_Click

        protected override void ButtonRemove_Click(object sender, EventArgs e)
        {
            _manager.RemoveSelection();
            IsDataChanged = true;
        } // ButtonRemove_Click

        protected override void ButtonAdd_Click(object sender, EventArgs e)
        {
            SafeCall(() =>
            {
                if (!GetNewItem(out var newItem)) return;
                listItems.SelectedIndex = listItems.Items.Add(newItem);
                IsDataChanged = true;
            });
        } // ButtonAdd_Click

        protected override void ButtonMoveUp_Click(object sender, EventArgs e)
        {
            _manager.MoveSelectionUp();
            IsDataChanged = true;
        } // ButtonMoveUp_Click

        protected override void ButtonMoveDown_Click(object sender, EventArgs e)
        {
            _manager.MoveSelectionDown();
            IsDataChanged = true;
        } // ButtonMoveDown_Click

        protected virtual bool GetNewItem(out string newItem)
        {
            newItem = null;
            return InputBox.ShowDialog(ParentForm, CommonForm.StringListEditorNewItem, null, ref newItem);
        } // GetNewItem

        protected virtual bool EditItem(string item, out string newItem)
        {
            newItem = null;
            if (!InputBox.ShowDialog(ParentForm, CommonForm.StringListEditorEditItem, null, ref item)) return false;

            newItem = item;
            return true;
        } // EditItem
    } // class ArgumentsEditor
} // namespace
