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

namespace IpTviewr.DvbStp.Client
{
    public partial class PayloadStorage
    {
        private readonly Dictionary<int, VersionStorage> _sections;

        internal static byte[] EmptyData = new byte[0];

        public event EventHandler<SegmentStartedEventArgs> SegmentStarted;
        public event EventHandler<SectionReceivedEventArgs> SectionReceived;
        public event EventHandler<SegmentReceivedEventArgs> SegmentReceived;
        public event EventHandler<SegmentPayloadReceivedEventArgs> SegmentPayloadReceived;

        public bool SaveData
        {
            get;
            private set;
        } // SaveData

        public PayloadStorage(bool saveData)
        {
            _sections = new Dictionary<int, VersionStorage>();
            SaveData = saveData;
        } // constructor

        public bool AddSection(DvbStpHeader header, byte[] data, bool isRawData)
        {
            var p = (int)header.PayloadId;
            var s = header.SegmentId;
            var key = ((p << 16) | s);

            if (!_sections.TryGetValue(key, out var versions))
            {
                versions = CreateNewVersions(header);
                _sections[key] = versions;
            } // if

            return versions.AddSection(header, data, isRawData);
        } // AddSection

        private VersionStorage CreateNewVersions(DvbStpHeader header)
        {
            VersionStorage versions;

            versions = new VersionStorage(header.PayloadId, header.SegmentId, SaveData);
            if (SegmentStarted != null)
            {
                versions.SegmentStarted += Versions_SegmentStarted; 
            } // if
            if (SectionReceived != null)
            {
                versions.SectionReceived += Versions_SectionReceived;
            } // if
            if (SegmentReceived != null)
            {
                versions.SegmentReceived += Versions_SegmentReceived;
            } // if
            if (SegmentPayloadReceived != null)
            {
                versions.SegmentPayloadReceived += Versions_SegmentPayloadReceived;
            } // if

            return versions;
        } // CreateNewVersions

        private void Versions_SegmentStarted(object sender, SegmentStartedEventArgs e)
        {
            SegmentStarted?.Invoke(this, e);
        } // Versions_SegmentStarted

        private void Versions_SectionReceived(object sender, SectionReceivedEventArgs e)
        {
            SectionReceived?.Invoke(this, e);
        } // Versions_SectionReceived

        private void Versions_SegmentReceived(object sender, SegmentReceivedEventArgs e)
        {
            SegmentReceived?.Invoke(this, e);
        } // Versions_SegmentReceived

        private void Versions_SegmentPayloadReceived(object sender, SegmentPayloadReceivedEventArgs e)
        {
            SegmentPayloadReceived?.Invoke(this, e);
        } // Versions_SegmentPayloadReceived
    } // class PayloadStorage
} // namespace
