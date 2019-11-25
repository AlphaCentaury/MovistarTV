// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
{
    public partial class AboutBox : CommonBaseForm
    {
        public AboutBox()
        {
            InitializeComponent();
        } // constructor

        public AboutBoxApplicationData ApplicationData
        {
            get;
            set;
        } // ApplicationData

        protected override bool SendLoadEvent => false;

        private void AboutBox_Load(object sender, EventArgs e)
        {
            AppTelemetry.FormLoad(this, Assembly.GetEntryAssembly().GetName().Name);
            Text = string.Format(Text, Assembly.GetEntryAssembly().GetName().Name);
            if (ApplicationData != null)
            {
                if (ApplicationData.LargeIcon != null)
                {
                    logoPictureBox.Image?.Dispose();
                    logoPictureBox.Image = ApplicationData.LargeIcon;
                } // if
                labelAppName.Text = $"{ApplicationData.Name}";
                labelAppVersion.Text = $"{ApplicationData.Version} - {ApplicationData.Status}";
                if (ApplicationData.LicenseTextRtf != null)
                {
                    textBoxDescription.Rtf = ApplicationData.LicenseTextRtf;
                }
                else
                {
                    textBoxDescription.Text = ApplicationData.LicenseText;
                } // if-else
            }
            else
            {
                labelAppName.Text = AssemblyTitle;
                labelAppVersion.Text = AssemblyVersion;
                labelEULA.Visible = false;
                textBoxDescription.Visible = false;
            } // if-else

            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = string.Format(labelVersion.Text, AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
        } // AboutBox_Load

        private void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(this, sender as LinkLabel);
        } // linkLabelWebsite_LinkClicked

        private void LinkLabelSourceCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(this, sender as LinkLabel);
        } // LinkLabelSourceCode_LinkClicked

        private void OpenUrl(Form parent, LinkLabel link)
        {
            if (link == null) return;

            var uri = new UriBuilder(new Uri(link.Text));
            if (uri.Scheme == "http") uri.Scheme = "https";
            Launcher.OpenUrl(parent, uri.ToString(), HandleException, null);
        } // OpenUrl

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetCallingAssembly().GetName().Version.ToString();

        public string AssemblyDescription
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    } // class AboutBox
} // namespace
