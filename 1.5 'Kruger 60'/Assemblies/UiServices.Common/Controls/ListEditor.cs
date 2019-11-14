// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    public partial class ListEditor : UserControl
    {
        public ListEditor()
        {
            InitializeComponent();
        } // constructor

        public bool IsDataChanged
        {
            get;
            protected set;
        } // IsDataChanged

        private void ListEditor_Load(object sender, EventArgs e)
        {
            listItems.DisplayMember = null;
            listItems.ValueMember = null;
            buttonEdit.Enabled = false;
            buttonRemove.Enabled = false;
            buttonAdd.Enabled = CanAdd;
            buttonMoveUp.Enabled = false;
            buttonMoveDown.Enabled = false;
        } // ListEditor_Load

        public virtual bool IsReadOnly { get; set; }

        public virtual bool CanEdit => !IsReadOnly;

        public virtual bool CanRemove => !IsReadOnly;

        public virtual bool CanAdd => !IsReadOnly;

        public virtual bool CanMove => !IsReadOnly;

        protected virtual void ListItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            var enable = listItems.SelectedIndex >= 0;

            buttonEdit.Enabled = enable && CanEdit;
            buttonRemove.Enabled = enable && CanRemove;
        } // ListItems_SelectedIndexChanged

        protected virtual void ListItems_DoubleClick(object sender, EventArgs e)
        {
            buttonEdit.PerformClick();
        } // ListItems_DoubleClick

        protected virtual void ButtonEdit_Click(object sender, EventArgs e)
        {
            // no-op
        } // buttonEdit_Click

        protected virtual void ButtonRemove_Click(object sender, EventArgs e)
        {
            // no-op
        } // ButtonRemove_Click

        protected virtual void ButtonAdd_Click(object sender, EventArgs e)
        {
            // no-op
        } // buttonAdd_Click

        protected virtual void ButtonMoveUp_Click(object sender, EventArgs e)
        {
            // no-op
        } // buttonMoveUp_Click

        protected virtual void ButtonMoveDown_Click(object sender, EventArgs e)
        {
            // no-op
        } // buttonMoveDown_Click
    } // class ListEditor
} // namespace
