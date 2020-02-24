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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class RtfViewer : Form
    {
        public RtfViewer()
        {
            InitializeComponent();
        }

        private void RtfViewer_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent("FileDrop", true) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void RtfViewer_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData("FileDrop", true);
            if ((data is string[] files) && (files.Length > 0))
            {
                var rtf = File.ReadAllText(files[0], Encoding.ASCII);
                if (rtf.StartsWith(@"{\rtf"))
                {
                    richTextBox1.Rtf = rtf;
                    return;
                } // if
            } // if
            e.Effect = DragDropEffects.None;
        }
    }
}
