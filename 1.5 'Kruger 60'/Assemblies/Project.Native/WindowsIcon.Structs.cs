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

using System.Runtime.InteropServices;

namespace IpTviewr.Native
{
    partial class WindowsIcon
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct IconDirHeader
        {
            /// <summary>
            /// Reserved (must be 0)
            /// </summary>
            public ushort Reserved;

            /// <summary>
            /// Resource Type (1 for icons)
            /// </summary>
            public ushort Type;

            /// <summary>
            ///  Number of images ("icons")
            /// </summary>
            public ushort Count;

            public IconDirHeader(int count)
            {
                Reserved = 0;
                Type = 1;
                Count = checked((ushort)count);
            } // constructor
        } // struct IconDirHeader

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct IconDirEntry
        {
            /// <summary>
            /// Width, in pixels, of the image
            /// </summary>
            /// <remarks>0 = 256</remarks>
            public byte Width;

            /// <summary>
            /// Height, in pixels, of the image
            /// </summary>
            /// <remarks>0 = 256</remarks>
            public byte Height;

            /// <summary>
            /// Number of colors in image
            /// </summary>
            /// <remarks>0 if >=8bpp</remarks>
            public byte ColorCount;

            /// <summary>
            /// Reserved (must be 0)
            /// </summary>
            public byte Reserved;

            /// <summary>
            /// Color Planes
            /// </summary>
            public ushort Planes;

            /// <summary>
            /// Bits per pixel
            /// </summary>
            public ushort BitCount;

            /// <summary>
            /// Size of the bitmap (in bytes)
            /// </summary>
            public uint BytesInRes;

            /// <summary>
            /// Where in the file is this image?
            /// </summary>
            public uint ImageOffset;
        } // struct IconDirEntry

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BitmapInfoHeader
        {
            /// <summary>
            /// The number of bytes required by the structure
            /// </summary>
            public uint Size;

            /// <summary>
            /// The width of the bitmap, in pixels
            /// </summary>
            public int Width;

            /// <summary>
            /// The height of the bitmap, in pixels.
            /// </summary>
            /// <remarks>
            /// If biHeight is positive, the bitmap is a bottom-up DIB and its origin is the lower-left corner. If biHeight is negative, the bitmap is a top-down DIB and its origin is the upper-left corner
            /// </remarks>
            public int Height;

            /// <summary>
            /// The number of planes for the target device. This value must be set to 1
            /// </summary>
            public ushort Planes;

            /// <summary>
            /// The number of bits-per-pixel
            /// </summary>
            /// <remarks>
            /// This member must be one of the following values: 0, 1, 4, 8, 16, 24 or 32
            /// </remarks>
            public ushort BitCount;

            /// <summary>
            /// The type of compression for a compressed bottom-up bitmap
            /// </summary>
            /// <remarks>
            /// Top-down DIBs cannot be compressed
            /// </remarks>
            public uint Compression;

            /// <summary>
            /// The size, in bytes, of the image. This may be set to zero for BI_RGB bitmaps
            /// </summary>
            /// <remarks>
            /// This may be set to zero for RGB bitmaps
            /// For JPEG or PNG compression, indicates the size of the JPEG or PNG image buffer, respectively
            /// </remarks>
            public uint SizeImage;

            /// <summary>
            /// The horizontal resolution, in pixels-per-meter, of the target device for the bitmap
            /// </summary>
            public int XPelsPerMeter;

            /// <summary>
            /// The vertical resolution, in pixels-per-meter, of the target device for the bitmap
            /// </summary>
            public int YPelsPerMeter;

            /// <summary>
            /// The number of color indexes in the color table that are actually used by the bitmap
            /// </summary>
            /// <remarks>
            /// If this value is zero, the bitmap uses the maximum number of colors
            /// </remarks>
            public uint ClrUsed;

            /// <summary>
            /// The number of color indexes that are required for displaying the bitmap
            /// </summary>
            /// <remarks>
            /// If this value is zero, all colors are required
            /// </remarks>
            public uint ClrImportant;
        } // struct BitmapInfoHeader

        private struct IconKind
        {
            public readonly short Size;
            public readonly byte BitsPerPixel;
            public readonly SaveAs SaveAs;

            public IconKind(int size, byte bitsPerPixel, SaveAs saveAs)
            {
                Size = (short)size;
                BitsPerPixel = bitsPerPixel;
                SaveAs = saveAs;
            } // constructor

            public override int GetHashCode()
            {
                var hash = ((ushort)Size << 8) | BitsPerPixel;
                return hash;
            } // GetHashCode
        } // IconKind
    } // partial class WindowsIcon
} // namespace
