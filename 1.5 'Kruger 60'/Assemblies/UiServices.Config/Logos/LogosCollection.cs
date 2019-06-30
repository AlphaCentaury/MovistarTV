using System;
using System.IO;

namespace IpTviewr.UiServices.Configuration.Logos
{
    internal class LogosCollection
    {
        public LogosCollection(string name, string package, string archivePath, string key)
        {
            Name = name;
            Package = package;
            ArchivePath = archivePath;
            Key = key;
        } // LogosCollection

        public string Name { get; }
        public string Package { get; }
        public  string ArchivePath { get; }
        public string Key { get; }

        public DateTime GetLastModifiedUtc()
        {
            var info = new FileInfo(ArchivePath);
            var result = info.CreationTimeUtc;
            if (info.LastWriteTimeUtc > result)
            {
                result = info.LastWriteTimeUtc;
            } // if

            return result;
        } // GetLastModifiedUtc

        public override string ToString() => Key;
    } // LogosCollection
} // namespace
