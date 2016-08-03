// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.Common;
using Project.IpTv.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Common.Forms
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

        private void AboutBox_Load(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this, Assembly.GetEntryAssembly().GetName().Name);
            this.Text = String.Format(this.Text, Assembly.GetEntryAssembly().GetName().Name);
            if (ApplicationData != null)
            {
                if (ApplicationData.LargeIcon != null)
                {
                    if (logoPictureBox.Image != null) logoPictureBox.Image.Dispose();
                    logoPictureBox.Image = ApplicationData.LargeIcon;
                } // if
                labelAppName.Text = string.Format("{0}", ApplicationData.Name);
                labelAppVersion.Text = string.Format("{0} - {1}", ApplicationData.Version, ApplicationData.Status);
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
            labelVersion.Text = String.Format(labelVersion.Text, AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
        } // AboutBox_Load

        private void linkLabelCodeplex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(this, "http://movistartv.codeplex.com");
        } // linkLabelCodeplex_LinkClicked

        private void OpenUrl(Form parent, string url)
        {
            Launcher.OpenUrl(parent, url, HandleException, null);
        } // OpenUrl

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetCallingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
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
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
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
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
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
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
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
