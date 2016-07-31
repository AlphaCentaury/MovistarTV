// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    /// <remarks>
    /// DVBSTP protocol, like allmost all protocols, use MSB 0 bit order transmission
    /// Thus, for C#, bit 0 is bit 7 and so on
    /// Hence, the masks seems to be "reversed"
    /// http://en.wikipedia.org/wiki/Bit_numbering
    /// </remarks>
    public static class DvbStpHeaderMasks
    {
        // byte 0
        public const byte Version = 0xC0; // 1100 0000
        public const byte Reserved = 0x38; // 0011 1000
        public const byte Encription = 0x06; // 0000 0110
        public const byte HasCRC = 0x01; // 0000 0001

        // byte 8-9
        public const short SectionNumberLSB = 0xF0; // 1111 0000

        // byte 9-10
        public const short LastSectionNumberMSB = 0x0F; // 0000 1111

        // byte 11
        public const byte Compression = 0xE0; // 1110 0000
        public const byte HasServiceProviderId = 0x10; // 0001 0000
        public const byte PrivateHeaderLength = 0x0F; // 0000 1111
    } //static class DvbStpHeaderMasks
} // namespace
