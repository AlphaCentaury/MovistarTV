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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    partial class LicensingDataViewer
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Licensing file", -2, -2);
            this.splitContainerLicensing = new System.Windows.Forms.SplitContainer();
            this.treeViewLicensingData = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertiesViewer = new IpTviewr.UiServices.Common.Forms.PropertiesViewer();
            this.tabControlLicensingDetailsText = new System.Windows.Forms.TabControl();
            this.tabPageLicensingRaw = new System.Windows.Forms.TabPage();
            this.textBoxLicensingText = new System.Windows.Forms.TextBox();
            this.tabPageLicensingHtml = new System.Windows.Forms.TabPage();
            this.htmlPanelLicensingText = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.tabPageLicensingRtf = new System.Windows.Forms.TabPage();
            this.richTextBoxLicensingText = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLicensing)).BeginInit();
            this.splitContainerLicensing.Panel1.SuspendLayout();
            this.splitContainerLicensing.Panel2.SuspendLayout();
            this.splitContainerLicensing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlLicensingDetailsText.SuspendLayout();
            this.tabPageLicensingRaw.SuspendLayout();
            this.tabPageLicensingHtml.SuspendLayout();
            this.tabPageLicensingRtf.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerLicensing
            // 
            this.splitContainerLicensing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLicensing.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLicensing.Name = "splitContainerLicensing";
            // 
            // splitContainerLicensing.Panel1
            // 
            this.splitContainerLicensing.Panel1.Controls.Add(this.treeViewLicensingData);
            // 
            // splitContainerLicensing.Panel2
            // 
            this.splitContainerLicensing.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainerLicensing.Size = new System.Drawing.Size(400, 400);
            this.splitContainerLicensing.SplitterDistance = 186;
            this.splitContainerLicensing.TabIndex = 1;
            // 
            // treeViewLicensingData
            // 
            this.treeViewLicensingData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLicensingData.FullRowSelect = true;
            this.treeViewLicensingData.HideSelection = false;
            this.treeViewLicensingData.Location = new System.Drawing.Point(0, 0);
            this.treeViewLicensingData.Name = "treeViewLicensingData";
            treeNode1.ImageIndex = -2;
            treeNode1.Name = "DummySolutionNode";
            treeNode1.SelectedImageIndex = -2;
            treeNode1.Text = "Licensing file";
            this.treeViewLicensingData.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewLicensingData.Size = new System.Drawing.Size(186, 400);
            this.treeViewLicensingData.TabIndex = 0;
            this.treeViewLicensingData.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLicensingData_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertiesViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlLicensingDetailsText);
            this.splitContainer1.Size = new System.Drawing.Size(210, 400);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 1;
            // 
            // propertiesViewer
            // 
            this.propertiesViewer.AutoResizeColumns = true;
            this.propertiesViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesViewer.Location = new System.Drawing.Point(0, 0);
            this.propertiesViewer.Name = "propertiesViewer";
            this.propertiesViewer.Size = new System.Drawing.Size(210, 200);
            this.propertiesViewer.TabIndex = 0;
            this.propertiesViewer.PropertySelected += new System.EventHandler<IpTviewr.UiServices.Common.Controls.PropertySelectedEventArgs>(this.propertiesViewer_PropertySelected);
            // 
            // tabControlLicensingDetailsText
            // 
            this.tabControlLicensingDetailsText.Controls.Add(this.tabPageLicensingRaw);
            this.tabControlLicensingDetailsText.Controls.Add(this.tabPageLicensingHtml);
            this.tabControlLicensingDetailsText.Controls.Add(this.tabPageLicensingRtf);
            this.tabControlLicensingDetailsText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLicensingDetailsText.Location = new System.Drawing.Point(0, 0);
            this.tabControlLicensingDetailsText.Name = "tabControlLicensingDetailsText";
            this.tabControlLicensingDetailsText.SelectedIndex = 0;
            this.tabControlLicensingDetailsText.Size = new System.Drawing.Size(210, 196);
            this.tabControlLicensingDetailsText.TabIndex = 0;
            // 
            // tabPageLicensingRaw
            // 
            this.tabPageLicensingRaw.Controls.Add(this.textBoxLicensingText);
            this.tabPageLicensingRaw.Location = new System.Drawing.Point(4, 22);
            this.tabPageLicensingRaw.Name = "tabPageLicensingRaw";
            this.tabPageLicensingRaw.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLicensingRaw.Size = new System.Drawing.Size(202, 170);
            this.tabPageLicensingRaw.TabIndex = 0;
            this.tabPageLicensingRaw.Text = "Raw content";
            this.tabPageLicensingRaw.UseVisualStyleBackColor = true;
            // 
            // textBoxLicensingText
            // 
            this.textBoxLicensingText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLicensingText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLicensingText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLicensingText.Location = new System.Drawing.Point(3, 3);
            this.textBoxLicensingText.Multiline = true;
            this.textBoxLicensingText.Name = "textBoxLicensingText";
            this.textBoxLicensingText.ReadOnly = true;
            this.textBoxLicensingText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLicensingText.Size = new System.Drawing.Size(196, 164);
            this.textBoxLicensingText.TabIndex = 0;
            // 
            // tabPageLicensingHtml
            // 
            this.tabPageLicensingHtml.Controls.Add(this.htmlPanelLicensingText);
            this.tabPageLicensingHtml.Location = new System.Drawing.Point(4, 22);
            this.tabPageLicensingHtml.Name = "tabPageLicensingHtml";
            this.tabPageLicensingHtml.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLicensingHtml.Size = new System.Drawing.Size(202, 170);
            this.tabPageLicensingHtml.TabIndex = 1;
            this.tabPageLicensingHtml.Text = "MD/HTML";
            this.tabPageLicensingHtml.UseVisualStyleBackColor = true;
            // 
            // htmlPanelLicensingText
            // 
            this.htmlPanelLicensingText.AutoScroll = true;
            this.htmlPanelLicensingText.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanelLicensingText.BaseStylesheet = null;
            this.htmlPanelLicensingText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlPanelLicensingText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanelLicensingText.Location = new System.Drawing.Point(3, 3);
            this.htmlPanelLicensingText.Name = "htmlPanelLicensingText";
            this.htmlPanelLicensingText.Size = new System.Drawing.Size(196, 164);
            this.htmlPanelLicensingText.TabIndex = 0;
            this.htmlPanelLicensingText.Text = null;
            // 
            // tabPageLicensingRtf
            // 
            this.tabPageLicensingRtf.Controls.Add(this.richTextBoxLicensingText);
            this.tabPageLicensingRtf.Location = new System.Drawing.Point(4, 22);
            this.tabPageLicensingRtf.Name = "tabPageLicensingRtf";
            this.tabPageLicensingRtf.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLicensingRtf.Size = new System.Drawing.Size(202, 170);
            this.tabPageLicensingRtf.TabIndex = 2;
            this.tabPageLicensingRtf.Text = "RTF";
            this.tabPageLicensingRtf.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLicensingText
            // 
            this.richTextBoxLicensingText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLicensingText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLicensingText.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxLicensingText.Name = "richTextBoxLicensingText";
            this.richTextBoxLicensingText.ReadOnly = true;
            this.richTextBoxLicensingText.Size = new System.Drawing.Size(196, 164);
            this.richTextBoxLicensingText.TabIndex = 0;
            this.richTextBoxLicensingText.Text = "";
            // 
            // LicensingDataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerLicensing);
            this.Name = "LicensingDataViewer";
            this.Size = new System.Drawing.Size(400, 400);
            this.splitContainerLicensing.Panel1.ResumeLayout(false);
            this.splitContainerLicensing.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLicensing)).EndInit();
            this.splitContainerLicensing.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlLicensingDetailsText.ResumeLayout(false);
            this.tabPageLicensingRaw.ResumeLayout(false);
            this.tabPageLicensingRaw.PerformLayout();
            this.tabPageLicensingHtml.ResumeLayout(false);
            this.tabPageLicensingRtf.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerLicensing;
        private System.Windows.Forms.TreeView treeViewLicensingData;
        private System.Windows.Forms.TabControl tabControlLicensingDetailsText;
        private System.Windows.Forms.TabPage tabPageLicensingRaw;
        private System.Windows.Forms.TextBox textBoxLicensingText;
        private System.Windows.Forms.TabPage tabPageLicensingHtml;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private IpTviewr.UiServices.Common.Forms.PropertiesViewer propertiesViewer;
        private System.Windows.Forms.TabPage tabPageLicensingRtf;
        private System.Windows.Forms.RichTextBox richTextBoxLicensingText;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanelLicensingText;
    }
}
