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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch
{
    partial class BatchDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxTools = new System.Windows.Forms.ComboBox();
            this.listBatch = new System.Windows.Forms.ListView();
            this.columnHeaderTool = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderArguments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.toolStripForm = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.executeStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyStripButton = new System.Windows.Forms.ToolStripButton();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeStripButton = new System.Windows.Forms.ToolStripButton();
            this.openBatchDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveBatchDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonUsage = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerRefreshOutput = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.toolStripForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tool:";
            // 
            // comboBoxTools
            // 
            this.comboBoxTools.DisplayMember = "Name";
            this.comboBoxTools.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTools.FormattingEnabled = true;
            this.comboBoxTools.Location = new System.Drawing.Point(49, 31);
            this.comboBoxTools.Name = "comboBoxTools";
            this.comboBoxTools.Size = new System.Drawing.Size(320, 21);
            this.comboBoxTools.TabIndex = 1;
            this.comboBoxTools.SelectedIndexChanged += new System.EventHandler(this.comboBoxTools_SelectedIndexChanged);
            // 
            // listBatch
            // 
            this.listBatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTool,
            this.columnHeaderArguments});
            this.listBatch.FullRowSelect = true;
            this.listBatch.GridLines = true;
            this.listBatch.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listBatch.HideSelection = false;
            this.listBatch.Location = new System.Drawing.Point(12, 59);
            this.listBatch.MultiSelect = false;
            this.listBatch.Name = "listBatch";
            this.listBatch.ShowItemToolTips = true;
            this.listBatch.Size = new System.Drawing.Size(494, 172);
            this.listBatch.TabIndex = 8;
            this.listBatch.UseCompatibleStateImageBehavior = false;
            this.listBatch.View = System.Windows.Forms.View.Details;
            this.listBatch.SelectedIndexChanged += new System.EventHandler(this.listBatch_SelectedIndexChanged);
            this.listBatch.DoubleClick += new System.EventHandler(this.listBatch_DoubleClick);
            // 
            // columnHeaderTool
            // 
            this.columnHeaderTool.Text = "Tool";
            this.columnHeaderTool.Width = 150;
            // 
            // columnHeaderArguments
            // 
            this.columnHeaderArguments.Text = "Arguments";
            this.columnHeaderArguments.Width = 310;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxResults);
            this.groupBox1.Location = new System.Drawing.Point(12, 237);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(600, 192);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Execution result";
            // 
            // textBoxResults
            // 
            this.textBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResults.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxResults.Location = new System.Drawing.Point(6, 19);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResults.Size = new System.Drawing.Size(588, 167);
            this.textBoxResults.TabIndex = 0;
            this.textBoxResults.WordWrap = false;
            // 
            // toolStripForm
            // 
            this.toolStripForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator,
            this.executeStripButton,
            this.copyStripButton,
            this.cutToolStripButton,
            this.toolStripSeparator1,
            this.closeStripButton});
            this.toolStripForm.Location = new System.Drawing.Point(0, 0);
            this.toolStripForm.Name = "toolStripForm";
            this.toolStripForm.Size = new System.Drawing.Size(624, 25);
            this.toolStripForm.TabIndex = 14;
            this.toolStripForm.Text = "toolStrip1";
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(51, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.ToolTipText = "Create new batch (Ctrl+N)";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(56, 22);
            this.openToolStripButton.Text = "&Open";
            this.openToolStripButton.ToolTipText = "Open batch file (Ctrl+O)";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(51, 22);
            this.saveToolStripButton.Text = "&Save";
            this.saveToolStripButton.ToolTipText = "Save batch to file (Ctrl+S)";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // executeStripButton
            // 
            this.executeStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Go_16x16;
            this.executeStripButton.Name = "executeStripButton";
            this.executeStripButton.Size = new System.Drawing.Size(91, 22);
            this.executeStripButton.Text = "E&xecute (F5)";
            this.executeStripButton.ToolTipText = "Execute batch (F5)";
            this.executeStripButton.Click += new System.EventHandler(this.executeStripButton_Click);
            // 
            // copyStripButton
            // 
            this.copyStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyStripButton.Image = ((System.Drawing.Image)(resources.GetObject("copyStripButton.Image")));
            this.copyStripButton.Name = "copyStripButton";
            this.copyStripButton.Size = new System.Drawing.Size(23, 22);
            this.copyStripButton.Text = "Copy";
            this.copyStripButton.ToolTipText = "Copy execution results";
            this.copyStripButton.Click += new System.EventHandler(this.copyStripButton_Click);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Delete_16x16;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "Cle&ar";
            this.cutToolStripButton.ToolTipText = "Clear execution result";
            this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // closeStripButton
            // 
            this.closeStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Close_16x16;
            this.closeStripButton.Name = "closeStripButton";
            this.closeStripButton.Size = new System.Drawing.Size(72, 22);
            this.closeStripButton.Text = "CANCEL";
            this.closeStripButton.ToolTipText = "Cancels current execution";
            // 
            // openBatchDialog
            // 
            this.openBatchDialog.Filter = "Batch files (*.batch)|*.batch";
            this.openBatchDialog.RestoreDirectory = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // saveBatchDialog
            // 
            this.saveBatchDialog.Filter = "Batch files (*.batch)|*.batch";
            this.saveBatchDialog.RestoreDirectory = true;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Edit_16x16;
            this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEdit.Location = new System.Drawing.Point(512, 59);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(100, 25);
            this.buttonEdit.TabIndex = 9;
            this.buttonEdit.Text = "&Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonUsage
            // 
            this.buttonUsage.FlatAppearance.BorderSize = 0;
            this.buttonUsage.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Status_Unknown_16x616;
            this.buttonUsage.Location = new System.Drawing.Point(375, 28);
            this.buttonUsage.Name = "buttonUsage";
            this.buttonUsage.Size = new System.Drawing.Size(25, 25);
            this.buttonUsage.TabIndex = 2;
            this.buttonUsage.UseVisualStyleBackColor = true;
            this.buttonUsage.Click += new System.EventHandler(this.buttonUsage_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveDown.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_GoNextDown_16x16;
            this.buttonMoveDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMoveDown.Location = new System.Drawing.Point(512, 206);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(100, 25);
            this.buttonMoveDown.TabIndex = 12;
            this.buttonMoveDown.Text = "Move &down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoveUp.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_GoPreviousUp_16x16;
            this.buttonMoveUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonMoveUp.Location = new System.Drawing.Point(512, 175);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(100, 25);
            this.buttonMoveUp.TabIndex = 11;
            this.buttonMoveUp.Text = "Move &up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Delete_16x16;
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.Location = new System.Drawing.Point(512, 90);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(100, 25);
            this.buttonRemove.TabIndex = 10;
            this.buttonRemove.Text = "&Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Add_16xM;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(406, 28);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(100, 25);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "&Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // timerRefreshOutput
            // 
            this.timerRefreshOutput.Interval = 1500;
            // 
            // BatchDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonUsage);
            this.Controls.Add(this.toolStripForm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.listBatch);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxTools);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "BatchDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch creation and execution";
            this.Load += new System.EventHandler(this.BatchDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStripForm.ResumeLayout(false);
            this.toolStripForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxTools;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ListView listBatch;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxResults;
        private System.Windows.Forms.ToolStrip toolStripForm;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton executeStripButton;
        private System.Windows.Forms.ToolStripButton copyStripButton;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton closeStripButton;
        private System.Windows.Forms.ColumnHeader columnHeaderTool;
        private System.Windows.Forms.ColumnHeader columnHeaderArguments;
        private System.Windows.Forms.Button buttonUsage;
        private System.Windows.Forms.OpenFileDialog openBatchDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveBatchDialog;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer timerRefreshOutput;
    }
}
