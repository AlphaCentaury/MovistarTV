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

using IpTviewr.UiServices.Configuration.Properties;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration.Push.UI
{
    internal partial class PushWarningForm : Form
    {
        private string _detailsUrl;
        private string _flagColor;
        private string _markAsRead;
        private string _dontShowAgain;

        public PushWarningForm()
        {
            InitializeComponent();
            Caption = null;
            Title = null;
            Details = null;
            DetailsUrl = null;
            FlagColor = null;

            var split = checkBoxRead.Text.Split('|');
            _markAsRead = split[0];
            _dontShowAgain = split[1];
            checkBoxRead.Text = null;
            checkBoxRead.Visible = false;
        } // constructor

        [DefaultValue(null)]
        public string Caption
        {
            get => labelCaption.Text;
            set => labelCaption.Text = value;
        } // Caption

        [DefaultValue(null)]
        public string Title
        {
            get => labelTitle.Text;
            set => labelTitle.Text = value;
        } // Title

        [DefaultValue(null)]
        public string Details
        {
            get => textBoxDetails.Text;
            set => textBoxDetails.Text = value;
        } // Details

        [DefaultValue(null)]
        public string DetailsUrl
        {
            get => _detailsUrl;
            set
            {
                _detailsUrl = value;
                buttonDetails.Visible = !string.IsNullOrEmpty(value);
            } // set
        } // DetailsUrl

        [DefaultValue(null)]
        public string FlagColor
        {
            get => _flagColor;
            set
            {
                _flagColor = value.ToLowerInvariant();
                pictureBoxEx1.Image = _flagColor switch
                {
                    "green" => Resources.Flag_Green_256x,
                    "red" => Resources.Flag_Red_256x,
                    "yellow" => Resources.Flag_Yellow_256x,
                    "download" => Resources.Flag_Downloading_256x,
                    _ => Resources.Flag_Blue_256x,
                };
            } // set
        } // FlagColor

        private void buttonDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
