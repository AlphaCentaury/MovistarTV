using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    partial class SegmentDataStorage
    {
        private class SegmentInfo
        {
            private BitArray IsSectionReceived;
            private SegmentAssembler SegmentData;

            public event SegmentDataDownloadedCallback SegmentDataDownloaded;
            public event SegmentReceivedCallback SegmentReceived;

            public DvbStpSegmentIdentity SegmentIdentity
            {
                get;
                private set;
            } // SegmentIdentity

            public int TotalSectionsCount
            {
                get;
                private set;
            } // TotalSectionsCount

            public int RemainingSections
            {
                get;
                private set;
            } //  RemainingSections

            public int Round
            {
                get;
                private set;
            } // Round

            public SegmentInfo(DvbStpHeader header)
            {
                Reset(header);
            } // constructor

            public void AddSectionData(DvbStpHeader header, byte[] data, bool isRawData)
            {
                // section received?
                if (IsSectionReceived[header.SectionNumber]) return;

                // store data
                if (SegmentData != null)
                {
                    if (isRawData)
                    {
                        SegmentData.AddSectionData(header.SectionNumber, data, header.PayloadOffset, header.PayloadSize);
                    }
                    else
                    {
                        SegmentData.AddSectionData(header.SectionNumber, data, 0, data.Length);
                    } // if-else
                } // if

                IsSectionReceived[header.SectionNumber] = true;
                RemainingSections--;

                CheckSegmentComplete();
            } // AddSectionData

            public bool IsSegmentComplete
            {
                get { return (RemainingSections <= 0); }
            } // IsSegmentComplete

            public void Reset(DvbStpHeader header)
            {
                TotalSectionsCount = header.LastSectionNumber + 1;
                RemainingSections = TotalSectionsCount;
                IsSectionReceived = new BitArray(TotalSectionsCount);

                SegmentIdentity = new DvbStpSegmentIdentity(header);
                SegmentData = new SegmentAssembler(new DvbStpSegmentIdentity(header), header.LastSectionNumber);

                Round = 0;
            } // Reset

            public void AdjustRound(int minusValue)
            {
                Round -= minusValue;
                if (Round < 0) Round = 0;
            } // AdjustRound

            private void CheckSegmentComplete()
            {
                if (!IsSegmentComplete) return;

                if (SegmentData != null)
                {
                    if (SegmentDataDownloaded != null) SegmentDataDownloaded(SegmentData);
                    SegmentData = null;
                } // if

                if (SegmentReceived != null)
                {
                    SegmentReceived(SegmentIdentity, Round);
                } // if

                IsSectionReceived.SetAll(false);
                RemainingSections = TotalSectionsCount;
                Round++;
            } // CheckSegmentComplete
        } // class SegmentInfo
    } // partial class SegmentDataStorage
} // namespace
