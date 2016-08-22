// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    public partial class PayloadStorage
    {
        private Dictionary<int, VersionStorage> Sections;

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
            Sections = new Dictionary<int, VersionStorage>();
            SaveData = saveData;
        } // constructor

        public bool AddSection(DvbStpHeader header, byte[] data, bool isRawData)
        {
            VersionStorage versions;

            var p = (int)header.PayloadId;
            var s = (int)header.SegmentId;
            var key = ((p << 16) | s);

            if (!Sections.TryGetValue(key, out versions))
            {
                versions = CreateNewVersions(header);
                Sections[key] = versions;
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

        void Versions_SegmentStarted(object sender, SegmentStartedEventArgs e)
        {
            if (SegmentStarted == null) return;

            SegmentStarted(this, e);
        } // Versions_SegmentStarted

        private void Versions_SectionReceived(object sender, SectionReceivedEventArgs e)
        {
            if (SectionReceived == null) return;

            SectionReceived(this, e);
        } // Versions_SectionReceived

        private void Versions_SegmentReceived(object sender, SegmentReceivedEventArgs e)
        {
            if (SegmentReceived == null) return;

            SegmentReceived(this, e);
        } // Versions_SegmentReceived

        private void Versions_SegmentPayloadReceived(object sender, SegmentPayloadReceivedEventArgs e)
        {
            if (SegmentPayloadReceived == null) return;

            SegmentPayloadReceived(this, e);
        } // Versions_SegmentPayloadReceived
    } // class PayloadStorage
} // namespace
