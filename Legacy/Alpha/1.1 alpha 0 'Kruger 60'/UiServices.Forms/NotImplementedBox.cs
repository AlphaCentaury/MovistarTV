// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.UiServices.Forms
{
    public partial class NotImplementedBox : Form
    {
        public NotImplementedBox()
        {
            InitializeComponent();
        } // constructor

        public static void ShowBox(IWin32Window owner)
        {
            using (var box = new NotImplementedBox())
            {
                box.ShowDialog(owner);
            } // using
        } // ShowBox
    } // class NotImplementedBox
} // namespace
