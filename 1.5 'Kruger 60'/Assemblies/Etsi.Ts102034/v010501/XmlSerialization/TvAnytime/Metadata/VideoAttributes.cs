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
    public class VideoAttributes
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
