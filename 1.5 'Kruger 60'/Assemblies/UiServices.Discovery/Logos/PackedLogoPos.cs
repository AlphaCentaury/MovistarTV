// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Discovery.Logos
{
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(Namespace = XmlPackedLogosRoot.Namespace)]
    public struct PackedLogoPos: IComparable<PackedLogoPos>
    {
        public PackedLogoPos(short size)
        {
            Size = size;
            X = 0;
            Y = 0;
        } // constructor

        public PackedLogoPos(int size)
        {
            Size = checked((short)size);
            X = 0;
            Y = 0;
        } // constructor

        [XmlAttribute("x")]
        public short X
        {
            get;
            set;
        } // X

        [XmlAttribute("y")]
        public short Y
        {
            get;
            set;
        } // Y

        [XmlAttribute("size")]
        public short Size
        {
            get;
            set;
        } // Size

        public bool IsEmpty
        {
            get { return Size == 0; }
        } // IsEmpty

        public int CompareTo(PackedLogoPos other)
        {
            return Size.CompareTo(other.Size);
        } // CompareTo
    } // class PackedLogoPos
} // namespace
