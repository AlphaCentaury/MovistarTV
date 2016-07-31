// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DvbIpTypes.Schema2006
{
    /// <remarks/>
    public abstract partial class OfferingBase
    {
        /// <summary>
        /// An internet DNS domain name registered by the Service Provider that uniquely identifies the Service Provider
        /// </summary>
        [XmlAttribute()]
        public string DomainName
        {
            get;
            set;
        } // DomainName

        /// <summary>
        /// Version of the Service Provider(s) Discovery record; the version number shall be incremented every time a change in
        /// any of the records that comprise the service discovery information for this Service Provider occurs.
        /// </summary>
        [XmlAttribute(DataType = "integer")]
        public string Version
        {
            get;
            set;
        } // Version
    } // public abstract partial class OfferingBase

    /// <summary>
    /// Specifies an element containing a textual message, which has a Language attribute specifying the language of the
    /// string, using the ISO 639-2 three letter language code.
    /// </summary>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "MultilingualType", Namespace = "urn:dvb:ipisdns:2006")]
    public partial class MultilingualText // MultilingualType
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Language
        {
            get;
            set;
        } // Language

        /// <remarks/>
        [XmlText()]
        public string Value
        {
            get;
            set;
        } // Value
    } // MultilingualText
} // namespace
