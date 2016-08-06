// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList.Controls
{
    public class PictureBoxEx : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            base.OnPaint(pe);
        }
    }
}
