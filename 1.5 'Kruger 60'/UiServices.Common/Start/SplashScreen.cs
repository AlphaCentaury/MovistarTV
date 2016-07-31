// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Common.Start
{
    internal partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.Text = Path.GetFileName(Application.ExecutablePath);
        }  // constructor
    } // class SplashScreen
} // namespace
