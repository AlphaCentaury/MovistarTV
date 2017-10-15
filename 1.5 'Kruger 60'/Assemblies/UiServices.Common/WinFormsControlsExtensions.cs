// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common
{
    public static class WinFormsControlsExtensions
    {
        public static void ChangeImage(this Button control, Image newImage)
        {
            if (control.Image != null) control.Image.Dispose();
            control.Image = newImage;
        } // ChangeImage

        public static void ChangeImage(this PictureBox control, Image newImage)
        {
            if (control.Image != null) control.Image.Dispose();
            control.Image = newImage;
        } // ChangeImage
    } // static class WinFormsControlsExtensions
} // namespace
