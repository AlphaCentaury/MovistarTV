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
                context = context == null ? owner.GetType().FullName : $"{owner.GetType().FullName}/{context}";

                AppTelemetry.FormEvent(AppTelemetry.LoadEvent, box, context);
                box.ShowDialog(owner);
            } // using
        } // ShowBox

        private void NotImplementedBox_Load(object sender, System.EventArgs e)
        {
            labelCaption.Text = Owner?.Text;
        } // NotImplementedBox_Load
    } // class NotImplementedBox
} // namespace
