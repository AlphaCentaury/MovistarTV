namespace Project.DvbIpTv.ChannelList
{
    partial class PropertiesDlg
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
            System.Windows.Forms.ColumnHeader Property;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesDlg));
            System.Windows.Forms.ColumnHeader Value;
            this.listViewProperties = new System.Windows.Forms.ListView();
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxEx1 = new Project.DvbIpTv.ChannelList.Controls.PictureBoxEx();
            Property = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // Property
            // 
            resources.ApplyResources(Property, "Property");
            // 
            // Value
            // 
            resources.ApplyResources(Value, "Value");
            // 
            // listViewProperties
            // 
            resources.ApplyResources(this.listViewProperties, "listViewProperties");
            this.listViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Property,
            Value});
            this.listViewProperties.GridLines = true;
            this.listViewProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewProperties.MultiSelect = false;
            this.listViewProperties.Name = "listViewProperties";
            this.listViewProperties.UseCompatibleStateImageBehavior = false;
            this.listViewProperties.View = System.Windows.Forms.View.Details;
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.AutoEllipsis = true;
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.UseMnemonic = false;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureBoxEx1
            // 
            resources.ApplyResources(this.pictureBoxEx1, "pictureBoxEx1");
            this.pictureBoxEx1.Name = "pictureBoxEx1";
            this.pictureBoxEx1.TabStop = false;
            // 
            // PropertiesDlg
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOk;
            this.Controls.Add(this.pictureBoxEx1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.listViewProperties);
            this.MinimizeBox = false;
            this.Name = "PropertiesDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PropertiesDlg_Load);
            this.Shown += new System.EventHandler(this.PropertiesDlg_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewProperties;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonOk;
        private Controls.PictureBoxEx pictureBoxEx1;
    }
}