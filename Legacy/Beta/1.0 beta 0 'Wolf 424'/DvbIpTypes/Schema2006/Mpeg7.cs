// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

// Types for urn:mpeg:mpeg7:schema:2001

namespace DvbIpTypes.Schema2006.Mpeg7
{
    /// <remarks/>
    //[XmlInclude(typeof(TitleType))]
    //[XmlInclude(typeof(ShortTitleType))]
    [XmlInclude(typeof(Mpeg7Multilingual))]
    //[XmlInclude(typeof(OrganizationNameType))]
    //[XmlInclude(typeof(ServiceInformationNameType))]
    //[XmlInclude(typeof(SynopsisType))]
    //[XmlInclude(typeof(KeywordType))]
    //[XmlInclude(typeof(TermNameType))]
    //[XmlInclude(typeof(NameComponentType))]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "TextualBaseType", Namespace = "urn:mpeg:mpeg7:schema:2001")]
    public abstract partial class Mpeg7MultilingualBase
    {
        /// <remarks/>
        [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get;
            set;
        } // lang

        /// <remarks/>
        [XmlText()]
        public string Value
        {
            get;
            set;
        } // Value
    } // abstract partial class Mpeg7MultilingualBase

    /// <remarks/>
    //[XmlInclude(typeof(OrganizationNameType))]
    //[XmlInclude(typeof(ServiceInformationNameType))]
    //[XmlInclude(typeof(SynopsisType))]
    //[XmlInclude(typeof(KeywordType))]
    //[XmlInclude(typeof(TermNameType))]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName="TextualType", Namespace = "urn:mpeg:mpeg7:schema:2001")]
    public partial class Mpeg7Multilingual : Mpeg7MultilingualBase // TextualType : TextualBaseType
    {
    } // partial class Mpeg7Multilingual
} // namespace
