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

using IpTviewr.Native.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace IpTviewr.Native
{
    /// <summary>
    /// This class allows to create icons in Windows .ico file-format
    /// </summary>
    /// <remarks>
    /// Only 32bpp ARGB images are supported
    /// </remarks>
    public partial class WindowsIcon: IDisposable
    {
        private Dictionary<IconKind, Bitmap> _images;

        public enum SaveAs
        {
            Auto = 0,
            Bmp = 1,
            Png = 2
        } // SaveAs

        public WindowsIcon()
        {
            _images = new Dictionary<IconKind, Bitmap>();
        } // constructor

        public WindowsIcon(int imageCount)
        {
            _images = new Dictionary<IconKind, Bitmap>(imageCount);
        } // constructor

        public bool AllowNonSquareImages
        {
            get;
            set;
        } // AllowNowSquareImages

        /// <summary>
        /// Adds an image to the Icon
        /// </summary>
        /// <param name="image">The image to add</param>
        /// <param name="saveAs">How the image will be stored within the icon file</param>
        /// <remarks>
        /// Only 32bpp ARGB images are supported. Therefore, the supplied image wil be converted to PixelFormat.Format32bppArgb
        /// This will cause ArgumentException to be thrown if two images of different bit depth are supplied for the same size
        /// </remarks>
        public void AddImage(Bitmap image, SaveAs saveAs = SaveAs.Auto)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if (!AllowNonSquareImages)
            {
                if ((image.Width != image.Height)) throw new ArgumentOutOfRangeException(nameof(image), Texts.WindowsIcon_NonSquare);
            } // if
            if (image.Width > 256) throw new ArgumentOutOfRangeException(nameof(image), Texts.WindowsIcon_TooBig);
            if (image.Width < 16) throw new ArgumentOutOfRangeException(nameof(image), Texts.WindowsIcon_TooSmall);

            // we only support 32bpp ARGB images; therefore, we need to convert the image to this format
            if (image.PixelFormat != PixelFormat.Format32bppArgb)
            {
                var newImage = To32BppArgbBitmap(image);
                image.Dispose();
                image = newImage;
            } // if

            // this code is retained for future enhancement
            var bitsPerPixel = GetBitsPerPixel(image.PixelFormat);
            if (bitsPerPixel > 32)
            {
                var newImage = To32BppArgbBitmap(image);
                image.Dispose();
                image = newImage;
            } // if

            // adding a duplicated image will throw ArgumentException
            _images.Add(new IconKind(image.Width, bitsPerPixel, saveAs), image);
        } // AddImage

        /// <summary>
        /// Clears all added images and disposes of them
        /// </summary>
        public void Clear()
        {
            foreach (var entry in _images) entry.Value.Dispose();
            _images.Clear();
        } // Clear

        /// <summary>
        /// Disposes the instance
        /// </summary>
        public void Dispose()
        {
            Clear();
            _images = null;
        } // Dispose

        /// <summary>
        /// Creates an .ico file format file from the supplied images
        /// </summary>
        /// <param name="path">The path of the file</param>
        public void Save(string path)
        {
            using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096))
            {
                Save(output);
            } // using output
        } // Save

        /// <summary>
        /// Saves the supplied images in .ico file format
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            // although not strictly necessary, it's customary to
            // write the images sorted by size and bpp
            var icons = from entry in _images
                        orderby entry.Key.Size ascending, entry.Key.BitsPerPixel ascending
                        select new
                        {
                            Image = entry.Value,
                            entry.Key.SaveAs
                        };

            // create icon directory header
            var iconDirHeader = new IconDirHeader(_images.Count);

            // create icon directory entries
            var entries = new IconDirEntry[_images.Count];
            var imageData = new byte[_images.Count][];

            var index = 0;
            foreach (var icon in icons)
            {
                imageData[index] = GetIconImageData(icon.Image, icon.SaveAs);
                var entry = new IconDirEntry()
                {
                    Width = (icon.Image.Width < 256) ? (byte)icon.Image.Width : (byte)0,
                    Height = (icon.Image.Height < 256) ? (byte)icon.Image.Height : (byte)0,
                    ColorCount = 0, // more than 256 colors
                    Reserved = 0, // must be zero
                    Planes = 1,
                    BitCount = GetBitsPerPixel(icon.Image.PixelFormat),
                    BytesInRes = (uint)imageData[index].Length,
                    ImageOffset = 0 // to be set later
                };
                entries[index++] = entry;
            } // foreach

            // set offsets
            var sizeOfIconDirEntry = Marshal.SizeOf<IconDirEntry>();
            var offset = Marshal.SizeOf<IconDirHeader>() + (sizeOfIconDirEntry * _images.Count);
            for (index=0;index<entries.Length;index++)
            {
                entries[index].ImageOffset = (uint)offset;
                offset += imageData[index].Length;
            } // for index

            // write data
            // 1. IconDirHeader
            WriteStruct(stream, iconDirHeader);
            
            // 2. IconDirEntries (one per icon image)
            foreach(var entry in entries)
            {
                WriteStruct(stream, entry);
            } // foreach entry

            // 3. Icon image data
            foreach (var data in imageData)
            {
                stream.Write(data, 0, data.Length);
            } // foreach
        } // Save

        private byte[] GetIconImageData(Bitmap image, SaveAs saveAs)
        {
            var estimatedCapacity = GetEstimatedIconImageSize(image);
            using (var output = new MemoryStream(estimatedCapacity))
            {
                if (saveAs == SaveAs.Auto)
                {
                    saveAs = (image.Width < 256) ? SaveAs.Bmp : SaveAs.Png;
                } // if

                switch (saveAs)
                {
                    case SaveAs.Bmp:
                        GetIconImageBitmapData(output, image);
                        break;
                    case SaveAs.Png:
                        image.Save(output, ImageFormat.Png);
                        break;
                } // switch saveAs

                output.Close();
                return output.ToArray();
            } // using
        } // GetIconImageData

        private void GetIconImageBitmapData(MemoryStream output, Bitmap image)
        {
            // this code assumes the image is in 32-bit ARGB format

            BitmapData data = null;
            try
            {
                var sizeOfHeader = Marshal.SizeOf<BitmapInfoHeader>();
                var paddedWidth = RoundTo(image.Width, 4);
                var pixelCount = paddedWidth * image.Height;
                var maskWidth = paddedWidth / 8; // bitmask is 1-bit per pixel (1 byte per 8 pixels)

                // create the BitmapInfoHeader of the given image
                var bitmapInfoHeader = new BitmapInfoHeader()
                {
                    Size = (uint)sizeOfHeader,
                    Width = image.Width,
                    Height = image.Height * 2, // bitmap data and bitmask
                    Planes = 1,
                    BitCount = 32,
                    Compression = 0, // no compression
                    SizeImage = (uint)((pixelCount * 4) + (maskWidth * image.Height)),
                    XPelsPerMeter = 0, // not specified
                    YPelsPerMeter = 0, // not specified
                    ClrUsed = 0, // all colors
                    ClrImportant = 0 // all colors
                };
                WriteStruct(output, bitmapInfoHeader);

                // get the bytes of the image
                var imageRectangle = new Rectangle(0, 0, image.Width, image.Height);
                data = image.LockBits(imageRectangle, ImageLockMode.ReadOnly, image.PixelFormat);
                var scanData = new byte[data.Stride];

                // write bitmap data
                // remember: bitmaps in icons are bottom-up DIBs
                for (var scanIndex = image.Height - 1; scanIndex >= 0; scanIndex--)
                {
                    var scan = data.Scan0 + (data.Stride * scanIndex);
                    Marshal.Copy(scan, scanData, 0, scanData.Length);
                    output.Write(scanData, 0, scanData.Length);
                } // for scanIndex

                // create bottom-up bitmask
                for (var scanIndex = image.Height - 1; scanIndex >= 0; scanIndex--)
                {
                    byte bits = 0;
                    var bitIndex = 0;
                    var scan = data.Scan0 + (data.Stride * scanIndex);
                    Marshal.Copy(scan, scanData, 0, scanData.Length);

                    // get alpha values for current scan line
                    for (var alphaIndex = 0; alphaIndex < scanData.Length; alphaIndex += 4)
                    {
                        var alpha = scanData[alphaIndex];
                        if (alpha < 128) bits |= (byte)(1 << (7 - bitIndex));

                        bitIndex++;
                        if (bitIndex < 8) continue;

                        output.WriteByte(bits);
                        bits = 0;
                        bitIndex = 0;
                    } // for alphaIndex

                    // add padding to 4 bytes if necessary
                    var width = image.Width;
                    for (var padding = 0; padding < ((width / 8) % 4); padding++)
                    {
                        output.WriteByte(0);
                    } // for
                } // for scanIndex
            }
            finally
            {
                if (data != null) image.UnlockBits(data);
            } // finally
        } // GetIconImageBitmapData

        private int GetEstimatedIconImageSize(Bitmap image)
        {
            // this code assumes the image is in 32-bit ARGB format

            var sizeOfHeader = Marshal.SizeOf<BitmapInfoHeader>();
            var paddedWidth = RoundTo(image.Width, 4);
            var pixelCount = paddedWidth * image.Height;
            var maskWidth = paddedWidth / 8; // bitmask is 1-bit per pixel (1 byte per 8 pixels)

            var estimatedCapacity = sizeOfHeader + (pixelCount * 4) + (maskWidth * image.Height); // pixels are in 32-bit ARGB format (4 bytes per pixel)

            return estimatedCapacity;
        } // GetEstimatedIconImageSize

        private int RoundTo(int value, int round)
        {
            var mod = value % round;
            if (mod == 0) return value;

            return value + (round - mod);
        } // RoundTo

        private byte GetBitsPerPixel(PixelFormat format)
        {
            switch(format)
            {
                case PixelFormat.Format1bppIndexed:
                    return 1;

                case PixelFormat.Format4bppIndexed:
                    return 4;

                case PixelFormat.Format8bppIndexed:
                    return 8;

                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format16bppGrayScale:
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                    return 16;

                case PixelFormat.Format24bppRgb:
                    return 24;

                case PixelFormat.Canonical:
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    return 32;

                case PixelFormat.Format48bppRgb:
                    return 48;

                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    return 64;

                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, "Unknow or unsupported PixelFormat");
            } // switch format
        } // GetBitsPerPixel

        private static Bitmap To32BppArgbBitmap(Bitmap image)
        {
            Bitmap result = null;
            Graphics g = null;
            var ok = false;

            try
            {
                result = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                g = Graphics.FromImage(result);
                g.Clear(Color.Transparent);
                g.DrawImageUnscaled(image, 0, 0);
                ok = true;

                return result;
            }
            finally
            {
                g?.Dispose();
                if (!ok) result?.Dispose();
            } // try-catch-finally
        } // To32BitBitmap

        private static int WriteStruct<T>(Stream stream, T structure) where T : struct
        {
            var bytes = NativeUtils.StructToBytes(structure);
            stream.Write(bytes, 0, bytes.Length);

            return bytes.Length;
        } // WriteStruct<T>
    } // WindowsIcon
} // namespace
