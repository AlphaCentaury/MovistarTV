// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
    public partial class AudioAttributes
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
