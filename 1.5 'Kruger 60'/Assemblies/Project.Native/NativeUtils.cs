// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Native
{
    public static class NativeUtils
    {
        public static byte[] StructToBytes<T>(T structure) where T : struct
        {
            GCHandle gcHandle = new GCHandle();

            byte[] bytes = new byte[Marshal.SizeOf(typeof(T))];
            try
            {
                gcHandle = GCHandle.Alloc(structure, GCHandleType.Pinned);
                Marshal.Copy(gcHandle.AddrOfPinnedObject(), bytes, 0, bytes.Length);

                return bytes;
            }
            finally
            {
                if (gcHandle.IsAllocated) gcHandle.Free();
            } // finally
        } // StructToBytes
    } // static class NativeUtils
} // namespace
