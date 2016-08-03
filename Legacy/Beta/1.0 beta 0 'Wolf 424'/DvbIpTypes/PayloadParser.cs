// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DvbIpTypes
{
    public static class PayloadParser
    {
        public static object Parse(Type payloadType, byte[] payloadData, byte payloadId)
        {
            try
            {
                using (MemoryStream input = new MemoryStream(payloadData, false))
                {
                    using (XmlReader reader = new XmlTextReader(input))
                    {
                        var serializer = new XmlSerializer(payloadType);
                        return serializer.Deserialize(reader);
                    } // using
                } // using input
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("The received data can't be successfully parsed and extracted.\r\n" +
                    "Data Id: 0x{0:X2}. Data type: {1}",
                    payloadId, payloadType.FullName), ex);
            } // try-catch
        } // Parse

        public static T Parse<T>(byte[] payloadData, byte payloadId) where T : class
        {
            return Parse(typeof(T), payloadData, payloadId) as T;
        } // Parse<T>
    } // PayloadParser
} // namespace
