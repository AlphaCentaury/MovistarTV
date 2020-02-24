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
    [XmlType("AudioAttributesType", Namespace = "urn:tva:metadata:2011")]
    public class AudioAttributes
    {
        public ControlledTerm Coding;

        [XmlElement("NumOfChannels")]
        public ushort NumberOfChannels;

        [XmlIgnore]
        public bool NumberOfChannelsSpecified;

        public ControlledTerm MixType;

        public AudioLanguage AudioLanguage;

        public uint SampleFrequency;

        [XmlIgnore]
        public bool SampleFrequencySpecified;

        public uint BitsPerSample;

        [XmlIgnore]
        public bool BitsPerSampleSpecified;

        public BitRate BitRate;
    } // class AudioAttributes
} // namespace
