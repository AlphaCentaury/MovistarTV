// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Forms
{
    public partial class NotImplementedBox : Form
    {
        public NotImplementedBox()
        {
            InitializeComponent();
        } // constructor

        public static void ShowBox(IWin32Window owner, string context)
        {
            using (var box = new NotImplementedBox())
            {
                if (context == null)
                {
                    context = owner.GetType().Name;
                }
                else
                {
                    string.Format("{0}/{1}", owner.GetType().Name, context);
                } // if-else

                BasicGoogleTelemetry.SendScreenHit(box, context);
                box.ShowDialog(owner);
            } // using
        } // ShowBox
    } // class NotImplementedBox
} // namespace
