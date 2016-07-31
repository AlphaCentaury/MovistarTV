// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

// v1.0 RC 0: Moved from ChannelList > PropertiesDlg.Designer.cs

namespace Project.DvbIpTv.UiServices.Forms
{
    partial class PropertiesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesDialog));
            System.Windows.Forms.ColumnHeader Value;
            this.listViewProperties = new Project.DvbIpTv.UiServices.Controls.ListViewSortable();
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxEx1 = new Project.DvbIpTv.UiServices.Controls.PictureBoxEx();
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
            this.listViewProperties.HeaderCustomFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewProperties.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewProperties.HeaderUsesCustomFont = true;
            this.listViewProperties.MultiSelect = false;
            this.listViewProperties.Name = "listViewProperties";
            this.listViewProperties.OwnerDraw = true;
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
            this.buttonOk.Image = global::Project.DvbIpTv.UiServices.Forms.Properties.SharedResources.ActionOk_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureBoxEx1
            // 
            resources.ApplyResources(this.pictureBoxEx1, "pictureBoxEx1");
            this.pictureBoxEx1.Name = "pictureBoxEx1";
            this.pictureBoxEx1.TabStop = false;
            // 
            // PropertiesDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOk;
            this.Controls.Add(this.pictureBoxEx1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.listViewProperties);
            this.MinimizeBox = false;
            this.Name = "PropertiesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PropertiesDialog_Load);
            this.Shown += new System.EventHandler(this.PropertiesDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Project.DvbIpTv.UiServices.Controls.ListViewSortable listViewProperties;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonOk;
        private Project.DvbIpTv.UiServices.Controls.PictureBoxEx pictureBoxEx1;
    }
}