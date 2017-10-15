// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordRecorder
    {
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        } // Name

        public string Path
        {
            get;
            set;
        } // Path

        [XmlArray("Arguments", Namespace = RecordTask.XmlNamespace)]
        [XmlArrayItem("Arg")]
        public string[] Arguments
        {
            get;
            set;
        } // Parameters
    } // RecordRecorder
} // namespace
