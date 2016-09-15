using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    class WindowsIcon
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct IconDirHeader
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
        private struct IconDirEntry
        {
            /// <summary>
            /// Width, in pixels, of the image (0 = 256)
            /// </summary>
            public byte Width;

            /// <summary>
            /// Height, in pixels, of the image (0 = 256)
            /// </summary>
            public byte Height;

            /// <summary>
            /// Number of colors in image (0 if >=8bpp)
            /// </summary>
            public byte ColorCount;

            /// <summary>
            /// Reserved ( must be 0)
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

        private List<Image> Images;

        public WindowsIcon()
        {
            Images = new List<Image>();
        } // constructor

        public WindowsIcon(int imageCount)
        {
            Images = new List<Image>(imageCount);
        } // constructor

        public void AddImage(Image image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            if ((image.Width != image.Height)) throw new ArgumentOutOfRangeException(nameof(image), "Non-square icons are not supported");
            if ((image.Width > 256) || (image.Height > 256)) throw new ArgumentOutOfRangeException(nameof(image), "Icons bigger than 256x256 are not supported in Windows");
            if ((image.PixelFormat != PixelFormat.Format32bppArgb)) throw new ArgumentOutOfRangeException(nameof(image), "Only 32-bit icons are currently supported");

            Images.Add(image);
        } // AddImage

        public void Clear()
        {
            Images.Clear();
        } // Clear

        public void Save(string filename)
        {

        } // Save

        public void Save(Stream stream)
        {
        } // Save

        public void Save(MemoryStream stream)
        {

        } // Save

        private byte[] StructToBytes<T>(T structT) where T : struct
        {
            GCHandle gcHandle = new GCHandle();

            byte[] bytes = new byte[Marshal.SizeOf(typeof(T))];
            try
            {
                gcHandle = GCHandle.Alloc(structT, GCHandleType.Pinned);
                Marshal.Copy(gcHandle.AddrOfPinnedObject(), bytes, 0, bytes.Length);

                return bytes;
            }
            finally
            {
                if (gcHandle.IsAllocated) gcHandle.Free();
            } // finally

        } // StructToBytes
    } // WindowsIcon
} // namespace
