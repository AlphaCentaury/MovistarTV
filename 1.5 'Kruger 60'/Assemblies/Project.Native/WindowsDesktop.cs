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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

namespace IpTviewr.Native
{
    public static class WindowsDesktop
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        internal enum DeviceCap
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90
        } // enum DeviceCap

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        internal static extern int GetDeviceCaps(IntPtr hDC, DeviceCap cap);

        public static void GetDpi(out int x, out int y)
        {
            var g = (Graphics) null;
            var desktopHdc = IntPtr.Zero;

            try
            {
                g = Graphics.FromHwnd(IntPtr.Zero);
                desktopHdc = g.GetHdc();
                x = GetDeviceCaps(desktopHdc, DeviceCap.LOGPIXELSX);
                y = GetDeviceCaps(desktopHdc, DeviceCap.LOGPIXELSY);
            }
            catch
            {
                x = 96;
                y = 96;
            }
            finally
            {
                if (desktopHdc != IntPtr.Zero)
                {
                    g?.ReleaseHdc(desktopHdc);
                }
                g?.Dispose();
            } // try-catch-finally
        } // GetDpi
    } // WindowsDesktop
} // namespace
