// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Controls;

namespace IpTviewr.UiServices.Configuration.Editors
{
    public partial class ArgumentsEditor : ListEditor
    {
        private readonly ListItemsManager<string> _manager;

        public ArgumentsEditor()
        {
            InitializeComponent();
            _manager = new ListItemsManager<string>(listItems, buttonRemove, buttonMoveUp, buttonMoveDown);
        } // constructor

        public string OpenBraceText
        {
            get;
            set;
        } // OpenBraceText

        public string CloseBraceText
        {
            get;
            set;
        } // CloseBraceText

        public string ParametersList
        {
            get;
            set;
        } // ParametersList

        public string[] Arguments
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
            using (var dialog = GetArgumentEditorDialog())
            {
                dialog.Parameter = listItems.SelectedItem.ToString();
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                } // if

                var index = listItems.SelectedIndex;
                listItems.Items[index] = dialog.Parameter;
                IsDataChanged = true;
            } // using
        } // ButtonEdit_Click

        protected override void ButtonRemove_Click(object sender, EventArgs e)
        {
            _manager.RemoveSelection();
            IsDataChanged = true;
        } // ButtonRemove_Click

        protected override void ButtonAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = GetArgumentEditorDialog())
            {
                dialog.Parameter = null;
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                } // if

                listItems.SelectedIndex = listItems.Items.Add(dialog.Parameter);
                IsDataChanged = true;
            } // using
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

        private ArgumentEditorDialog GetArgumentEditorDialog()
        {
            var dialog = new ArgumentEditorDialog()
            {
                OpenBraceText = this.OpenBraceText,
                CloseBraceText = this.CloseBraceText,
                ParametersList = this.ParametersList
            };

            return dialog;
        } // GetArgumentEditorDialog
    } // class ArgumentsEditor
} // namespace
