using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpTviewr.IpTvServices.Implementation;
using IpTviewr.UiServices.Configuration.IpTvService;

namespace IpTviewr.IpTvServices.MovistarPlus.Implementation
{
    public sealed class MovistarTexts: ServiceTexts
    {
        protected override ITvServiceProviderTexts GetProviderTexts() => new MovistarProviderTexts();
    } // class MovistarTexts
} // namespace
