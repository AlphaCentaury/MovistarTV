using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Discovery.Logos
{
    public sealed class LogoService: LogoImage
    {
        public LogoService(PackedLogo logo, string folder): base(logo)
        {
            // no-op
        } // constructor
    } // sealed class LogoService
} // namespace
