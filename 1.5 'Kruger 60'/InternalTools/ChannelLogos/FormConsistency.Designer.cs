namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    partial class FormConsistency
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConsistency));
            this.comboCheck = new System.Windows.Forms.ComboBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.listViewResults = new System.Windows.Forms.ListView();
            this.imageListSeverity = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressLocal = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboCheck
            // 
            this.comboCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCheck.FormattingEnabled = true;
            this.comboCheck.Items.AddRange(new object[] {
            "-- BroadcastDiscovery",
            "01 Missing logos",
            "-- service-mappings.xml",
            "02 Unused entries",
            "03 Missing logo files",
            "04 Unused logo files"});
            this.comboCheck.Location = new System.Drawing.Point(12, 15);
            this.comboCheck.Name = "comboCheck";
            this.comboCheck.Size = new System.Drawing.Size(300, 21);
            this.comboCheck.TabIndex = 0;
            this.comboCheck.SelectedIndexChanged += new System.EventHandler(this.comboCheck_SelectedIndexChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(318, 12);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(100, 25);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Execute";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // listViewResults
            // 
            this.listViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.GridLines = true;
            this.listViewResults.HideSelection = false;
            this.listViewResults.LargeImageList = this.imageListSeverity;
            this.listViewResults.Location = new System.Drawing.Point(12, 42);
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(410, 294);
            this.listViewResults.SmallImageList = this.imageListSeverity;
            this.listViewResults.TabIndex = 2;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            // 
            // imageListSeverity
            // 
            this.imageListSeverity.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSeverity.ImageStream")));
            this.imageListSeverity.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSeverity.Images.SetKeyName(0, "Info");
            this.imageListSeverity.Images.SetKeyName(1, "Ok");
            this.imageListSeverity.Images.SetKeyName(2, "Warning");
            this.imageListSeverity.Images.SetKeyName(3, "Error");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.progressLocal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(434, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoToolTip = true;
            this.labelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.labelStatus.Size = new System.Drawing.Size(342, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressLocal
            // 
            this.progressLocal.Name = "progressLocal";
            this.progressLocal.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressLocal.Size = new System.Drawing.Size(75, 16);
            this.progressLocal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // FormConsistency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listViewResults);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.comboCheck);
            this.Name = "FormConsistency";
            this.Text = "Consistency checks - Channel logos";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboCheck;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.ListView listViewResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripProgressBar progressLocal;
        private System.Windows.Forms.ImageList imageListSeverity;
    }
}