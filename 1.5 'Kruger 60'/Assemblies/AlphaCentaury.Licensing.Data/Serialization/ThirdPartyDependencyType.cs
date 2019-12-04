// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    public enum ThirdPartyDependencyType
    {
        [XmlEnum("other")]
        Other = 0,

        [XmlEnum("nuget")]
        NugetPackage,

        [XmlEnum("library")]
        Library,

        [XmlEnum("images")]
        ImageLibrary,

        [XmlEnum("code")]
        SourceCode,
    } // enum ThirdPartyDependencyType
} // namespace
