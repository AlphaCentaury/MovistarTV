using System.Collections.Generic;

namespace IpTviewr.IpTvServices.MovistarPlus.Serialization
{
    public class Opch
    {
        public string MulticastAddress
        {
            get;
            set;
        } // MulticastAddress

        public int MulticastPort
        {
            get;
            set;
        } // MulticastPort

        public IDictionary<string, string> Default
        {
            get;
            set;
        } // DefaultValues
    } // Opch
} // namespace