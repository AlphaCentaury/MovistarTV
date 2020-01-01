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
    partial class RecordingDailyScheduleFragment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordingDailyScheduleFragment));
            this.labelRecurSufix = new System.Windows.Forms.Label();
            this.labelRecurPrefix = new System.Windows.Forms.Label();
            this.numericRecurEvery = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericRecurEvery)).BeginInit();
            this.SuspendLayout();
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
            // SchedulePatternDaily
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericRecurEvery);
            this.Controls.Add(this.labelRecurSufix);
            this.Controls.Add(this.labelRecurPrefix);
            this.Name = "SchedulePatternDaily";
            this.Load += new System.EventHandler(this.SchedulePatternDaily_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericRecurEvery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRecurSufix;
        private System.Windows.Forms.Label labelRecurPrefix;
        private System.Windows.Forms.NumericUpDown numericRecurEvery;
    }
}
