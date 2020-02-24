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

namespace IpTviewr.UiServices.Configuration.Editors
{
    partial class ArgumentEditor
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
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArgumentEditor));
            System.Windows.Forms.ColumnHeader columnHeader2;
            this.textBoxCommandLine = new System.Windows.Forms.TextBox();
            this.groupBoxParameters = new System.Windows.Forms.GroupBox();
            this.labelAddParam = new System.Windows.Forms.Label();
            this.buttonAddParam = new System.Windows.Forms.Button();
            this.listParameters = new System.Windows.Forms.ListView();
            columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            resources.ApplyResources(columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(columnHeader2, "columnHeader2");
            // 
            // textBoxCommandLine
            // 
            resources.ApplyResources(this.textBoxCommandLine, "textBoxCommandLine");
            this.textBoxCommandLine.Name = "textBoxCommandLine";
            this.textBoxCommandLine.TextChanged += new System.EventHandler(this.textBoxCommandLine_TextChanged);
            // 
            // groupBoxParameters
            // 
            resources.ApplyResources(this.groupBoxParameters, "groupBoxParameters");
            this.groupBoxParameters.Controls.Add(this.labelAddParam);
            this.groupBoxParameters.Controls.Add(this.buttonAddParam);
            this.groupBoxParameters.Controls.Add(this.listParameters);
            this.groupBoxParameters.Name = "groupBoxParameters";
            this.groupBoxParameters.TabStop = false;
            // 
            // labelAddParam
            // 
            resources.ApplyResources(this.labelAddParam, "labelAddParam");
            this.labelAddParam.Name = "labelAddParam";
            // 
            // buttonAddParam
            // 
            resources.ApplyResources(this.buttonAddParam, "buttonAddParam");
            this.buttonAddParam.Image = global::IpTviewr.UiServices.Configuration.Properties.Resources.Action_AddVariable_16;
            this.buttonAddParam.Name = "buttonAddParam";
            this.buttonAddParam.UseVisualStyleBackColor = true;
            this.buttonAddParam.Click += new System.EventHandler(this.buttonAddParam_Click);
            // 
            // listParameters
            // 
            resources.ApplyResources(this.listParameters, "listParameters");
            this.listParameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2});
            this.listParameters.FullRowSelect = true;
            this.listParameters.GridLines = true;
            this.listParameters.HideSelection = false;
            this.listParameters.MultiSelect = false;
            this.listParameters.Name = "listParameters";
            this.listParameters.ShowItemToolTips = true;
            this.listParameters.UseCompatibleStateImageBehavior = false;
            this.listParameters.View = System.Windows.Forms.View.Details;
            this.listParameters.SelectedIndexChanged += new System.EventHandler(this.listParameters_SelectedIndexChanged);
            this.listParameters.DoubleClick += new System.EventHandler(this.listParameters_DoubleClick);
            // 
            // ArgumentEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxParameters);
            this.Controls.Add(this.textBoxCommandLine);
            this.Name = "ArgumentEditor";
            this.Load += new System.EventHandler(this.ParametersEditor_Load);
            this.groupBoxParameters.ResumeLayout(false);
            this.groupBoxParameters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCommandLine;
        private System.Windows.Forms.GroupBox groupBoxParameters;
        private System.Windows.Forms.Button buttonAddParam;
        private System.Windows.Forms.Label labelAddParam;
        private System.Windows.Forms.ListView listParameters;
    } // class ArgumentEditor
} // namespace
