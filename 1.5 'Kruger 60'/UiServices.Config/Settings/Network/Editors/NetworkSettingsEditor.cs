// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration.Settings.Network.Editors
{
    public partial class NetworkSettingsEditor : UserControl, IConfigurationItemEditor
    {
        private int ManualUpdateLock; 

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

        UserControl IConfigurationItemEditor.UserInterfaceItem
        {
            get { return this; }
        } // IConfigurationItemEditor.UserInterfaceItem

        bool IConfigurationItemEditor.SupportsWinFormsValidation
        {
            get { return false; }
        } // IConfigurationItemEditor.SupportsWinFormsValidation

        public bool IsDataChanged
        {
            get;
            protected set;
        } // IsDataChanged

        public bool IsAppRestartNeeded
        {
            get { return false; }
        } // IsAppRestartNeeded

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
            ManualUpdateLock++;
            checkBoxEnableMulticastProxy.Checked = Settings.MulticastProxy.IsEnabled;
            parametersEditorMulticastProxy.Enabled = checkBoxEnableMulticastProxy.Checked;
            parametersEditorMulticastProxy.OpenBraceText = MulticastProxy.ParameterOpenBrace;
            parametersEditorMulticastProxy.CloseBraceText = MulticastProxy.ParameterCloseBrace;
            parametersEditorMulticastProxy.CommandLine = Settings.MulticastProxy.ProxyConfiguration;
            parametersEditorMulticastProxy.ParametersList = Properties.SettingsTexts.NetworkUpdProxyParameters;
            ManualUpdateLock--;
        } // NetworkSettingsEditor_Load

        private void checkBoxEnableMulticastProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;

            parametersEditorMulticastProxy.Enabled = checkBoxEnableMulticastProxy.Checked;
            IsDataChanged = true;
        } // checkBoxEnableMulticastProxy_CheckedChanged

        private void parametersEditorMulticastProxy_CommandLineChanged(object sender, EventArgs e)
        {
            if (ManualUpdateLock > 0) return;
            
            IsDataChanged = true;
        } // parametersEditorMulticastProxy_CommandLineChanged
    } // class NetworkSettingsEditor
} // namespace
