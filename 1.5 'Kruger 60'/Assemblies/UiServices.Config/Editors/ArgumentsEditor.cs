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

using System.Windows.Forms;
using IpTviewr.UiServices.Common.Controls;

namespace IpTviewr.UiServices.Configuration.Editors
{
    public partial class ArgumentsEditor : StringListEditor
    {
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
            get => Items;
            set => Items = value;
        } // Arguments

        #region Overrides of StringListEditor

        protected override bool GetNewItem(out string newItem)
        {
            using var dialog = GetArgumentEditorDialog();
            dialog.Parameter = null;
            var ok = dialog.ShowDialog(this) != DialogResult.OK;
            newItem = dialog.Parameter;
            return ok;
        } // GetNewItem

        protected override bool EditItem(string item, out string newItem)
        {
            using var dialog = GetArgumentEditorDialog();
            dialog.Parameter = listItems.SelectedItem.ToString();
            var ok = dialog.ShowDialog(this) != DialogResult.OK;
            newItem = dialog.Parameter;
            return ok;
        } // EditItem

        #endregion

        private ArgumentEditorDialog GetArgumentEditorDialog()
        {
            var dialog = new ArgumentEditorDialog()
            {
                OpenBraceText = OpenBraceText,
                CloseBraceText = CloseBraceText,
                ParametersList = ParametersList
            };

            return dialog;
        } // GetArgumentEditorDialog
    } // class ArgumentsEditor
} // namespace
