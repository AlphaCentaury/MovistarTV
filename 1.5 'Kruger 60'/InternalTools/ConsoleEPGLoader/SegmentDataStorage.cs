using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    public partial class SegmentDataStorage
    {
        private Dictionary<int, SegmentInfo> Segments;

        public delegate void SegmentNewCallback(DvbStpSegmentIdentity segmentIdentity);
        public delegate void SegmentDataDownloadedCallback(SegmentAssembler segmentData);
        public delegate void SegmentVersionChangedCallback(byte oldVersion, DvbStpSegmentIdentity newVersion, bool wasComplete);
        public delegate void SegmentReceivedCallback(DvbStpSegmentIdentity segmentIdentity, int round);

        public event SegmentNewCallback SegmentNew;
        public event SegmentDataDownloadedCallback SegmentDataDownloaded;
        public event SegmentVersionChangedCallback SegmentVersionChanged;
        public event SegmentReceivedCallback SegmentReceived;

        public SegmentDataStorage()
        {
            Segments = new Dictionary<int, SegmentInfo>();
        } // constructor

        public int SegmentsCount
        {
            get { return Segments.Count; }
        } // SegmentsCount

        public void AddSectionData(DvbStpHeader header, byte[] data, bool isRawData)
        {
            SegmentInfo info;

            var p = (int)header.PayloadId;
            var s = (int)header.SegmentId;
            var key = ((p << 16) | s);

            if (!Segments.TryGetValue(key, out info))
            {
                info = NewSegmentInfo(header);
                Segments[key] = info;
            } // if

            DetectVersionChange(header, info);
            info.AddSectionData(header, data, isRawData);
        } // AddSectionData

        private SegmentInfo NewSegmentInfo(DvbStpHeader header)
        {
            var info = new SegmentInfo(header);

            // hook-up events
            info.SegmentDataDownloaded += Info_SegmentDataComplete;
            info.SegmentReceived += Info_SegmentReceived;

            if (SegmentNew != null) SegmentNew(info.SegmentIdentity);

            return info;
        }  // NewSegmentInfo

        private void DetectVersionChange(DvbStpHeader header, SegmentInfo info)
        {
            if (header.SegmentVersion == info.SegmentIdentity.Version) return;

            foreach (var segment in Segments.Values)
            {
                segment.AdjustRound(-1);
            } // foreach

            var oldVersion = info.SegmentIdentity.Version;
            var wasComplete = (info.Round > 0);
            info.Reset(header);
            if (SegmentVersionChanged != null)
            {
                SegmentVersionChanged(oldVersion, info.SegmentIdentity, wasComplete);
            } // if
        } // DetectVersionChange

        private void Info_SegmentDataComplete(SegmentAssembler segmentData)
        {
            if (SegmentDataDownloaded != null) SegmentDataDownloaded(segmentData);
        } // Info_SegmentDataComplete

        void Info_SegmentReceived(DvbStpSegmentIdentity segmentIdentity, int round)
        {
            if (SegmentReceived != null) SegmentReceived(segmentIdentity, round);
        } // Info_SegmentReceived
    } // public class SegmentDataStorage
} // namespace
