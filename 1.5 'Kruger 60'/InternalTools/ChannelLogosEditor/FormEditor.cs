// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.ChannelLogosEditor.Properties;
using IpTviewr.UiServices.Common.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogosEditor
{
    public partial class FormEditor : CommonBaseForm
    {
        private bool _isDirty;
        private bool _isOpen;

        public FormEditor()
        {
            InitializeComponent();
        } // constructor

        private bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                menuItemEditorSave.Enabled = _isDirty;
            } // set
        } // IsDirty

        private bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                IsDirty = false;
                menuItemEditorOpen.Enabled = !_isOpen;
                CollectionToolStripMenuItem.Enabled = _isOpen;
                menuItemEditorClose.Enabled = _isOpen;
            }
        } // IsOpen

        private void FormEditor_Load(object sender, EventArgs e)
        {
            IsOpen = false;
        } // FormEditor_Load

        private void FormEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            SafeCall(FormEditor_FormClosingImplementation, sender, e);
        } // FormEditor_FormClosing

        private void FormEditor_FormClosingImplementation(object sender, FormClosingEventArgs e)
        {
            if (!IsDirty) return;

            var result = MessageBox.Show(this, Texts.SaveChanges, Text,
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            switch (result)
            {
                case DialogResult.Yes:
                    Save();
                    break;
                case DialogResult.No:
                    return;
                default:
                    e.Cancel = true;
                    break;
            } // switch
        } // Internal_FormEditor_FormClosing_Implementation

        private void MenuItemEditorOpen_Click(object sender, EventArgs e)
        {
            SafeCall(Open);
        } // MenuItemEditorOpen_Click

        private void MenuItemEditorSave_Click(object sender, EventArgs e)
        {
            SafeCall(Save);
        } // MenuItemEditorSave_Click

        private void MenuItemEditorClose_Click(object sender, EventArgs e)
        {
            SafeCall(MenuItemEditorClose_ClickImplementation, sender, e);
        } // MenuItemEditorClose_Click

        private void MenuItemEditorClose_ClickImplementation(object sender, EventArgs e)
        {
            var closing = new FormClosingEventArgs(CloseReason.None, false);
            FormEditor_FormClosingImplementation(sender, closing);

            if (closing.Cancel) return;
            IsOpen = false;
        } // MenuItemEditorClose_ClickImplementation

        private void MenuItemEditorCollectionAdd_Click(object sender, EventArgs e)
        {
            SafeCall(MenuItemEditorCollectionAdd_ClickImplementation, sender, e);
        } // MenuItemEditorCollectionAdd_Click

        private void MenuItemEditorCollectionAdd_ClickImplementation(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, nameof(MenuItemEditorCollectionAdd_ClickImplementation));
        } // MenuItemEditorCollectionAdd_ClickImplementation

        private void MenuItemEditorCollectionEdit_Click(object sender, EventArgs e)
        {
            SafeCall(MenuItemEditorCollectionEdit_ClickImplementation, sender, e);
        } // MenuItemEditorCollectionEdit_Click

        private void MenuItemEditorCollectionEdit_ClickImplementation(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, nameof(MenuItemEditorCollectionEdit_ClickImplementation));
        } // MenuItemEditorCollectionEdit_ClickImplementation

        private void MenuItemEditorExit_Click(object sender, EventArgs e)
        {
            Close();
        } // MenuItemEditorExit_Click

        private void Open()
        {
            if (IsOpen) return;

            IsOpen = true;
        } // Open

        private void Save()
        {
            if (!IsDirty) return;

            IsDirty = false;
        } // Save
    } // class FormEditor
} // namespace
