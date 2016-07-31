// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006.Mpeg7;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

// Types for urn:tva:metadata:2004
namespace DvbIpTypes.Schema2006.TvAnytime
{
    #region General or base xml types

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "TermNameType", Namespace = "urn:tva:metadata:2004")]
    public partial class TermName : Mpeg7Multilingual // TermNameType : TextualType
    {
        /// <remarks/>
        [XmlAttribute("preferred")]
        public bool Preferred
        {
            get;
            set;
        } // Preferred

        /// <remarks/>
        [XmlIgnore()]
        public bool PreferredSpecified
        {
            get;
            set;
        } // PreferredSpecified
    } // TermName

    /// <remarks/>
    [XmlInclude(typeof(GenreType))]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName="ControlledTermType", Namespace = "urn:tva:metadata:2004")]
    public partial class ControlledTerm
    {
        /// <remarks/>
        public TermName Name
        {
            get;
            set;
        } // Name

        /// <remarks/>
        public Mpeg7Multilingual Definition
        {
            get;
            set;
        } // Definition

        /// <remarks/>
        [XmlAttribute("href")]
        public string HRef
        {
            get;
            set;
        } // HRef
    } // partial class ControlledTerm

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "GenreType", Namespace = "urn:tva:metadata:2004")]
    public partial class ContentGenre : ControlledTerm // GenreType : ControlledTermType
    {
        public ContentGenre()
        {
            this.GenreType = GenreType.main;
        } // constructor

        /// <remarks/>
        [XmlAttribute("type")]
        [DefaultValueAttribute(GenreType.main)]
        public GenreType GenreType
        {
            get;
            set;
        } // GenreType
    } // partial class ContentGenre

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "urn:tva:metadata:2004")]
    public enum GenreType // GenreTypeType
    {
        /// <remarks/>
        main,
        /// <remarks/>
        secondary,
        /// <remarks/>
        other,
    } // enum GenreType

    /// <remarks/>
    [XmlInclude(typeof(AudioLanguage))]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "ExtendedLanguageType", Namespace = "urn:mpeg:mpeg7:schema:2001")]
    public partial class ExtendedLanguage
    {
        public ExtendedLanguage()
        {
            this.Type = ExtendedLanguageType.original;
            this.Supplemental = false;
        } // constructor

        /// <remarks/>
        [XmlAttribute("type")]
        [DefaultValue(ExtendedLanguageType.original)]
        public ExtendedLanguageType Type
        {
            get;
            set;
        } // Type

        /// <remarks/>
        [XmlAttribute("supplemental")]
        [DefaultValueAttribute(false)]
        public bool Supplemental
        {
            get;
            set;
        } // Supplemental

        /// <remarks/>
        [XmlText(DataType = "language")]
        public string Value
        {
            get;
            set;
        } // Value
    } // partial class ExtendedLanguage

    /// <remarks/>
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "urn:mpeg:mpeg7:schema:2001")]
    public enum ExtendedLanguageType // ExtendedLanguageTypeType
    {
        /// <remarks/>
        original,
        /// <remarks/>
        dubbed,
        /// <remarks/>
        background,
    } // enum ExtendedLanguageType

    #endregion

    #region AudioAttributes and related xml types

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "AudioAttributesType", Namespace = "urn:tva:metadata:2004")]
    public partial class AudioAttributes // AudioAttributesType
    {
        /// <remarks/>
        public ControlledTerm Coding
        {
            get;
            set;
        } // Coding

        /// <remarks/>
        public ushort NumOfChannels
        {
            get;
            set;
        } // NumOfChannels

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool NumOfChannelsSpecified
        {
            get;
            set;
        } // NumOfChannelsSpecified

        /// <remarks/>
        public ControlledTerm MixType
        {
            get;
            set;
        } // MixType

        /// <remarks/>
        public AudioLanguage AudioLanguage
        {
            get;
            set;
        } // AudioLanguage
    } // partial class AudioAttributes

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "AudioLanguageType", Namespace = "urn:tva:metadata:2004")]
    public partial class AudioLanguage : ExtendedLanguage // AudioLanguageType : ExtendedLanguageType
    {
        /// <remarks/>
        [XmlAttribute("purpose")]
        public string Purpose
        {
            get;
            set;
        } // Purpose
    } // partial class AudioLanguage

    #endregion

    #region VideoAttributes and related xml types

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName="VideoAttributesType", Namespace = "urn:tva:metadata:2004")]
    public partial class VideoAttributes // VideoAttributesType
    {
        /// <remarks/>
        public ControlledTerm Coding
        {
            get;
            set;
        }

        /// <remarks/>
        public VideoScanType Scan
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool ScanSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        public ushort HorizontalSize
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlIgnoreAttribute()]
        public bool HorizontalSizeSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        public ushort VerticalSize
        {
            get;
            set;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VerticalSizeSpecified
        {
            get;
            set;
        }

        /// <remarks/>
        [XmlElement("AspectRatio")]
        public VideoAspectRatio[] AspectRatio
        {
            get;
            set;
        }

        /// <remarks/>
        public VideoColor Color
        {
            get;
            set;
        }
    } // public partial class VideoAttributes

    /// <remarks/>
    [Serializable()]
    [XmlType(TypeName = "ScanType", Namespace = "urn:tva:metadata:2004")]
    public enum VideoScanType // ScanType
    {
        /// <remarks/>
        interlaced,
        /// <remarks/>
        progressive,
    } // VideoScanType

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName="AspectRatioType", Namespace = "urn:tva:metadata:2004")]
    public partial class VideoAspectRatio // AspectRatioType
    {
        public VideoAspectRatio()
        {
            this.RatioType = VideoAspectRatioType.original;
        } // constructor

        /// <remarks/>
        [XmlAttribute("type")]
        [DefaultValueAttribute(VideoAspectRatioType.original)]
        public VideoAspectRatioType RatioType
        {
            get;
            set;
        } // Type

        /// <remarks/>
        [XmlTextAttribute()]
        public string Value
        {
            get;
            set;
        } // Value
    } // VideoAspectRatio

    /// <remarks/>
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "urn:tva:metadata:2004")]
    public enum VideoAspectRatioType // AspectRatioTypeType
    {
        /// <remarks/>
        original,
        /// <remarks/>
        publication,
    } // enum VideoAspectRatioType

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "ColorType", Namespace = "urn:tva:metadata:2004")]
    public partial class VideoColor // ColorType
    {
        /// <remarks/>
        [XmlAttribute("type")]
        public VideoColorType ColorType
        {
            get;
            set;
        } // ColorType
    } // partial class VideoColor

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [XmlType(TypeName = "ColorTypeType", Namespace = "urn:tva:metadata:2004")]
    public enum VideoColorType // ColorTypeType
    {
        /// <remarks/>
        color,
        /// <remarks/>
        blackAndWhite,
        /// <remarks/>
        blackAndWhiteAndColor,
        /// <remarks/>
        colorized,
    } // enum VideoColorType

    #endregion
} // namespace
