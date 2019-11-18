using System;
using System.Collections.Generic;

namespace IpTviewr.IpTvServices.MovistarPlus.Serialization
{
    [Serializable]
    public class MovistarPlusConfig
    {
        public Opch Opch { get; set; }
        public IDictionary<string, string> Environment;
        public IDictionary<string, IDictionary<string, string>> Variables;
    } // class MovistarPlusConfig
} // namespace
