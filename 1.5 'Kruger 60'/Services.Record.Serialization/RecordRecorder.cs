using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.Record.Serialization
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
