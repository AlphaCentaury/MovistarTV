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

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader ColumnName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordChannelDialog));
            System.Windows.Forms.ColumnHeader ColumnLocation;
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabProperties = new System.Windows.Forms.TabControl();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.dateTimeExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.checkBoxExpiryDate = new System.Windows.Forms.CheckBox();
            this.labelStartMarginSufix = new System.Windows.Forms.Label();
            this.numericStartMargin = new System.Windows.Forms.NumericUpDown();
            this.checkBoxStartMargin = new System.Windows.Forms.CheckBox();
            this.recordingSchedule = new IpTviewr.UiServices.Record.Controls.RecordingSchedule();
            this.tabPageLength = new System.Windows.Forms.TabPage();
            this.recordingTime = new IpTviewr.UiServices.Record.Controls.RecordingDuration();
            this.tabPageSave = new System.Windows.Forms.TabPage();
            this.listViewLocations = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.imageListLocations = new System.Windows.Forms.ImageList(this.components);
            this.buttonSelectFolder = new System.Windows.Forms.Button();
            this.comboFileExtension = new System.Windows.Forms.ComboBox();
            this.textFilename = new IpTviewr.UiServices.Common.Controls.FilenameTextBox();
            this.labelFilename = new System.Windows.Forms.Label();
            this.labelSaveLocation = new System.Windows.Forms.Label();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.checkAppendRecordingDetails = new System.Windows.Forms.CheckBox();
            this.textTaskDescription = new System.Windows.Forms.TextBox();
            this.labelTaskDescription = new System.Windows.Forms.Label();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.checkAddTaskPrefix = new System.Windows.Forms.CheckBox();
            this.labelTaskName = new System.Windows.Forms.Label();
            this.textTaskName = new IpTviewr.UiServices.Common.Controls.FilenameTextBox();
            this.timeSpanSchedulerDeleteTaskAfter = new IpTviewr.UiServices.Common.Controls.TimeSpanUpDown();
            this.timeSpanSchedulerRetry = new IpTviewr.UiServices.Common.Controls.TimeSpanUpDown();
            this.comboSchedulerAlreadyRunning = new System.Windows.Forms.ComboBox();
            this.labelSchedulerConcurrent = new System.Windows.Forms.Label();
            this.checkSchedulerDeleteTask = new System.Windows.Forms.CheckBox();
            this.numericSchedulerMaxRetries = new System.Windows.Forms.NumericUpDown();
            this.labelSchedulerMaxRetries = new System.Windows.Forms.Label();
            this.checkSchedulerRetry = new System.Windows.Forms.CheckBox();
            this.checkSchedulerASAP = new System.Windows.Forms.CheckBox();
            this.comboSchedulerFolder = new System.Windows.Forms.ComboBox();
            this.labelSchedulerFolder = new System.Windows.Forms.Label();
            this.selectFolder = new IpTviewr.Native.WinForms.SelectFolderDialog();
            this.labelProgramSchedule = new System.Windows.Forms.Label();
            this.labelProgramDescription = new System.Windows.Forms.Label();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.pictureChannelLogo = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            ColumnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ColumnLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabProperties.SuspendLayout();
            this.tabPageSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStartMargin)).BeginInit();
            this.tabPageLength.SuspendLayout();
            this.tabPageSave.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSchedulerMaxRetries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // ColumnName
            // 
            resources.ApplyResources(ColumnName, "ColumnName");
            // 
            // ColumnLocation
            // 
            resources.ApplyResources(ColumnLocation, "ColumnLocation");
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Image = global::IpTviewr.UiServices.Record.Properties.Resources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::IpTviewr.UiServices.Record.Properties.Resources.Action_Cancel_16x16;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // tabProperties
            // 
            resources.ApplyResources(this.tabProperties, "tabProperties");
            this.tabProperties.Controls.Add(this.tabPageSchedule);
            this.tabProperties.Controls.Add(this.tabPageLength);
            this.tabProperties.Controls.Add(this.tabPageSave);
            this.tabProperties.Controls.Add(this.tabPageDescription);
            this.tabProperties.Controls.Add(this.tabPageAdvanced);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.SelectedIndex = 0;
            // 
            // tabPageSchedule
            // 
            resources.ApplyResources(this.tabPageSchedule, "tabPageSchedule");
            this.tabPageSchedule.Controls.Add(this.dateTimeExpiryDate);
            this.tabPageSchedule.Controls.Add(this.checkBoxExpiryDate);
            this.tabPageSchedule.Controls.Add(this.labelStartMarginSufix);
            this.tabPageSchedule.Controls.Add(this.numericStartMargin);
            this.tabPageSchedule.Controls.Add(this.checkBoxStartMargin);
            this.tabPageSchedule.Controls.Add(this.recordingSchedule);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.UseVisualStyleBackColor = true;
            // 
            // dateTimeExpiryDate
            // 
            resources.ApplyResources(this.dateTimeExpiryDate, "dateTimeExpiryDate");
            this.dateTimeExpiryDate.CausesValidation = false;
            this.dateTimeExpiryDate.Name = "dateTimeExpiryDate";
            // 
            // checkBoxExpiryDate
            // 
            resources.ApplyResources(this.checkBoxExpiryDate, "checkBoxExpiryDate");
            this.checkBoxExpiryDate.Name = "checkBoxExpiryDate";
            this.checkBoxExpiryDate.UseVisualStyleBackColor = true;
            this.checkBoxExpiryDate.CheckedChanged += new System.EventHandler(this.checkBoxExpiryDate_CheckedChanged);
            // 
            // labelStartMarginSufix
            // 
            resources.ApplyResources(this.labelStartMarginSufix, "labelStartMarginSufix");
            this.labelStartMarginSufix.Name = "labelStartMarginSufix";
            // 
            // numericStartMargin
            // 
            resources.ApplyResources(this.numericStartMargin, "numericStartMargin");
            this.numericStartMargin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericStartMargin.Name = "numericStartMargin";
            // 
            // checkBoxStartMargin
            // 
            resources.ApplyResources(this.checkBoxStartMargin, "checkBoxStartMargin");
            this.checkBoxStartMargin.Name = "checkBoxStartMargin";
            this.checkBoxStartMargin.UseVisualStyleBackColor = true;
            this.checkBoxStartMargin.CheckedChanged += new System.EventHandler(this.checkBoxStartMargin_CheckedChanged);
            // 
            // recordingSchedule
            // 
            resources.ApplyResources(this.recordingSchedule, "recordingSchedule");
            this.recordingSchedule.Name = "recordingSchedule";
            this.recordingSchedule.ScheduleKindChanged += new System.EventHandler<IpTviewr.UiServices.Record.Controls.RecordingSchedule.KindChangedEventArgs>(this.recordingSchedule_ScheduleKindChanged);
            this.recordingSchedule.DateTimeChanged += new System.EventHandler<IpTviewr.UiServices.Record.Controls.RecordingSchedule.DateTimeChangedEventArgs>(this.recordingSchedule_DateTimeChanged);
            // 
            // tabPageLength
            // 
            resources.ApplyResources(this.tabPageLength, "tabPageLength");
            this.tabPageLength.Controls.Add(this.recordingTime);
            this.tabPageLength.Name = "tabPageLength";
            this.tabPageLength.UseVisualStyleBackColor = true;
            // 
            // recordingTime
            // 
            resources.ApplyResources(this.recordingTime, "recordingTime");
            this.recordingTime.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.recordingTime.CausesValidation = false;
            this.recordingTime.Name = "recordingTime";
            // 
            // tabPageSave
            // 
            resources.ApplyResources(this.tabPageSave, "tabPageSave");
            this.tabPageSave.Controls.Add(this.listViewLocations);
            this.tabPageSave.Controls.Add(this.buttonSelectFolder);
            this.tabPageSave.Controls.Add(this.comboFileExtension);
            this.tabPageSave.Controls.Add(this.textFilename);
            this.tabPageSave.Controls.Add(this.labelFilename);
            this.tabPageSave.Controls.Add(this.labelSaveLocation);
            this.tabPageSave.Name = "tabPageSave";
            this.tabPageSave.UseVisualStyleBackColor = true;
            // 
            // listViewLocations
            // 
            resources.ApplyResources(this.listViewLocations, "listViewLocations");
            this.listViewLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            ColumnName,
            ColumnLocation});
            this.listViewLocations.FullRowSelect = true;
            this.listViewLocations.GridLines = true;
            this.listViewLocations.HeaderCustomFont = null;
            this.listViewLocations.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewLocations.HideSelection = false;
            this.listViewLocations.IsDoubleBuffered = true;
            this.listViewLocations.MultiSelect = false;
            this.listViewLocations.Name = "listViewLocations";
            this.listViewLocations.OwnerDraw = true;
            this.listViewLocations.SmallImageList = this.imageListLocations;
            this.listViewLocations.UseCompatibleStateImageBehavior = false;
            this.listViewLocations.View = System.Windows.Forms.View.Details;
            this.listViewLocations.SelectedIndexChanged += new System.EventHandler(this.listViewLocations_SelectedIndexChanged);
            this.listViewLocations.Validating += new System.ComponentModel.CancelEventHandler(this.listViewLocations_Validating);
            // 
            // imageListLocations
            // 
            this.imageListLocations.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListLocations.ImageStream")));
            this.imageListLocations.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListLocations.Images.SetKeyName(0, "selected");
            this.imageListLocations.Images.SetKeyName(1, "folder");
            // 
            // buttonSelectFolder
            // 
            resources.ApplyResources(this.buttonSelectFolder, "buttonSelectFolder");
            this.buttonSelectFolder.Name = "buttonSelectFolder";
            this.buttonSelectFolder.UseVisualStyleBackColor = true;
            this.buttonSelectFolder.Click += new System.EventHandler(this.buttonSelectFolder_Click);
            // 
            // comboFileExtension
            // 
            resources.ApplyResources(this.comboFileExtension, "comboFileExtension");
            this.comboFileExtension.FormattingEnabled = true;
            this.comboFileExtension.Name = "comboFileExtension";
            this.comboFileExtension.Validating += new System.ComponentModel.CancelEventHandler(this.comboFileExtension_Validating);
            // 
            // textFilename
            // 
            resources.ApplyResources(this.textFilename, "textFilename");
            this.textFilename.Name = "textFilename";
            this.textFilename.Validating += new System.ComponentModel.CancelEventHandler(this.textFilename_Validating);
            // 
            // labelFilename
            // 
            resources.ApplyResources(this.labelFilename, "labelFilename");
            this.labelFilename.Name = "labelFilename";
            // 
            // labelSaveLocation
            // 
            resources.ApplyResources(this.labelSaveLocation, "labelSaveLocation");
            this.labelSaveLocation.Name = "labelSaveLocation";
            // 
            // tabPageDescription
            // 
            resources.ApplyResources(this.tabPageDescription, "tabPageDescription");
            this.tabPageDescription.Controls.Add(this.checkAppendRecordingDetails);
            this.tabPageDescription.Controls.Add(this.textTaskDescription);
            this.tabPageDescription.Controls.Add(this.labelTaskDescription);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.UseVisualStyleBackColor = true;
            // 
            // checkAppendRecordingDetails
            // 
            resources.ApplyResources(this.checkAppendRecordingDetails, "checkAppendRecordingDetails");
            this.checkAppendRecordingDetails.Checked = true;
            this.checkAppendRecordingDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAppendRecordingDetails.Name = "checkAppendRecordingDetails";
            this.checkAppendRecordingDetails.UseVisualStyleBackColor = true;
            // 
            // textTaskDescription
            // 
            resources.ApplyResources(this.textTaskDescription, "textTaskDescription");
            this.textTaskDescription.Name = "textTaskDescription";
            // 
            // labelTaskDescription
            // 
            resources.ApplyResources(this.labelTaskDescription, "labelTaskDescription");
            this.labelTaskDescription.Name = "labelTaskDescription";
            // 
            // tabPageAdvanced
            // 
            resources.ApplyResources(this.tabPageAdvanced, "tabPageAdvanced");
            this.tabPageAdvanced.Controls.Add(this.checkAddTaskPrefix);
            this.tabPageAdvanced.Controls.Add(this.labelTaskName);
            this.tabPageAdvanced.Controls.Add(this.textTaskName);
            this.tabPageAdvanced.Controls.Add(this.timeSpanSchedulerDeleteTaskAfter);
            this.tabPageAdvanced.Controls.Add(this.timeSpanSchedulerRetry);
            this.tabPageAdvanced.Controls.Add(this.comboSchedulerAlreadyRunning);
            this.tabPageAdvanced.Controls.Add(this.labelSchedulerConcurrent);
            this.tabPageAdvanced.Controls.Add(this.checkSchedulerDeleteTask);
            this.tabPageAdvanced.Controls.Add(this.numericSchedulerMaxRetries);
            this.tabPageAdvanced.Controls.Add(this.labelSchedulerMaxRetries);
            this.tabPageAdvanced.Controls.Add(this.checkSchedulerRetry);
            this.tabPageAdvanced.Controls.Add(this.checkSchedulerASAP);
            this.tabPageAdvanced.Controls.Add(this.comboSchedulerFolder);
            this.tabPageAdvanced.Controls.Add(this.labelSchedulerFolder);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // checkAddTaskPrefix
            // 
            resources.ApplyResources(this.checkAddTaskPrefix, "checkAddTaskPrefix");
            this.checkAddTaskPrefix.Checked = true;
            this.checkAddTaskPrefix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAddTaskPrefix.Name = "checkAddTaskPrefix";
            this.checkAddTaskPrefix.UseVisualStyleBackColor = true;
            // 
            // labelTaskName
            // 
            resources.ApplyResources(this.labelTaskName, "labelTaskName");
            this.labelTaskName.Name = "labelTaskName";
            // 
            // textTaskName
            // 
            resources.ApplyResources(this.textTaskName, "textTaskName");
            this.textTaskName.Name = "textTaskName";
            // 
            // timeSpanSchedulerDeleteTaskAfter
            // 
            resources.ApplyResources(this.timeSpanSchedulerDeleteTaskAfter, "timeSpanSchedulerDeleteTaskAfter");
            this.timeSpanSchedulerDeleteTaskAfter.MaxDays = 999;
            this.timeSpanSchedulerDeleteTaskAfter.Name = "timeSpanSchedulerDeleteTaskAfter";
            this.timeSpanSchedulerDeleteTaskAfter.Value = System.TimeSpan.Parse("00:00:00");
            // 
            // timeSpanSchedulerRetry
            // 
            resources.ApplyResources(this.timeSpanSchedulerRetry, "timeSpanSchedulerRetry");
            this.timeSpanSchedulerRetry.MaxDays = 0;
            this.timeSpanSchedulerRetry.Name = "timeSpanSchedulerRetry";
            this.timeSpanSchedulerRetry.Value = System.TimeSpan.Parse("00:00:00");
            // 
            // comboSchedulerAlreadyRunning
            // 
            resources.ApplyResources(this.comboSchedulerAlreadyRunning, "comboSchedulerAlreadyRunning");
            this.comboSchedulerAlreadyRunning.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSchedulerAlreadyRunning.FormattingEnabled = true;
            this.comboSchedulerAlreadyRunning.Items.AddRange(new object[] {
            resources.GetString("comboSchedulerAlreadyRunning.Items"),
            resources.GetString("comboSchedulerAlreadyRunning.Items1"),
            resources.GetString("comboSchedulerAlreadyRunning.Items2"),
            resources.GetString("comboSchedulerAlreadyRunning.Items3")});
            this.comboSchedulerAlreadyRunning.Name = "comboSchedulerAlreadyRunning";
            // 
            // labelSchedulerConcurrent
            // 
            resources.ApplyResources(this.labelSchedulerConcurrent, "labelSchedulerConcurrent");
            this.labelSchedulerConcurrent.Name = "labelSchedulerConcurrent";
            // 
            // checkSchedulerDeleteTask
            // 
            resources.ApplyResources(this.checkSchedulerDeleteTask, "checkSchedulerDeleteTask");
            this.checkSchedulerDeleteTask.Name = "checkSchedulerDeleteTask";
            this.checkSchedulerDeleteTask.UseVisualStyleBackColor = true;
            this.checkSchedulerDeleteTask.CheckedChanged += new System.EventHandler(this.checkSchedulerDeleteTask_CheckedChanged);
            // 
            // numericSchedulerMaxRetries
            // 
            resources.ApplyResources(this.numericSchedulerMaxRetries, "numericSchedulerMaxRetries");
            this.numericSchedulerMaxRetries.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericSchedulerMaxRetries.Name = "numericSchedulerMaxRetries";
            // 
            // labelSchedulerMaxRetries
            // 
            resources.ApplyResources(this.labelSchedulerMaxRetries, "labelSchedulerMaxRetries");
            this.labelSchedulerMaxRetries.Name = "labelSchedulerMaxRetries";
            // 
            // checkSchedulerRetry
            // 
            resources.ApplyResources(this.checkSchedulerRetry, "checkSchedulerRetry");
            this.checkSchedulerRetry.Name = "checkSchedulerRetry";
            this.checkSchedulerRetry.UseVisualStyleBackColor = true;
            this.checkSchedulerRetry.CheckedChanged += new System.EventHandler(this.checkSchedulerRetry_CheckedChanged);
            // 
            // checkSchedulerASAP
            // 
            resources.ApplyResources(this.checkSchedulerASAP, "checkSchedulerASAP");
            this.checkSchedulerASAP.Name = "checkSchedulerASAP";
            this.checkSchedulerASAP.UseVisualStyleBackColor = true;
            // 
            // comboSchedulerFolder
            // 
            resources.ApplyResources(this.comboSchedulerFolder, "comboSchedulerFolder");
            this.comboSchedulerFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSchedulerFolder.FormattingEnabled = true;
            this.comboSchedulerFolder.Name = "comboSchedulerFolder";
            // 
            // labelSchedulerFolder
            // 
            resources.ApplyResources(this.labelSchedulerFolder, "labelSchedulerFolder");
            this.labelSchedulerFolder.Name = "labelSchedulerFolder";
            // 
            // labelProgramSchedule
            // 
            resources.ApplyResources(this.labelProgramSchedule, "labelProgramSchedule");
            this.labelProgramSchedule.AutoEllipsis = true;
            this.labelProgramSchedule.Name = "labelProgramSchedule";
            this.labelProgramSchedule.UseMnemonic = false;
            // 
            // labelProgramDescription
            // 
            resources.ApplyResources(this.labelProgramDescription, "labelProgramDescription");
            this.labelProgramDescription.AutoEllipsis = true;
            this.labelProgramDescription.Name = "labelProgramDescription";
            this.labelProgramDescription.UseMnemonic = false;
            // 
            // labelChannelName
            // 
            resources.ApplyResources(this.labelChannelName, "labelChannelName");
            this.labelChannelName.AutoEllipsis = true;
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.UseMnemonic = false;
            // 
            // pictureChannelLogo
            // 
            resources.ApplyResources(this.pictureChannelLogo, "pictureChannelLogo");
            this.pictureChannelLogo.BackColor = System.Drawing.SystemColors.Control;
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.TabStop = false;
            // 
            // RecordChannelDialog
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CancelButton = this.buttonCancel;
            this.CausesValidation = false;
            this.Controls.Add(this.labelProgramSchedule);
            this.Controls.Add(this.labelProgramDescription);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.pictureChannelLogo);
            this.Controls.Add(this.tabProperties);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordChannelDialog";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.DialogRecordChannel_Load);
            this.Shown += new System.EventHandler(this.DialogRecordChannel_Shown);
            this.tabProperties.ResumeLayout(false);
            this.tabPageSchedule.ResumeLayout(false);
            this.tabPageSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStartMargin)).EndInit();
            this.tabPageLength.ResumeLayout(false);
            this.tabPageSave.ResumeLayout(false);
            this.tabPageSave.PerformLayout();
            this.tabPageDescription.ResumeLayout(false);
            this.tabPageDescription.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSchedulerMaxRetries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabProperties;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.Label labelStartMarginSufix;
        private System.Windows.Forms.NumericUpDown numericStartMargin;
        private System.Windows.Forms.CheckBox checkBoxStartMargin;
        private IpTviewr.UiServices.Record.Controls.RecordingSchedule recordingSchedule;
        private System.Windows.Forms.TabPage tabPageLength;
        private IpTviewr.UiServices.Record.Controls.RecordingDuration recordingTime;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.CheckBox checkAppendRecordingDetails;
        private System.Windows.Forms.TextBox textTaskDescription;
        private System.Windows.Forms.Label labelTaskDescription;
        private System.Windows.Forms.TabPage tabPageSave;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Label labelSaveLocation;
        private System.Windows.Forms.ComboBox comboFileExtension;
        private IpTviewr.UiServices.Common.Controls.FilenameTextBox textFilename;
        private System.Windows.Forms.Button buttonSelectFolder;
        private IpTviewr.UiServices.Common.Controls.ListViewSortable listViewLocations;
        private System.Windows.Forms.ComboBox comboSchedulerFolder;
        private System.Windows.Forms.Label labelSchedulerFolder;
        private System.Windows.Forms.ComboBox comboSchedulerAlreadyRunning;
        private System.Windows.Forms.Label labelSchedulerConcurrent;
        private System.Windows.Forms.CheckBox checkSchedulerDeleteTask;
        private System.Windows.Forms.NumericUpDown numericSchedulerMaxRetries;
        private System.Windows.Forms.Label labelSchedulerMaxRetries;
        private System.Windows.Forms.CheckBox checkSchedulerRetry;
        private System.Windows.Forms.CheckBox checkSchedulerASAP;
        private System.Windows.Forms.ImageList imageListLocations;
        private System.Windows.Forms.DateTimePicker dateTimeExpiryDate;
        private System.Windows.Forms.CheckBox checkBoxExpiryDate;
        private IpTviewr.UiServices.Common.Controls.TimeSpanUpDown timeSpanSchedulerRetry;
        private IpTviewr.UiServices.Common.Controls.TimeSpanUpDown timeSpanSchedulerDeleteTaskAfter;
        private IpTviewr.Native.WinForms.SelectFolderDialog selectFolder;
        private System.Windows.Forms.CheckBox checkAddTaskPrefix;
        private System.Windows.Forms.Label labelTaskName;
        private Common.Controls.FilenameTextBox textTaskName;
        private System.Windows.Forms.Label labelProgramSchedule;
        private System.Windows.Forms.Label labelProgramDescription;
        private System.Windows.Forms.Label labelChannelName;
        private Common.Controls.PictureBoxEx pictureChannelLogo;
    }
}
