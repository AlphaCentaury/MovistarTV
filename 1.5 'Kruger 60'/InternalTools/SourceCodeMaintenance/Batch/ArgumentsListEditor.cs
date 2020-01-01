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

using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.UiServices.Common.Controls;
using JetBrains.Annotations;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    public partial class ArgumentsListEditor : StringListEditor
    {
        private Action<TextBox> _selectFileAction;

        public ArgumentsListEditor()
        {
            InitializeComponent();
        } // ArgumentsListEditor

        [PublicAPI]
        public Image SelectFileButtonImage
        {
            get => buttonSelectFile.Image;
            set => buttonSelectFile.Image = value;
        } // SelectFileButtonImage

        [PublicAPI]
        public Action<TextBox> SelectFileAction
        {
            get => _selectFileAction;
            set
            {
                _selectFileAction = value;
                buttonSelectFile.Enabled = (value != null);
            } // set
        } // SelectFileAction

        private void ArgumentsListEditor_Load(object sender, EventArgs e)
        {
            if (SelectFileAction == null)
            {
                buttonSelectFile.Visible = false;
                textBoxArgument.Width = buttonSelectFile.Right - textBoxArgument.Left;
            } // if
        } // ArgumentsListEditor_Load

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            SelectFileAction?.Invoke(textBoxArgument);
        } // buttonSelectFile_Click

        #region Overrides of StringListEditor

        protected override bool GetNewItem(out string newItem)
        {
            newItem = textBoxArgument.Text.Trim();
            if (newItem.Length == 0)
            {
                MessageBox.Show(this, BatchResources.ArgumentsNotOptional, BatchResources.ArgumentsNotOptionalCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            } // if

            textBoxArgument.Text = null;
            return true;
        } // GetNewItem

        #endregion
    }
}
