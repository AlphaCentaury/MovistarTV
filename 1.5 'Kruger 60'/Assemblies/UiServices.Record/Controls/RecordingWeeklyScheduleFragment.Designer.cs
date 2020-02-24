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

namespace IpTviewr.UiServices.Record.Controls
{
    partial class RecordingWeeklyScheduleFragment
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordingWeeklyScheduleFragment));
            this.numericRecurEvery = new System.Windows.Forms.NumericUpDown();
            this.labelRecurSufix = new System.Windows.Forms.Label();
            this.labelRecurPrefix = new System.Windows.Forms.Label();
            this.checkedListDays = new System.Windows.Forms.CheckedListBox();
            this.checkAllDays = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericRecurEvery)).BeginInit();
            this.SuspendLayout();
            // 
            // numericRecurEvery
            // 
            resources.ApplyResources(this.numericRecurEvery, "numericRecurEvery");
            this.numericRecurEvery.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericRecurEvery.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericRecurEvery.Name = "numericRecurEvery";
            this.numericRecurEvery.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelRecurSufix
            // 
            resources.ApplyResources(this.labelRecurSufix, "labelRecurSufix");
            this.labelRecurSufix.Name = "labelRecurSufix";
            // 
            // labelRecurPrefix
            // 
            resources.ApplyResources(this.labelRecurPrefix, "labelRecurPrefix");
            this.labelRecurPrefix.Name = "labelRecurPrefix";
            // 
            // checkedListDays
            // 
            resources.ApplyResources(this.checkedListDays, "checkedListDays");
            this.checkedListDays.CheckOnClick = true;
            this.checkedListDays.FormattingEnabled = true;
            this.checkedListDays.Name = "checkedListDays";
            this.checkedListDays.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListDays_ItemCheck);
            this.checkedListDays.Validating += new System.ComponentModel.CancelEventHandler(this.checkedListDays_Validating);
            // 
            // checkAllDays
            // 
            resources.ApplyResources(this.checkAllDays, "checkAllDays");
            this.checkAllDays.Name = "checkAllDays";
            this.checkAllDays.UseVisualStyleBackColor = true;
            this.checkAllDays.CheckedChanged += new System.EventHandler(this.checkAllDays_CheckedChanged);
            // 
            // RecordingWeeklyScheduleFragment
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkAllDays);
            this.Controls.Add(this.checkedListDays);
            this.Controls.Add(this.numericRecurEvery);
            this.Controls.Add(this.labelRecurSufix);
            this.Controls.Add(this.labelRecurPrefix);
            this.Name = "RecordingWeeklyScheduleFragment";
            this.Load += new System.EventHandler(this.SchedulePatternWeekly_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericRecurEvery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericRecurEvery;
        private System.Windows.Forms.Label labelRecurSufix;
        private System.Windows.Forms.Label labelRecurPrefix;
        private System.Windows.Forms.CheckedListBox checkedListDays;
        private System.Windows.Forms.CheckBox checkAllDays;
    }
}
