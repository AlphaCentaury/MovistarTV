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

using AlphaCentaury.Licensing.Data.Serialization;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    internal class LicensingDataNode
    {
        private LicensingData _file;

        public LicensingDataNode(string filePath, bool exists)
        {
            FilePath = filePath;
            Exists = exists;
        } // constructor

        public string FilePath { get; }
        public bool Exists { get; }

        public bool IsValueCreated => _file != null;

        public LicensingData Value
        {
            get
            {
                if (!Exists) return null;
                if (IsValueCreated) return _file;

                _file = XmlSerialization.Deserialize<LicensingData>(FilePath);
                return _file;
            } // get
        } // Value
    } // LicensingDataNode
} // namespace
