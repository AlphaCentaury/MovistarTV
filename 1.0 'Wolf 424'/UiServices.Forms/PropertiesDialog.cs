// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

// v1.0 RC 0: Moved from ChannelList > PropertiesDlg.cs

using Project.DvbIpTv.UiServices.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace Project.DvbIpTv.UiServices.Forms
{
    public partial class PropertiesDialog : Form
    {
        public IEnumerable<Property> Properties { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public Image Logo { get; set; }

        public PropertiesDialog()
        {
            InitializeComponent();
        } // constructor

        private void PropertiesDialog_Load(object sender, EventArgs e)
        {
            this.Text = Caption;
            this.labelDescription.Text = (Description ?? Texts.PropertiesDlgCaptionDefault);
            this.pictureBoxEx1.Image = Logo;
        } // PropertiesDialog_Load

        private void PropertiesDialog_Shown(object sender, EventArgs e)
        {
            foreach (var property in Properties)
            {
                AddProperty(property.Key, property.Value);
            } // foreach
        } // PropertiesDialog_Shown

        private void AddProperty(string name, string value)
        {
            ListViewItem item;

            item = listViewProperties.Items.Add(name ?? Texts.PropertiesDlgNameNull);
            item.UseItemStyleForSubItems = false;
            if (value != null)
            {
                item.SubItems.Add(value);
            }
            else
            {
                item.SubItems.Add(Texts.PropertiesDlgValueNull);
                item.SubItems[1].BackColor = Color.LightYellow;
            } // if-else
        } // AddProperty
    } // class PropertiesDlg
} // namespace
