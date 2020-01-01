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

using IpTviewr.Common.Telemetry;
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Common.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Record
{
    public partial class RecordChannelDialog : CommonBaseForm
    {
        private DateTime _currentStartDateTime;
        private RecordScheduleKind _currentScheduleKind;
        private int _currentSelectedLocationIndex;

        public RecordTask Task
        {
            get;
            set;
        } // Task

        public bool IsNewTask
        {
            get;
            set;
        } // IsNewTask

        public DateTime LocalReferenceTime
        {
            get;
            set;
        } // LocalReferenceTime

        public static string[] GetFilenameExtensions()
        {
            var separators = new[] { "\r\n" };
            var extensions = Properties.RecordChannel.FileExtensions.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            return extensions;
        } // GetFilenameExtensions

        public RecordChannelDialog()
        {
            InitializeComponent();
            Icon = Properties.Resources.Icon_Recorder;
            LocalReferenceTime = DateTime.Now;
        } // constructor

        #region Events

        private void DialogRecordChannel_Load(object sender, EventArgs e)
        {
            SafeCall(DialogRecordChannel_Load_Implementation, sender, e);
        } // DialogRecordChannel_Load

        private void DialogRecordChannel_Shown(object sender, EventArgs e)
        {
            SafeCall(DialogRecordChannel_Shown_Implementation, sender, e);
        } // DialogRecordChannel_Shown

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SafeCall(buttonOk_Click_Implementation, sender, e);
        } // buttonOk_Click

        #endregion

        #region Events implementation

        private void DialogRecordChannel_Load_Implementation(object sender, EventArgs e)
        {
            // Initialize
            if (Task == null)
            {
                if (DesignMode)
                {
                    Task = RecordTask.CreateWithDefaultValues(null);
                    IsNewTask = true;
                }
                else
                {
                    throw new ArgumentNullException();
                } // if-else
            } // if

            // General
            InitGeneralData();
            // Schedule tab
            InitScheduleData();
            // Duration tab
            InitDurationData();
            // Description tab
            InitDescriptionData();
            // Save tab
            InitSaveData();
            // Advanced tab
            InitAdvancedData();
        } // DialogRecordChannel_Load_Implementation

        private void DialogRecordChannel_Shown_Implementation(object sender, EventArgs e)
        {
            if (_currentSelectedLocationIndex >= 0)
            {
                // force creation of the control
                // .NET WinForms BUG: selecting an item when the control is NOT created does not update the SelectedItems/SelectedIndexes collection
                _ = listViewLocations.Handle;
                Debug.Assert(listViewLocations.IsHandleCreated);
                listViewLocations.Items[_currentSelectedLocationIndex].Selected = true;
            } // if
        } // DialogRecordChannel_Shown_Implementation

        private void buttonOk_Click_Implementation(object sender, EventArgs e)
        {
            var isValid = ValidateChildren(ValidationConstraints.Enabled);
            if (!isValid) return;

            // General
            GetGeneralData();
            // Schedule tab
            GetScheduleData();
            // Duration tab
            GetDurationData();
            // Description tab
            GetDescriptionData();
            // Save tab
            GetSaveData();
            // Advanced tab
            GetAdvancedData();

            DialogResult = DialogResult.OK;
        } // buttonOk_Click_Implementation

        #endregion

        #region Private methods

        private void ControlValidationFailed(string text, Control control)
        {
            MessageBox.Show(this, text, Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var tabPage = control.Parent as TabPage;
            if (tabPage != null)
            {
                tabProperties.SelectedTab = tabPage;
            } // if
            control.Focus();
        }  // ControlValidationFailed

        #endregion
    } // class DialogRecordChannel
} // namespace
