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

using System.Drawing;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common
{
    public static class WinFormsControlsExtensions
    {
        public static void ChangeImage(this Button control, Image newImage)
        {
            control.Image?.Dispose();
            control.Image = newImage;
        } // ChangeImage

        public static void ChangeImage(this PictureBox control, Image newImage)
        {
            control.Image?.Dispose();
            control.Image = newImage;
        } // ChangeImage
    } // static class WinFormsControlsExtensions
} // namespace
