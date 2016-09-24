using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTViewr.Internal.Logos
{
    [Flags]
    public enum Quality
    {
        None = 0x00,
        SD = 0x01,
        HD = 0x02,
    } // Quality

    class Package
    {
        public string Filename;
        public Size Size;
        public Image Image;
        public Graphics G;
        public int MaxWidth;
        public int PosX, PosY;
#if DEBUG
        public long FileSizes;
#endif
    } // class Package
} // namespace
