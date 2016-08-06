// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Project.DvbIpTv.Internal.ConsoleEPGLoader
{
    internal partial class UnsafeNativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleIcon(IntPtr hIcon);
    } // class UnsafeNativeMethods
} // namespace
