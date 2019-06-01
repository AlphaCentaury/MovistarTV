// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.DvbStpClient
{
    public abstract class UiDvbStpBaseDownloadResponse
    {
        public Exception DownloadException
        {
            get;
            set;
        } // DownloadException

        public bool UserCancelled
        {
            get;
            set;
        } // UserCancelled

        public static object ParsePayload(Type payloadType, byte[] payloadData, byte payloadId, bool trimExtraWhitespace, Func<string, string> namespaceReplacer)
        {
            try
            {
                using (MemoryStream input = new MemoryStream(payloadData, false))
                {
                    return XmlSerialization.Deserialize(input, payloadType, trimExtraWhitespace, namespaceReplacer);
                } // using input
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(Properties.Texts.ParsePayloadException,
                    payloadId, payloadType.FullName), ex);
            } // try-catch
        } // ParsePayload<T>

        public static T ParsePayload<T>(byte[] payloadData, byte payloadId, bool trimExtraWhitespace, Func<string, string> namespaceReplacer) where T : class
        {
            return ParsePayload(typeof(T), payloadData, payloadId, trimExtraWhitespace, namespaceReplacer) as T;
        } // ParsePayload<T>
    } // abstract class UiDvbStpBaseDownloadResponse
} // namespace
