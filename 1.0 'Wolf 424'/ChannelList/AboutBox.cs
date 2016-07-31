// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        } // constructor

        private void AboutBox_Load(object sender, EventArgs e)
        {
            this.Text = String.Format(this.Text, Assembly.GetEntryAssembly().GetName().Name);
            labelAppName.Text = string.Format("{0}", Properties.Texts.AppName);
            labelAppVersion.Text = string.Format("{0} - {1}", Properties.Texts.AppVersion, Properties.Texts.AppState);
            labelProductName.Text = AssemblyProduct;
            labelVersion.Text = String.Format(labelVersion.Text, AssemblyVersion);
            labelCopyright.Text = AssemblyCopyright;
            labelCompanyName.Text = AssemblyCompany;
            textBoxDescription.Rtf = Properties.Texts.SolutionLicenseRtf;
        } // AboutBox_Load

        private void linkLabelCodeplex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl(this, "http://movistartv.codeplex.com");
        } // linkLabelCodeplex_LinkClicked

        public static void OpenUrl(Form parent, string url)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = url,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent.Handle,
                    };
                    process.Start();
                } // using process
            }
            catch (Exception ex)
            {
                MessageBox.Show(parent,
                    string.Format(Properties.Texts.OpenUrlError, url, ex.ToString()),
                    parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } // try-catch
        } // OpenUrl

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
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
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
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
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
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
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
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
