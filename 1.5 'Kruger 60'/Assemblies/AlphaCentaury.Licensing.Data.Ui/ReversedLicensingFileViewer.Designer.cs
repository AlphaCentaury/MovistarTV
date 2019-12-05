namespace AlphaCentaury.Licensing.Data.Ui
{
    partial class ReversedLicensingFileViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReversedLicensingFileViewer));
            this.splitContainerHorizontal = new System.Windows.Forms.SplitContainer();
            this.splitContainerVerticalTop = new System.Windows.Forms.SplitContainer();
            this.listViewLicenses = new System.Windows.Forms.ListView();
            this.columnHeaderLicenseName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelLicenses = new System.Windows.Forms.Label();
            this.listViewComponents = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelAppliesTo = new System.Windows.Forms.Label();
            this.splitContainerVerticalBottom = new System.Windows.Forms.SplitContainer();
            this.richTextBoxLicense = new System.Windows.Forms.RichTextBox();
            this.listViewProperties = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListLicenses = new System.Windows.Forms.ImageList(this.components);
            this.imageListComponents = new System.Windows.Forms.ImageList(this.components);
            this.imageListProperties = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHorizontal)).BeginInit();
            this.splitContainerHorizontal.Panel1.SuspendLayout();
            this.splitContainerHorizontal.Panel2.SuspendLayout();
            this.splitContainerHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVerticalTop)).BeginInit();
            this.splitContainerVerticalTop.Panel1.SuspendLayout();
            this.splitContainerVerticalTop.Panel2.SuspendLayout();
            this.splitContainerVerticalTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVerticalBottom)).BeginInit();
            this.splitContainerVerticalBottom.Panel1.SuspendLayout();
            this.splitContainerVerticalBottom.Panel2.SuspendLayout();
            this.splitContainerVerticalBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerHorizontal
            // 
            resources.ApplyResources(this.splitContainerHorizontal, "splitContainerHorizontal");
            this.splitContainerHorizontal.Name = "splitContainerHorizontal";
            // 
            // splitContainerHorizontal.Panel1
            // 
            this.splitContainerHorizontal.Panel1.Controls.Add(this.splitContainerVerticalTop);
            // 
            // splitContainerHorizontal.Panel2
            // 
            this.splitContainerHorizontal.Panel2.Controls.Add(this.splitContainerVerticalBottom);
            // 
            // splitContainerVerticalTop
            // 
            resources.ApplyResources(this.splitContainerVerticalTop, "splitContainerVerticalTop");
            this.splitContainerVerticalTop.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerVerticalTop.Name = "splitContainerVerticalTop";
            // 
            // splitContainerVerticalTop.Panel1
            // 
            this.splitContainerVerticalTop.Panel1.Controls.Add(this.listViewLicenses);
            this.splitContainerVerticalTop.Panel1.Controls.Add(this.labelLicenses);
            // 
            // splitContainerVerticalTop.Panel2
            // 
            this.splitContainerVerticalTop.Panel2.Controls.Add(this.listViewComponents);
            this.splitContainerVerticalTop.Panel2.Controls.Add(this.labelAppliesTo);
            // 
            // listViewLicenses
            // 
            resources.ApplyResources(this.listViewLicenses, "listViewLicenses");
            this.listViewLicenses.CausesValidation = false;
            this.listViewLicenses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLicenseName});
            this.listViewLicenses.FullRowSelect = true;
            this.listViewLicenses.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewLicenses.HideSelection = false;
            this.listViewLicenses.Name = "listViewLicenses";
            this.listViewLicenses.UseCompatibleStateImageBehavior = false;
            this.listViewLicenses.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderLicenseName
            // 
            resources.ApplyResources(this.columnHeaderLicenseName, "columnHeaderLicenseName");
            // 
            // labelLicenses
            // 
            resources.ApplyResources(this.labelLicenses, "labelLicenses");
            this.labelLicenses.Name = "labelLicenses";
            // 
            // listViewComponents
            // 
            resources.ApplyResources(this.listViewComponents, "listViewComponents");
            this.listViewComponents.CausesValidation = false;
            this.listViewComponents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewComponents.FullRowSelect = true;
            this.listViewComponents.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewComponents.Groups"))),
            ((System.Windows.Forms.ListViewGroup)(resources.GetObject("listViewComponents.Groups1")))});
            this.listViewComponents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewComponents.HideSelection = false;
            this.listViewComponents.Name = "listViewComponents";
            this.listViewComponents.UseCompatibleStateImageBehavior = false;
            this.listViewComponents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // labelAppliesTo
            // 
            resources.ApplyResources(this.labelAppliesTo, "labelAppliesTo");
            this.labelAppliesTo.Name = "labelAppliesTo";
            // 
            // splitContainerVerticalBottom
            // 
            resources.ApplyResources(this.splitContainerVerticalBottom, "splitContainerVerticalBottom");
            this.splitContainerVerticalBottom.Name = "splitContainerVerticalBottom";
            // 
            // splitContainerVerticalBottom.Panel1
            // 
            this.splitContainerVerticalBottom.Panel1.Controls.Add(this.richTextBoxLicense);
            // 
            // splitContainerVerticalBottom.Panel2
            // 
            this.splitContainerVerticalBottom.Panel2.Controls.Add(this.listViewProperties);
            // 
            // richTextBoxLicense
            // 
            resources.ApplyResources(this.richTextBoxLicense, "richTextBoxLicense");
            this.richTextBoxLicense.AutoWordSelection = true;
            this.richTextBoxLicense.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxLicense.CausesValidation = false;
            this.richTextBoxLicense.HideSelection = false;
            this.richTextBoxLicense.Name = "richTextBoxLicense";
            this.richTextBoxLicense.ReadOnly = true;
            // 
            // listViewProperties
            // 
            resources.ApplyResources(this.listViewProperties, "listViewProperties");
            this.listViewProperties.CausesValidation = false;
            this.listViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewProperties.FullRowSelect = true;
            this.listViewProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewProperties.HideSelection = false;
            this.listViewProperties.Name = "listViewProperties";
            this.listViewProperties.UseCompatibleStateImageBehavior = false;
            this.listViewProperties.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // imageListLicenses
            // 
            this.imageListLicenses.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListLicenses, "imageListLicenses");
            this.imageListLicenses.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListComponents
            // 
            this.imageListComponents.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListComponents, "imageListComponents");
            this.imageListComponents.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListProperties
            // 
            this.imageListProperties.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListProperties, "imageListProperties");
            this.imageListProperties.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ReversedLicensingFileViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerHorizontal);
            this.Name = "ReversedLicensingFileViewer";
            this.splitContainerHorizontal.Panel1.ResumeLayout(false);
            this.splitContainerHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerHorizontal)).EndInit();
            this.splitContainerHorizontal.ResumeLayout(false);
            this.splitContainerVerticalTop.Panel1.ResumeLayout(false);
            this.splitContainerVerticalTop.Panel1.PerformLayout();
            this.splitContainerVerticalTop.Panel2.ResumeLayout(false);
            this.splitContainerVerticalTop.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVerticalTop)).EndInit();
            this.splitContainerVerticalTop.ResumeLayout(false);
            this.splitContainerVerticalBottom.Panel1.ResumeLayout(false);
            this.splitContainerVerticalBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerVerticalBottom)).EndInit();
            this.splitContainerVerticalBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerHorizontal;
        private System.Windows.Forms.SplitContainer splitContainerVerticalTop;
        private System.Windows.Forms.ListView listViewLicenses;
        private System.Windows.Forms.ColumnHeader columnHeaderLicenseName;
        private System.Windows.Forms.Label labelLicenses;
        private System.Windows.Forms.ListView listViewComponents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label labelAppliesTo;
        private System.Windows.Forms.SplitContainer splitContainerVerticalBottom;
        private System.Windows.Forms.RichTextBox richTextBoxLicense;
        private System.Windows.Forms.ListView listViewProperties;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ImageList imageListLicenses;
        private System.Windows.Forms.ImageList imageListComponents;
        private System.Windows.Forms.ImageList imageListProperties;
    }
}
