// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration.IpTvService;

namespace IpTviewr.IpTvServices.Implementation
{
    public class ServiceTexts: ITvServiceTexts
    {
        public ITvServiceProviderTexts Provider { get; private set; }

        public void Initialize()
        {
            Provider = GetProviderTexts();
        } // Initialize

        protected virtual ITvServiceProviderTexts GetProviderTexts() => new ServiceProviderTexts();
    } // class ServiceTexts
} // namespace
