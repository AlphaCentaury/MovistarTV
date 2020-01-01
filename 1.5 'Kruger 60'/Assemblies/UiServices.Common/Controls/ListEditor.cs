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

using System;

namespace IpTviewr.UiServices.Common.Controls
{
    public partial class ListEditor : CommonBaseUserControl
    {
        private bool _isDataChanged;

        public event EventHandler DataChanged;

        public ListEditor()
        {
            InitializeComponent();
        } // constructor

        public string ListName
        {
            get => groupBoxData.Text;
            set => groupBoxData.Text = value;
        } // ListName

        public bool IsDataChanged
        {
            get => _isDataChanged;
            protected set
            {
                _isDataChanged = value;
                OnDataChanged(this, EventArgs.Empty);
            } // set
        } // IsDataChanged

        public virtual int ItemsCount => listItems.Items.Count;

        public virtual bool IsReadOnly { get; set; }

        public virtual bool CanEdit => !IsReadOnly;

        public virtual bool CanRemove => !IsReadOnly;

        public virtual bool CanAdd => !IsReadOnly;

        public virtual bool CanMove => !IsReadOnly;

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

        protected virtual void OnDataChanged(object sender, EventArgs e)
        {
            DataChanged?.Invoke(sender, e);
        } // OnDataChanged

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
