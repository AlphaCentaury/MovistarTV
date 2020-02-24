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
using System.Windows.Forms;
using IpTviewr.Common.Configuration;
using IpTviewr.Internal.Tools.ChannelLogosEditor.Properties;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;

namespace IpTviewr.Internal.Tools.ChannelLogosEditor
{
    public partial class FormEditor : CommonBaseForm
    {
        private bool _isConfigLoaded;
        private bool _isDirty;
        private bool _isOpen;
        private ServiceCollection _currentCollection;
        private ServiceMappingsXml _serviceMappingsXml;

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
                CurrentCollection = null;
                menuItemEditorOpen.Enabled = !_isOpen;
                menuItemCollectionEditor.Enabled = _isOpen;
                menuItemEditorClose.Enabled = _isOpen;
                labelStatus.Text = _isOpen ? "Editor is ready" : "Editor closed";
            }
        } // IsOpen

        private ServiceCollection CurrentCollection
        {
            get => _currentCollection;
            set
            {
                _currentCollection = value;
                menuItemCollectionSeparator1.Visible = value != null;
                menuItemCollectionCurrent.Visible = value != null;
                menuItemCollectionCurrent.Text = value?.Name;
            } // set
        } // CurrentCollection

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

        private void MenuItemEditorExit_Click(object sender, EventArgs e)
        {
            Close();
        } // MenuItemEditorExit_Click

        private void MenuItemCollectionSelect_Click(object sender, EventArgs e)
        {
            using (var dialog = new DialogCollectionsEditor())
            {
                dialog.IsReadOnly = true;
                dialog.Text = "Select collection";
                dialog.Collections = _serviceMappingsXml.Collections;
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                CurrentCollection = dialog.SelectedCollection;
            } // using
        } // MenuItemCollectionSelect_Click

        private void MenuItemCollectionEditor_Click(object sender, EventArgs e)
        {
            using (var dialog = new DialogCollectionsEditor())
            {
                dialog.Collections = _serviceMappingsXml.Collections;
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                _serviceMappingsXml.Collections = dialog.Collections;
            } // using
        } // MenuItemCollectionEditor_Click

        private void Open()
        {
            if (IsOpen) return;

            if (!LoadConfiguration()) return;
            _serviceMappingsXml = LogosCommon.ParseServiceMappingsXml(AppConfig.Current.Folders.Logos.FileServiceMappings);

            IsOpen = true;
        } // Open

        private void Save()
        {
            if (!IsDirty) return;

            IsDirty = false;
        } // Save

        #region Configuration loader

        private bool LoadConfiguration()
        {
            if (_isConfigLoaded) return true;

            var result = GetConfiguration();
            _isConfigLoaded = (result.IsOk);
            if (_isConfigLoaded) return true;

            LoadConfigurationDisplayProgress(result.Message);
            Program.HandleException(this, result.Caption, result.Message, result.InnerException);

            return false;
        } // LoadConfiguration

        private InitializationResult GetConfiguration()
        {
            try
            {
                var result = AppConfig.Load(null, LoadConfigurationDisplayProgress);
                return result.IsError ? result : InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = "Application configuration error",
                    Message = "An unexpected error has occured while loading the application configuration.",
                    InnerException = ex
                };
            } // try-catch
        } // GetConfiguration

        private void LoadConfigurationDisplayProgress(string text)
        {
            labelStatus.Text = text;
            statusStripMain.Refresh();
        } // LoadDisplayProgress

        #endregion
    } // class FormEditor
} // namespace
