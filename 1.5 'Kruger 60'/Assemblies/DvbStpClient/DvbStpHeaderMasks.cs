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

namespace IpTviewr.DvbStp.Client
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
        public const byte Encryption = 0x06; // 0000 0110
        public const byte HasCrc = 0x01; // 0000 0001

        // byte 8-9
        public const ushort SectionNumberLsb = 0xF0; // 1111 0000

        // byte 9-10
        public const ushort LastSectionNumberMsb = 0x0F; // 0000 1111

        // byte 11
        public const byte Compression = 0xE0; // 1110 0000
        public const byte HasServiceProviderId = 0x10; // 0001 0000
        public const byte PrivateHeaderLength = 0x0F; // 0000 1111
    } //static class DvbStpHeaderMasks
} // namespace
