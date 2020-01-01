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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(PictureBox))]
    public class PictureBoxEx : PictureBox
    {
        public static Bitmap ToGreyscale(Image original)
        {
            // create the gray scale ColorMatrix
            var colorMatrix = new ColorMatrix(new[]
            {
                new[] {.3f, .3f, .3f, 0, 0},
                new[] {.59f, .59f, .59f, 0, 0},
                new[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            // set the color matrix to an image attributes object
            var attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            // create a new bitmap with the same size as the original; then,
            // create a Graphics from it
            var greyscaleBitmap = new Bitmap(original.Width, original.Height);
            using (var g = Graphics.FromImage(greyscaleBitmap))
            {
                // copy the original image to the new image (by means of the Graphics) using the color matrix
                g.DrawImage(original,
                    new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel, attributes);
            } // using
            
            return greyscaleBitmap;
        } // ToGreyscale

        public void SetImage(Image image)
        {
            Image?.Dispose();
            Image = image;
        } // SetImage

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            base.OnPaint(pe);
        } // OnPaint
    } // class PictureBoxEx
} // namespace
