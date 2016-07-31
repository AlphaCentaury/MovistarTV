// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

// v1.0 RC 0: Moved from ChannelList > PictureBoxEx.cs

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(PictureBox))]
    public class PictureBoxEx : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            pe.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            base.OnPaint(pe);
        } // OnPaint

        public static Bitmap ToGrayscale(Image original)
        {
            // create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(new float[][] 
            {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            // set the color matrix to an image attributes object
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            // create a new bitmap with the same size as the original; then,
            // create a Graphics from it
            Bitmap greyscaleBitmap = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(greyscaleBitmap))
            {
                // copy the original image to the new image (by means of the Graphics) using the color matrix
                g.DrawImage(original,
                    new Rectangle(0, 0, original.Width, original.Height),
                    0, 0, original.Width, original.Height,
                    GraphicsUnit.Pixel, attributes);
            } // using
            
            return greyscaleBitmap;
        } // ToGrayscale
    } // class PictureBoxEx
} // namespace
