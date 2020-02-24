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
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration
{
    [Serializable]
    public class XmlConfigurationItems
    {
        public static XmlElement GetXmlElement(Guid id, IConfigurationItem item)
        {
            var xDoc = new XmlDocument();
            var xNavigator = xDoc.CreateNavigator();
            using (var writer = xNavigator.AppendChild())
            {
                var ser = new XmlSerializer(item.GetType());
                ser.Serialize(writer, item);
            } // using

            var xAttr = xDoc.CreateAttribute("configurationId");
            xAttr.Value = id.ToString();
            xDoc.DocumentElement.Attributes.Append(xAttr);

            return xDoc.DocumentElement;
        } // GetXmlElement

        [XmlArray("Registry")]
        [XmlArrayItem("Type")]
        public List<string> Registry
        {
            get;
            set;
        } // Registry

        [XmlAnyElement]
        public List<XmlElement> XmlData
        {
            get;
            set;
        } // XmlData
    } // class XmlConfigurationItems
} // namespace
