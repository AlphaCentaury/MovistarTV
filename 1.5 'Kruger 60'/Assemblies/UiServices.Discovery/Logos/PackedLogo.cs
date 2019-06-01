// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
    public class PackedLogo: PackedLogoName
    {
        public PackedLogo()
        {
            // no-op
        } // constructor

        public PackedLogo(PackedLogoName name, short[] sizes)
        {
            Name = name.Name;
            IsHD = name.IsHD;

            Positions = new PackedLogoPos[sizes.Length];

            for (int index = 0; index < sizes.Length; index++)
            {
                Positions[index] = new PackedLogoPos(sizes[index]);
            } // for index
        } // constructor

        [XmlElement("Pos")]
        public PackedLogoPos[] Positions
        {
            get;
            set;
        } // Positions

        public PackedLogoPos this[short size]
        {
            get
            {
                var index = GetSizeIndex(size);

                return (index >= 0) ? Positions[index] : default(PackedLogoPos);
            } // get
        } // this[]

        public int GetSizeIndex(short size)
        {
            var index = Array.BinarySearch(Positions, new PackedLogoPos(size), new PackedLogoPosComparer());

            return index;
        } // GetSizeIndex
    } // class PackedLogo
} // namespace
