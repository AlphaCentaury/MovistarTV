namespace IpTviewr.Internal.Tools.ChannelLogos
{
    partial class FormStart
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioLogosGrid = new System.Windows.Forms.RadioButton();
            this.radioConsistency = new System.Windows.Forms.RadioButton();
            this.buttonGo = new System.Windows.Forms.Button();
            this.labelLoadingConfiguration = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a tool:";
            // 
            // radioLogosGrid
            // 
            this.radioLogosGrid.AutoSize = true;
            this.radioLogosGrid.Checked = true;
            this.radioLogosGrid.Location = new System.Drawing.Point(12, 34);
            this.radioLogosGrid.Name = "radioLogosGrid";
            this.radioLogosGrid.Size = new System.Drawing.Size(74, 17);
            this.radioLogosGrid.TabIndex = 1;
            this.radioLogosGrid.TabStop = true;
            this.radioLogosGrid.Text = "Logos grid";
            this.radioLogosGrid.UseVisualStyleBackColor = true;
            // 
            // radioConsistency
            // 
            this.radioConsistency.AutoSize = true;
            this.radioConsistency.Location = new System.Drawing.Point(12, 57);
            this.radioConsistency.Name = "radioConsistency";
            this.radioConsistency.Size = new System.Drawing.Size(120, 17);
            this.radioConsistency.TabIndex = 2;
            this.radioConsistency.Text = "Consistency checks";
            this.radioConsistency.UseVisualStyleBackColor = true;
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(12, 92);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(100, 25);
            this.buttonGo.TabIndex = 3;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // labelLoadingConfiguration
            // 
            this.labelLoadingConfiguration.AutoSize = true;
            this.labelLoadingConfiguration.Location = new System.Drawing.Point(12, 239);
            this.labelLoadingConfiguration.Name = "labelLoadingConfiguration";
            this.labelLoadingConfiguration.Size = new System.Drawing.Size(109, 13);
            this.labelLoadingConfiguration.TabIndex = 4;
            this.labelLoadingConfiguration.Text = "(Config load progress)";
            this.labelLoadingConfiguration.Visible = false;
            // 
            // FormStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.labelLoadingConfiguration);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.radioConsistency);
            this.Controls.Add(this.radioLogosGrid);
            this.Controls.Add(this.label1);
            this.Name = "FormStart";
            this.Text = "Select tool - Channel logos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioLogosGrid;
        private System.Windows.Forms.RadioButton radioConsistency;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Label labelLoadingConfiguration;
    }
}