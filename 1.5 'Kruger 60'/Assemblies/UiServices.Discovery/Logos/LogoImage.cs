// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Discovery.Logos
{
    public abstract class LogoImage
    {
        public LogoImage(PackedLogo logo)
        {
            Key = logo.Key;
            Positions = logo.Positions;
        } // constructor

        public static Bitmap GetMissingLogoBitmap(LogoSize size)
        {
            switch (size)
            {
                case LogoSize.Pixel16: return Properties.Resources.MissingLogo16;
                case LogoSize.Pixel24: return Properties.Resources.MissingLogo24;
                case LogoSize.Pixel32: return Properties.Resources.MissingLogo32;
                case LogoSize.Pixel48: return Properties.Resources.MissingLogo48;
                case LogoSize.Pixel64: return Properties.Resources.MissingLogo64;
                case LogoSize.Pixel96: return Properties.Resources.MissingLogo96;
                case LogoSize.Pixel128: return Properties.Resources.MissingLogo128;
                case LogoSize.Pixel256: return Properties.Resources.MissingLogo256;
                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            } // switch
        } // GetMissingLogoBitmap

        public string Key
        {
            get;
            private set;
        } // Key

        public string Kind
        {
            get;
            private set;
        } // Kind

        private PackedLogoPos[] Positions
        {
            get;
            set;
        } // Positions

        public Bitmap Get(LogoSize size)
        {
            var index = Array.BinarySearch(Positions, new PackedLogoPos(size.GetPixelSize()));

            if (index < 0)
            {
                if (size >= LogoSize.Pixel32) return GetMissingLogoBitmap(size);
                if (size == LogoSize.Pixel16) return Get(16);
                return Get(24);
            }
            else
            {
                throw new NotImplementedException();
            } // if-else
        } // Get

        public Bitmap Get(int size)
        {
            Bitmap original = null;
            Bitmap resized = null;

            try
            {
                original = Get(LogoSizeUtils.ClosestSize(size));
                resized = new Bitmap(size, size);
                resized.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                using (var g = Graphics.FromImage(resized))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(original, 0, 0, size, size);
                } // using g

                var result = resized;
                resized = null;

                return result;
            }
            finally
            {
                if (original != null) original.Dispose();
                if (resized != null) resized.Dispose();
            } // try-finally
        } // Get

        protected abstract Bitmap GetPackedLogosBitmap();

        protected virtual Bitmap LoadLogo(PackedLogoPos position, LogoSize size)
        {
            Stream input = null;

            try
            {
                input = new FileStream(GetPackedLogosFile(), FileMode.Open, FileAccess.Read, FileShare.Read);
                var result = new Bitmap();
            }
            finally
            {

            } // try-finally
        } // LoadLogo
    } // LogoImage
} // namespace
