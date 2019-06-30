// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Telemetry;
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
