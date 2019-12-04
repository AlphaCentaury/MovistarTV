// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaCentaury.Licensing.Data.Serialization;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    internal class LicensingFileNode
    {
        private LicensingFile _file;

        public LicensingFileNode(string filePath, bool exists)
        {
            FilePath = filePath;
            Exists = exists;
        } // constructor

        public string FilePath { get; }
        public bool Exists { get; }

        public bool IsValueCreated => _file != null;

        public LicensingFile Value
        {
            get
            {
                if (!Exists) return null;
                if (IsValueCreated) return _file;

                _file = XmlSerialization.Deserialize<LicensingFile>(FilePath);
                return _file;
            } // get
        } // Value
    } // LicensingFileNode
} // namespace
