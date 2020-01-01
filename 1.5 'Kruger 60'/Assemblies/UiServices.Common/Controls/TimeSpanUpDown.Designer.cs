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

namespace IpTviewr.UiServices.Common.Controls
{
    partial class TimeSpanUpDown
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
            this.labelTimeSpanS = new System.Windows.Forms.Label();
            this.numericTimeSpanSeconds = new System.Windows.Forms.NumericUpDown();
            this.labelTimeSpanM = new System.Windows.Forms.Label();
            this.numericTimeSpanMinutes = new System.Windows.Forms.NumericUpDown();
            this.labelTimeSpanH = new System.Windows.Forms.Label();
            this.numericTimeSpanHours = new System.Windows.Forms.NumericUpDown();
            this.labelTimeSpanD = new System.Windows.Forms.Label();
            this.numericTimeSpanDays = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanDays)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTimeSpanS
            // 
            this.labelTimeSpanS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTimeSpanS.Location = new System.Drawing.Point(228, 3);
            this.labelTimeSpanS.Name = "labelTimeSpanS";
            this.labelTimeSpanS.Size = new System.Drawing.Size(23, 13);
            this.labelTimeSpanS.TabIndex = 7;
            this.labelTimeSpanS.Text = "mm";
            // 
            // numericTimeSpanSeconds
            // 
            this.numericTimeSpanSeconds.CausesValidation = false;
            this.numericTimeSpanSeconds.Location = new System.Drawing.Point(192, 0);
            this.numericTimeSpanSeconds.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericTimeSpanSeconds.Name = "numericTimeSpanSeconds";
            this.numericTimeSpanSeconds.Size = new System.Drawing.Size(35, 20);
            this.numericTimeSpanSeconds.TabIndex = 6;
            this.numericTimeSpanSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericTimeSpanSeconds.ValueChanged += new System.EventHandler(this.numericTimeSpanSeconds_ValueChanged);
            // 
            // labelTimeSpanM
            // 
            this.labelTimeSpanM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTimeSpanM.Location = new System.Drawing.Point(168, 3);
            this.labelTimeSpanM.Name = "labelTimeSpanM";
            this.labelTimeSpanM.Size = new System.Drawing.Size(23, 13);
            this.labelTimeSpanM.TabIndex = 5;
            this.labelTimeSpanM.Text = "mm";
            // 
            // numericTimeSpanMinutes
            // 
            this.numericTimeSpanMinutes.CausesValidation = false;
            this.numericTimeSpanMinutes.Location = new System.Drawing.Point(132, 0);
            this.numericTimeSpanMinutes.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericTimeSpanMinutes.Name = "numericTimeSpanMinutes";
            this.numericTimeSpanMinutes.Size = new System.Drawing.Size(35, 20);
            this.numericTimeSpanMinutes.TabIndex = 4;
            this.numericTimeSpanMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericTimeSpanMinutes.ValueChanged += new System.EventHandler(this.numericTimeSpanMinutes_ValueChanged);
            // 
            // labelTimeSpanH
            // 
            this.labelTimeSpanH.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTimeSpanH.Location = new System.Drawing.Point(107, 3);
            this.labelTimeSpanH.Name = "labelTimeSpanH";
            this.labelTimeSpanH.Size = new System.Drawing.Size(23, 13);
            this.labelTimeSpanH.TabIndex = 3;
            this.labelTimeSpanH.Text = "mm";
            // 
            // numericTimeSpanHours
            // 
            this.numericTimeSpanHours.CausesValidation = false;
            this.numericTimeSpanHours.Location = new System.Drawing.Point(66, 0);
            this.numericTimeSpanHours.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericTimeSpanHours.Name = "numericTimeSpanHours";
            this.numericTimeSpanHours.Size = new System.Drawing.Size(40, 20);
            this.numericTimeSpanHours.TabIndex = 2;
            this.numericTimeSpanHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericTimeSpanHours.ValueChanged += new System.EventHandler(this.numericTimeSpanHours_ValueChanged);
            // 
            // labelTimeSpanD
            // 
            this.labelTimeSpanD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTimeSpanD.Location = new System.Drawing.Point(41, 2);
            this.labelTimeSpanD.Name = "labelTimeSpanD";
            this.labelTimeSpanD.Size = new System.Drawing.Size(23, 13);
            this.labelTimeSpanD.TabIndex = 1;
            this.labelTimeSpanD.Text = "mm";
            // 
            // numericTimeSpanDays
            // 
            this.numericTimeSpanDays.CausesValidation = false;
            this.numericTimeSpanDays.Location = new System.Drawing.Point(0, 0);
            this.numericTimeSpanDays.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericTimeSpanDays.Name = "numericTimeSpanDays";
            this.numericTimeSpanDays.Size = new System.Drawing.Size(40, 20);
            this.numericTimeSpanDays.TabIndex = 0;
            this.numericTimeSpanDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericTimeSpanDays.ValueChanged += new System.EventHandler(this.numericTimeSpanDays_ValueChanged);
            // 
            // TimeSpanUpDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTimeSpanD);
            this.Controls.Add(this.numericTimeSpanDays);
            this.Controls.Add(this.labelTimeSpanS);
            this.Controls.Add(this.numericTimeSpanSeconds);
            this.Controls.Add(this.labelTimeSpanM);
            this.Controls.Add(this.numericTimeSpanMinutes);
            this.Controls.Add(this.labelTimeSpanH);
            this.Controls.Add(this.numericTimeSpanHours);
            this.Name = "TimeSpanUpDown";
            this.Size = new System.Drawing.Size(253, 20);
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeSpanDays)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTimeSpanS;
        private System.Windows.Forms.NumericUpDown numericTimeSpanSeconds;
        private System.Windows.Forms.Label labelTimeSpanM;
        private System.Windows.Forms.NumericUpDown numericTimeSpanMinutes;
        private System.Windows.Forms.Label labelTimeSpanH;
        private System.Windows.Forms.NumericUpDown numericTimeSpanHours;
        private System.Windows.Forms.Label labelTimeSpanD;
        private System.Windows.Forms.NumericUpDown numericTimeSpanDays;
    }
}
