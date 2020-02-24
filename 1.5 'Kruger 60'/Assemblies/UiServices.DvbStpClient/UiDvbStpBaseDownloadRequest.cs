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
using System.ComponentModel;
using System.Net;

namespace IpTviewr.UiServices.DvbStpClient
{
    public abstract class UiDvbStpBaseDownloadRequest
    {
        public UiDvbStpBaseDownloadRequest()
        {
            ReceiveDatagramTimeout = 10000;
            NoDataTimeout = 45000;
            DialogCloseDelay = 500;
        } // constructor

        public int ReceiveDatagramTimeout // in milliseconds
        {
            get;
            set;
        } // ReceiveDatagramTimeout

        public int NoDataTimeout // in milliseconds
        {
            get;
            set;
        } // NoDataTimeout

        public IPAddress MulticastAddress
        {
            get;
            set;
        } // MulticastAddress

        public int MulticastPort
        {
            get;
            set;
        } // MulticastPort

        public string Description
        {
            get;
            set;
        } // Description

        public string DescriptionParsing
        {
            get;
            set;
        } // DescriptionParsing

        public bool AllowXmlExtraWhitespace
        {
            get;
            set;
        } // AllowXmlExtraWhitespace

        public Func<string, string> XmlNamespaceReplacer
        {
            get;
            set;
        } // XmlNamespaceReplacer

        [DefaultValue(500)]
        public int DialogCloseDelay
        {
            get;
            set;
        } // DialogCloseDelay
    } // abstract class UiDvbStpBaseDownloadRequest
} // namespace
