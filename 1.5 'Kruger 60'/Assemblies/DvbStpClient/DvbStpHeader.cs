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
using System.Net;

namespace IpTviewr.DvbStp.Client
{
    public class DvbStpHeader
    {
        public const int MinHeaderLength = 12;

        public byte Version;
        public byte Reserved;
        public byte Encryption;
        public bool HasCrc;
        public int TotalSegmentSize;
        public byte PayloadId;
        public int SegmentId;
        public byte SegmentIdNetworkLo;
        public byte SegmentIdNetworkHi;
        public byte SegmentVersion;
        public int SectionNumber;
        public int LastSectionNumber;
        public CompressionMethod Compression;
        public bool HasServiceProviderId;
        public byte PrivateHeaderLength;
        public int ServiceProviderId;
        public int PrivateHeaderOffset;
        public int PayloadOffset;
        public int PayloadSize;
        public int Crc;

        public enum CompressionMethod: byte
        {
            None = 0,
            BiM = 1,
            GZip = 2,
            Reserved1 = 3,
            Reserved2 = 4,
            Reserved3 = 5,
            Reserved4 = 6,
            User = 7,
        } // enum CompressionMethod

        public static DvbStpHeader PartialDecode(byte[] headerData)
        {
            var header = new DvbStpHeader
            {
                Version = (byte)(headerData[0] & DvbStpHeaderMasks.Version),

                // byte 4
                PayloadId = headerData[4],

                // byte 5-6
                SegmentIdNetworkLo = headerData[5],
                SegmentIdNetworkHi = headerData[6],

                // byte 7
                SegmentVersion = headerData[7]
            };

            return header;
        } // PartialDecode

        public static DvbStpHeader Decode(byte[] headerData, int receivedBytes)
        {
            var header = PartialDecode(headerData);
            if (header.Version != 0) throw new NotImplementedException();

            header.CompleteDecoding(headerData, receivedBytes);

            return header;
        } // Decode

        public DvbStpHeader Clone()
        {
            return MemberwiseClone() as DvbStpHeader;
        } // Clone

        public override string ToString()
        {
            return $"p{PayloadId:X2}s{SegmentId:X4}v{SegmentVersion:X2}-{SectionNumber}";
        } // ToString

        public void CompleteDecoding(byte[] headerData, int receivedBytes)
        {
            int networkInt;
            var tempBytesBuffer = new byte[4];

            // byte 0
            // Decoded in BasicDecode: Version = Header[0] & DvbStpHeaderMasks.Version;
            HasCrc = (headerData[0] & DvbStpHeaderMasks.HasCrc) != 0;
            Reserved = (byte)((headerData[0] & DvbStpHeaderMasks.Reserved) >> 3);
            Encryption = (byte)((headerData[0] & DvbStpHeaderMasks.Encryption) >> 1);

            // byte 1-3
            if (BitConverter.IsLittleEndian)
            {
                tempBytesBuffer[3] = 0x00;
                tempBytesBuffer[2] = headerData[1];
                tempBytesBuffer[1] = headerData[2];
                tempBytesBuffer[0] = headerData[3];
            }
            else
            {
                tempBytesBuffer[0] = 0x00;
                tempBytesBuffer[1] = headerData[1];
                tempBytesBuffer[2] = headerData[2];
                tempBytesBuffer[3] = headerData[3];
            } // if-else IsLittleEndian
            TotalSegmentSize = BitConverter.ToInt32(tempBytesBuffer, 0);

            // byte 4
            // Decoded in BasicDecode: PayloadId = Header[4]

            // byte 5-6
            // Decoded in BasicDecode: Header.SegmentIdNetworkLo = Header[5];
            // Decoded in BasicDecode: Header.SegmentIdNetworkHi = Header[6];
            if (BitConverter.IsLittleEndian)
            {
                tempBytesBuffer[1] = headerData[5];
                tempBytesBuffer[0] = headerData[6];
            }
            else
            {
                tempBytesBuffer[0] = headerData[5];
                tempBytesBuffer[1] = headerData[6];
            } // if-else IsLittleEndian
            SegmentId = BitConverter.ToUInt16(tempBytesBuffer, 0);

            // byte 7
            // Decoded in BasicDecode: Header.SegmentVersion = Header[7];

            // byte 8-9
            if (BitConverter.IsLittleEndian)
            {
                tempBytesBuffer[1] = headerData[8];
                tempBytesBuffer[0] = (byte)(headerData[9] & DvbStpHeaderMasks.SectionNumberLsb);
            }
            else
            {
                tempBytesBuffer[0] = headerData[8];
                tempBytesBuffer[1] = (byte)(headerData[9] & DvbStpHeaderMasks.SectionNumberLsb);
            } // if-else IsLittleEndian
            var number = (int)BitConverter.ToUInt16(tempBytesBuffer, 0);
            SectionNumber = ((number >> 4) & 0x00000FFF);

            // byte 9-10
            if (BitConverter.IsLittleEndian)
            {
                tempBytesBuffer[1] = (byte)(headerData[9] & DvbStpHeaderMasks.LastSectionNumberMsb);
                tempBytesBuffer[0] = headerData[10];
            }
            else
            {
                tempBytesBuffer[0] = (byte)(headerData[9] & DvbStpHeaderMasks.LastSectionNumberMsb);
                tempBytesBuffer[1] = headerData[10];
            } // if-else IsLittleEndian
            LastSectionNumber = BitConverter.ToInt16(tempBytesBuffer, 0);

            // byte 11
            HasServiceProviderId = (headerData[11] & DvbStpHeaderMasks.HasServiceProviderId) == 0 ? false : true;
            PrivateHeaderLength = (byte)(headerData[11] & DvbStpHeaderMasks.PrivateHeaderLength);
            Compression = (CompressionMethod)(headerData[0] & DvbStpHeaderMasks.Compression);

            if (PrivateHeaderLength > 0)
            {
                PrivateHeaderOffset = MinHeaderLength + (HasServiceProviderId ? 4 : 0);
            }
            else
            {
                PrivateHeaderOffset = -1;
            } // if-else

            // byte 12-15
            if (HasServiceProviderId)
            {
                networkInt = BitConverter.ToInt32(headerData, 12);
                ServiceProviderId = IPAddress.NetworkToHostOrder(networkInt);
            } // if

            PayloadOffset = MinHeaderLength + (HasServiceProviderId ? 4 : 0) + PrivateHeaderLength;
            PayloadSize = receivedBytes - PayloadOffset - (HasCrc ? 4 : 0);

            // CRC
            if (HasCrc)
            {
                networkInt = BitConverter.ToInt32(headerData, PayloadOffset + PayloadSize);
                Crc = IPAddress.NetworkToHostOrder(networkInt);
            } // if
        } // CompleteDecoding
    } // class DvbStpHeader
} // namespace
