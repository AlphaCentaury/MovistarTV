// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Metadata
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("VideoAttributesType", Namespace = "urn:tva:metadata:2011")]
    public partial class VideoAttributes
    {
        public ControlledTerm Coding;

        public ScanKind Scan;

        [XmlIgnore]
        public bool ScanSpecified;

        public ushort HorizontalSize;

        [XmlIgnore]
        public bool HorizontalSizeSpecified;

        public ushort VerticalSize;

        [XmlIgnore]
        public bool VerticalSizeSpecified;

        [XmlElement("AspectRatio")]
        public AspectRatio[] AspectRatio;

        public VideoColor Color;

        public string FrameRate;

        public BitRate BitRate;

        public ControlledTerm PictureFormat;
    } // class VideoAttributes
} // namespace
