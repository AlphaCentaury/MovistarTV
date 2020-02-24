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

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    partial class FormSvgLogosExporter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSvgLogosExporter));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.labelTo = new System.Windows.Forms.Label();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.checkBoxExportAll = new System.Windows.Forms.CheckBox();
            this.radioIsProviders = new System.Windows.Forms.RadioButton();
            this.radioIsServices = new System.Windows.Forms.RadioButton();
            this.groupBoxSizes = new System.Windows.Forms.GroupBox();
            this.checkBoxSize256 = new System.Windows.Forms.CheckBox();
            this.checkBoxSize128 = new System.Windows.Forms.CheckBox();
            this.checkBoxSize96 = new System.Windows.Forms.CheckBox();
            this.checkBoxSize64 = new System.Windows.Forms.CheckBox();
            this.checkBoxSize48 = new System.Windows.Forms.CheckBox();
            this.checkBoxSize32 = new System.Windows.Forms.CheckBox();
            this.checkBoxSize24 = new System.Windows.Forms.CheckBox();
            this.checkBoxRebuildAllIcons = new System.Windows.Forms.CheckBox();
            this.checkBoxIcons = new System.Windows.Forms.CheckBox();
            this.checkBoxExport = new System.Windows.Forms.CheckBox();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxInkscape = new System.Windows.Forms.TextBox();
            this.labelInkscape = new System.Windows.Forms.Label();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.timerOuput = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.groupBoxSizes.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageOptions);
            this.tabControlMain.Controls.Add(this.tabPageOutput);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(784, 388);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.buttonExecute);
            this.tabPageOptions.Controls.Add(this.textBoxTo);
            this.tabPageOptions.Controls.Add(this.labelTo);
            this.tabPageOptions.Controls.Add(this.groupBoxActions);
            this.tabPageOptions.Controls.Add(this.textBoxFrom);
            this.tabPageOptions.Controls.Add(this.labelFrom);
            this.tabPageOptions.Controls.Add(this.textBoxInkscape);
            this.tabPageOptions.Controls.Add(this.labelInkscape);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 22);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(776, 362);
            this.tabPageOptions.TabIndex = 0;
            this.tabPageOptions.Text = "Options";
            this.tabPageOptions.UseVisualStyleBackColor = true;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(273, 89);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(100, 25);
            this.buttonExecute.TabIndex = 7;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // textBoxTo
            // 
            this.textBoxTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTo.Location = new System.Drawing.Point(115, 58);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(655, 20);
            this.textBoxTo.TabIndex = 5;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(8, 61);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(101, 13);
            this.labelTo.TabIndex = 4;
            this.labelTo.Text = "Logos output folder:";
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.checkBoxExportAll);
            this.groupBoxActions.Controls.Add(this.radioIsProviders);
            this.groupBoxActions.Controls.Add(this.radioIsServices);
            this.groupBoxActions.Controls.Add(this.groupBoxSizes);
            this.groupBoxActions.Controls.Add(this.checkBoxRebuildAllIcons);
            this.groupBoxActions.Controls.Add(this.checkBoxIcons);
            this.groupBoxActions.Controls.Add(this.checkBoxExport);
            this.groupBoxActions.Location = new System.Drawing.Point(6, 84);
            this.groupBoxActions.MinimumSize = new System.Drawing.Size(244, 233);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(261, 262);
            this.groupBoxActions.TabIndex = 6;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // checkBoxExportAll
            // 
            this.checkBoxExportAll.AutoSize = true;
            this.checkBoxExportAll.Location = new System.Drawing.Point(26, 180);
            this.checkBoxExportAll.Name = "checkBoxExportAll";
            this.checkBoxExportAll.Size = new System.Drawing.Size(90, 17);
            this.checkBoxExportAll.TabIndex = 4;
            this.checkBoxExportAll.Text = "Export all files";
            this.checkBoxExportAll.UseVisualStyleBackColor = true;
            // 
            // radioIsProviders
            // 
            this.radioIsProviders.AutoSize = true;
            this.radioIsProviders.Location = new System.Drawing.Point(113, 19);
            this.radioIsProviders.Name = "radioIsProviders";
            this.radioIsProviders.Size = new System.Drawing.Size(69, 17);
            this.radioIsProviders.TabIndex = 1;
            this.radioIsProviders.Text = "Providers";
            this.radioIsProviders.UseVisualStyleBackColor = true;
            // 
            // radioIsServices
            // 
            this.radioIsServices.AutoSize = true;
            this.radioIsServices.Checked = true;
            this.radioIsServices.Location = new System.Drawing.Point(15, 19);
            this.radioIsServices.Name = "radioIsServices";
            this.radioIsServices.Size = new System.Drawing.Size(66, 17);
            this.radioIsServices.TabIndex = 0;
            this.radioIsServices.TabStop = true;
            this.radioIsServices.Text = "Services";
            this.radioIsServices.UseVisualStyleBackColor = true;
            // 
            // groupBoxSizes
            // 
            this.groupBoxSizes.Controls.Add(this.checkBoxSize256);
            this.groupBoxSizes.Controls.Add(this.checkBoxSize128);
            this.groupBoxSizes.Controls.Add(this.checkBoxSize96);
            this.groupBoxSizes.Controls.Add(this.checkBoxSize64);
            this.groupBoxSizes.Controls.Add(this.checkBoxSize48);
            this.groupBoxSizes.Controls.Add(this.checkBoxSize32);
            this.groupBoxSizes.Controls.Add(this.checkBoxSize24);
            this.groupBoxSizes.Location = new System.Drawing.Point(26, 74);
            this.groupBoxSizes.Name = "groupBoxSizes";
            this.groupBoxSizes.Size = new System.Drawing.Size(212, 100);
            this.groupBoxSizes.TabIndex = 3;
            this.groupBoxSizes.TabStop = false;
            this.groupBoxSizes.Text = "Export sizes";
            // 
            // checkBoxSize256
            // 
            this.checkBoxSize256.AutoSize = true;
            this.checkBoxSize256.Checked = true;
            this.checkBoxSize256.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize256.Location = new System.Drawing.Point(138, 19);
            this.checkBoxSize256.Name = "checkBoxSize256";
            this.checkBoxSize256.Size = new System.Drawing.Size(49, 17);
            this.checkBoxSize256.TabIndex = 6;
            this.checkBoxSize256.Text = "256x";
            this.checkBoxSize256.UseVisualStyleBackColor = true;
            // 
            // checkBoxSize128
            // 
            this.checkBoxSize128.AutoSize = true;
            this.checkBoxSize128.Checked = true;
            this.checkBoxSize128.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize128.Location = new System.Drawing.Point(75, 68);
            this.checkBoxSize128.Name = "checkBoxSize128";
            this.checkBoxSize128.Size = new System.Drawing.Size(49, 17);
            this.checkBoxSize128.TabIndex = 5;
            this.checkBoxSize128.Text = "128x";
            this.checkBoxSize128.UseVisualStyleBackColor = true;
            // 
            // checkBoxSize96
            // 
            this.checkBoxSize96.AutoSize = true;
            this.checkBoxSize96.Checked = true;
            this.checkBoxSize96.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize96.Location = new System.Drawing.Point(75, 45);
            this.checkBoxSize96.Name = "checkBoxSize96";
            this.checkBoxSize96.Size = new System.Drawing.Size(43, 17);
            this.checkBoxSize96.TabIndex = 4;
            this.checkBoxSize96.Text = "96x";
            this.checkBoxSize96.UseVisualStyleBackColor = true;
            // 
            // checkBoxSize64
            // 
            this.checkBoxSize64.AutoSize = true;
            this.checkBoxSize64.Checked = true;
            this.checkBoxSize64.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize64.Location = new System.Drawing.Point(75, 19);
            this.checkBoxSize64.Name = "checkBoxSize64";
            this.checkBoxSize64.Size = new System.Drawing.Size(43, 17);
            this.checkBoxSize64.TabIndex = 3;
            this.checkBoxSize64.Text = "64x";
            this.checkBoxSize64.UseVisualStyleBackColor = true;
            // 
            // checkBoxSize48
            // 
            this.checkBoxSize48.AutoSize = true;
            this.checkBoxSize48.Checked = true;
            this.checkBoxSize48.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize48.Location = new System.Drawing.Point(12, 68);
            this.checkBoxSize48.Name = "checkBoxSize48";
            this.checkBoxSize48.Size = new System.Drawing.Size(43, 17);
            this.checkBoxSize48.TabIndex = 2;
            this.checkBoxSize48.Text = "48x";
            this.checkBoxSize48.UseVisualStyleBackColor = true;
            // 
            // checkBoxSize32
            // 
            this.checkBoxSize32.AutoSize = true;
            this.checkBoxSize32.Checked = true;
            this.checkBoxSize32.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize32.Location = new System.Drawing.Point(12, 45);
            this.checkBoxSize32.Name = "checkBoxSize32";
            this.checkBoxSize32.Size = new System.Drawing.Size(43, 17);
            this.checkBoxSize32.TabIndex = 1;
            this.checkBoxSize32.Text = "32x";
            this.checkBoxSize32.UseVisualStyleBackColor = true;
            // 
            // checkBoxSize24
            // 
            this.checkBoxSize24.AutoSize = true;
            this.checkBoxSize24.Checked = true;
            this.checkBoxSize24.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSize24.Location = new System.Drawing.Point(12, 19);
            this.checkBoxSize24.Name = "checkBoxSize24";
            this.checkBoxSize24.Size = new System.Drawing.Size(43, 17);
            this.checkBoxSize24.TabIndex = 0;
            this.checkBoxSize24.Text = "24x";
            this.checkBoxSize24.UseVisualStyleBackColor = true;
            // 
            // checkBoxRebuildAllIcons
            // 
            this.checkBoxRebuildAllIcons.AutoSize = true;
            this.checkBoxRebuildAllIcons.Location = new System.Drawing.Point(36, 234);
            this.checkBoxRebuildAllIcons.Name = "checkBoxRebuildAllIcons";
            this.checkBoxRebuildAllIcons.Size = new System.Drawing.Size(75, 17);
            this.checkBoxRebuildAllIcons.TabIndex = 6;
            this.checkBoxRebuildAllIcons.Text = "Rebuild all";
            this.checkBoxRebuildAllIcons.UseVisualStyleBackColor = true;
            // 
            // checkBoxIcons
            // 
            this.checkBoxIcons.AutoSize = true;
            this.checkBoxIcons.Checked = true;
            this.checkBoxIcons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIcons.Location = new System.Drawing.Point(15, 211);
            this.checkBoxIcons.Name = "checkBoxIcons";
            this.checkBoxIcons.Size = new System.Drawing.Size(105, 17);
            this.checkBoxIcons.TabIndex = 5;
            this.checkBoxIcons.Text = "Build logos icons";
            this.checkBoxIcons.UseVisualStyleBackColor = true;
            this.checkBoxIcons.CheckedChanged += new System.EventHandler(this.checkBoxIcons_CheckedChanged);
            // 
            // checkBoxExport
            // 
            this.checkBoxExport.AutoSize = true;
            this.checkBoxExport.Checked = true;
            this.checkBoxExport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExport.Location = new System.Drawing.Point(15, 51);
            this.checkBoxExport.Name = "checkBoxExport";
            this.checkBoxExport.Size = new System.Drawing.Size(140, 17);
            this.checkBoxExport.TabIndex = 2;
            this.checkBoxExport.Text = "Export SVG files to PNG";
            this.checkBoxExport.UseVisualStyleBackColor = true;
            this.checkBoxExport.CheckedChanged += new System.EventHandler(this.checkBoxExport_CheckedChanged);
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFrom.Location = new System.Drawing.Point(115, 32);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.Size = new System.Drawing.Size(655, 20);
            this.textBoxFrom.TabIndex = 3;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(8, 35);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(73, 13);
            this.labelFrom.TabIndex = 2;
            this.labelFrom.Text = "Source folder:";
            // 
            // textBoxInkscape
            // 
            this.textBoxInkscape.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInkscape.Location = new System.Drawing.Point(115, 6);
            this.textBoxInkscape.Name = "textBoxInkscape";
            this.textBoxInkscape.Size = new System.Drawing.Size(655, 20);
            this.textBoxInkscape.TabIndex = 1;
            // 
            // labelInkscape
            // 
            this.labelInkscape.AutoSize = true;
            this.labelInkscape.Location = new System.Drawing.Point(8, 9);
            this.labelInkscape.Name = "labelInkscape";
            this.labelInkscape.Size = new System.Drawing.Size(95, 13);
            this.labelInkscape.TabIndex = 0;
            this.labelInkscape.Text = "Inkscape program:";
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.textBoxOutput);
            this.tabPageOutput.Controls.Add(this.buttonCancel);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(776, 362);
            this.tabPageOutput.TabIndex = 1;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutput.Location = new System.Drawing.Point(6, 37);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutput.Size = new System.Drawing.Size(764, 319);
            this.textBoxOutput.TabIndex = 1;
            this.textBoxOutput.WordWrap = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.Location = new System.Drawing.Point(6, 6);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 25);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // timerOuput
            // 
            this.timerOuput.Interval = 1500;
            // 
            // FormSvgLogosExporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 388);
            this.Controls.Add(this.tabControlMain);
            this.Name = "FormSvgLogosExporter";
            this.Text = "Logos: SVG exporter";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.tabPageOptions.PerformLayout();
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxActions.PerformLayout();
            this.groupBoxSizes.ResumeLayout(false);
            this.groupBoxSizes.PerformLayout();
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.GroupBox groupBoxSizes;
        private System.Windows.Forms.CheckBox checkBoxSize256;
        private System.Windows.Forms.CheckBox checkBoxSize128;
        private System.Windows.Forms.CheckBox checkBoxSize96;
        private System.Windows.Forms.CheckBox checkBoxSize64;
        private System.Windows.Forms.CheckBox checkBoxSize48;
        private System.Windows.Forms.CheckBox checkBoxSize32;
        private System.Windows.Forms.CheckBox checkBoxSize24;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.CheckBox checkBoxIcons;
        private System.Windows.Forms.CheckBox checkBoxExport;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.TextBox textBoxInkscape;
        private System.Windows.Forms.Label labelInkscape;
        private System.Windows.Forms.CheckBox checkBoxRebuildAllIcons;
        private System.Windows.Forms.Timer timerOuput;
        private System.Windows.Forms.RadioButton radioIsProviders;
        private System.Windows.Forms.RadioButton radioIsServices;
        private System.Windows.Forms.CheckBox checkBoxExportAll;
    }
}
