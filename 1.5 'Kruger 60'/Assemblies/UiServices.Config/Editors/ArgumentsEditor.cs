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
    public partial class ArgumentsEditor : UserControl
    {
        private ListItemsManager<string> ItemsManager;

        public ArgumentsEditor()
        {
            InitializeComponent();
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
                var arguments = new string[listArguments.Items.Count];
                for (var index = 0; index < arguments.Length; index++)
                {
                    arguments[index] = listArguments.Items[index].ToString();
                } // for

                return arguments;
            }
            set
            {
                if (value != null)
                {
                    listArguments.Items.AddRange(value);
                }
                else
                {
                    listArguments.Items.Clear();
                } // if-else
            } // set
        } // Arguments

        public bool IsDataChanged
        {
            get;
            private set;
        } // IsDataChanged

        private void ArgumentsEditor_Load(object sender, EventArgs e)
        {
            ItemsManager = new ListItemsManager<string>(listArguments, buttonRemove, buttonMoveUp, buttonMoveDown);
            listArguments.DisplayMember = null;
            listArguments.ValueMember = null;
            buttonEdit.Enabled = false;
        } // ArgumentsEditor_Load

        private void listArguments_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEdit.Enabled = (listArguments.SelectedIndex >= 0);
        } // listArguments_SelectedIndexChanged

        private void listArguments_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit.PerformClick();
        } // listArguments_DoubleClick

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (var dialog = GetArgumentEditorDialog())
            {
                dialog.Parameter = listArguments.SelectedItem.ToString();
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                } // if

                var index = listArguments.SelectedIndex;
                listArguments.Items[index] = dialog.Parameter;
                IsDataChanged = true;
            } // using
        } // buttonEdit_Click

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            ItemsManager.RemoveSelection();
            IsDataChanged = true;
        } // buttonRemove_Click

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = GetArgumentEditorDialog())
            {
                dialog.Parameter = null;
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                } // if

                listArguments.SelectedIndex = listArguments.Items.Add(dialog.Parameter);
                IsDataChanged = true;
            } // using
        } // buttonAdd_Click

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            ItemsManager.MoveSelectionUp();
            IsDataChanged = true;
        } // buttonMoveUp_Click

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            ItemsManager.MoveSelectionDown();
            IsDataChanged = true;
        } // buttonMoveDown_Click

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
