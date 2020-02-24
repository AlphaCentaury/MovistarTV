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

namespace IpTviewr.UiServices.Configuration.Settings.Network.Editors
{
    public partial class NetworkSettingsEditor : UserControl, IConfigurationItemEditor
    {
        private int _manualUpdateLock;

        public NetworkSettingsEditor()
        {
            InitializeComponent();

            parametersEditorMulticastProxy.ParametersList = Properties.SettingsTexts.NetworkUpdProxyParameters;
        } // constructor

        public NetworkSettings Settings
        {
            get;
            set;
        } // Settings

        #region IConfigurationItemEditor implementation

        UserControl IConfigurationItemEditor.UserInterfaceItem => this;

        bool IConfigurationItemEditor.SupportsWinFormsValidation => false;

        public bool IsDataChanged
        {
            get;
            protected set;
        } // IsDataChanged

        public bool IsAppRestartNeeded => false;

        bool IConfigurationItemEditor.Validate()
        {
            if ((checkBoxEnableMulticastProxy.Enabled) && (parametersEditorMulticastProxy.CommandLine == ""))
            {
                MessageBox.Show(this, Properties.SettingsTexts.NetworkMulticastProxyValidation, Properties.SettingsTexts.NetworkValidationErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            } // if

            return true;
        } // IConfigurationItemEditor.Validate

        IConfigurationItem IConfigurationItemEditor.GetNewData()
        {
            Settings.MulticastProxy.IsEnabled = checkBoxEnableMulticastProxy.Checked;
            Settings.MulticastProxy.ProxyConfiguration = parametersEditorMulticastProxy.CommandLine;

            return Settings;
        } // IConfigurationItemEditor.GetNewData

        void IConfigurationItemEditor.EditorClosing(out bool cancelClose)
        {
            cancelClose = false;
        } // IConfigurationItemEditor.EditorClosing

        void IConfigurationItemEditor.EditorClosed(bool userCancel)
        {
            // no op
        } // IConfigurationItemEditor.EditorClosed

        #endregion

        private void NetworkSettingsEditor_Load(object sender, EventArgs e)
        {
            _manualUpdateLock++;
            checkBoxEnableMulticastProxy.Checked = Settings.MulticastProxy.IsEnabled;
            parametersEditorMulticastProxy.Enabled = checkBoxEnableMulticastProxy.Checked;
            parametersEditorMulticastProxy.OpenBraceText = MulticastProxy.ParameterOpenBrace;
            parametersEditorMulticastProxy.CloseBraceText = MulticastProxy.ParameterCloseBrace;
            parametersEditorMulticastProxy.CommandLine = Settings.MulticastProxy.ProxyConfiguration;
            parametersEditorMulticastProxy.ParametersList = Properties.SettingsTexts.NetworkUpdProxyParameters;
            _manualUpdateLock--;
        } // NetworkSettingsEditor_Load

        private void checkBoxEnableMulticastProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (_manualUpdateLock > 0) return;

            parametersEditorMulticastProxy.Enabled = checkBoxEnableMulticastProxy.Checked;
            IsDataChanged = true;
        } // checkBoxEnableMulticastProxy_CheckedChanged

        private void parametersEditorMulticastProxy_CommandLineChanged(object sender, EventArgs e)
        {
            if (_manualUpdateLock > 0) return;

            IsDataChanged = true;
        } // parametersEditorMulticastProxy_CommandLineChanged
    } // class NetworkSettingsEditor
} // namespace
