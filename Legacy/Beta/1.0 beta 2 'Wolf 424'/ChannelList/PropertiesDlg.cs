// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    public partial class PropertiesDlg : Form
    {
        public class Property
        {
            public string Name { get; set;}
            public string Value { get; set; }

            public Property()
            {
                // no op
            } // constructor

            public Property(string name, string value)
            {
                Name = name;
                Value = value;
            } // constructor
        } // class Property

        public IEnumerable<Property> Properties { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public Image Logo { get; set; }

        public PropertiesDlg()
        {
            InitializeComponent();
        }

        private void PropertiesDlg_Load(object sender, EventArgs e)
        {
            this.Text = Caption;
            this.labelDescription.Text = (Description ?? "Properties of selected item");
            this.pictureBoxEx1.Image = Logo;
        }

        private void PropertiesDlg_Shown(object sender, EventArgs e)
        {
            foreach (var property in Properties)
            {
                AddProperty(property.Name, property.Value);
            } // foreach
        } // PropertiesDlg_Shown

        private void AddProperty(string name, string value)
        {
            ListViewItem item;

            item = listViewProperties.Items.Add(name ?? "<unknown>");
            item.UseItemStyleForSubItems = false;
            if (value != null)
            {
                item.SubItems.Add(value);
            }
            else
            {
                item.SubItems.Add("<not specified>");
                item.SubItems[1].BackColor = Color.LightYellow;
            } // if-else
        } // AddProperty
    } // class PropertiesDlg
} // namespace
