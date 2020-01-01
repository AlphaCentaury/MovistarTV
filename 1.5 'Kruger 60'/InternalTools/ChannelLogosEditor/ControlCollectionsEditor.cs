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
using System.Linq;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.ChannelLogosEditor.Properties;
using IpTviewr.UiServices.Common.Controls;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;

namespace IpTviewr.Internal.Tools.ChannelLogosEditor
{
    public partial class ControlCollectionsEditor : ListEditor
    {
        private readonly ListItemsManager<ServiceCollection> _manager;

        public ControlCollectionsEditor()
        {
            InitializeComponent();
            _manager = new ListItemsManager<ServiceCollection>(listItems, buttonRemove, buttonMoveUp, buttonMoveDown);
        } // constructor

        public override bool IsReadOnly
        {
            get => _manager.IsReadOnly;
            set => _manager.IsReadOnly = value;
        } // IsReadOnly

        public ServiceCollection[] Collections
        {
            get
            {
                var arguments = new ServiceCollection[listItems.Items.Count];
                for (var index = 0; index < arguments.Length; index++)
                {
                    arguments[index] = listItems.Items[index] as ServiceCollection;
                } // for

                return arguments;
            }
            set
            {
                listItems.Items.Clear();
                if (value == null) return;

                var q = from collection in value
                        select (object)collection.ShallowClone();

                listItems.Items.AddRange(q.ToArray());
            } // set
        } // Collections

        public ServiceCollection SelectedCollection => listItems.SelectedItem as ServiceCollection;

        private void ControlCollectionsEditor_Load(object sender, EventArgs e)
        {
            listItems.DisplayMember = "Name";
            listItems.ValueMember = "Package";
        } // ControlCollectionsEditor_Load

        protected override void ButtonEdit_Click(object sender, EventArgs e)
        {
            /*
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
            */
        } // ButtonEdit_Click

        protected override void ButtonRemove_Click(object sender, EventArgs e)
        {
            var text = string.Format((SelectedCollection.Domains.Length == 0) ? Texts.RemoveEmptyCollection : Texts.RemoveNonEmptyCollection, SelectedCollection.Domains.Length);
            if (MessageBox.Show(this, text, Texts.RemoveCollectionCaption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;

            _manager.RemoveSelection();
            IsDataChanged = true;
        } // ButtonRemove_Click

        protected override void ButtonAdd_Click(object sender, EventArgs e)
        {
            /*
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
            */
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
    } // class ControlCollectionsEditor
} // namespace
